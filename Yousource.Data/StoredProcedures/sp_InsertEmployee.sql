CREATE PROCEDURE [dbo].[sp_InsertEmployee]
	@firstName VARCHAR(100),
	@lastName VARCHAR(100),
    @address VARCHAR(256),
	@age int
AS
	INSERT INTO
		[dbo].Employees([first_name], [last_name], [address], [age])
	VALUES
		(@firstName, @lastName, @address, @age)
RETURN 0
