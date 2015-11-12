CREATE TABLE [dbo].[Activity]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(MAX) NOT NULL, 
    [SummaryInclusion] BIT NOT NULL DEFAULT 0    
)
