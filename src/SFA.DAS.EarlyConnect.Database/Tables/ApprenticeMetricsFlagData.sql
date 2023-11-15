CREATE TABLE dbo.ApprenticeMetricsFlagData (
	Id				INT NOT NULL PRIMARY KEY IDENTITY,
	MetricsId		INT NOT NULL ,
	FlagId			INT NOT NULL ,
	FlagValue		BIT NOT NULL DEFAULT (0),
	IsDeleted		BIT NOT NULL DEFAULT (0),
	DateAdded		DATETIME NOT NULL DEFAULT GETDATE()
	CONSTRAINT FK_ApprenticeMetricsFlagData_ApprenticeMetricsData 
        FOREIGN KEY(MetricsId) 
        REFERENCES dbo.ApprenticeMetricsData(Id),
	CONSTRAINT FK_ApprenticeMetricsFlagDatag_MetricsFlag 
        FOREIGN KEY(FlagId) 
        REFERENCES dbo.MetricsFlag(Id)
)