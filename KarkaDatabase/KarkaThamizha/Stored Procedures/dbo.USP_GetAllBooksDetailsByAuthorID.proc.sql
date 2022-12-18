
IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'USP_GetAllBooksDetailsByAuthorID')
                    AND type IN ( N'P', N'PC' ) ) 
BEGIN
 DROP PROCEDURE [dbo].[USP_GetAllBooksDetailsByAuthorID]
 END
GO

/****** Object:  StoredProcedure [dbo].[USP_GetAllBooksDetailsByAuthorID] 
exec [dbo].[#USP_GetAllBooksDetailsByAuthorID] 63, 178
******/  
CREATE PROCEDURE [dbo].[USP_GetAllBooksDetailsByAuthorID] --62  
(
 @AuthorID INT,
 @UserID INT = NULL
)                
AS                            
BEGIN      
SET NOCOUNT ON   
	 SELECT B.BookID,                
	   BD.BookDetailsID,
	   B.Book AS BookName,
	   U.Userid, 
	   U.UserName, 
	   Price,  
	   Pages,BooksReviewID,                
	   BookCode,  
	   BookDescription,
	   BD.PublisherID,  
	   NoofCopy,  
	   BF.BookFormat,            
	   BC.CategoryID as SubCategoryID,
	   C.Category as SubCategoryName,
	   C.ParentID AS CategoryID,          
	   M.Category as CategoryName, 
	   BD.ImgBookSmallFS,
	   BD.ImgBookSmallBS,
	   BD.ImgBookLarge,
	   ISBN13, 
	   FirstEdition,            
	   CurrentEdition,
	   IsKarkaBook,  
	   Dimensions, 
	   B.RecStatus,
	   BA.UserTypeID,   
	   NULL AS 'UsrRating',
	   NULL AS 'GlobalRating' 
	   INTO #tempBookDetails
	 FROM Books B 
	 LEFT OUTER JOIN dbo.BooksDetails  BD WITH(NOLOCK)  ON BD.BookID = B.BookID       
	 LEFT OUTER JOIN BookFormat BF WITH(NOLOCK)  ON BF.BookFormatID = BD.BookFormat                
	 LEFT OUTER JOIN BookAuthor BA WITH(NOLOCK)  ON  B.BookID=BA.BookID                
	 LEFT OUTER JOIN Users U WITH(NOLOCK)  ON BA.AuthorID=U.UserID                
	 LEFT OUTER JOIN BookCategory BC WITH(NOLOCK)  ON BC.BookID=B.BookID                
	 LEFT OUTER JOIN Category C WITH(NOLOCK)  ON C.CategoryID=BC.CategoryID                
	 LEFT OUTER JOIN Category M WITH(NOLOCK)  ON M.CategoryID=C.Parentid                
	 LEFT OUTER JOIN BooksReview BR WITH(NOLOCK)  ON BD.BookDetailsID=BR.BookDetailsID  
	 WHERE U.UserID=@AuthorID and  B.RecStatus <>'D'   
 
		UPDATE  #tempBookDetails
		SET     UsrRating = ur.Rating
		FROM    #tempBookDetails tbd
			INNER JOIN  UserRating UR on UR.BookID = tbd.BookID 
			WHERE ur.UserID = @UserID
  
		UPDATE  #tempBookDetails
		SET     GlobalRating = AvgRating 
		FROM    #tempBookDetails tbd
			INNER JOIN (SELECT BookID,  (SUM(Rating)/COUNT(UserID)) AS AvgRating FROM UserRating WITH(NOLOCK) GROUP BY BookID) AS S
				ON tbd.BookID = S.BookID  

  SELECT * FROM #tempBookDetails
END    