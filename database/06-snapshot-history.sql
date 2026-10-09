-- Stage 43: immutable snapshot history (run against AshkanFitPro database)
-- Back up the database before running. Idempotent.
IF OBJECT_ID(N'dbo.AppSnapshotHistory', N'U') IS NULL
BEGIN
 CREATE TABLE dbo.AppSnapshotHistory (
  HistoryId BIGINT IDENTITY(1,1) NOT NULL CONSTRAINT PK_AppSnapshotHistory PRIMARY KEY,
  SnapshotId INT NOT NULL,
  Payload VARBINARY(MAX) NOT NULL,
  PayloadSha256 BINARY(32) NOT NULL,
  RecordedAtUtc DATETIME2(3) NOT NULL
 );
 CREATE INDEX IX_AppSnapshotHistory_RecordedAtUtc ON dbo.AppSnapshotHistory(RecordedAtUtc DESC);
END;
GO
-- Read-only inspection of saved revisions:
SELECT TOP (25) HistoryId, SnapshotId, RecordedAtUtc,
       DATALENGTH(Payload) AS PayloadBytes,
       CONVERT(VARCHAR(64), PayloadSha256, 2) AS Sha256
FROM dbo.AppSnapshotHistory ORDER BY HistoryId DESC;
GO
