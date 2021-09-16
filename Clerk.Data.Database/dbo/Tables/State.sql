CREATE TABLE [dbo].[State] (
    [StateID] INT           IDENTITY (1, 1) NOT NULL,
    [Code]    NVARCHAR (10) NULL,
    [Name]    NVARCHAR (50) NULL,
    CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED ([StateID] ASC)
);

