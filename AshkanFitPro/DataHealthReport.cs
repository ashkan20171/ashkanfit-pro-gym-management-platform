using System;
using System.Collections.Generic;
using System.Linq;

namespace AshkanFitPro {
 public sealed class DataHealthFinding {
  public string Severity {get;set;}
  public string Category {get;set;}
  public string Detail {get;set;}
 }
 public static class DataHealthReport {
  public static List<DataHealthFinding> Analyze(Store data, DateTime today) {
   if(data==null)throw new ArgumentNullException("data");
   var result=new List<DataHealthFinding>();
   Action<string,string,string> add=(severity,category,detail)=>result.Add(new DataHealthFinding{Severity=severity,Category=category,Detail=detail});
   var members=data.Members??new List<Member>();
   var payments=data.Payments??new List<Payment>();
   var trainers=data.Trainers??new List<Trainer>();
   var ids=new HashSet<Guid>(members.Where(x=>x!=null).Select(x=>x.Id));
   var trainerIds=new HashSet<Guid>(trainers.Where(x=>x!=null).Select(x=>x.Id));
   foreach(var m in members.Where(x=>x!=null)) {
    if(m.Id==Guid.Empty)add("Error","Members","Member has an empty identifier: "+(m.Name??""));
    if(string.IsNullOrWhiteSpace(m.Name))add("Warning","Members","Member "+m.Id+" has no name");
    if(string.IsNullOrWhiteSpace(m.Phone))add("Info","Members","Member "+m.Id+" has no phone number");
    if(m.Expiry.Date<m.Start.Date)add("Error","Members","Membership ends before start: "+m.Id);
    if(m.Fee<0)add("Error","Members","Negative membership fee: "+m.Id);
    if(m.Expiry.Date<today.Date)add("Info","Renewals","Expired membership: "+(m.Name??m.Id.ToString()));
    else if(m.Expiry.Date<=today.Date.AddDays(7))add("Warning","Renewals","Expires within 7 days: "+(m.Name??m.Id.ToString()));
   }
   foreach(var group in members.Where(x=>x!=null).GroupBy(x=>x.Id).Where(g=>g.Count()>1))add("Error","Members","Duplicate member identifier: "+group.Key);
   foreach(var payment in payments.Where(x=>x!=null)) {
    if(!ids.Contains(payment.MemberId))add("Error","Payments","Payment "+payment.Id+" references a missing member");
    if(payment.Amount<0)add("Error","Payments","Negative payment: "+payment.Id);
   }
   foreach(var expense in (data.Expenses??new List<Expense>()).Where(x=>x!=null))if(expense.Amount<0)add("Error","Expenses","Negative expense: "+expense.Id);
   foreach(var plan in (data.WorkoutPlans??new List<WorkoutPlan>()).Where(x=>x!=null)) {
    if(!ids.Contains(plan.MemberId))add("Error","Workouts","Workout plan "+plan.Id+" references a missing member");
    if(!trainerIds.Contains(plan.TrainerId))add("Error","Workouts","Workout plan "+plan.Id+" references a missing trainer");
   }
   foreach(var attendance in (data.AttendanceRecords??new List<Attendance>()).Where(x=>x!=null))if(!ids.Contains(attendance.MemberId))add("Error","Attendance","Attendance "+attendance.Id+" references a missing member");
   foreach(var progress in (data.BodyProgressRecords??new List<BodyProgress>()).Where(x=>x!=null))if(!ids.Contains(progress.MemberId))add("Error","Progress","Body progress "+progress.Id+" references a missing member");
   foreach(var cls in (data.Classes??new List<GymClass>()).Where(x=>x!=null)) {
    if(!trainerIds.Contains(cls.TrainerId))add("Error","Classes","Class "+cls.Id+" references a missing trainer");
    if(cls.Capacity<0)add("Error","Classes","Class "+cls.Id+" has negative capacity");
   }
   return result.OrderBy(x=>x.Severity=="Error"?0:x.Severity=="Warning"?1:2).ThenBy(x=>x.Category).ToList();
  }
 }
}
