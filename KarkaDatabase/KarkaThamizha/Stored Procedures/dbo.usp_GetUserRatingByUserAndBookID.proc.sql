
IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'usp_GetUserRatingByUserAndBookID')
                    AND type IN ( N'P', N'PC' ) ) 
BEGIN
 DROP PROCEDURE [dbo].[usp_GetUserRatingByUserAndBookID]
 END
GO
/*
exec [dbo].[usp_GetUserRatingByUserAndBookID]  @UserID = 178, @bookid =333
select * from UserRating
*/
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetUserRatingByUserAndBookID] 
	 @UserID		INT = NULL,
	 @BookID		INT
AS
BEGIN
	DECLARE @AvgRate DECIMAL = 0
	SELECT @AvgRate = SUM(Rating)/COUNT(UserID) FROM UserRating WITH(NOLOCK) 
			WHERE BookID  = @BookID AND Rating <> 0
			GROUP BY BookID  

		 
	SET NOCOUNT ON;
	IF EXISTS(SELECT 1 FROM UserRating  UR WITH(NOLOCK) WHERE UR.BookID = @BookID   AND UR.UserID = @UserID )
	BEGIN
		SELECT 
		 B.Book
		,B.[Name]
		,UR.UserRatingID
		,UR.BookID		
		,UR.UserID		
		,UR.Rating		
		,@AvgRate AS 'AvgRating'
		,UR.CreatedOn	
		,UR.ModifiedOn	 FROM UserRating  UR WITH(NOLOCK)
		INNER JOIN Books B WITH(NOLOCK) ON B.BookID = UR.BookID
		WHERE	UR.BookID = @BookID 
			AND UR.UserID = @UserID
	END
	ELSE
	BEGIN
		SELECT  B.Book
		,B.[Name]
		,0 AS 'UserRatingID'
		,B.BookID		
		,@UserID AS 'UserID'		
		,0 AS 'Rating'		
		,@AvgRate AS 'AvgRating'
		,NULL AS 'CreatedOn'	
		,NULL AS 'ModifiedOn'	FROM Books B WITH(NOLOCK) WHERE B.BookID = @BookID
	END
    
END
 

