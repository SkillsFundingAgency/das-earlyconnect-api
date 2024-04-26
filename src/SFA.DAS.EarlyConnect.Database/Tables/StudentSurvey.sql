CREATE TABLE [dbo].[StudentSurvey]
(
	Id				UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
	StudentId       INT NOT NULL,
	SurveyId		INT NULL DEFAULT(0),
	LastUpdated		DATETIME NULL,
	DateCompleted	DATETIME NULL,
	DateEmailSent	DATETIME NULL,
	DateEmailReminderSent	DATETIME NULL,
	DateAdded		DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT FK_StudentSurvey_StudentData
        FOREIGN KEY(StudentId)
        REFERENCES dbo.StudentData(Id),
	CONSTRAINT FK_StudentSurvey_Survey
        FOREIGN KEY(SurveyId) 
        REFERENCES dbo.Survey(Id)
)
