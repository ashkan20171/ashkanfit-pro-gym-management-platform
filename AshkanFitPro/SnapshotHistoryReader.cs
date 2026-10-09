using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;

namespace AshkanFitPro {
 public sealed class HistoryRecord {
  public long Id { get; set; }
  public DateTime RecordedAtUtc { get; set; }
  public long ByteCount { get; set; }
  public bool HashMatches { get; set; }
 }
 /// <summary>Read-only snapshot inspection; never changes live gym data.</summary>
 public sealed class SnapshotHistoryReader {
  readonly string connectionString;
  public SnapshotHistoryReader() {
   var setting=ConfigurationManager.ConnectionStrings["AshkanFitPro"];
   if(setting==null || string.IsNullOrWhiteSpace(setting.ConnectionString))throw new InvalidOperationException("SQL connection is not configured.");
   connectionString=setting.ConnectionString;
  }
  public List<HistoryRecord> Recent(int maxCount) {
   if(!AccountService.IsAdmin)throw new UnauthorizedAccessException("Administrator role required.");
   if(maxCount<1 || maxCount>500)throw new ArgumentOutOfRangeException("maxCount");
   var results=new List<HistoryRecord>();
   using(var conn=new SqlConnection(connectionString)) {
    conn.Open();
    using(var cmd=new SqlCommand("SELECT TOP (@max) HistoryId,RecordedAtUtc,Payload,PayloadSha256 FROM dbo.AppSnapshotHistory ORDER BY HistoryId DESC",conn)) {
     cmd.Parameters.Add("@max",SqlDbType.Int).Value=maxCount;
     using(var reader=cmd.ExecuteReader())while(reader.Read()) {
      var bytes=(byte[])reader[2];var stored=(byte[])reader[3];
      results.Add(new HistoryRecord{Id=reader.GetInt64(0),RecordedAtUtc=reader.GetDateTime(1),ByteCount=bytes.LongLength,HashMatches=Matches(bytes,stored)});
     }
    }
   }
   return results;
  }
  public byte[] ExportVerified(long historyId) {
   if(!AccountService.IsAdmin)throw new UnauthorizedAccessException("Administrator role required.");
   using(var conn=new SqlConnection(connectionString)) {
    conn.Open();
    using(var cmd=new SqlCommand("SELECT Payload,PayloadSha256 FROM dbo.AppSnapshotHistory WHERE HistoryId=@id",conn)) {
     cmd.Parameters.Add("@id",SqlDbType.BigInt).Value=historyId;
     using(var reader=cmd.ExecuteReader()) {
      if(!reader.Read())throw new InvalidOperationException("History entry was not found.");
      var bytes=(byte[])reader[0];
      if(!Matches(bytes,(byte[])reader[1]))throw new InvalidOperationException("SHA-256 mismatch. Export was blocked.");
      return bytes;
     }
    }
   }
  }
  static bool Matches(byte[] payload,byte[] expected) {
   if(expected==null || expected.Length!=32)return false;
   using(var sha=SHA256.Create()) {
    var actual=sha.ComputeHash(payload);
    int diff=0;for(int i=0;i<32;i++)diff|=actual[i]^expected[i];return diff==0;
   }
  }
 }
}
