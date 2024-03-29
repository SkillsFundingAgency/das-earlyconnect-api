﻿CREATE TABLE [dbo].[Question]
(
	Id						INT NOT NULL PRIMARY KEY IDENTITY,
	SurveyId				INT NOT NULL,
	QuestionTypeId			INT NOT NULL,
	QuestionText			NVARCHAR(250) NOT NULL DEFAULT(''),
	ShortDescription		NVARCHAR(250) NOT NULL DEFAULT(''),
	SummaryLabel			NVARCHAR(150) NOT NULL DEFAULT(''),
	ValidationMessage		NVARCHAR(250) NOT NULL DEFAULT(''),
	DefaultToggleAnswerId	INT NULL,
	SortOrder				INT NOT NULL DEFAULT(0),
	IsActive				BIT NOT NULL DEFAULT (0),
	DateAdded				DATETIME NOT NULL DEFAULT GETDATE(),
	GroupNumber			    INT NOT NULL DEFAULT(0),
	GroupLabel				NVARCHAR(100) NOT NULL DEFAULT(''),
	CONSTRAINT FK_Question_Survey
        FOREIGN KEY(SurveyId)
        REFERENCES dbo.Survey(Id),
	CONSTRAINT FK_Question_QuestionType
        FOREIGN KEY(QuestionTypeId) 
        REFERENCES dbo.QuestionType(Id)
)
