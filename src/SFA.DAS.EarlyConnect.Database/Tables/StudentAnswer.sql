CREATE TABLE [dbo].[StudentAnswer]
(
	Id					INT NOT NULL PRIMARY KEY,
	StudentSurveyId		UNIQUEIDENTIFIER NOT NULL,
	QuestionId			INT NOT NULL,
	AnswerId			INT NOT NULL,
	Response			NVARCHAR(250) NOT NULL DEFAULT (''),
	DateAdded			DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT FK_StudentAnswer_StudentSurvey
        FOREIGN KEY(StudentSurveyId)
        REFERENCES dbo.StudentSurvey(Id),
	CONSTRAINT FK_StudentAnswer_Question
        FOREIGN KEY(QuestionId) 
        REFERENCES dbo.Question(Id),
	CONSTRAINT FK_StudentAnswer_Answer
        FOREIGN KEY(AnswerId) 
        REFERENCES dbo.Answer(Id),
)
