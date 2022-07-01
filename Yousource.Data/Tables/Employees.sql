CREATE TABLE [dbo].[Employees]
(
	[id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
    [first_name] VARCHAR(190) NOT NULL, 
    [last_name] VARCHAR(100) NOT NULL, 
    [age] int NOT NULL, 
    [address] VARCHAR(256) NOT NULL
)
