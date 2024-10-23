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
:r .\SeedData\LEPSData.sql
:r .\SeedData\LEPSUser.sql
:r .\SeedData\MetricsFlag.sql
:r .\SeedData\Survey.sql
:r .\SeedData\QuestionType.sql
:r .\SeedData\Question.sql
:r .\SeedData\Answer.sql
:r .\Schema\LEPSCoverageIndexes.sql
:r .\SeedData\Postcode.sql
:r .\SeedData\LanSchools.sql