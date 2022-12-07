using KarkaThamizha.Common.Utility;
using KarkaThamizha.Controller;
using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.DAL;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarkaThamizha.Controllers
{
    public class BooksReviewController : KarkaThamizhaBaseController
    {
        /// <summary>
        /// Called from Layout Page
        /// </summary>
        /// <param name="Review"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult BooksReviewBy(string reviewBy)
        {
            ViewData["ReviewBy"] = reviewBy;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult BooksReview(int booksReviewID)
        {
            ViewBag.MetaKeywords = "Book Reivew,Tamil Book Reivew,Novel Review,Tamil Novel Review";
            ViewBag.MetaDescription = "Book Reivew,Tamil Book Reivew,Novel Review,Tamil Novel Review";
            BooksReviewRepository repoBR = new BooksReviewRepository();
            BooksReviewModels bookReview = new BooksReviewModels();
            bookReview = repoBR.GetBooksReviewByReviewID(booksReviewID).Where(x => x.BooksReviewID == booksReviewID).FirstOrDefault();
            return View(bookReview);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult UsersReview(int? page)
        {
            int pageNumber = 1;
            int pageSize = 5;

            List<BooksReviewModels> lstUserBookReview = new List<BooksReviewModels>();
            BooksReviewRepository repoBookReview = new BooksReviewRepository();
            lstUserBookReview = repoBookReview.GetBooksReview((Int16)EnumCode.UserType.User).OrderByDescending(x => x.CreatedDate).ToList();
            string url = HttpUtility.UrlDecode("_BookReviewByUser");

            if (lstUserBookReview != null && lstUserBookReview.Count > 0)
            {
                pageNumber = (page ?? 1);
            }
            return PartialView(url, lstUserBookReview.ToPagedList(pageNumber, pageSize));
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult WritersReview(int? page)
        {
            int pageNumber = 1;
            int pageSize = 5;

            List<BooksReviewModels> lstWriterReview = new List<BooksReviewModels>();
            BooksReviewRepository repoBookReview = new BooksReviewRepository();
            lstWriterReview = repoBookReview.GetBooksReview((Byte)EnumCode.UserType.Author).OrderByDescending(x => x.CreatedDate).ToList();
            string url = HttpUtility.UrlDecode("_BookReviewByWriter");

            if (lstWriterReview != null && lstWriterReview.Count > 0)
            {
                pageNumber = (page ?? 1);
            }
            return PartialView(url, lstWriterReview.ToPagedList(pageNumber, pageSize));
        }
    }
}