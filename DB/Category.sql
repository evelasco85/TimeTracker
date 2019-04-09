CREATE TABLE [dbo].[Category] (
    [Id]                   INT            IDENTITY (1, 1) NOT NULL,
    [Name]                 NVARCHAR (150) NOT NULL,
    [ShowInSummary]        BIT            DEFAULT ((0)) NOT NULL,
    [System_Created]       DATETIME       NOT NULL,
    [SystemUpdateDateTime] DATETIME       DEFAULT (getdate()) NOT NULL,
    [ShowInTaskEntry]      BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


