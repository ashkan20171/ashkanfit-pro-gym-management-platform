-- Stage 46: transactional security audit. Run after database scripts 01-06.
IF OBJECT_ID(N'dbo.SecurityAuditEvents',N'U') IS NULL
BEGIN
 CREATE TABLE dbo.SecurityAuditEvents (
  AuditEventId BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  OccurredAtUtc DATETIME2(3) NOT NULL,
  Actor NVARCHAR(128) NOT NULL,
  ActionName NVARCHAR(80) NOT NULL,
  Details NVARCHAR(400) NULL
 );
 CREATE INDEX IX_SecurityAuditEvents_OccurredAtUtc ON dbo.SecurityAuditEvents(OccurredAtUtc DESC);
END;
GO
SELECT TOP (100) AuditEventId,OccurredAtUtc,Actor,ActionName,Details
FROM dbo.SecurityAuditEvents ORDER BY AuditEventId DESC;
GO
