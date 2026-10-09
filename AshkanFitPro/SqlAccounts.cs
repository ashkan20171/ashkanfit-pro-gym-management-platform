using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AshkanFitPro {
 /// <summary>SQL-backed identity directory; credentials never stored as plaintext.</summary>
 internal sealed class SqlAccounts {
  readonly string connectionString;
  internal SqlAccounts() {
   var setting=ConfigurationManager.ConnectionStrings["AshkanFitPro"];
   if(setting==null||string.IsNullOrWhiteSpace(setting.ConnectionString))throw new InvalidOperationException("SQL connection string AshkanFitPro is missing.");
   connectionString=setting.ConnectionString;
  }
  SqlConnection Open(){var c=new SqlConnection(connectionString);c.Open();return c;}
  void Ensure(SqlConnection c){using(var cmd=new SqlCommand(@"IF OBJECT_ID(N'dbo.AppUsers',N'U') IS NULL
BEGIN
 CREATE TABLE dbo.AppUsers(UserName NVARCHAR(100) NOT NULL CONSTRAINT PK_AppUsers PRIMARY KEY,
 Salt NVARCHAR(100) NOT NULL, PasswordHash NVARCHAR(150) NOT NULL,
 Iterations INT NOT NULL, Role NVARCHAR(20) NOT NULL,
 TrainerId UNIQUEIDENTIFIER NULL, CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_AppUsers_CreatedAt DEFAULT SYSUTCDATETIME(),
 CONSTRAINT CK_AppUsers_Role CHECK(Role IN ('Admin','Trainer','Reception')));
END",c))cmd.ExecuteNonQuery();}
  internal List<AccountRecord> Read(){using(var c=Open()){Ensure(c);using(var cmd=new SqlCommand("SELECT UserName,Salt,PasswordHash,Iterations,Role,TrainerId FROM dbo.AppUsers ORDER BY UserName",c))using(var r=cmd.ExecuteReader()){var list=new List<AccountRecord>();while(r.Read())list.Add(new AccountRecord{UserName=r.GetString(0),Salt=r.GetString(1),Hash=r.GetString(2),Iterations=r.GetInt32(3),Role=r.GetString(4),TrainerId=r.IsDBNull(5)?null:(Guid?)r.GetGuid(5)});return list;}}}
  internal void Insert(AccountRecord a,bool setup){using(var c=Open()){Ensure(c);using(var tx=c.BeginTransaction(IsolationLevel.Serializable)){try{
   if(setup){using(var check=new SqlCommand("SELECT COUNT(*) FROM dbo.AppUsers WITH (UPDLOCK,HOLDLOCK)",c,tx))if(Convert.ToInt32(check.ExecuteScalar())!=0)throw new InvalidOperationException("An administrator account already exists.");}
   using(var cmd=new SqlCommand("INSERT INTO dbo.AppUsers(UserName,Salt,PasswordHash,Iterations,Role,TrainerId) VALUES(@name,@salt,@hash,@rounds,@role,@trainer)",c,tx)){
    cmd.Parameters.Add("@name",SqlDbType.NVarChar,100).Value=a.UserName;cmd.Parameters.Add("@salt",SqlDbType.NVarChar,100).Value=a.Salt;
    cmd.Parameters.Add("@hash",SqlDbType.NVarChar,150).Value=a.Hash;cmd.Parameters.Add("@rounds",SqlDbType.Int).Value=a.Iterations;
    cmd.Parameters.Add("@role",SqlDbType.NVarChar,20).Value=a.Role;cmd.Parameters.Add("@trainer",SqlDbType.UniqueIdentifier).Value=(object)a.TrainerId??DBNull.Value;
    cmd.ExecuteNonQuery();}tx.Commit();}catch{tx.Rollback();throw;}}}}
  internal void ChangePassword(AccountRecord a){using(var c=Open()){Ensure(c);using(var cmd=new SqlCommand("UPDATE dbo.AppUsers SET Salt=@salt,PasswordHash=@hash,Iterations=@rounds WHERE UserName=@name",c)){
   cmd.Parameters.Add("@salt",SqlDbType.NVarChar,100).Value=a.Salt;cmd.Parameters.Add("@hash",SqlDbType.NVarChar,150).Value=a.Hash;
   cmd.Parameters.Add("@rounds",SqlDbType.Int).Value=a.Iterations;cmd.Parameters.Add("@name",SqlDbType.NVarChar,100).Value=a.UserName;
   if(cmd.ExecuteNonQuery()!=1)throw new InvalidOperationException("Account no longer exists.");}}}
  internal void Delete(string username,string currentUser){using(var c=Open()){Ensure(c);using(var tx=c.BeginTransaction(IsolationLevel.Serializable)){try{
   using(var cmd=new SqlCommand(@"DELETE FROM dbo.AppUsers WHERE UserName=@name AND UserName<>@current AND
(Role<>'Admin' OR (SELECT COUNT(*) FROM dbo.AppUsers WITH(UPDLOCK,HOLDLOCK) WHERE Role='Admin')>1)",c,tx)){
    cmd.Parameters.Add("@name",SqlDbType.NVarChar,100).Value=username;cmd.Parameters.Add("@current",SqlDbType.NVarChar,100).Value=currentUser??"";
    if(cmd.ExecuteNonQuery()!=1)throw new InvalidOperationException("Cannot remove current user, last admin, or missing account.");}
   tx.Commit();}catch{tx.Rollback();throw;}}}}
  internal void SetTrainer(string username,Guid? trainerId){using(var c=Open()){Ensure(c);using(var cmd=new SqlCommand("UPDATE dbo.AppUsers SET TrainerId=@id WHERE UserName=@name AND Role='Trainer'",c)){
   cmd.Parameters.Add("@name",SqlDbType.NVarChar,100).Value=username;cmd.Parameters.Add("@id",SqlDbType.UniqueIdentifier).Value=(object)trainerId??DBNull.Value;
   if(cmd.ExecuteNonQuery()!=1)throw new InvalidOperationException("Trainer account not found.");}}}
 }
}
