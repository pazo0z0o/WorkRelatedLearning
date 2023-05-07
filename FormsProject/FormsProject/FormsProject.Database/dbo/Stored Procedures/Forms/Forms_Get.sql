CREATE PROCEDURE [dbo].[Forms_Get]
AS
	SELECT 
		[Id],
		[Title],
		[CreatedAt],
		[UpdatedAt]
	FROM dbo.TBL_FORMS

