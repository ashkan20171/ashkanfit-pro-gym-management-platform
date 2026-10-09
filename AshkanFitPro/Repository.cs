using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Windows.Forms;

namespace AshkanFitPro {
 public class Repository {
  readonly string path;
  readonly SqlStore sql;
  public Store Data { get; private set; }
  public Repository() {
   var dir=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"AshkanFitPro");
   Directory.CreateDirectory(dir);
   path=Path.Combine(dir,"data.json");
   if(SqlStore.Enabled)sql=new SqlStore();
   Load();
  }
  public void Load() {
   try {
    if(sql!=null) {
     Data=sql.Load();
     // A new SQL installation may import the existing local data once, on first save.
     if(Data==null && File.Exists(path))Data=ReadJson(path);
    } else if(File.Exists(path))Data=ReadJson(path);
   } catch(Exception ex) {
    throw new InvalidOperationException("Could not load gym data. Verify storage configuration and database connectivity.",ex);
   }
   if(Data==null)Data=new Store();
   Normalize();
  }
  static Store ReadJson(string filename) {
   using(var stream=File.OpenRead(filename))return (Store)new DataContractJsonSerializer(typeof(Store)).ReadObject(stream);
  }
  void Normalize() {
   if(Data.AuditEntries==null)Data.AuditEntries=new System.Collections.Generic.List<AuditEntry>();
   if(Data.Expenses==null)Data.Expenses=new System.Collections.Generic.List<Expense>();
   if(Data.BodyProgressRecords==null)Data.BodyProgressRecords=new System.Collections.Generic.List<BodyProgress>();
   if(Data.Members==null)Data.Members=new System.Collections.Generic.List<Member>();
   if(Data.Payments==null)Data.Payments=new System.Collections.Generic.List<Payment>();
   if(Data.WorkoutPlans==null)Data.WorkoutPlans=new System.Collections.Generic.List<WorkoutPlan>();
   if(Data.Classes==null)Data.Classes=new System.Collections.Generic.List<GymClass>();
   if(Data.Trainers==null)Data.Trainers=new System.Collections.Generic.List<Trainer>();
   if(Data.AttendanceRecords==null)Data.AttendanceRecords=new System.Collections.Generic.List<Attendance>();
  }
  public void Save() {
   Normalize();
   Data.AuditEntries.Add(new AuditEntry { Id=Guid.NewGuid(),At=DateTime.Now,Action="Save",Details="Gym data saved" });
   try {
    if(sql!=null) { sql.Save(Data); return; }
    var temp=path+".tmp";
    using(var stream=File.Create(temp))new DataContractJsonSerializer(typeof(Store)).WriteObject(stream,Data);
    if(File.Exists(path))File.Replace(temp,path,path+".bak");else File.Move(temp,path);
   } catch(Exception ex) { throw new IOException("Saving gym data failed. Changes were not persisted.",ex); }
  }
 }
}
