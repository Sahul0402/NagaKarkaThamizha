using KarkaThamizha.Common.Utility;
using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.CacheData;
using KarkaThamizha.Repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarkaThamizha.Controllers
{
    [CustomErrorAttribute]
    public class HomeController : Controller
    {
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            try
            {
                List<BooksReviewModels> mdlBR = new List<BooksReviewModels>();
                DataCaching cache = new DataCaching();

                mdlBR = cache.GetBooksReview((int)EnumCode.UserType.User).OrderByDescending(x => x.SourceDate).ToList();
                return View(mdlBR);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[ChildActionOnly]
        //public ActionResult ShowMainPageEvents()
        //{
        //    ViewBag.MetaKeywords = "Book";
        //    ViewBag.MetaDescription = "Book";
        //    EventsController ctrlEvent = new EventsController();
        //    EventsModels lstEvents = ctrlEvent.GetEventsList();
        //    string url = HttpUtility.UrlDecode("_EventsTab");

        //    return PartialView(url, lstEvents);
        //}

        //[ChildActionOnly]
        //public ActionResult ShowInterview()
        //{
        //    ViewBag.MetaKeywords = "Book";
        //    ViewBag.MetaDescription = "Book";
        //    ArticleRepository repoContent = new ArticleRepository();
        //    List<ArticleModels> content = repoContent.GetAllInterviews().OrderByDescending(x => x.SourceDate).Take(5).ToList();
        //    return PartialView("_Interviews", content);
        //}

        //[ChildActionOnly]
        //public ActionResult ShowGeneralArticles()
        //{
        //    ViewBag.MetaKeywords = "Book";
        //    ViewBag.MetaDescription = "Book";
        //    ArticleController model = new ArticleController();
        //    List<ArticleModels> content = new List<ArticleModels>();
        //    content = model.GetGeneralArticles().Where(x => x.ArticleType.ArticleTypeID == 4).ToList();
        //    return PartialView("_GeneralArticles", content);
        //}

        //[ChildActionOnly]
        //public ActionResult ShowAuthorActivities()
        //{
        //    ArticleModels mdlArticle = new ArticleModels();
        //    ArticleController model = new ArticleController();
        //    List<ArticleModels> content = new List<ArticleModels>();
        //    List<ArticleModels> lstArticle = model.GetGeneralArticles();
        //    mdlArticle.LstArticle = lstArticle;
        //    string url = HttpUtility.UrlDecode("_Articles");

        //    return PartialView(url, mdlArticle);
        //}

        public ActionResult AllReview(int bookDetailsID)
        {
            ViewBag.MetaKeywords = "Book Reivew,Tamil Book Reivew,Novel Review,Tamil Novel Review";
            ViewBag.MetaDescription = "Book Reivew,Tamil Book Reivew,Novel Review,Tamil Novel Review";
            BooksReviewRepository ctrlBookReview = new BooksReviewRepository();
            List<BooksReviewModels> lstBookReview = new List<BooksReviewModels>();
            lstBookReview = ctrlBookReview.GetAllReviewByBookDetailsID(bookDetailsID);
            return View(lstBookReview);
        }

        [HttpGet]
        public ActionResult BooksReview(int booksReviewID)
        {
            ViewBag.MetaKeywords = "Book Reivew,Tamil Book Reivew,Novel Review,Tamil Novel Review";
            ViewBag.MetaDescription = "Book Reivew,Tamil Book Reivew,Novel Review,Tamil Novel Review";
            BooksReviewRepository ctrlBookReview = new BooksReviewRepository();
            BooksReviewModels lstBookReview = new BooksReviewModels();
            lstBookReview = ctrlBookReview.GetBooksReviewByReviewID(booksReviewID).Where(x => x.BooksReviewID == booksReviewID).FirstOrDefault();
            return View(lstBookReview);
        }


    }
}