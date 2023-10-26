CREATE TABLE dbo.ApprenticeMetricsFlag (
	Id				INT NOT NULL PRIMARY KEY IDENTITY,
	MetricsId		INT NOT NULL ,
	FlagId			INT NOT NULL ,
	FlagValue		BIT NOT NULL DEFAULT (0),
	IsDeleted		BIT NOT NULL DEFAULT (0),
	DateAdded		DATETIME NOT NULL DEFAULT GETDATE()
	CONSTRAINT FK_ApprenticeMetricsFlag_ApprenticeMetricsData 
        FOREIGN KEY(MetricsId) 
        REFERENCES dbo.ApprenticeMetricsData(Id),
	CONSTRAINT FK_ApprenticeMetricsFlag_MetricsFlagLookUp 
        FOREIGN KEY(FlagId) 
        REFERENCES dbo.MetricsFlagLookUp(Id)
)