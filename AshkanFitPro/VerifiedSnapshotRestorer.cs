using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;

namespace AshkanFitPro {
 /// <summary>Administrator-only verified restore with a pre-restore backup in one SQL transaction.</summary>
 public sealed class VerifiedSnapshotRestorer {
  readonly string connectionString;
  public VerifiedSnapshotRestorer() {
   var setting=ConfigurationManager.ConnectionStrings["AshkanFitPro"];
   if(setting==null || string.IsNullOrWhiteSpace(setting.ConnectionString))throw new InvalidOperationException("SQL connection is not configured.");
   connectionString=setting.ConnectionString;
  }
  public void Restore(long historyId) {
   if(!SqlStore.Enabled)throw new InvalidOperationException("SQL Server storage is not active.");
   if(!AccountService.IsAdmin)throw new UnauthorizedAccessException("Administrator role required.");
   if(historyId<=0)throw new ArgumentOutOfRangeException("historyId");
   using(var connection=new SqlConnection(connectionString)) {
    connection.Open();
    using(var transaction=connection.BeginTransaction(IsolationLevel.Serializable)) {
     try {
      byte[] restored, expected;
      using(var command=new SqlCommand("SELECT Payload,PayloadSha256 FROM dbo.AppSnapshotHistory WHERE HistoryId=@id",connection,transaction)) {
       command.Parameters.Add("@id",SqlDbType.BigInt).Value=historyId;
       using(var reader=command.ExecuteReader()) {
        if(!reader.Read())throw new InvalidOperationException("History entry not found.");
        restored=(byte[])reader[0];expected=(byte[])reader[1];
       }
      }
      using(var sha=SHA256.Create()) {
       var digest=sha.ComputeHash(restored);int diff=expected.Length==digest.Length?0:1;
       if(expected.Length==digest.Length)for(int i=0;i<digest.Length;i++)diff|=digest[i]^expected[i];
       if(diff!=0)throw new InvalidOperationException("History integrity check failed. Restore cancelled.");
      }
      Store state;
      using(var stream=new MemoryStream(restored))state=(Store)new DataContractJsonSerializer(typeof(Store)).ReadObject(stream);
      StoreIntegrity.Validate(state);
      byte[] current;
      using(var command=new SqlCommand("SELECT Payload FROM dbo.AppSnapshots WITH (UPDLOCK,HOLDLOCK) WHERE SnapshotId=1",connection,transaction))current=(byte[])command.ExecuteScalar();
      if(current==null)throw new InvalidOperationException("No current snapshot exists; restore cancelled.");
      // Keep a separate recovery point before changing the live snapshot.
      SnapshotHistory.Append(connection,transaction,current);
      using(var command=new SqlCommand("UPDATE dbo.AppSnapshots SET Payload=@payload,UpdatedAt=SYSUTCDATETIME() WHERE SnapshotId=1",connection,transaction)) {
       command.Parameters.Add("@payload",SqlDbType.VarBinary,-1).Value=restored;
       if(command.ExecuteNonQuery()!=1)throw new DBConcurrencyException("Live snapshot update failed.");
      }
      RelationalMirror.Save(connection,transaction,state);
      SnapshotHistory.Append(connection,transaction,restored);
      SqlAuditTrail.Append(connection,transaction,"SNAPSHOT_RESTORE","HistoryId="+historyId);
      transaction.Commit();
     }catch {transaction.Rollback();throw;}
    }
   }
  }
 }
}
