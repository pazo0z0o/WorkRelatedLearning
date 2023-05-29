CREATE PROCEDURE [dbo].[spUser_Delete]
@Id int	
AS	
Begin
delete  
from dbo.[User]
where Id = @Id;
End
