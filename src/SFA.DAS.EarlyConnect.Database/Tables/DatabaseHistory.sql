﻿CREATE TABLE [dbo].[DatabaseHistory]
(
	Id					INT NOT NULL PRIMARY KEY IDENTITY,
	ScriptName			NVARCHAR(100) NOT NULL DEFAULT (''),
	[RunDate]			DATETIME NOT NULL DEFAULT GETDATE()
	
)
