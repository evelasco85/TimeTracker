CREATE TABLE [dbo].[SummarizedLogCategory]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [ShowInSummary] BIT NOT NULL DEFAULT 0
)
