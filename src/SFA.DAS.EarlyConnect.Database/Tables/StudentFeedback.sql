SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[StudentFeedback](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[StudentSurveyId] [uniqueidentifier] NOT NULL,
	[LogId] [int] NOT NULL,
	[StatusUpdate] [nvarchar](100) NOT NULL,
	[Notes] [nvarchar](max) NOT NULL,
	[UpdatedBy] [nvarchar](100) NOT NULL,
	[DateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_StudentFeedback] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[StudentFeedback] ADD  CONSTRAINT [DF_StudentFeedback_StatusUpdate]  DEFAULT ('') FOR [StatusUpdate]
GO

ALTER TABLE [dbo].[StudentFeedback] ADD  CONSTRAINT [DF_StudentFeedback_Notes]  DEFAULT ('') FOR [Notes]
GO

ALTER TABLE [dbo].[StudentFeedback] ADD  CONSTRAINT [DF_Table_1_User]  DEFAULT ('') FOR [UpdatedBy]
GO

ALTER TABLE [dbo].[StudentFeedback] ADD  CONSTRAINT [DF_StudentFeedback_DateAdded]  DEFAULT (getdate()) FOR [DateAdded]
GO

ALTER TABLE [dbo].[StudentFeedback]  WITH CHECK ADD  CONSTRAINT [FK_StudentFeedback_ECAPILog] FOREIGN KEY([LogId])
REFERENCES [dbo].[ECAPILog] ([Id])
GO

ALTER TABLE [dbo].[StudentFeedback] CHECK CONSTRAINT [FK_StudentFeedback_ECAPILog]
GO

ALTER TABLE [dbo].[StudentFeedback]  WITH CHECK ADD  CONSTRAINT [FK_StudentFeedback_StudentData] FOREIGN KEY([StudentId])
REFERENCES [dbo].[StudentData] ([Id])
GO

ALTER TABLE [dbo].[StudentFeedback] CHECK CONSTRAINT [FK_StudentFeedback_StudentData]
GO

ALTER TABLE [dbo].[StudentFeedback]  WITH CHECK ADD  CONSTRAINT [FK_StudentFeedback_StudentSurvey] FOREIGN KEY([StudentSurveyId])
REFERENCES [dbo].[StudentSurvey] ([Id])
GO

ALTER TABLE [dbo].[StudentFeedback] CHECK CONSTRAINT [FK_StudentFeedback_StudentSurvey]
GO


