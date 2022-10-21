using KarkaThamizha.Common.Utility;
using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.CacheData;
using KarkaThamizha.Repository.DAL;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static KarkaThamizha.Object.Models.MasterModels;

namespace KarkaThamizha.Admin.Controllers
{
    public class MasterController : Controller
    {
        #region Profession
        public MasterModels.ProfessionModels GetAllProfession()
        {
            DataCaching caching = new DataCaching();
            MasterModels.ProfessionModels mdlProfession = new MasterModels.ProfessionModels();

            List<MasterModels.ProfessionModels> lstProfession = caching.GetAllProfession().ToList();
            mdlProfession.lstProfession = new SelectList(lstProfession.AsEnumerable(), "ProfessionID", "Profession", 0);
            return mdlProfession;
        }
        #endregion

        #region Special Name
        public MasterModels.SpecialNameModels GetAllSpecialName()
        {
            DataCaching caching = new DataCaching();
            MasterModels.SpecialNameModels mdlUserType = new MasterModels.SpecialNameModels();

            List<MasterModels.SpecialNameModels> lstName = caching.GetAllSpecialName().ToList();
            mdlUserType.lstSpecialName = new SelectList(lstName.AsEnumerable(), "SpecialNameID", "SpecialName", 0);
            return mdlUserType;
        }
        #endregion

        #region Get All Cache Details
        public ActionResult Cache()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAllCacheDetails(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            List<MasterModels.CacheModels> lstCacheDetails = new List<MasterModels.CacheModels>();
            DataCaching cacheCache = new DataCaching();
            lstCacheDetails = cacheCache.GetAllCacheDetails().OrderBy(x => x.Code).ToList();

            return PartialView("_Cache", lstCacheDetails.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ClearCache(string cacheName)
        {
            if (!string.IsNullOrEmpty(cacheName))
            {
                switch (cacheName)
                {
                    case "Admin":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_ADMIN);
                        break;
                    case "Article":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_ARTICLE);
                        break;
                    case "ArticleType":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_ARTICLETYPE);
                        break;
                    case "AuthorProfile":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_AUTHORPROFILE);
                        break;
                    case "BestBooks":
                        //System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_BestBooks);
                        break;
                    case "BookDetails":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_BOOKDETAILS);
                        break;
                    case "BookFair":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_BOOKFAIR);
                        break;
                    case "BookFormat":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_BOOKFORMAT);
                        break;
                    case "Books":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_BOOKS);
                        break;
                    case "BooksContent":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_BOOKSCONTENT);
                        break;
                    case "BooksRecommend":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_BOOKSRECOMMEND);
                        break;
                    case "BooksReview":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_BOOKSREVIEW);
                        break;
                    case "CacheName":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_CACHENAME);
                        break;
                    case "Category":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_CATEGORY);
                        break;
                    case "Country":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_COUNTRY);
                        break;
                    case "Events":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_EVENTS);
                        break;
                    case "EventsType":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_EVENTSTYPE);
                        break;
                    case "GeneralArticle":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_GENERALARTICLE);
                        break;
                    case "KarkaEvents":
                        //System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_KarkaEvents);
                        break;
                    case "KarkaEventsType":
                        //System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_KarkaEventsType);
                        break;
                    case "Magazine":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_MAGAZINE);
                        break;
                    case "MagazineIssue":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_MAGAZINEISSUE);
                        break;
                    case "MagazineReview":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_MAGAZINEREVIEW);
                        break;
                    case "MagazineType":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_MAGAZINETYPE);
                        break;
                    case "Profession":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_PROFESSION);
                        break;
                    case "Publishers":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_PUBLISHERS);
                        break;
                    case "SeriesType":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_SERIESTYPE);
                        break;
                    case "SpecialName":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_SPECIALNAME);
                        break;
                    case "Subscription":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_SUBSCRIPTION);
                        break;
                    case "User":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_USER);
                        break;
                    case "UserByAuthor":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_USERBYAUTHOR);
                        break;
                    case "UserType":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_USERTYPE);
                        break;
                    case "WeekDays":
                        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_WEEKDAYS);
                        break;
                }

                //key = "CacheConstants.CACHE_ALL_" + cacheName.Replace(" ", "").ToUpper();
                //System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_PROFESSION);
            }
            return RedirectToAction("Cache");
        }

        #endregion

        #region Book Format
        [AcceptVerbs(HttpVerbs.Get)]
        public BookFormatModels GetBookFormat()
        {
            DataCaching caching = new DataCaching();
            List<BookFormatModels> lstBookFormat = caching.GetAllBookFormat();
            BookFormatModels mdlBookFormat = new BookFormatModels();
            mdlBookFormat.lstBookFormat = new SelectList(lstBookFormat.AsEnumerable(), "BookFormatID", "BookFormat", 0);
            return mdlBookFormat;
        }
        #endregion

        #region Week Days
        //public WeekDaysModels GetWeekDays()
        //{
        //    DataCaching caching = new DataCaching();
        //    List<WeekDaysModels> lstWeekDays = caching.GetAllWeekDays();
        //    WeekDaysModels mdlWeekDays = new WeekDaysModels();
        //    mdlWeekDays.LstWeekDays = new SelectList(lstWeekDays.AsEnumerable(), "WeekDaysID", "WeekDay", 0);
        //    return mdlWeekDays;
        //}
        #endregion

        #region Article Type
        public ArticleTypeModels GetArticleType(int selectedValue)
        {
            DataCaching caching = new DataCaching();
            ArticleTypeModels mdlArticle = new ArticleTypeModels();
            mdlArticle.LstArticleType = new SelectList(caching.GetAllArticleType().AsEnumerable(), "ArticleTypeID", "ArticleType", selectedValue);
            return mdlArticle;
        }
        #endregion

        #region SeriesType
        [NonAction]
        public MasterModels.SeriesTypeModels GetAllSeriesType()
        {
            DataCaching cacheSeriesType = new DataCaching();
            MasterModels.SeriesTypeModels mdlSeriesType = new MasterModels.SeriesTypeModels
            {
                SelectLstSeriesType = new SelectList(cacheSeriesType.GetAllSeriesType().AsEnumerable(), "SeriesTypeID", "SeriesType", 0)
            };
            return mdlSeriesType;


        }
        #endregion

        #region ToDo
        [HttpGet]
        public ActionResult GetAllToDo()
        {
            List<ToDoModels> model = null;
            MasterRepository repo = new MasterRepository();
            model = repo.GetAllToDo();
            return PartialView("_ToDoList", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AddToDoList(string description)
        {
            MasterRepository repo = new MasterRepository();
            int result = repo.AddToDoList(description);
            return Json(new { message = result, JsonRequestBehavior.AllowGet });
        }

        public JsonResult Delete(int id)
        {
            string result = "Delete";
            return Json(new { message = result, JsonRequestBehavior.AllowGet });
        }
        #endregion
    }
}