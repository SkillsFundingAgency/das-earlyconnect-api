﻿CREATE TABLE dbo.StudentData (
	Id					INT NOT NULL PRIMARY KEY IDENTITY,
	LogId               INT NOT NULL,
	LEPSId				INT NULL DEFAULT(0),
	FirstName			NVARCHAR(150) NOT NULL DEFAULT(''),
	LastName			NVARCHAR(150) NOT NULL DEFAULT(''),
	DateOfBirth			DATE NULL,
	Email				NVARCHAR(100) NOT NULL,
	Telephone			NVARCHAR(50) NOT NULL DEFAULT (''),
	PostCode			NVARCHAR(10) NOT NULL DEFAULT(''),
	Industry			NVARCHAR(MAX) NOT NULL DEFAULT(''),
	DataSource			NVARCHAR(150) NOT NULL DEFAULT(''),
	SchoolName			NVARCHAR(100) NOT NULL DEFAULT(''),
	DateInterestShown	DATETIME NULL,
	LepDateSent			DATETIME NULL,
	DateAdded			DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT FK_StudentData_ECAPILOG 
        FOREIGN KEY(LogId) 
        REFERENCES dbo.ECAPILog(Id),
	CONSTRAINT FK_StudentData_LEPSData 
        FOREIGN KEY(LEPSId) 
        REFERENCES dbo.LEPSData(Id)
)
