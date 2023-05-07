CREATE PROCEDURE [dbo].[Forms_Update]
	@id INT,
	@title NVARCHAR(64)
AS
	UPDATE dbo.TBL_FORMS 
	SET
		[Title] = @title,
		[UpdatedAt] = GETUTCDATE()
	WHERE
		[Id] = @id
