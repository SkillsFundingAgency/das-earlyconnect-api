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

IF NOT EXISTS (SELECT * FROM [dbo].[Answer])
    BEGIN
		SET IDENTITY_INSERT [dbo].[Answer] ON	
		INSERT INTO dbo.Answer ([Id],[QuestionId],[AnswerText],[ShortDescription],[SortOrder],[IsActive],[DateAdded])
			VALUES
				   (1, 1, N'', N'', 1, 1, getdate()),
				   (2, 2, N'Intermediate apprenticeships (Level 2)', N'Equal to GCSEs', 1,1 , CAST(N'2024-01-17T09:49:39.977' AS DateTime)),
				   (3, 2, N'Advanced apprenticeships (Level 3)', N'Equal to A levels', 2,1, CAST(N'2024-01-17T09:49:39.977' AS DateTime)),
				   (4, 2, N'Higher apprenticeships (Levels 4-5)', N'Equal to a Higher National Certificate or foundation degree', 3,1, CAST(N'2024-01-17T09:49:39.977' AS DateTime)),
				   (5, 2, N'Degree apprenticeships (Levels 6-7)', N'Equal to an undergraduate or postgraduate degree', 4,1, CAST(N'2024-01-17T09:49:39.977' AS DateTime)),
				   (6, 2, N'Not sure', N'', 5,1, CAST(N'2024-01-17T09:49:39.977' AS DateTime)),
				   (7, 3, N'Apprenticeship', N'', 1,1, CAST(N'2024-01-17T09:49:39.977' AS DateTime)),
				   (8, 3, N'University', N'', 2,1, CAST(N'2024-01-17T09:49:39.977' AS DateTime)),
				   (9, 3, N'None of the above', N'', 3,1, CAST(N'2024-01-17T09:49:39.977' AS DateTime)),
				   (10, 4, N'Yes, for the right apprenticeship', N'', 1,1, CAST(N'2024-01-17T09:49:39.977' AS DateTime)),
				   (12, 4, N'No', N'', 2,1, CAST(N'2024-01-17T09:49:39.977' AS DateTime)),
				   (13, 5, N'Understanding apprenticeships', N'Discuss the types of apprenticeship you could do', 1,1, CAST(N'2024-01-17T09:49:39.977' AS DateTime)),
				   (14, 5, N'Finding apprenticeship opportunities', N'Get help from our team to find an apprenticeship', 2,1, CAST(N'2024-01-17T09:49:39.977' AS DateTime)),
				   (15, 5, N'Applying for an apprenticeship', N'Support with your applications and interviews', 3,1, CAST(N'2024-01-17T09:49:39.977' AS DateTime))
		SET IDENTITY_INSERT [dbo].[Answer] OFF
    END