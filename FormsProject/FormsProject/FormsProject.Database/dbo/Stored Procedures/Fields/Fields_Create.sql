CREATE PROCEDURE [dbo].[Fields_Create]
	@formId INT,
	@name NVARCHAR(64),
	@value NUMERIC(15, 3)
AS
	DECLARE @ID INT;
	INSERT INTO dbo.TBL_FORM_FIELDS (
		[FormId],
		[Name],
		[Value]
	) VALUES (
		@formId,
		@name,
		@value
	)

	SELECT DISTINCT @ID = SCOPE_IDENTITY() FROM dbo.TBL_FORM_FIELDS

	SELECT @ID AS Id;

	
