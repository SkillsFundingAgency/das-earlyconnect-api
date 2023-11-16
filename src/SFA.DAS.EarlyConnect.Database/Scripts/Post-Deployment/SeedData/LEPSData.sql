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
IF NOT EXISTS(SELECT * FROM [dbo].[LEPSData])
    BEGIN
        INSERT INTO dbo.LEPSData
            (LepCode, LepName, EntityEmail, Region, AddressLine1, AddressLine2, AddressLine3, Country, PostCode, PhoneNumber, TechnicalContact, PostAPIUrl, APIKeyName, APIKeyValue, DateAdded)
        VALUES
            ('E37000025', 'Example LEP', 'example@example.com', 'North East', '123 Main St', 'Suite 100', '', 'Country1', '12345', '123-456-7890', 'John Doe', 'http://example.com', 'APIKey1', 'APIValue1', GETDATE()),
            ('E37000019', 'Sample LEP', 'sample@example.com', 'Lancashire', '456 Elm St', '', 'Apt 200', 'Country2', '67890', '987-654-3210', 'Jane Smith', 'http://sample.com', 'APIKey2', 'APIValue2', GETDATE()),
            ('E37000051', 'Test LEP', 'test@example.com', 'London', '789 Oak Ave', '', 'Unit 300', 'Country3', '45678', '555-555-5555', 'Test User', 'http://test.com', 'APIKey3', 'APIValue3', GETDATE());
    END
