CREATE TABLE [dbo].[Holiday]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Date] DATETIME NOT NULL, 
    [Description] NVARCHAR(150) NOT NULL, 
    [SystemCreated] DATETIME NOT NULL, 
    [SystemUpdated] DATETIME NOT NULL
)
