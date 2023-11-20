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

IF NOT EXISTS(SELECT * FROM [dbo].[LEPSUser])
    BEGIN
        INSERT INTO dbo.LEPSUser (LEPSId, FirstName, LastName, Email, PhoneNumber, JobTitle, GDPRCompliance, DateAdded)
        VALUES
            ((SELECT Id FROM dbo.LEPSData WHERE LepCode = 'E37000025'), 'John', 'Doe', 'john@example.com', '123-456-7890', 'Manager', 1, GETDATE()),
	        ((SELECT Id FROM dbo.LEPSData WHERE LepCode = 'E37000025'), 'Jack', 'Dan', 'Jack@example.com', '123-456-7890', 'Designer', 1, GETDATE()),
            ((SELECT Id FROM dbo.LEPSData WHERE LepCode = 'E37000019'), 'Jane', 'Smith', 'jane@example.com', '987-654-3210', 'Supervisor', 1, GETDATE()),
            ((SELECT Id FROM dbo.LEPSData WHERE LepCode = 'E37000051'), 'Test', 'User', 'test@example.com', '555-555-5555', 'Analyst', 0, GETDATE());
    END