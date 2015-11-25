CREATE TABLE [dbo].[DayAttribute]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Date] DATETIME NOT NULL ,
	[Description] NVARCHAR(MAX) NOT NULL, 
    [Link] NVARCHAR(150) NULL,
	[System_Created] DATETIME NOT NULL,
	[SystemUpdateDateTime] DATETIME NOT NULL DEFAULT GetDate()
)
