CREATE TABLE [dbo].[Authors]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Phone] NVARCHAR(50) NOT NULL, 
    [Email] NVARCHAR(128) NOT NULL, 
    [CreatedDate] NCHAR(10) NOT NULL DEFAULT getutcdate()
)
