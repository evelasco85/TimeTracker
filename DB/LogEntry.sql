CREATE TABLE [dbo].[LogEntry]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Created] DATETIME NOT NULL, 
    [System_Created] DATETIME NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [Category] NVARCHAR(50) NOT NULL, 
    [SystemUpdateDateTime] DATETIME NOT NULL DEFAULT GetDate() 
)
