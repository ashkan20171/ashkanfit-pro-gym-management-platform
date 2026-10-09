using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AshkanFitPro {
 /// <summary>Read-only SQL queries scoped by the authenticated account's server-side role and trainer assignment.</summary>
 internal sealed class TrainerScopedQueries {
  readonly string connectionString;
  internal TrainerScopedQueries() {
   var settings=ConfigurationManager.ConnectionStrings["AshkanFitPro"];
   if(settings==null || string.IsNullOrWhiteSpace(settings.ConnectionString))throw new InvalidOperationException("SQL connection string is missing.");
   connectionString=settings.ConnectionString;
  }
  internal List<TrainerMemberSummary> GetAssignedMembers(string authenticatedUserName) {
   if(string.IsNullOrWhiteSpace(authenticatedUserName))throw new UnauthorizedAccessException("Authentication required.");
   const string query=@"SELECT DISTINCT m.MemberId,m.Name,m.Phone,m.ExpiryDate
FROM dbo.AppUsers AS u
JOIN dbo.WorkoutPlans AS w ON w.TrainerId=u.TrainerId
JOIN dbo.Members AS m ON m.MemberId=w.MemberId
WHERE u.UserName=@username AND u.Role='Trainer' AND u.TrainerId IS NOT NULL
ORDER BY m.Name";
   var results=new List<TrainerMemberSummary>();
   using(var c=new SqlConnection(connectionString)) {c.Open();
    using(var cmd=new SqlCommand(query,c)) {
     cmd.Parameters.Add("@username",SqlDbType.NVarChar,100).Value=authenticatedUserName;
     using(var reader=cmd.ExecuteReader())while(reader.Read())results.Add(new TrainerMemberSummary {
      MemberId=reader.GetGuid(0),Name=reader.GetString(1),Phone=reader.IsDBNull(2)?"":reader.GetString(2),ExpiryDate=reader.GetDateTime(3)
     });
    }
   }
   return results;
  }
 }
 internal sealed class TrainerMemberSummary {
  internal Guid MemberId {get;set;}
  internal string Name {get;set;}
  internal string Phone {get;set;}
  internal DateTime ExpiryDate {get;set;}
 }
}
