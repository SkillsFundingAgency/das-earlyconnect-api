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
        INSERT INTO dbo.Answer (Id, QuestionId, AnswerText, ShortDescription, GroupNumber, GroupLabel, SortOrder, IsActive, DateAdded)
        VALUES
            (1, 1, 'Red', 'Option for Red', 1, 101, 0, 1, GETDATE()),
            (2, 1, 'Blue', 'Option for Blue', 1, 102, 1, 1, GETDATE()),
            (3, 2, 'Good', 'Positive Feedback', 0, 0, 0, 1, GETDATE()),
            (4, 2, 'Average', 'Neutral Feedback', 0, 0, 1, 1, GETDATE()),
            (5, 2, 'Poor', 'Negative Feedback', 0, 0, 2, 1, GETDATE()),
            (6, 3, 'Option 1', 'Checkbox Option 1', 1, 201, 0, 1, GETDATE()),
            (7, 3, 'Option 2', 'Checkbox Option 2', 1, 202, 1, 1, GETDATE());
    SET IDENTITY_INSERT [dbo].[Answer] OFF
END



