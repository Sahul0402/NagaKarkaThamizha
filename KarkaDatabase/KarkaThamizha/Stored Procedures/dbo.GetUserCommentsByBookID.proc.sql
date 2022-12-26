/*
 select * from [dbo].[UserComments]
 select * from users
 select * from books
  --delete from  [dbo].[UserComments]
select * from BookAuthor
exec GetUserCommentsByBookID 330,63,178
*/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE GetUserCommentsByBookID
@BookID					INT,
@AuthorID				INT,
@CurrentUserID				INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT [ID]
		  ,[BookID]
		  ,[AuthorID]
		  ,[ParentID]
		  ,[CreatedOn]
		  ,[ModifiedOn]
		  ,[CreatorID]
		  ,[Content]
		  ,[FullName]
		  ,[ProfilePicUrl]
		  ,[CreatedByAdmin]
		  ,CASE WHEN [CreatorID] = @CurrentUserID THEN ConvERT(bit, 1) ELSE  ConvERT(bit,0) END AS [CreatedByCurrentUser]
		  ,[UpVoteCount]
		  ,[UserHasUpVoted]
		  ,[IsNew]
		  ,[Status]
	  FROM [dbo].[UserComments] WITH(NOLOCK)
	  WHERE BookID = @BookID
		AND AuthorID = @AuthorID
		AND [Status] = 1
END
GO
