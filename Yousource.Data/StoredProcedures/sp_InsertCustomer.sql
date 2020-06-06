CREATE PROCEDURE [dbo].[sp_InsertCustomer]
	@name NVARCHAR(50),
	@email NVARCHAR(50)
AS
	INSERT INTO
		[dbo].[Customers] ([Name], Email)
	VALUES
		(@name, @email)
RETURN 0
