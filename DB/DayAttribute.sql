CREATE TABLE [dbo].[DayAttribute]
(
	[Attribute_Id] INT NOT NULL,
    [Date] DATETIME NOT NULL, 
    CONSTRAINT [PK_DayAttribute] PRIMARY KEY ([Attribute_Id], [Date]) 
)
