CREATE Procedure [dbo].[USP_ChangePassword]  
(              
 @UserID int,        
 @Password nvarchar(60),         
 @Result int Output              
)              
as              
Begin                 
      
update Users set Password = @Password  
where UserId=@UserID              
set @Result=@UserID              
return @Result     
  
End 