SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE AddOrUpdateUserCommentsByBookID 
@ID						INT = NULL, 
@BookID					INT,
@AuthorID				INT,
@ParentID				VARCHAR(10), 
@CreatorID				INT,
@Content				VARCHAR(5000), 
@Status 				BIT

AS
BEGIN
    DECLARE @curDate DATETIME = GETDATE();
	DECLARE @fullName VARCHAR(250);

	SELECT @fullName = UserName 
	FROM Users WITH(NOLOCK) 
	WHERE UserID = @CreatorID

	SET NOCOUNT ON;

	IF EXISTS(SELECT 1 FROM [dbo].[UserComments] WITH(NOLOCK) WHERE ID = @ID)
	BEGIN
		UPDATE [dbo].[UserComments] 
			SET		[Content]	= @Content, 
					[ModifiedOn]	= @curDate 
		WHERE ID = @ID 
	END
	ELSE
	BEGIN
		INSERT INTO [dbo].[UserComments]
			   ([BookID]
			   ,[AuthorID]
			   ,[ParentID]
			   ,[CreatedOn]
			   ,[ModifiedOn]
			   ,[CreatorID]
			   ,[Content]
			   ,[FullName]  
			   ,[Status])
		 VALUES
			   (@BookID 
			   ,@AuthorID
			   ,@ParentID 
			   ,@curDate 
			   ,@curDate 
			   ,@CreatorID 
			   ,@Content 
			   ,@FullName 
			   ,@Status)
	END
END
GO
