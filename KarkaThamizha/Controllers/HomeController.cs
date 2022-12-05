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