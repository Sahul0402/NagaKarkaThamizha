SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE UpVoteUserCommentsByID 
@ID						INT = NULL,  
@UpVoteCount			INT, 
@UserHasUpVoted			BIT
AS
BEGIN
	SET NOCOUNT ON;

    DECLARE @curDate DATETIME = GETDATE(); 

	UPDATE [dbo].[UserComments] 
		SET		 UpVoteCount	= @UpVoteCount, 
			  UserHasUpVoted	= @UserHasUpVoted, 
				[ModifiedOn]	= @curDate 
	WHERE ID = @ID 
END
GO
