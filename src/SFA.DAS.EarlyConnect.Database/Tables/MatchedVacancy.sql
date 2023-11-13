CREATE TABLE dbo.MatchedVacancy (
    Id                      INT NOT NULL PRIMARY KEY IDENTITY,
    StudentId               INT NOT NULL,
    VacancyTitle            NVARCHAR(150) NOT NULL DEFAULT (''),
    VacancyUrl              NVARCHAR(250) NOT NULL DEFAULT (''),
    VacancyRef              NVARCHAR(100) NOT NULL DEFAULT (''),
    DistanceInMiles         INT NOT NULL DEFAULT (0),
    DatePosted              DATETIME NULL,
    Sector                  NVARCHAR(MAX) NOT NULL DEFAULT (''),
    ApprenticeshipLevel     NVARCHAR(150) NOT NULL DEFAULT (''),
    FrameworkOrStandardName NVARCHAR(150) NOT NULL DEFAULT (''),
    ThirdpartyRef           NVARCHAR(50) NOT NULL DEFAULT (''),
    DateAdded               DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_MatchedVacancy_StudentData 
        FOREIGN KEY(StudentId) 
        REFERENCES dbo.StudentData(Id)
);
