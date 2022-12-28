using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarkaThamizha.Repository.DAL
{
    public class SQLObjects
    {
        #region Books
        public const string GetAllBooksWithAuthor = "USP_GetAllBooksWithAuthor";
        public const string GetAllBooksWithCategory = "dbo.USP_GetAllBooksWithCategory";

        public const string GetAllBooksDetails = "dbo.USP_GetAllBooksDetails";
        public const string GetAllBooksDetailsByID = "dbo.USP_GetAllBooksDetailsByID";
        public const string AddBooksDetails = "dbo.USP_AddBooksDetails";
        public const string AddBooks = "dbo.USP_AddBooks";
        public const string GetAllBooks = "dbo.USP_GetAllBooks";
        public const string UpdateBook = "dbo.USP_UpdateBook";
        public const string DeleteBook = "dbo.USP_DeleteBook";
        public const string GetSelectedAuthor = "USP_GetSelectedAuthor";
        public const string GetAllBestBooks = "USP_GetAllBestBooks";
        public const string UpdateBookStatus = "USP_UpdateBookStatus";
        public const string GetRelatedBooksByCategoryID = "USP_GetRelatedBooksByCategoryID";
        public const string GetBooksDetailsByBookID = "USP_GetBooksDetailsByBookID";
        public const string GetAllBooksByAuthorID = "USP_GetAllBooksByAuthorID";


        #endregion

        #region Book Format
        public const string GetAllBookFormat = "dbo.USP_GetAllBookFormat";
        #endregion

        #region Category
        public const string GetAllCategoryList = "dbo.USP_GetCategoryList"; //Need ot delete
        public const string GetAllCategory = "dbo.USP_GetAllCategory";
        public const string UpdateCategory = "dbo.USP_UpdateCategory";
        public const string AddCategory = "dbo.USP_AddBookCategory";
        public const string AddCategories = "dbo.USP_AddBooksCategory";   //Add multiple books category at one time. Now not using
        public const string GetBookswithCategory = "dbo.USP_GetBookswithCategory";
        public const string GetBookCategoryByBookID = "dbo.USP_GetBookCategoryByBookID";
        public const string GetSelectedCategories = "dbo.USP_GetSelectedCategories";
        public const string GetCategoryByBookID = "dbo.USP_GetCategoryByBookID";
        #endregion

        #region BooksDetails

        public const string GetAllBookDetails = "dbo.USP_GetAllBookDetails";
        public const string GetAllBooksDetailsByAuthorID = "USP_GetAllBooksDetailsByAuthorID";

        public const string GetAllBooksDetailsByBookID = "dbo.USP_GetAllBooksDetailsByBookID";
        #endregion

        #region BooksReview
        public const string GetAllBooksReviewDetails = "dbo.USP_GetAllBooksReviewDetails";
        public const string GetBooksReview = "dbo.USP_GetBooksReview";

        public const string AddBooksReview = "dbo.USP_AddBooksReview";
        public const string UpdateBooksReview = "dbo.USP_UpdateBooksReview";
        public const string DeleteBookReview = "dbo.USP_DeleteBookReview";
        public const string GetAllBookReviewsByBookID = "dbo.USP_GetAllBookReviewsByBookID";
        public const string GetAllBooksReviewByBookDetailsID = "dbo.USP_GetAllBooksReviewByBookDetailsID";
        public const string GetBooksReviewByReviewID = "dbo.USP_GetBooksReviewByReviewID";
        public const string GetAllBookReviewsByUserID = "dbo.USP_GetAllBookReviewsByUserID";

        #endregion

        #region Country
        public const string GetCityByStateID = "dbo.USP_GetCityByStateID";
        public const string GetAllCountries = "dbo.USP_GetAllCountries";
        public const string GetStatesByCountryID = "dbo.USP_GetStatesByCountryID";
        #endregion

        #region Event
        public const string GetAllEvents = "dbo.USP_GetAllEvents";
        public const string AddEvents = "dbo.USP_AddEvents";

        public const string UpdateEvent = "dbo.USP_UpdateEvent";
        public const string DeleteEvent = "dbo.USP_DeleteEvent";
        public const string GetAllBookFair = "dbo.USP_GetAllBookFair";

        #endregion

        #region Magazine & Review
        public const string GetAllMagazines = "dbo.USP_GetAllMagazines";
        public const string GetAllMagazineType = "dbo.USP_GetAllMagazineType";
        public const string GetAllMagazineIssue = "dbo.USP_GetAllMagazineIssue";
        public const string GetAllPeriodical = "dbo.USP_GetAllPeriodical";
        public const string GetMagazineReviewByMonthAndYear = "dbo.USP_GetMagazineReviewByMonthAndYear";
        public const string GetAllMagazineReviewPending = "dbo.USP_GetAllMagazineReviewPending";

        public const string AddMagazine = "dbo.USP_AddMagazine";
        public const string AddMagazineReview = "dbo.USP_AddMagazineReview";
        public const string GetAllMagazineReview = "dbo.USP_GetAllMagazineReview";
        public const string GetBooksReviewByMag = "dbo.USP_GetBooksReviewByMag";
        public const string AddMagazineIssue = "dbo.USP_AddMagazineIssue";


        #region Magazine Price
        public const string GetAllMagSubscription = "dbo.USP_GetAllMagSubscription";

        public const string AddMagSubscription = "dbo.USP_AddMagSubscription";
        #endregion

        #region Magazine Details
        public const string AddMagazineDetails = "dbo.USP_AddMagazineDetails";
        #endregion
        #endregion

        #region Publisher
        public const string GetAllPublishers = "dbo.USP_GetAllPublishers";

        public const string GetPublishers = "dbo.USP_GetPublishers";
        public const string AddPublisher = "dbo.USP_AddPublisher";
        public const string UpdatePublisher = "dbo.USP_UpdatePublisher";
        public const string AddPublisherDetails = "dbo.USP_AddPublisherDetails";
        public const string GetBooksByPublisherID = "dbo.USP_GetBooksByPublisherID";


        #endregion

        #region User
        public const string GetAllUsers = "dbo.USP_GetAllUsers";
        public const string GetAllAuthorsProfile = "dbo.USP_GetAllAuthorsProfile";
        public const string GetAuthorsProfileByID = "USP_GetAuthorsProfileByID";

        public const string AddUser = "dbo.USP_AddUser";
        public const string GetUserByUserID = "dbo.USP_GetUserByUserID";
        public const string AddUserDetails = "dbo.USP_AddUserDetails";
        public const string GetUsersDetailsByUserId = "dbo.USP_GetUsersDetailsByUserId";
        public const string CheckMailExists = "USP_CheckMailExists";
        public const string GetLogInUser = "USP_GetLogInUser";
        public const string GetUserProfileByUserID = "USP_GetUserProfileByUserID";
        public const string UpdateUserProfile = "USP_UpdateUserProfile";
        public const string ChangePassword = "USP_ChangePassword";

        #region User
        public const string GetAllUserType = "dbo.USP_GetAllUserType";
        public const string GetUserDetails = "dbo.USP_GetUserDetails";
        #endregion

        #region Author
        public const string GetUserByAuthorType = "dbo.USP_GetUserByAuthorType";
        public const string GetAllAuthorSeries = "dbo.USP_GetAllAuthorSeries";
        public const string GetAllWeekDays = "dbo.USP_GetAllWeekDays";
        public const string GetAllSpecialName = "dbo.USP_GetAllSpecialName";

        public const string AddBooksAuthor = "dbo.USP_AddBooksAuthor";
        public const string GetAuthorByBookID = "dbo.USP_GetAuthorByBookID";

        #endregion
        #endregion

        #region Article
        public const string GetAllInterviews = "dbo.USP_GetAllInterviews";
        public const string GetAllArticle = "dbo.USP_GetAllArticle";
        public const string GetAllArticleType = "dbo.USP_GetAllArticleType";

        public const string AddAuthorSeries = "dbo.USP_AddAuthorSeries";
        public const string AddInterview = "dbo.USP_AddInterview";
        public const string AddAuthorProfile = "dbo.USP_AddAuthorProfile";
        public const string DeleteArtcle = "dbo.USP_DeleteArtcle";
        public const string AddArticle = "dbo.USP_AddArticle";

        #endregion

        #region Karka Evnets
        public const string AddKarkaEventsType = "dbo.USP_AddKarkaEventsType";
        public const string GetAllKarkaEventsType = "dbo.USP_GetAllKarkaEventsType";

        public const string AddKarkaEvents = "dbo.USP_AddKarkaEvents";
        public const string GetAllKarkaEvents = "dbo.USP_GetAllKarkaEvents";

        #endregion

        #region Feedback
        public const string AddFeedback = "dbo.USP_AddFeedback";
        public const string GetAllFeedback = "dbo.USP_GetAllFeedback";
        public const string AddComments = "dbo.USP_AddComments";
        public const string GetAllComments = "dbo.USP_GetAllComments";

        public const string GetAuthorBookDetails = "dbo.USP_GetAuthorBookDetails";
        public const string GetAllBooksWithAuthorID = "dbo.USP_GetAllBooksWithAuthorID";
        #endregion

        #region Login Status
        public const string CheckUserExists = "dbo.USP_CheckUserExists";

        public const string AddBooksCategory = "dbo.USP_AddBooksCategory";
        #endregion

        #region Category
        public const string GetSelectedCategory = "USP_GetSelectedCategory";

        #endregion

        #region UserTrack
        public const string AddUserTrack = "USP_AddUserTrack";

        #endregion

        public const string GetAllToDo = "dbo.USP_GetAllToDo";
        public const string AddToDoList = "dbo.USP_AddToDoList";

        #region Master
        public const string GetAllProfession = "dbo.USP_GetAllProfession";
        public const string GetAllCacheName = "dbo.USP_GetAllCacheName";
        public const string GetAllSeriesType = "dbo.USP_GetAllSeriesType";
        #endregion

        #region Rating
        public const string AddOrUpdateUserRating = "dbo.USP_AddOrUpdateUserRating";
        public const string GetUserRatingByUserAndBookID = "dbo.USP_GetUserRatingByUserAndBookID";
        #endregion

        #region userComments
        public const string GetUserCommentsByBookID = "dbo.GetUserCommentsByBookID";
        public const string AddOrUpdateUserCommentsByBookID = "dbo.AddOrUpdateUserCommentsByBookID";
        public const string UpVoteUserCommentsByID = "dbo.UpVoteUserCommentsByID";
        public const string DeleteUserCommentsByID = "dbo.DeleteUserCommentsByID";
        #endregion

        public const string GetLatestNews = "dbo.USP_GetLatestNews";
    }
}