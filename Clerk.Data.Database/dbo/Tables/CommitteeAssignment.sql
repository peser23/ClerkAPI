CREATE TABLE [dbo].[CommitteeAssignment] (
    [AssignmentID] INT IDENTITY (1, 1) NOT NULL,
    [MemberID]     INT NOT NULL,
    [CommitteeID]  INT NOT NULL,
    [Rank]         INT NULL,
    CONSTRAINT [PK_CommitteeAssignment] PRIMARY KEY CLUSTERED ([AssignmentID] ASC),
    CONSTRAINT [FK_CommitteeAssignment_Committee] FOREIGN KEY ([CommitteeID]) REFERENCES [dbo].[Committee] ([CommitteeID]),
    CONSTRAINT [FK_CommitteeAssignment_Member] FOREIGN KEY ([MemberID]) REFERENCES [dbo].[Member] ([MemberID])
);

