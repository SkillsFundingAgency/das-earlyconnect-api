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
	SET IDENTITY_INSERT [dbo].[QuestionType] ON
        INSERT INTO dbo.Question (Id, SurveyId, QuestionTypeId, QuestionText, ShortDescription, SummaryLabel, ValidationMessage, DefaultToggleAnswerId, SortOrder, IsActive, DateAdded)
        VALUES
            (1, 1, 1, 'What is your favorite color?', 'Color Question', 'Color Preference', 'Please choose a color', NULL, 0, 1, GETDATE()),
            (2, 1, 2, 'What is your feedback?', 'Feedback Question', 'Feedback Summary', 'Provide your feedback', NULL, 1, 1, GETDATE()),
            (3, 1, 3, 'Select your preferences:', 'Preferences Question', 'Preferences Summary', 'Select one or more preferences', NULL, 2, 1, GETDATE());
    SET IDENTITY_INSERT [dbo].[QuestionType] OFF
END



