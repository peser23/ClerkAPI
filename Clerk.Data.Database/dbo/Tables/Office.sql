CREATE TABLE [dbo].[Office] (
    [OfficeID]  INT           IDENTITY (1, 1) NOT NULL,
    [Building]  NVARCHAR (50) NULL,
    [Room]      NVARCHAR (50) NULL,
    [ZipCode]   NVARCHAR (50) NULL,
    [ZipSuffix] NVARCHAR (50) NULL,
    CONSTRAINT [PK_Office] PRIMARY KEY CLUSTERED ([OfficeID] ASC)
);

