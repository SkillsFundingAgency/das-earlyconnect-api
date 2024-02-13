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
		INSERT INTO dbo.Answer ([Id],[QuestionId],[AnswerText],[ShortDescription],[SortOrder],[IsActive],[DateAdded],[GroupNumber],[GroupLabel])
		VALUES
				(1, 1, N'Intermediate apprenticeships (Level 2)', N'Equal to GCSEs', 1,1 , CAST(N'2024-01-17T09:49:39.977' AS DateTime),2, 'Support Details'),
				(2, 1, N'Advanced apprenticeships (Level 3)', N'Equal to A levels', 2,1, CAST(N'2024-01-17T09:49:39.977' AS DateTime),2, 'Support Details'),
				(3, 1, N'Higher apprenticeships (Levels 4-5)', N'Equal to a Higher National Certificate or foundation degree', 3,1, CAST(N'2024-01-17T09:49:39.977' AS DateTime),2, 'Support Details'),
				(4, 1, N'Degree apprenticeships (Levels 6-7)', N'Equal to an undergraduate or postgraduate degree', 4,1, CAST(N'2024-01-17T09:49:39.977' AS DateTime),2, 'Support Details'),
				(5, 1, N'Not sure', N'', 5,1, CAST(N'2024-01-17T09:49:39.977' AS DateTime),2, 'Support Details'),
				(6, 2, N'Apprenticeship', N'', 1,1, CAST(N'2024-01-17T09:49:39.977' AS DateTime),1, 'Education and Preferences'),
				(7, 2, N'University', N'', 2,1, CAST(N'2024-01-17T09:49:39.977' AS DateTime),1, 'Education and Preferences'),
				(8, 2, N'None of the above', N'', 3,1, CAST(N'2024-01-17T09:49:39.977' AS DateTime),1, 'Education and Preferences'),
				(9, 3, N'Yes, for the right apprenticeship', N'', 1,1, CAST(N'2024-01-17T09:49:39.977' AS DateTime),1, 'Education and Preferences'),
				(10, 3, N'No', N'', 2,1, CAST(N'2024-01-17T09:49:39.977' AS DateTime),1, 'Education and Preferences'),
				(11, 4, N'Understanding apprenticeships', N'Discuss the types of apprenticeship you could do', 1,1, CAST(N'2024-01-17T09:49:39.977' AS DateTime),2, 'Support Details'),
				(12, 4, N'Finding apprenticeship opportunities', N'Get help from our team to find an apprenticeship', 2,1, CAST(N'2024-01-17T09:49:39.977' AS DateTime),2, 'Support Details'),
				(13, 4, N'Applying for an apprenticeship', N'Support with your applications and interviews', 3,1, CAST(N'2024-01-17T09:49:39.977' AS DateTime),2, 'Support Details')
		SET IDENTITY_INSERT [dbo].[Answer] OFF
    END

