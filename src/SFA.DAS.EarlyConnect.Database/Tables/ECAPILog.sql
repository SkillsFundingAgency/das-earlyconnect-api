﻿CREATE TABLE dbo.ECAPILog (
	Id					INT NOT NULL PRIMARY KEY IDENTITY,
	RequestType			NVARCHAR(50) NOT NULL DEFAULT (''),
	RequestSource		NVARCHAR(150) NOT NULL DEFAULT (''),
	RequestIP			NVARCHAR(15) NOT NULL DEFAULT (''),
	Payload				NVARCHAR(MAX) NOT NULL DEFAULT (''),
	FileName			NVARCHAR(150) NOT NULL DEFAULT (''),
	Status			    NVARCHAR(50) NOT NULL DEFAULT (''),
	Error			    NVARCHAR(MAX) NOT NULL DEFAULT (''),
	CompletedDate		DATETIME NULL,
	DateAdded			DATETIME NOT NULL DEFAULT GETDATE()
)