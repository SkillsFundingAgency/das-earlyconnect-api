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

IF NOT EXISTS(SELECT * FROM dbo.[Survey])
    BEGIN
			INSERT INTO dbo.[Survey] ([Id], [Title], [Description], [StartDate], [EndDate], [IsActive], [DateAdded])
			VALUES
				(1, 'Default Dummy Survey', 'Default Dummy Survey', CAST(N'2023-11-09T09:51:49.550' AS DateTime), CAST(N'2024-11-09T09:51:49.550' AS DateTime), 1, GETDATE())
    END