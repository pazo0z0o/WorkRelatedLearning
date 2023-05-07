CREATE PROCEDURE [dbo].[Fields_Get_ByFormId]
	@formId INT
AS
	SELECT 
		[Id],
		[FormId],
		[Name],
		[Value]
	FROM dbo.TBL_FORM_FIELDS
	WHERE
		[FormId] = @formId
