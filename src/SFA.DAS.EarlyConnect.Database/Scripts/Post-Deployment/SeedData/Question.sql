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

IF NOT EXISTS (SELECT * FROM [dbo].[Question])
    BEGIN
		SET IDENTITY_INSERT [dbo].[Question] ON
	INSERT INTO dbo.Question ([Id], [SurveyId], [QuestionTypeId], [QuestionText], [ShortDescription], [SummaryLabel], [ValidationMessage],[DefaultToggleAnswerId], [SortOrder], [IsActive], [DateAdded], [GroupNumber], [GroupLabel])
	VALUES 
	(1, 1, 1, N'What level of apprenticeship are you interested in?',N'Select all that apply',N'Apprenticeship level',N'Select levels of apprenticeship you are interested in, or select ''Not sure''', 5, 1, 1, CAST(N'2024-01-17T09:49:39.977' AS DateTime), 2, 'Support Details'),
	(2, 1, 1, N'Have you applied for any of the following?',N'This includes current and past applications. Select all that apply.',N'Current and past applications',N'Select options you have applied for, or select ''None of the above''', 8, 2, 1, CAST(N'2024-01-17T09:49:39.977' AS DateTime),1, 'Education and Preferences'),
	(3, 1, 2, N'Would you move to another area of England for an apprenticeship?',N'',N'Working in other areas of the country',N'Select if you would move to another area of England for an apprenticeship', null, 3, 1, CAST(N'2024-01-17T09:49:39.977' AS DateTime),1, 'Education and Preferences'),
	(4, 1, 1, N'What would you like support with?',N'Select all that apply',N'Support request',N'Select what you would like support with', null, 4, 1, CAST(N'2024-01-17T09:49:39.977' AS DateTime), 2, 'Support Details')

SET IDENTITY_INSERT [dbo].[Question] OFF
    END
