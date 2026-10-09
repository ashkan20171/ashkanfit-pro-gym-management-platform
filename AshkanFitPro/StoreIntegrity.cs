using System;
using System.Collections.Generic;
using System.Linq;

namespace AshkanFitPro {
 /// <summary>Checks referential integrity before a SQL snapshot and its relational mirror are committed.</summary>
 internal static class StoreIntegrity {
  static void Unique<T>(IEnumerable<T> records, Func<T,Guid> id, string entity) {
   var seen=new HashSet<Guid>();
   foreach(var item in records) {
    var key=id(item);
    if(key==Guid.Empty || !seen.Add(key))throw new InvalidOperationException(entity+" has an empty or duplicate ID. Save cancelled.");
   }
  }
  internal static void Validate(Store s) {
   if(s==null)throw new ArgumentNullException("s");
   Unique(s.Members,x=>x.Id,"Member");Unique(s.Trainers,x=>x.Id,"Trainer");
   Unique(s.Payments,x=>x.Id,"Payment");Unique(s.AttendanceRecords,x=>x.Id,"Attendance");
   Unique(s.WorkoutPlans,x=>x.Id,"Workout plan");Unique(s.Classes,x=>x.Id,"Class");
   Unique(s.BodyProgressRecords,x=>x.Id,"Body progress");Unique(s.Expenses,x=>x.Id,"Expense");
   Unique(s.AuditEntries,x=>x.Id,"Audit entry");
   var members=new HashSet<Guid>(s.Members.Select(x=>x.Id));
   var trainers=new HashSet<Guid>(s.Trainers.Select(x=>x.Id));
   foreach(var x in s.Payments) {if(!members.Contains(x.MemberId))throw new InvalidOperationException("Payment references a missing member.");if(x.Amount<0)throw new InvalidOperationException("Payment amount cannot be negative.");}
   foreach(var x in s.AttendanceRecords)if(!members.Contains(x.MemberId))throw new InvalidOperationException("Attendance references a missing member.");
   foreach(var x in s.BodyProgressRecords) {if(!members.Contains(x.MemberId))throw new InvalidOperationException("Progress references a missing member.");if(x.WeightKg<0||x.HeightCm<0||x.WaistCm<0||x.BodyFatPercent<0||x.BodyFatPercent>100)throw new InvalidOperationException("Invalid body measurements.");}
   foreach(var x in s.WorkoutPlans)if(!members.Contains(x.MemberId)||!trainers.Contains(x.TrainerId))throw new InvalidOperationException("Workout plan references a missing member or trainer.");
   foreach(var x in s.Classes)if(!trainers.Contains(x.TrainerId)||x.Capacity<0)throw new InvalidOperationException("Class references a missing trainer or has invalid capacity.");
   foreach(var x in s.Members)if(x.Fee<0)throw new InvalidOperationException("Membership fee cannot be negative.");
   foreach(var x in s.Expenses)if(x.Amount<0)throw new InvalidOperationException("Expense amount cannot be negative.");
  }
 }
}
