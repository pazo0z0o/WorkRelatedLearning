CREATE PROCEDURE [dbo].[spUser_Update]
	@Id int ,
	@FirstName nvarchar(50),
	@LastName nvarchar(50)
AS
Begin
Update dbo.[User] 
set FirstName = @FirstName , LastName = @LastName
where Id = @Id;
End