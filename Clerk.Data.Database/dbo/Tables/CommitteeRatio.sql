CREATE TABLE [dbo].[CommitteeRatio] (
    [CommitteeRatioID] INT           IDENTITY (1, 1) NOT NULL,
    [CommitteeID]      INT           NOT NULL,
    [Majority]         NVARCHAR (50) NULL,
    CONSTRAINT [PK_CommitteeRatio] PRIMARY KEY CLUSTERED ([CommitteeRatioID] ASC),
    CONSTRAINT [FK_CommitteeRatio_Committee] FOREIGN KEY ([CommitteeID]) REFERENCES [dbo].[Committee] ([CommitteeID])
);

