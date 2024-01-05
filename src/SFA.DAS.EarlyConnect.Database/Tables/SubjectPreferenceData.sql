CREATE TABLE dbo.SubjectPreferenceData (
	Id					INT NOT NULL PRIMARY KEY IDENTITY,
	LogId               INT NOT NULL,
	LEPSId				INT NOT NULL,
	IntendedStartYear	NUMERIC(4,0) NOT NULL DEFAULT(0),
	SubjectPreference	NVARCHAR(250) NOT NULL DEFAULT(''),
	NoOfStudents		INT NOT NULL DEFAULT(0),
	IsDeleted           BIT NOT NULL DEFAULT 0,
	LepDateSent			DATETIME NULL,
    DateAdded           DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT FK_SubjectPreferenceData_ECAPILOG 
        FOREIGN KEY(LogId) 
        REFERENCES dbo.ECAPILog(Id),
	CONSTRAINT FK_SubjectPreferenceData_LEPSData 
        FOREIGN KEY(LEPSId) 
        REFERENCES dbo.LEPSData(Id)
)
