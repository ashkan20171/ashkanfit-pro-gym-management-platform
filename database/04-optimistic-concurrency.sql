-- Stage 39: prevent silent lost updates from concurrent sessions.
USE [AshkanFitPro];
GO
IF OBJECT_ID(N'dbo.AppSnapshots',N'U') IS NOT NULL AND COL_LENGTH('dbo.AppSnapshots','RowVersion') IS NULL
BEGIN
    ALTER TABLE dbo.AppSnapshots ADD RowVersion ROWVERSION;
END;
GO
