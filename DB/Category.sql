CREATE TABLE [dbo].[Category]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(150) NOT NULL, 
    [ShowInSummary] BIT NOT NULL DEFAULT 0,
	[System_Created] DATETIME NOT NULL,
	[SystemUpdateDateTime] DATETIME NOT NULL DEFAULT GetDate()
)
