-- Run as a SQL Server login with CREATE DATABASE privileges.
IF DB_ID(N'AshkanFitPro') IS NULL
    CREATE DATABASE AshkanFitPro;
GO
USE AshkanFitPro;
GO
IF OBJECT_ID(N'dbo.AppSnapshots', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.AppSnapshots
    (
        SnapshotId INT NOT NULL CONSTRAINT PK_AppSnapshots PRIMARY KEY,
        Payload VARBINARY(MAX) NOT NULL,
        UpdatedAt DATETIME2 NOT NULL
    );
END;
GO
