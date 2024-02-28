CREATE TABLE dbo.ApprenticeMetricsData (
    Id                    INT NOT NULL PRIMARY KEY IDENTITY,
    LogId                 INT NOT NULL,
    LEPSId                INT NOT NULL,
    IntendedStartYear     NUMERIC(4, 0) NOT NULL DEFAULT 0,
    MaxTravelInMiles      INT NOT NULL DEFAULT 0,
    WillingnessToRelocate BIT NOT NULL DEFAULT 0,
    NoOfGCSCs             INT NOT NULL DEFAULT 0,
    NoOfStudents          INT NOT NULL DEFAULT 0,
    IsDeleted             BIT NOT NULL DEFAULT 0,
    DateAdded             DATETIME NOT NULL DEFAULT GETDATE(),
    LepDateSent           DATETIME NULL, 
    CONSTRAINT FK_ApprenticeMetricsData_ECAPILOG 
        FOREIGN KEY(LogId) 
        REFERENCES dbo.ECAPILog(Id),
    CONSTRAINT FK_ApprenticeMetricsData_LEPSData 
        FOREIGN KEY(LEPSId) 
        REFERENCES dbo.LEPSData(Id)
);
