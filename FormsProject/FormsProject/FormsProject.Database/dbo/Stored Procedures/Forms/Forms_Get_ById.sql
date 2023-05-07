CREATE PROCEDURE [dbo].[Forms_Get_ById]
	@id INT
AS
	SELECT 
		[Id],
		[Title],
		[CreatedAt],
		[UpdatedAt]
	FROM dbo.TBL_FORMS
	WHERE
		[Id] = @id
