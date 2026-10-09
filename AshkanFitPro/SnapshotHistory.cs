using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace AshkanFitPro {
 /// <summary>Append-only, transactional history of committed gym snapshots.</summary>
 internal static class SnapshotHistory {
  public static void Append(SqlConnection connection, SqlTransaction transaction, byte[] payload) {
   if(connection==null || transaction==null || payload==null)throw new ArgumentNullException("Snapshot history requires a transaction and payload.");
   // A snapshot is recorded only if the entire enclosing save commits.
   using(var hash=SHA256.Create()) {
    var digest=hash.ComputeHash(payload);
    using(var command=new SqlCommand(@"INSERT INTO dbo.AppSnapshotHistory (SnapshotId,Payload,PayloadSha256,RecordedAtUtc)
VALUES (@id,@payload,@hash,SYSUTCDATETIME());",connection,transaction)) {
     command.Parameters.Add("@id",SqlDbType.Int).Value=1;
     command.Parameters.Add("@payload",SqlDbType.VarBinary,-1).Value=payload;
     command.Parameters.Add("@hash",SqlDbType.Binary,32).Value=digest;
     command.ExecuteNonQuery();
    }
   }
  }
 }
}
