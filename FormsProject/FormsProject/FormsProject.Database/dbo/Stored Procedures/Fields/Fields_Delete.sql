CREATE PROCEDURE [dbo].[Fields_Delete]
	@id INT
AS
	DELETE FROM dbo.TBL_FORM_FIELDS 
	WHERE
		[Id] = @id
