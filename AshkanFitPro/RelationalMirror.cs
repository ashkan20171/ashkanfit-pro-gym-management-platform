using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AshkanFitPro {
 /// <summary>
 /// Maintains queryable, relational reporting tables in the SAME transaction as the
 /// legacy snapshot. The snapshot remains authoritative until repository migration.
 /// </summary>
 internal static class RelationalMirror {
  static void Execute(SqlConnection c,SqlTransaction tx,string sql,params object[] args) {
   using(var cmd=new SqlCommand(sql,c,tx)) {
    for(int i=0;i<args.Length;i+=2)cmd.Parameters.AddWithValue((string)args[i],args[i+1]??DBNull.Value);
    cmd.ExecuteNonQuery();
   }
  }
  static void Insert(SqlConnection c,SqlTransaction tx,string table,string[] columns,params object[] values) {
   var names=new List<string>();var parameters=new List<string>();var args=new List<object>();
   for(int i=0;i<columns.Length;i++) {names.Add("["+columns[i]+"]");parameters.Add("@p"+i);args.Add("@p"+i);args.Add(values[i]);}
   Execute(c,tx,"INSERT INTO dbo.["+table+"] ("+string.Join(",",names)+") VALUES ("+string.Join(",",parameters)+")",args.ToArray());
  }
  public static void Save(SqlConnection c,SqlTransaction tx,Store s) {
   // Dependency order prevents FK violations. A single transaction makes mirror + snapshot atomic.
   foreach(var table in new[]{"Payments","Attendance","WorkoutPlans","GymClasses","BodyProgress","Expenses","AuditEntries","Members","Trainers"})
    Execute(c,tx,"DELETE FROM dbo.["+table+"]");
   foreach(var x in s.Members) Insert(c,tx,"Members",new[]{"MemberId","Name","Phone","PlanName","StartDate","ExpiryDate","Fee"},x.Id,x.Name??"",x.Phone,x.Plan,x.Start,x.Expiry,x.Fee);
   foreach(var x in s.Trainers) Insert(c,tx,"Trainers",new[]{"TrainerId","Name","Specialty","Phone"},x.Id,x.Name??"",x.Specialty,x.Phone);
   foreach(var x in s.Payments) Insert(c,tx,"Payments",new[]{"PaymentId","MemberId","Amount","PaidAt","Note"},x.Id,x.MemberId,x.Amount,x.Date,x.Note);
   foreach(var x in s.AttendanceRecords) Insert(c,tx,"Attendance",new[]{"AttendanceId","MemberId","CheckedInAt"},x.Id,x.MemberId,x.CheckedInAt);
   foreach(var x in s.WorkoutPlans) Insert(c,tx,"WorkoutPlans",new[]{"WorkoutPlanId","MemberId","TrainerId","Goal","Exercises","CreatedAt"},x.Id,x.MemberId,x.TrainerId,x.Goal,x.Exercises,x.CreatedAt);
   foreach(var x in s.Classes) Insert(c,tx,"GymClasses",new[]{"ClassId","TrainerId","Title","Weekday","ClassTime","Capacity"},x.Id,x.TrainerId,x.Title??"",(int)x.Day,x.Time,x.Capacity);
   foreach(var x in s.BodyProgressRecords) Insert(c,tx,"BodyProgress",new[]{"ProgressId","MemberId","RecordedAt","WeightKg","HeightCm","WaistCm","BodyFatPercent","Goal"},x.Id,x.MemberId,x.RecordedAt,x.WeightKg,x.HeightCm,x.WaistCm,x.BodyFatPercent,x.Goal);
   foreach(var x in s.Expenses) Insert(c,tx,"Expenses",new[]{"ExpenseId","ExpenseDate","Category","Description","Amount"},x.Id,x.Date,x.Category,x.Description,x.Amount);
   foreach(var x in s.AuditEntries) Insert(c,tx,"AuditEntries",new[]{"AuditId","OccurredAt","ActionName","Details"},x.Id,x.At,x.Action,x.Details);
  }
 }
}
