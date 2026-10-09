using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AshkanFitPro {
 /// <summary>Server-side, transaction-bound audit events. Do not store passwords or payloads.</summary>
 public static class SqlAuditTrail {
  public static void Append(SqlConnection connection, SqlTransaction transaction, string action, string details) {
   if(connection==null || transaction==null)throw new ArgumentNullException("transaction");
   using(var command=new SqlCommand(@"INSERT INTO dbo.SecurityAuditEvents (OccurredAtUtc, Actor, ActionName, Details)
VALUES (SYSUTCDATETIME(), @actor, @action, @details)",connection,transaction)) {
    command.Parameters.Add("@actor",SqlDbType.NVarChar,128).Value=(object)(AccountService.CurrentUser??"SYSTEM");
    command.Parameters.Add("@action",SqlDbType.NVarChar,80).Value=action;
    command.Parameters.Add("@details",SqlDbType.NVarChar,400).Value=(object)(details??"");
    command.ExecuteNonQuery();
   }
  }
  public static List<AuditEvent> Recent(int limit) {
   if(!AccountService.IsAdmin)throw new UnauthorizedAccessException("Administrator role required.");
   if(!SqlStore.Enabled)throw new InvalidOperationException("SQL Server storage required.");
   if(limit<1 || limit>500)throw new ArgumentOutOfRangeException("limit");
   var setting=ConfigurationManager.ConnectionStrings["AshkanFitPro"];
   if(setting==null)throw new InvalidOperationException("Missing SQL connection string.");
   var results=new List<AuditEvent>();
   using(var connection=new SqlConnection(setting.ConnectionString)) {
    connection.Open();
    using(var command=new SqlCommand(@"SELECT TOP (@limit) AuditEventId,OccurredAtUtc,Actor,ActionName,Details
FROM dbo.SecurityAuditEvents ORDER BY AuditEventId DESC",connection)) {
     command.Parameters.Add("@limit",SqlDbType.Int).Value=limit;
     using(var reader=command.ExecuteReader())while(reader.Read())results.Add(new AuditEvent {
      Id=reader.GetInt64(0),AtUtc=reader.GetDateTime(1),Actor=reader.GetString(2),Action=reader.GetString(3),Details=reader.IsDBNull(4)?"":reader.GetString(4)
     });
    }
   }
   return results;
  }
 }
 public sealed class AuditEvent {
  public long Id {get;set;}
  public DateTime AtUtc {get;set;}
  public string Actor {get;set;}
  public string Action {get;set;}
  public string Details {get;set;}
 }
}
