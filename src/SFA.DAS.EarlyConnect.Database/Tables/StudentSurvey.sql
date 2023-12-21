CREATE TABLE [dbo].[StudentSurvey]
(
	Id				UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	StudentId       INT NOT NULL,
	SurveyId		INT NULL DEFAULT(0),
	DateCompleted	DATETIME NULL,
	DateEmailSent	DATETIME NULL,
	DateAdded		DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT FK_StudentSurvey_StudentData
        FOREIGN KEY(StudentId)
        REFERENCES dbo.StudentData(Id),
	CONSTRAINT FK_StudentSurvey_Survey
        FOREIGN KEY(SurveyId) 
        REFERENCES dbo.Survey(Id)
)
