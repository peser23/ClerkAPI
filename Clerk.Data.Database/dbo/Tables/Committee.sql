CREATE TABLE [dbo].[Committee] (
    [CommitteeID]       INT            IDENTITY (1, 1) NOT NULL,
    [Code]              NVARCHAR (50)  NOT NULL,
    [Name]              NVARCHAR (50)  NULL,
    [Type]              NVARCHAR (50)  NULL,
    [Room]              NVARCHAR (50)  NULL,
    [HeaderText]        NVARCHAR (500) NULL,
    [ZipCode]           NVARCHAR (50)  NULL,
    [ZipSuffix]         NVARCHAR (50)  NULL,
    [Phone]             NVARCHAR (50)  NULL,
    [BuildingCode]      NVARCHAR (50)  NULL,
    [ParentCommitteeID] INT            NULL,
    CONSTRAINT [PK_Committee] PRIMARY KEY CLUSTERED ([CommitteeID] ASC),
    CONSTRAINT [FK_Committee_ParentCommittee] FOREIGN KEY ([ParentCommitteeID]) REFERENCES [dbo].[Committee] ([CommitteeID])
);

