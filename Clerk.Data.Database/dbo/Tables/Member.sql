CREATE TABLE [dbo].[Member] (
    [MemberID]    INT           IDENTITY (1, 1) NOT NULL,
    [Suffix]      NVARCHAR (50) NULL,
    [FirstName]   NVARCHAR (50) NULL,
    [MiddleName]  NVARCHAR (50) NULL,
    [LastName]    NVARCHAR (50) NULL,
    [BioguideID]  NVARCHAR (50) NULL,
    [Party]       NVARCHAR (10) NULL,
    [Caucus]      NVARCHAR (10) NULL,
    [TownName]    NVARCHAR (50) NULL,
    [District]    NVARCHAR (50) NULL,
    [Phone]       NVARCHAR (50) NULL,
    [ElectedDate] DATETIME      NULL,
    [SwornDate]   DATETIME      NULL,
    [StateID]     INT           NULL,
    [OfficeID]    INT           NULL,
    CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED ([MemberID] ASC),
    CONSTRAINT [FK_Member_Office] FOREIGN KEY ([OfficeID]) REFERENCES [dbo].[Office] ([OfficeID]),
    CONSTRAINT [FK_Member_State] FOREIGN KEY ([StateID]) REFERENCES [dbo].[State] ([StateID])
);

