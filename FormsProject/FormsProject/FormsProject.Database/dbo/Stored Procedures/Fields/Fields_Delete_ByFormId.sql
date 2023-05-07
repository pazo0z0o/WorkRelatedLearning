CREATE PROCEDURE [dbo].[Fields_Delete_ByFormId]
	@formId INT
AS
	DELETE FROM dbo.TBL_FORM_FIELDS 
	WHERE
		[Id] = @formId
