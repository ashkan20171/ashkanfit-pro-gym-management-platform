using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;

namespace AshkanFitPro {
 /// <summary>Transactional SQL Server persistence for the existing Store contract.</summary>
 public sealed class SqlStore {
  readonly string connectionString;
  byte[] lastVersion;
  public static bool Enabled { get { return string.Equals(ConfigurationManager.AppSettings["StorageProvider"], "SqlServer", StringComparison.OrdinalIgnoreCase); } }
  public SqlStore() {
   var setting=ConfigurationManager.ConnectionStrings["AshkanFitPro"];
   if(setting==null || string.IsNullOrWhiteSpace(setting.ConnectionString))throw new InvalidOperationException("Missing AshkanFitPro SQL Server connection string in App.config.");
   connectionString=setting.ConnectionString;
  }
  SqlConnection Open() { var connection=new SqlConnection(connectionString); connection.Open(); return connection; }
  public Store Load() {
   using(var connection=Open()) {
    EnsureSchema(connection);
    using(var command=new SqlCommand("SELECT Payload, RowVersion FROM dbo.AppSnapshots WHERE SnapshotId = @id",connection)) {
     command.Parameters.Add("@id",SqlDbType.Int).Value=1;
     byte[] payload;
     using(var reader=command.ExecuteReader()) {
      if(!reader.Read()) { lastVersion=null; return null; }
      payload=(byte[])reader[0];
      lastVersion=(byte[])reader[1];
     }
     using(var stream=new MemoryStream(payload))return (Store)new DataContractJsonSerializer(typeof(Store)).ReadObject(stream);
    }
   }
  }
  public void Save(Store store) {
   StoreIntegrity.Validate(store);
   byte[] payload;
   using(var stream=new MemoryStream()) { new DataContractJsonSerializer(typeof(Store)).WriteObject(stream,store); payload=stream.ToArray(); }
   using(var connection=Open()) {
    EnsureSchema(connection);
    using(var transaction=connection.BeginTransaction(IsolationLevel.Serializable)) {
     try {
      // Optimistic concurrency: reject stale saves instead of overwriting another session.
      if(lastVersion==null) {
       using(var command=new SqlCommand(@"IF NOT EXISTS (SELECT 1 FROM dbo.AppSnapshots WITH (UPDLOCK,HOLDLOCK) WHERE SnapshotId=1)
INSERT INTO dbo.AppSnapshots(SnapshotId,Payload,UpdatedAt) VALUES(1,@payload,SYSUTCDATETIME());
ELSE THROW 51001, 'Gym data was created by another session. Reload before saving.', 1;",connection,transaction)) {
        command.Parameters.Add("@payload",SqlDbType.VarBinary,-1).Value=payload;
        command.ExecuteNonQuery();
       }
      } else {
       using(var command=new SqlCommand(@"UPDATE dbo.AppSnapshots SET Payload=@payload,UpdatedAt=SYSUTCDATETIME()
WHERE SnapshotId=1 AND RowVersion=@expected;",connection,transaction)) {
        command.Parameters.Add("@payload",SqlDbType.VarBinary,-1).Value=payload;
        command.Parameters.Add("@expected",SqlDbType.Timestamp,8).Value=lastVersion;
        if(command.ExecuteNonQuery()!=1)throw new DBConcurrencyException("Gym data changed in another session. Restart the application to reload before saving. No changes were written.");
       }
      }
      RelationalMirror.Save(connection,transaction,store);
      SnapshotHistory.Append(connection,transaction,payload);
      SqlAuditTrail.Append(connection,transaction,"STORE_SAVE","Gym snapshot and relational mirror committed");
      byte[] committedVersion;
      using(var versionCommand=new SqlCommand("SELECT RowVersion FROM dbo.AppSnapshots WHERE SnapshotId=1",connection,transaction))
       committedVersion=(byte[])versionCommand.ExecuteScalar();
      transaction.Commit();
      lastVersion=committedVersion;
     } catch { transaction.Rollback(); throw; }
    }
   }
  }
  static void EnsureSchema(SqlConnection connection) {
   using(var command=new SqlCommand(@"IF OBJECT_ID(N'dbo.AppSnapshots', N'U') IS NULL
BEGIN
CREATE TABLE dbo.AppSnapshots (SnapshotId INT NOT NULL CONSTRAINT PK_AppSnapshots PRIMARY KEY,
Payload VARBINARY(MAX) NOT NULL, UpdatedAt DATETIME2 NOT NULL, RowVersion ROWVERSION NOT NULL);
END",connection))command.ExecuteNonQuery();
   using(var versionUpgrade=new SqlCommand(@"IF COL_LENGTH('dbo.AppSnapshots','RowVersion') IS NULL
ALTER TABLE dbo.AppSnapshots ADD RowVersion ROWVERSION;",connection))versionUpgrade.ExecuteNonQuery();
   using(var history=new SqlCommand(@"IF OBJECT_ID(N'dbo.AppSnapshotHistory',N'U') IS NULL
BEGIN
 CREATE TABLE dbo.AppSnapshotHistory (
  HistoryId BIGINT IDENTITY(1,1) NOT NULL CONSTRAINT PK_AppSnapshotHistory PRIMARY KEY,
  SnapshotId INT NOT NULL,
  Payload VARBINARY(MAX) NOT NULL,
  PayloadSha256 BINARY(32) NOT NULL,
  RecordedAtUtc DATETIME2(3) NOT NULL
 );
 CREATE INDEX IX_AppSnapshotHistory_RecordedAtUtc ON dbo.AppSnapshotHistory(RecordedAtUtc DESC);
END",connection))history.ExecuteNonQuery();
   using(var audit=new SqlCommand(@"IF OBJECT_ID(N'dbo.SecurityAuditEvents',N'U') IS NULL
BEGIN
 CREATE TABLE dbo.SecurityAuditEvents (
  AuditEventId BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  OccurredAtUtc DATETIME2(3) NOT NULL,
  Actor NVARCHAR(128) NOT NULL,
  ActionName NVARCHAR(80) NOT NULL,
  Details NVARCHAR(400) NULL
 );
 CREATE INDEX IX_SecurityAuditEvents_OccurredAtUtc ON dbo.SecurityAuditEvents(OccurredAtUtc DESC);
END",connection))audit.ExecuteNonQuery();
   using(var cmd=new SqlCommand(@"-- AshkanFit Pro relational reporting model. Execute after 01-create-database.sql.

IF OBJECT_ID(N'dbo.Members',N'U') IS NULL CREATE TABLE dbo.Members(MemberId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, Name NVARCHAR(200) NOT NULL, Phone NVARCHAR(40) NULL, PlanName NVARCHAR(120) NULL, StartDate DATETIME2 NOT NULL, ExpiryDate DATETIME2 NOT NULL, Fee DECIMAL(18,2) NOT NULL);
IF OBJECT_ID(N'dbo.Trainers',N'U') IS NULL CREATE TABLE dbo.Trainers(TrainerId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, Name NVARCHAR(200) NOT NULL, Specialty NVARCHAR(200) NULL, Phone NVARCHAR(40) NULL);
IF OBJECT_ID(N'dbo.Payments',N'U') IS NULL CREATE TABLE dbo.Payments(PaymentId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, MemberId UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Members(MemberId), Amount DECIMAL(18,2) NOT NULL, PaidAt DATETIME2 NOT NULL, Note NVARCHAR(MAX) NULL);
IF OBJECT_ID(N'dbo.Attendance',N'U') IS NULL CREATE TABLE dbo.Attendance(AttendanceId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, MemberId UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Members(MemberId), CheckedInAt DATETIME2 NOT NULL);
IF OBJECT_ID(N'dbo.WorkoutPlans',N'U') IS NULL CREATE TABLE dbo.WorkoutPlans(WorkoutPlanId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, MemberId UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Members(MemberId), TrainerId UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Trainers(TrainerId), Goal NVARCHAR(MAX) NULL, Exercises NVARCHAR(MAX) NULL, CreatedAt DATETIME2 NOT NULL);
IF OBJECT_ID(N'dbo.GymClasses',N'U') IS NULL CREATE TABLE dbo.GymClasses(ClassId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, TrainerId UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Trainers(TrainerId), Title NVARCHAR(200) NOT NULL, Weekday INT NOT NULL, ClassTime NVARCHAR(40) NULL, Capacity INT NOT NULL);
IF OBJECT_ID(N'dbo.BodyProgress',N'U') IS NULL CREATE TABLE dbo.BodyProgress(ProgressId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, MemberId UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Members(MemberId), RecordedAt DATETIME2 NOT NULL, WeightKg DECIMAL(12,2) NOT NULL, HeightCm DECIMAL(12,2) NOT NULL, WaistCm DECIMAL(12,2) NOT NULL, BodyFatPercent DECIMAL(8,2) NOT NULL, Goal NVARCHAR(MAX) NULL);
IF OBJECT_ID(N'dbo.Expenses',N'U') IS NULL CREATE TABLE dbo.Expenses(ExpenseId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, ExpenseDate DATETIME2 NOT NULL, Category NVARCHAR(150) NULL, Description NVARCHAR(MAX) NULL, Amount DECIMAL(18,2) NOT NULL);
IF OBJECT_ID(N'dbo.AuditEntries',N'U') IS NULL CREATE TABLE dbo.AuditEntries(AuditId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, OccurredAt DATETIME2 NOT NULL, ActionName NVARCHAR(100) NULL, Details NVARCHAR(MAX) NULL);
IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE name=N'IX_Payments_MemberId' AND object_id=OBJECT_ID(N'dbo.Payments')) CREATE INDEX IX_Payments_MemberId ON dbo.Payments(MemberId,PaidAt DESC);
IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE name=N'IX_Attendance_MemberId' AND object_id=OBJECT_ID(N'dbo.Attendance')) CREATE INDEX IX_Attendance_MemberId ON dbo.Attendance(MemberId,CheckedInAt DESC);
IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE name=N'IX_BodyProgress_MemberId' AND object_id=OBJECT_ID(N'dbo.BodyProgress')) CREATE INDEX IX_BodyProgress_MemberId ON dbo.BodyProgress(MemberId,RecordedAt DESC);
",connection))cmd.ExecuteNonQuery();
  }
 }
}
