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
    INSERT INTO dbo.QuestionType (Id, QuestionTypeText, DateAdded)
    VALUES
        (1, 'Multiple Choice', GETDATE()),
        (2, 'Text Response', GETDATE()),
        (3, 'Checkbox', GETDATE());
END


