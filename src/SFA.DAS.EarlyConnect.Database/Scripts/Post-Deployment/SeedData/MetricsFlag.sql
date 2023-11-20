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


IF NOT EXISTS (SELECT Id FROM [dbo].[MetricsFlag])
    BEGIN
        INSERT [dbo].[MetricsFlag] ([Id], [FlagName], [FlagCode], [IsActive], [DateAdded]) VALUES (1, N'GCSE Grade 4 English', N'gcse_grade4_english_flag', 1, CAST(N'2023-11-09T09:49:39.977' AS DateTime))
        INSERT [dbo].[MetricsFlag] ([Id], [FlagName], [FlagCode], [IsActive], [DateAdded]) VALUES (2, N'NGCSE Grade 4 MathsULL', N'gcse_grade4_maths_flag', 1, CAST(N'2023-11-09T09:50:10.183' AS DateTime))
        INSERT [dbo].[MetricsFlag] ([Id], [FlagName], [FlagCode], [IsActive], [DateAdded]) VALUES (3, N'Interested in Agriculture', N'interested_in_agricultute_flag', 1, CAST(N'2023-11-09T09:50:25.837' AS DateTime))
        INSERT [dbo].[MetricsFlag] ([Id], [FlagName], [FlagCode], [IsActive], [DateAdded]) VALUES (4, N'Interested in Arts', N'interested_in_arts_flag', 1, CAST(N'2023-11-09T09:50:40.833' AS DateTime))
        INSERT [dbo].[MetricsFlag] ([Id], [FlagName], [FlagCode], [IsActive], [DateAdded]) VALUES (5, N'Interested in Business', N'interested_in_business_flag', 1, CAST(N'2023-11-09T09:50:55.713' AS DateTime))
        INSERT [dbo].[MetricsFlag] ([Id], [FlagName], [FlagCode], [IsActive], [DateAdded]) VALUES (6, N'Interested in Construction', N'interested_in_construction_flag', 1, CAST(N'2023-11-09T09:51:08.943' AS DateTime))
        INSERT [dbo].[MetricsFlag] ([Id], [FlagName], [FlagCode], [IsActive], [DateAdded]) VALUES (7, N'Interested in Education', N'interested_in_education_flag', 1, CAST(N'2023-11-09T09:51:22.740' AS DateTime))
        INSERT [dbo].[MetricsFlag] ([Id], [FlagName], [FlagCode], [IsActive], [DateAdded]) VALUES (8, N'Interested in Engineering', N'interested_in_engineering_flag', 1, CAST(N'2023-11-09T09:51:35.790' AS DateTime))
        INSERT [dbo].[MetricsFlag] ([Id], [FlagName], [FlagCode], [IsActive], [DateAdded]) VALUES (9, N'Interested in Health', N'interested_in_health_flag', 1, CAST(N'2023-11-09T09:51:49.550' AS DateTime))
        INSERT [dbo].[MetricsFlag] ([Id], [FlagName], [FlagCode], [IsActive], [DateAdded]) VALUES (10, N'Interested in History', N'interested_in_history_flag', 1, CAST(N'2023-11-09T09:52:01.383' AS DateTime))
        INSERT [dbo].[MetricsFlag] ([Id], [FlagName], [FlagCode], [IsActive], [DateAdded]) VALUES (11, N'Interested in HR', N'interested_in_hr_flag', 1, CAST(N'2023-11-09T09:52:16.560' AS DateTime))
        INSERT [dbo].[MetricsFlag] ([Id], [FlagName], [FlagCode], [IsActive], [DateAdded]) VALUES (12, N'Interested in Information Technology', N'interested_in_information_flag', 1, CAST(N'2023-11-09T09:52:33.317' AS DateTime))
        INSERT [dbo].[MetricsFlag] ([Id], [FlagName], [FlagCode], [IsActive], [DateAdded]) VALUES (13, N'Interested in Languages', N'interested_in_languages_flag', 1, CAST(N'2023-11-09T09:52:48.160' AS DateTime))
        INSERT [dbo].[MetricsFlag] ([Id], [FlagName], [FlagCode], [IsActive], [DateAdded]) VALUES (14, N'Interested in Leisure', N'interested_in_leisure_flag', 1, CAST(N'2023-11-09T09:53:05.687' AS DateTime))
        INSERT [dbo].[MetricsFlag] ([Id], [FlagName], [FlagCode], [IsActive], [DateAdded]) VALUES (15, N'Interested in Retail', N'interested_in_retail_flag', 1, CAST(N'2023-11-09T09:53:18.787' AS DateTime))
        INSERT [dbo].[MetricsFlag] ([Id], [FlagName], [FlagCode], [IsActive], [DateAdded]) VALUES (16, N'Interested in Science Maths', N'interested_in_scimaths_flag', 1, CAST(N'2023-11-09T09:53:31.040' AS DateTime))
        INSERT [dbo].[MetricsFlag] ([Id], [FlagName], [FlagCode], [IsActive], [DateAdded]) VALUES (17, N'Interested in Services', N'interested_in_services_flag', 1, CAST(N'2023-11-09T09:53:45.850' AS DateTime))
        INSERT [dbo].[MetricsFlag] ([Id], [FlagName], [FlagCode], [IsActive], [DateAdded]) VALUES (18, N'Interested in Social Science', N'interested_in_socsci_flag', 1, CAST(N'2023-11-09T09:53:59.710' AS DateTime))
        INSERT [dbo].[MetricsFlag] ([Id], [FlagName], [FlagCode], [IsActive], [DateAdded]) VALUES (19, N'Interested in Transport', N'interested_in_transport_flag', 1, CAST(N'2023-11-09T09:54:10.927' AS DateTime))
        INSERT [dbo].[MetricsFlag] ([Id], [FlagName], [FlagCode], [IsActive], [DateAdded]) VALUES (20, N'Desired in Level 2', N'desired_level2_flag', 1, CAST(N'2023-11-09T09:54:26.573' AS DateTime))
        INSERT [dbo].[MetricsFlag] ([Id], [FlagName], [FlagCode], [IsActive], [DateAdded]) VALUES (21, N'Desired in Level 3', N'desired_level3_flag', 1, CAST(N'2023-11-09T09:54:39.930' AS DateTime))
        INSERT [dbo].[MetricsFlag] ([Id], [FlagName], [FlagCode], [IsActive], [DateAdded]) VALUES (22, N'Desired in Level 4 to 7', N'desired_level4_to_7_flag', 1, CAST(N'2023-11-09T09:54:51.757' AS DateTime))
        INSERT [dbo].[MetricsFlag] ([Id], [FlagName], [FlagCode], [IsActive], [DateAdded]) VALUES (23, N'Desired in Level 6 to 7', N'desired_level6_to_7_flag', 1, CAST(N'2023-11-09T09:55:03.723' AS DateTime))
    END