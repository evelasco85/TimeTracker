CREATE TABLE [dbo].[PersonalNote]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Subject] NVARCHAR(150), 
    [Description] NVARCHAR(MAX) NOT NULL, 
	[System_Created] DATETIME NOT NULL,
	[SystemUpdateDateTime] DATETIME NOT NULL DEFAULT GetDate()
)

GO
