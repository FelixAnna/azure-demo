CREATE TABLE [dbo].[Articles]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Title] NVARCHAR(128) NOT NULL, 
    [AuthorId] INT NULL, 
    [Link] VARCHAR(1024) NOT NULL, 
    [CreatedDate] DATETIME NOT NULL DEFAULT getutcdate()
)
GO

ALTER TABLE [dbo].[Articles]
	ADD CONSTRAINT [Article_Auther]
	FOREIGN KEY ([AuthorId])
	REFERENCES [dbo].[Authors](Id)