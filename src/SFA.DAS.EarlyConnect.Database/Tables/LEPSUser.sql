﻿CREATE TABLE dbo.LEPSUser (
	Id				INT NOT NULL PRIMARY KEY IDENTITY,
	LEPSId			INT NOT NULL ,
	FirstName		NVARCHAR(150) NOT NULL DEFAULT (''),
	LastName		NVARCHAR(150) NOT NULL DEFAULT (''),
	Email			NVARCHAR(100) NOT NULL DEFAULT (''),
	PhoneNumber		NVARCHAR(50) NOT NULL DEFAULT (''),
	JobTitle		NVARCHAR(150)  NULL ,
	GDPRCompliance	BIT NOT NULL DEFAULT (0),
	DateAdded		DATETIME NOT NULL DEFAULT GETDATE()
	CONSTRAINT FK_LEPSUser_LEPSData 
        FOREIGN KEY(LEPSId) 
        REFERENCES dbo.LEPSData(Id)
)