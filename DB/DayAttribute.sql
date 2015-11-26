CREATE TABLE [dbo].[DayAttribute]
(
	[Id] INT NOT NULL  IDENTITY,
	[Date] DATETIME NOT NULL ,
	[Name] NVARCHAR(50) NOT NULL, 
	[Description] NVARCHAR(MAX) NOT NULL, 
    [Link] NVARCHAR(150) NULL,
	[System_Created] DATETIME NOT NULL,
	[SystemUpdateDateTime] DATETIME NOT NULL DEFAULT GetDate(), 
    CONSTRAINT [PK_DayAttribute] PRIMARY KEY ([Id]) 
)
