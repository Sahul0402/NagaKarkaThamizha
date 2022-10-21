using KarkaThamizha.Common.Utility;
using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KarkaThamizha.Repository.CacheData
{
    public class DataCaching
    {
        #region Review - Books & Magazine
        public List<BooksReviewModels> GetBooksReviewByMag()
        {
            List<BooksReviewModels> mdlBooksDetails = (List<BooksReviewModels>)System.Web.HttpContext.Current.Cache[CacheConstants.CACHE_ALL_BOOKSREVIEW];
            if (mdlBooksDetails == null || mdlBooksDetails.Count == 0)
            {
                BooksReviewRepository repoBook = new BooksReviewRepository();
                mdlBooksDetails = repoBook.GetBooksReviewByMag();

                System.Web.HttpContext.Current.Cache.Insert(CacheConstants.CACHE_ALL_BOOKSREVIEW,
                    mdlBooksDetails, null, DateTime.Today.AddDays(1).AddHours(1),
                    System.Web.Caching.Cache.NoSlidingExpiration
                    );
            }
            return mdlBooksDetails;
        }

        public List<BooksReviewModels> GetBooksReview(Int16 userTypeID)
        {
            List<BooksReviewModels> mdlBooksDetails = (List<BooksReviewModels>)System.Web.HttpContext.Current.Cache[CacheConstants.CACHE_ALL_BOOKSREVIEW];
            if (mdlBooksDetails == null || mdlBooksDetails.Count == 0)
            {
                BooksReviewRepository repoBook = new BooksReviewRepository();
                mdlBooksDetails = repoBook.GetBooksReview(userTypeID);

                System.Web.HttpContext.Current.Cache.Insert(CacheConstants.CACHE_ALL_BOOKSREVIEW,
                    mdlBooksDetails, null, DateTime.Today.AddDays(1).AddHours(1),
                    System.Web.Caching.Cache.NoSlidingExpiration
                    );
            }
            return mdlBooksDetails;
        }

        #endregion

        #region Book Details
        public List<BooksDetailsModels> GetAllBooksDetails(string condition)
        {
            List<BooksDetailsModels> mdlBooksDetails = (List<BooksDetailsModels>)System.Web.HttpContext.Current.Cache[CacheConstants.CACHE_ALL_BOOKDETAILS];
            //if (mdlBooksDetails == null || mdlBooksDetails.Count == 0)
            {
                BooksDetailsRepository book = new BooksDetailsRepository();
                mdlBooksDetails = book.GetAllBooksDetails(condition);
                System.Web.HttpContext.Current.Cache.Insert(CacheConstants.CACHE_ALL_BOOKDETAILS,
                    mdlBooksDetails, null, DateTime.Today.AddDays(1).AddHours(1),
                    System.Web.Caching.Cache.NoSlidingExpiration
                    );
            }
            return mdlBooksDetails;
        } 
        #endregion

        #region Articles
        public List<ArticleTypeModels> GetAllArticleType()
        {
            List<ArticleTypeModels> lstEventType = (List<ArticleTypeModels>)System.Web.HttpContext.Current.Cache[CacheConstants.CACHE_ALL_ARTICLETYPE];
            if (lstEventType == null || lstEventType.Count == 0)
            {
                MasterRepository repoEventsType = new MasterRepository();
                lstEventType = repoEventsType.GetAllArticleType();
                System.Web.HttpContext.Current.Cache.Insert(CacheConstants.CACHE_ALL_ARTICLETYPE,
                    lstEventType, null, DateTime.Today.AddDays(1).AddHours(1),
                    System.Web.Caching.Cache.NoSlidingExpiration
                    );
            }
            return lstEventType;
        }
        public List<ArticleModels> GetGeneralArticles()
        {
            List<ArticleModels> mdlGeneralArticles = (List<ArticleModels>)System.Web.HttpContext.Current.Cache[CacheConstants.CACHE_ALL_GENERALARTICLE];
            if (mdlGeneralArticles == null || mdlGeneralArticles.Count() == 0)
            {
                ArticleRepository repoArticle = new ArticleRepository();
                mdlGeneralArticles = repoArticle.GetAllArticle();
                System.Web.HttpContext.Current.Cache.Insert(CacheConstants.CACHE_ALL_GENERALARTICLE,
                    mdlGeneralArticles, null, DateTime.Today.AddDays(1).AddHours(1),
                    System.Web.Caching.Cache.NoSlidingExpiration
                    );
            }
            return mdlGeneralArticles;
        }
        
        public List<ArticleModels> GetAllArticle()
        {
            List<ArticleModels> lstArticle = (List<ArticleModels>)System.Web.HttpContext.Current.Cache[CacheConstants.CACHE_ALL_ARTICLE];
            if (lstArticle == null || lstArticle.Count == 0)
            {
                ArticleRepository mdlArticle = new ArticleRepository();
                lstArticle = mdlArticle.GetAllArticle();
                System.Web.HttpContext.Current.Cache.Insert(CacheConstants.CACHE_ALL_ARTICLE,
                    lstArticle, null, DateTime.Today.AddDays(1).AddHours(1),
                    System.Web.Caching.Cache.NoSlidingExpiration
                    );
            }
            return lstArticle;
        }

        #endregion

        #region Events
        public List<EventsModels> GetAllEvents()
        {
            List<EventsModels> lstEvents = (List<EventsModels>)System.Web.HttpContext.Current.Cache[CacheConstants.CACHE_ALL_EVENTS];
            if (lstEvents == null || lstEvents.Count == 0)
            {
                EventsRepository repoEvents = new EventsRepository();
                lstEvents = repoEvents.GetAllEventsDetails().OrderByDescending(x => x.EventsDate).ToList();
                System.Web.HttpContext.Current.Cache.Insert(CacheConstants.CACHE_ALL_EVENTS,
                    lstEvents, null, DateTime.Today.AddDays(1).AddHours(1),
                    System.Web.Caching.Cache.NoSlidingExpiration
                    );
            }
            return lstEvents;
        }

        public List<EventsTypeModels> GetEventsTypes()
        {
            List<EventsTypeModels> lstEventType = (List<EventsTypeModels>)System.Web.HttpContext.Current.Cache[CacheConstants.CACHE_ALL_EVENTSTYPE];
            if (lstEventType == null || lstEventType.Count == 0)
            {
                EventsTypeRepository blEventsType = new EventsTypeRepository();
                lstEventType = blEventsType.GetAllEventType();
                System.Web.HttpContext.Current.Cache.Insert(CacheConstants.CACHE_ALL_EVENTSTYPE,
                    lstEventType, null, DateTime.Today.AddDays(1).AddHours(1),
                    System.Web.Caching.Cache.NoSlidingExpiration
                    );
            }
            return lstEventType;
        }
        #endregion

        #region Author
        public List<UserModels> GetAllAuthorsProfile(int userTypeID)
        {
            List<UserModels> lstAuthorProfile = (List<UserModels>)System.Web.HttpContext.Current.Cache[CacheConstants.CACHE_ALL_AUTHORPROFILE];
            //if (lstAuthorProfile == null || lstAuthorProfile.Count == 0)
            {
                List<UserModels> mdlUser = new List<UserModels>();
                UserRepository repoUser = new UserRepository();
                lstAuthorProfile = repoUser.GetAllAuthorsProfile(userTypeID);
                System.Web.HttpContext.Current.Cache.Insert(CacheConstants.CACHE_ALL_AUTHORPROFILE,
                    lstAuthorProfile, null, DateTime.Today.AddDays(1).AddHours(1),
                    System.Web.Caching.Cache.NoSlidingExpiration
                    );
            }
            return lstAuthorProfile;
        }
        #endregion

        #region Users
        public List<UserModels> GetAllUsers()
        {
            List<UserModels> users = (List<UserModels>)System.Web.HttpContext.Current.Cache[CacheConstants.CACHE_ALL_USER];
            if (users == null || users.Count == 0)
            {
                UserRepository lstUser = new UserRepository();
                users = lstUser.GetAllUsers();
                System.Web.HttpContext.Current.Cache.Insert(CacheConstants.CACHE_ALL_USER,
                    users, null, DateTime.Today.AddDays(1).AddHours(1),
                    System.Web.Caching.Cache.NoSlidingExpiration
                    );
            }
            return users;
        }
        #endregion

        #region Books
        public List<BooksModels> GetBooksList()
        {
            List<BooksModels> books = (List<BooksModels>)System.Web.HttpContext.Current.Cache[CacheConstants.CACHE_ALL_BOOKS];
            if (books == null || books.Count == 0)
            {
                BooksRepository book = new BooksRepository();
                books = book.GetAllBooksWithAuthor().Where(x => x.Users.UserType.ShortCode != EnumCode.UserType.Translator.ToString()).ToList();

                System.Web.HttpContext.Current.Cache.Insert(CacheConstants.CACHE_ALL_BOOKS,
                    books, null, DateTime.Today.AddDays(1).AddHours(1),
                    System.Web.Caching.Cache.NoSlidingExpiration
                    );
            }
            return books;
        }
        #endregion

        

        #region User Type
        public List<UserTypeModels> GetAllUserType()
        {
            List<UserTypeModels> lstUserType = (List<UserTypeModels>)System.Web.HttpContext.Current.Cache[CacheConstants.CACHE_ALL_USERTYPE];
            if (lstUserType == null || lstUserType.Count == 0)
            {
                UserTypeRepository mdlBookFormat = new UserTypeRepository();
                lstUserType = mdlBookFormat.GetAllUserType();
                System.Web.HttpContext.Current.Cache.Insert(CacheConstants.CACHE_ALL_USERTYPE,
                    lstUserType, null, DateTime.Today.AddDays(1).AddHours(1),
                    System.Web.Caching.Cache.NoSlidingExpiration
                    );
            }
            return lstUserType;
        }
        #endregion

        #region Master - Country, SpecialName, Profession, SeriesType
        public List<MasterModels.CountryModels> GetAllCountries()
        {
            List<MasterModels.CountryModels> countries = (List<MasterModels.CountryModels>)System.Web.HttpContext.Current.Cache[CacheConstants.CACHE_ALL_COUNTRY];
            if (countries == null || countries.Count == 0)
            {
                MasterRepository country = new MasterRepository();
                countries = country.GetAllCountries();
                System.Web.HttpContext.Current.Cache.Insert(CacheConstants.CACHE_ALL_COUNTRY,
                    countries, null, DateTime.Today.AddDays(1).AddHours(1),
                    System.Web.Caching.Cache.NoSlidingExpiration
                    );
            }
            return countries;
        }

        public List<MasterModels.SpecialNameModels> GetAllSpecialName()
        {
            List<MasterModels.SpecialNameModels> lstSpecialName = (List<MasterModels.SpecialNameModels>)System.Web.HttpContext.Current.Cache[CacheConstants.CACHE_ALL_SPECIALNAME];
            if (lstSpecialName == null || lstSpecialName.Count == 0)
            {
                MasterRepository master = new MasterRepository();
                lstSpecialName = master.GetAllSpecialName().OrderBy(x => x.SpecialName).ToList();
                System.Web.HttpContext.Current.Cache.Insert(CacheConstants.CACHE_ALL_SPECIALNAME,
                    lstSpecialName, null, DateTime.Today.AddDays(1).AddHours(1),
                    System.Web.Caching.Cache.NoSlidingExpiration
                    );
            }
            return lstSpecialName;
        }

        public List<MasterModels.ProfessionModels> GetAllProfession()
        {
            List<MasterModels.ProfessionModels> lstProfession = (List<MasterModels.ProfessionModels>)System.Web.HttpContext.Current.Cache[CacheConstants.CACHE_ALL_PROFESSION];
            if (lstProfession == null || lstProfession.Count == 0)
            {
                MasterRepository master = new MasterRepository();
                lstProfession = master.GetAllProfession().OrderBy(x => x.Profession).ToList();
                System.Web.HttpContext.Current.Cache.Insert(CacheConstants.CACHE_ALL_PROFESSION,
                    lstProfession, null, DateTime.Today.AddDays(1).AddHours(1),
                    System.Web.Caching.Cache.NoSlidingExpiration
                    );
            }
            return lstProfession;
        }

        public List<MasterModels.SeriesTypeModels> GetAllSeriesType()
        {
            List<MasterModels.SeriesTypeModels> lstSeriesType = (List<MasterModels.SeriesTypeModels>)System.Web.HttpContext.Current.Cache[CacheConstants.CACHE_ALL_SERIESTYPE];
            if (lstSeriesType == null || lstSeriesType.Count == 0)
            {
                MasterRepository mdlSeriesType = new MasterRepository();
                lstSeriesType = mdlSeriesType.GetAllSeriesType().OrderBy(x => x.SeriesType).ToList();
                System.Web.HttpContext.Current.Cache.Insert(CacheConstants.CACHE_ALL_SERIESTYPE,
                    lstSeriesType, null, DateTime.Today.AddDays(1).AddHours(1),
                    System.Web.Caching.Cache.NoSlidingExpiration
                    );
            }
            return lstSeriesType;
        }
        #endregion

        #region Category
        public List<CategoryModels> GetAllCategories()
        {
            List<CategoryModels> categories = (List<CategoryModels>)System.Web.HttpContext.Current.Cache[CacheConstants.CACHE_ALL_CATEGORY];
            if (categories == null || categories.Count == 0)
            {
                CategoryRepository category = new CategoryRepository();
                categories = category.GetAllCategory().OrderBy(x => x.Category).ToList();
                System.Web.HttpContext.Current.Cache.Insert(CacheConstants.CACHE_ALL_CATEGORY,
                    categories, null, DateTime.Today.AddDays(1).AddHours(1),
                    System.Web.Caching.Cache.NoSlidingExpiration
                    );
            }
            return categories;
        }
        #endregion

        #region Cache Name
        public List<MasterModels.CacheModels> GetAllCacheDetails()
        {
            List<MasterModels.CacheModels> lstCacheDetails = (List<MasterModels.CacheModels>)System.Web.HttpContext.Current.Cache[CacheConstants.CACHE_ALL_CACHENAME];
            if (lstCacheDetails == null || lstCacheDetails.Count == 0)
            {
                MasterRepository master = new MasterRepository();
                lstCacheDetails = master.GetAllCacheDetails().OrderBy(x => x.CacheName).ToList();
                System.Web.HttpContext.Current.Cache.Insert(CacheConstants.CACHE_ALL_CACHENAME,
                    lstCacheDetails, null, DateTime.Today.AddDays(1).AddHours(1),
                    System.Web.Caching.Cache.NoSlidingExpiration
                    );
            }
            return lstCacheDetails;
        }
        #endregion

        #region Book Format
        public List<BookFormatModels> GetAllBookFormat()
        {
            List<BookFormatModels> lstBookFormat = (List<BookFormatModels>)System.Web.HttpContext.Current.Cache[CacheConstants.CACHE_ALL_BOOKFORMAT];
            if (lstBookFormat == null || lstBookFormat.Count == 0)
            {
                MasterRepository mdlBookFormat = new MasterRepository();
                lstBookFormat = mdlBookFormat.GetAllBookFormat();
                System.Web.HttpContext.Current.Cache.Insert(CacheConstants.CACHE_ALL_BOOKFORMAT,
                    lstBookFormat, null, DateTime.Today.AddDays(1).AddHours(1),
                    System.Web.Caching.Cache.NoSlidingExpiration
                    );
            }
            return lstBookFormat;
        }
        #endregion

    }
}