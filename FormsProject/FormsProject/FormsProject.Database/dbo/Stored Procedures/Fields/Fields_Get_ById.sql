CREATE PROCEDURE [dbo].[Fields_Get_ById]
	@id INT
AS
	SELECT 
		[Id],
		[FormId],
		[Name],
		[Value]
	FROM dbo.TBL_FORM_FIELDS
	WHERE
		[Id] = @id
