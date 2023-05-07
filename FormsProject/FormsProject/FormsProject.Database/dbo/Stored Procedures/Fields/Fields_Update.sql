CREATE PROCEDURE [dbo].[Fields_Update]
	@id INT,
	@name NVARCHAR(64),
	@value NUMERIC(15, 3)
AS
	UPDATE dbo.TBL_FORM_FIELDS
	SET
		[Name] = @name,
		[Value] = @value
	WHERE
		[Id] = @id
