CREATE TABLE [dbo].[Activity]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
	[System_Created] DATETIME NOT NULL,
	[SystemUpdateDateTime] DATETIME NOT NULL DEFAULT GetDate()
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'E.g. Class, Review, Text, Programming, Interruption, Meeting',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Activity',
    @level2type = N'COLUMN',
    @level2name = N'Name'