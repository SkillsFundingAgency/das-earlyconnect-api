CREATE TABLE [dbo].[Answer]
(
	Id					INT NOT NULL PRIMARY KEY IDENTITY,
	QuestionId			INT NOT NULL,
	AnswerText			NVARCHAR(250) NOT NULL DEFAULT (''),
	ShortDescription	NVARCHAR(100) NOT NULL DEFAULT (''),
	GroupNumber 		INT NOT NULL DEFAULT(0),
	GroupLabel			INT NOT NULL DEFAULT(0),
	SortOrder			INT NOT NULL DEFAULT(0),
	IsActive			BIT NOT NULL DEFAULT (0),
	DateAdded			DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT FK_Answer_Question
        FOREIGN KEY(QuestionId)
        REFERENCES dbo.Question(Id)
)
