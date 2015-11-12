CREATE TABLE [dbo].[TimeLogAttribute]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TimeTrackingLog_Id] INT NOT NULL, 	    
    [Attribute_Name] NVARCHAR(50) NOT NULL,
	[MinuteDuration] INT NOT NULL DEFAULT 0
)
