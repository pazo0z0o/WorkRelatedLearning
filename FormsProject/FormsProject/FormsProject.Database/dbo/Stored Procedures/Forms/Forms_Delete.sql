CREATE PROCEDURE [dbo].[Forms_Delete]
	@id INT
AS
	DELETE FROM dbo.TBL_FORMS
	WHERE
		[Id] = @id
