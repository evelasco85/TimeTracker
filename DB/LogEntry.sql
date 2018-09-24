CREATE TABLE [dbo].[LogEntry]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Created] [datetime] NOT NULL,
	[System_Created] [datetime] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Category] [nvarchar](150) NOT NULL,
	[SystemUpdateDateTime] [datetime] NOT NULL DEFAULT (getdate()),
	[HoursRendered] [float] NOT NULL CONSTRAINT [DF_LogEntry_HoursRendered]  DEFAULT ((0)), 
    CONSTRAINT [PK_LogEntry] PRIMARY KEY ([Id])
)
