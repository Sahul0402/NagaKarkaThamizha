SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE DeleteUserCommentsByID 
@ID						INT = NULL 
AS
BEGIN
	SET NOCOUNT ON;

    DECLARE @curDate DATETIME = GETDATE(); 

	UPDATE [dbo].[UserComments] 
		SET		[Status]	= 0,  
				[ModifiedOn]	= @curDate 
	WHERE ID = @ID 
END
GO
