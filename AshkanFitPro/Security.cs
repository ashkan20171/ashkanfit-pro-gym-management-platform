using System;
using System.IO;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace AshkanFitPro {
 [DataContract] public class AccountRecord {
  [DataMember] public string UserName {get;set;}
  [DataMember] public string Salt {get;set;}
  [DataMember] public string Hash {get;set;}
  [DataMember] public int Iterations {get;set;}
  [DataMember] public string Role {get;set;}
  [DataMember(EmitDefaultValue=false)] public Guid? TrainerId {get;set;}
 }
 [DataContract] public class UserDirectory { [DataMember] public System.Collections.Generic.List<AccountRecord> Users {get;set;} = new System.Collections.Generic.List<AccountRecord>(); }
 public sealed class AccountService {
  public static string CurrentUser {get;private set;}
  public static string CurrentRole {get;private set;}
  public static bool IsAdmin {get{return CurrentRole=="Admin";}}
  public static Guid? CurrentTrainerId {get;private set;}
  bool SqlEnabled {get{return SqlStore.Enabled;}}
  SqlAccounts Sql {get{return new SqlAccounts();}}
  readonly string path;
  public AccountService(){var dir=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"AshkanFitPro");Directory.CreateDirectory(dir);path=Path.Combine(dir,"account.json");}
  public bool NeedsSetup {get{return SqlEnabled ? Sql.Read().Count==0 : !File.Exists(path) && !File.Exists(UsersPath);}}
  string UsersPath {get{return Path.Combine(Path.GetDirectoryName(path),"users.json");}}
  UserDirectory ReadUsers(){
   if(SqlEnabled)return new UserDirectory{Users=Sql.Read()};
   if(File.Exists(UsersPath)){using(var stream=File.OpenRead(UsersPath))return (UserDirectory)new DataContractJsonSerializer(typeof(UserDirectory)).ReadObject(stream);}
   var directory=new UserDirectory();
   if(File.Exists(path)){using(var stream=File.OpenRead(path)){var old=(AccountRecord)new DataContractJsonSerializer(typeof(AccountRecord)).ReadObject(stream);if(old!=null){old.Role="Admin";directory.Users.Add(old);}}}
   return directory;
  }
  void SaveUsers(UserDirectory directory){if(SqlEnabled)throw new InvalidOperationException("SQL users must be updated with transactional SQL operations.");var tmp=UsersPath+".tmp";using(var stream=File.Create(tmp))new DataContractJsonSerializer(typeof(UserDirectory)).WriteObject(stream,directory);if(File.Exists(UsersPath))File.Replace(tmp,UsersPath,null);else File.Move(tmp,UsersPath);}
  public System.Collections.Generic.List<AccountRecord> ListUsers(){if(!IsAdmin)throw new UnauthorizedAccessException();return ReadUsers().Users;}
  public void AddUser(string name,string password,string role){
   if(!IsAdmin)throw new UnauthorizedAccessException();
   if(role!="Admin"&&role!="Trainer"&&role!="Reception")throw new ArgumentException("Invalid role");
   if(string.IsNullOrWhiteSpace(name)||name.Trim().Length<3||password==null||password.Length<10)throw new ArgumentException("Username 3+, password 10+");
   var users=ReadUsers();if(users.Users.Exists(x=>string.Equals(x.UserName,name.Trim(),StringComparison.OrdinalIgnoreCase)))throw new InvalidOperationException("Username already exists");
   var salt=new byte[16];using(var rng=RandomNumberGenerator.Create())rng.GetBytes(salt);const int rounds=150000;users.Users.Add(new AccountRecord{UserName=name.Trim(),Role=role,Salt=Convert.ToBase64String(salt),Hash=Hash(password,salt,rounds),Iterations=rounds});if(SqlEnabled)Sql.Insert(users.Users[users.Users.Count-1],false);else SaveUsers(users);
  }
  public void ChangePassword(string username,string currentPassword,string newPassword){
   if(string.IsNullOrWhiteSpace(username)||newPassword==null||newPassword.Length<10)throw new ArgumentException("New password must be at least 10 characters.");
   var directory=ReadUsers();
   var record=directory.Users.Find(x=>string.Equals(x.UserName,username,StringComparison.OrdinalIgnoreCase));
   if(record==null||!Verify(record,username,currentPassword))throw new UnauthorizedAccessException("Current password is incorrect.");
   var salt=new byte[16];using(var rng=RandomNumberGenerator.Create())rng.GetBytes(salt);
   const int rounds=150000;record.Salt=Convert.ToBase64String(salt);record.Hash=Hash(newPassword,salt,rounds);record.Iterations=rounds;
   if(SqlEnabled)Sql.ChangePassword(record);else SaveUsers(directory);
  }
  public void DeleteUser(string name){if(!IsAdmin)throw new UnauthorizedAccessException();var users=ReadUsers();var user=users.Users.Find(x=>string.Equals(x.UserName,name,StringComparison.OrdinalIgnoreCase));if(user==null)throw new InvalidOperationException("Not found");if(user.UserName==CurrentUser)throw new InvalidOperationException("Cannot remove current user");if(user.Role=="Admin"&&users.Users.FindAll(x=>x.Role=="Admin").Count<=1)throw new InvalidOperationException("Last admin cannot be removed");users.Users.Remove(user);if(SqlEnabled)Sql.Delete(name,CurrentUser);else SaveUsers(users);}
  public void AssignTrainer(string username,Guid? trainerId){
   if(!IsAdmin)throw new UnauthorizedAccessException();
   if(!SqlEnabled)throw new NotSupportedException("Trainer account linking requires SQL Server mode.");
   Sql.SetTrainer(username,trainerId);
  }
  public static void SignOut(){CurrentUser=null;CurrentRole=null;CurrentTrainerId=null;}
  public void Create(string user,string password){
   if(!NeedsSetup)throw new InvalidOperationException("Account already configured.");
   if(string.IsNullOrWhiteSpace(user)||user.Trim().Length<3)throw new ArgumentException("Username must be at least 3 characters.");
   if(password==null||password.Length<10)throw new ArgumentException("Password must be at least 10 characters.");
   var salt=new byte[16];using(var rng=RandomNumberGenerator.Create())rng.GetBytes(salt);
   const int rounds=150000;var record=new AccountRecord{UserName=user.Trim(),Salt=Convert.ToBase64String(salt),Hash=Hash(password,salt,rounds),Iterations=rounds,Role="Admin"};
   var directory=new UserDirectory();directory.Users.Add(record);if(SqlEnabled)Sql.Insert(record,true);else SaveUsers(directory);CurrentUser=record.UserName;CurrentRole="Admin";CurrentTrainerId=null;
  }
  static string Hash(string password,byte[] salt,int iterations){using(var derive=new Rfc2898DeriveBytes(password,salt,iterations,HashAlgorithmName.SHA256))return Convert.ToBase64String(derive.GetBytes(32));}
  public bool Validate(string user,string password){
   var directory=ReadUsers();
   foreach(var item in directory.Users){if(Verify(item,user,password)){CurrentUser=item.UserName;CurrentRole=string.IsNullOrEmpty(item.Role)?"Admin":item.Role;CurrentTrainerId=item.TrainerId;return true;}}
   return false;
  }
  bool Verify(AccountRecord record,string user,string password){

   if(record==null||record.Iterations<10000)return false;
   var expected=Convert.FromBase64String(record.Hash);var actual=Convert.FromBase64String(Hash(password??"",Convert.FromBase64String(record.Salt),record.Iterations));
   int difference=expected.Length^actual.Length;for(int i=0;i<Math.Min(expected.Length,actual.Length);i++)difference|=expected[i]^actual[i];
   return difference==0&&string.Equals(record.UserName,user==null?"":user.Trim(),StringComparison.OrdinalIgnoreCase);
  }
 }
}
