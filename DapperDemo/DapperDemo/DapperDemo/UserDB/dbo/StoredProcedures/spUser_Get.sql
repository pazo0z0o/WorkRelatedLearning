CREATE PROCEDURE [dbo].[spUser_Get]
@Id int	
AS	
Begin
Select Id , FirstName, LastName from dbo.[User]
where Id = @Id;
End
