CREATE TABLE [dbo].[DayActivity]
(
	[Activity_Id] INT NOT NULL,
    [Date] DATETIME NOT NULL, 
    [Duration] DATETIME NOT NULL, 
    CONSTRAINT [PK_DayActivity] PRIMARY KEY ([Activity_Id], [Date]) 
)

GO
