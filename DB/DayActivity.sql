CREATE TABLE [dbo].[DayActivity]
(
	[Id] INT NOT NULL IDENTITY,
    [Date] DATETIME NOT NULL,
	[Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL,  
    [Duration_Hours] DECIMAL(18, 2) NOT NULL, 
	[System_Created] DATETIME NOT NULL,
	[SystemUpdateDateTime] DATETIME NOT NULL DEFAULT GetDate(), 
    CONSTRAINT [PK_DayActivity] PRIMARY KEY ([Id]) 
)

GO
