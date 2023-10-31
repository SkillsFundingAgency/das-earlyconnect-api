﻿CREATE TABLE [dbo].[StudentData]
(
    Id                    INT NOT NULL PRIMARY KEY IDENTITY,
    FirstName            NVARCHAR(150) NOT NULL,
    LastName            NVARCHAR(150) NOT NULL,
    DateOfBirth            DATE NOT NULL,
    Email                NVARCHAR(320) NOT NULL,
    PostCode            VARCHAR(10) NOT NULL,
    Industry            NVARCHAR(MAX) NOT NULL,
    DateInterestShown    DATETIME NULL,
    LepId                INT NULL DEFAULT(0),
    LepDateSent            DATETIME NULL,
    DateAdded            DATETIME NOT NULL DEFAULT GETDATE()
)
