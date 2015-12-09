CREATE TABLE [dbo].[Objective]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Date] DATETIME NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [SystemCreated] DATETIME NOT NULL, 
    [SystemUpdated] DATETIME NOT NULL    
)