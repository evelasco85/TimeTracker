CREATE TABLE [dbo].[TimeTrackingLog]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [StartedDate] DATETIME NOT NULL,
	[StoppedDate] DATETIME NOT NULL,
	[InterruptionMinuteDuration] INT NOT NULL DEFAULT 0,
	[Activity_Name] NVARCHAR(50) NOT NULL,
	[Comment] NVARCHAR(MAX) NOT NULL, 
    [System_Created] DATETIME NOT NULL, 
    [SystemUpdateDateTime] DATETIME NOT NULL DEFAULT GetDate()    
)
