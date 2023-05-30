CREATE PROCEDURE [dbo].[spUser_GetById]
@Id int	
AS	
Begin
Select Id , FirstName, LastName from dbo.[User]
where Id = @Id;
End
