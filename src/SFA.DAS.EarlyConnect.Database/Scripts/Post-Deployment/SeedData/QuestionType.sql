/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

IF NOT EXISTS (SELECT * FROM [dbo].[QuestionType])
    BEGIN
		SET IDENTITY_INSERT [dbo].[QuestionType] ON
			INSERT INTO dbo.QuestionType ([Id], [QuestionTypeText], [DateAdded])
			VALUES
				(1, N'Checkbox', CAST(N'2024-01-17T09:49:39.977' AS DateTime)),
				(2, N'Radio', CAST(N'2024-01-17T09:49:39.977' AS DateTime)),
				(3, N'Free Text', CAST(N'2024-01-17T09:49:39.977' AS DateTime))	
		SET IDENTITY_INSERT [dbo].[QuestionType] OFF
    END

