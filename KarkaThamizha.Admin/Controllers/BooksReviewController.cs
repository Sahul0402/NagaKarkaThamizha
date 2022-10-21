using KarkaThamizha.Common.Utility;
using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.DAL;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarkaThamizha.Admin.Controllers
{
    public class BooksReviewController : Controller
    {
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult BooksReview(string sortOrder, string currentFilter, string searchString, int? page, int? value)
        {
            List<BooksReviewModels> lstBooksReview = new List<BooksReviewModels>();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DescriptionSortParm = sortOrder == "Description" ? "Description_Desc" : "Description";

            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem { Text = "எழுத்தாளர்கள்/வாசகர்கள்", Value = "1" },
                new SelectListItem { Text = "பத்திரிக்கைகள்", Value = "2" },
            };

            ViewBag.Source = items;

            int pageNumber = 1;
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            lstBooksReview = GetBooksReviewList(value == null ? 1 : value.Value);

            if (!String.IsNullOrEmpty(searchString))
            {
                lstBooksReview = lstBooksReview.Where(p => p.BookName.Contains(searchString)).ToList();
            }

            if (value.HasValue && value.Value == 2)
            {
                items[0].Selected = false;
                items[1].Selected = true;
            }
            else
            {
                items[0].Selected = true;
                items[1].Selected = false;
            }

            switch (sortOrder)
            {
                case "name_desc":
                    lstBooksReview = lstBooksReview.OrderByDescending(p => p.BookName).ToList();
                    break;
                case "Description":
                    lstBooksReview = lstBooksReview.OrderBy(p => p.SourceDate).ToList();
                    break;
                case "Header_Desc":
                    lstBooksReview = lstBooksReview.OrderByDescending(p => p.Header).ToList();
                    break;
                default:  // Name ascending 
                    lstBooksReview = lstBooksReview.OrderByDescending(p => p.SourceDate).ToList();
                    break;
            }
            int pageSize = 10;
            pageNumber = page.HasValue ? Convert.ToInt32(page) : 1;

            return View(lstBooksReview.ToPagedList(pageNumber, pageSize));
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public List<BooksReviewModels> GetBooksReviewList(int type)
        {
            List<BooksReviewModels> mdlBooksDetails = new List<BooksReviewModels>();
            BooksReviewRepository book = new BooksReviewRepository();
            mdlBooksDetails = book.GetAllBooksReviewDetails(type);
            return mdlBooksDetails;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        //public ActionResult AddBooksReview(int bookDetailsID)
        //{
        //    BooksReviewRepository repoBooksReview = new BooksReviewRepository();
        //    BooksReviewModels mdlBooksReview = new BooksReviewModels();
        //    MagazineRepository mdlMagazine = new MagazineRepository();
        //    List<MagazineModels> lstMagazine = new List<MagazineModels>();

        //    mdlBooksReview = repoBooksReview.GetBooksDetailsByID(bookDetailsID);
        //    lstMagazine = mdlMagazine.GetAllMagazines();
        //    mdlBooksReview.LstMagazine = new SelectList(lstMagazine.AsEnumerable(), "MagazineID", "Name", 0);

        //    return View(mdlBooksReview);
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult AddBooksReview(BooksReviewModels bookReview)
        //{
        //    List<MagazineModels> lstMagazine = new List<MagazineModels>();

        //    if (bookReview.UserType == (Byte)EnumCode.UserType.User || bookReview.UserType == (Byte)EnumCode.UserType.User)
        //    {
        //        ModelState.Remove("MagazineID");
        //    }
        //    else
        //        ModelState.Remove("MagazineID");

        //    ModelState.Remove("BookDetail.Books.Name");
        //    ModelState.Remove("BookDetail.Books.BookDescription");

        //    //var errors = ModelState
        //    //.Where(x => x.Value.Errors.Count > 0)
        //    //.Select(x => new { x.Key, x.Value.Errors }).ToArray();  

        //    var errors1 = ModelState.Values.SelectMany(v => v.Errors);

        //    if (ModelState.IsValid)
        //    {
        //        int val = bookReview.BookDetail.BookDetailsID;
        //        BooksReviewRepository model = new BooksReviewRepository();
        //        var result = model.AddBooksReview(bookReview);
        //        System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_BOOKSREVIEW);
        //        ViewData["Result"] = result;
        //        ModelState.Clear();
        //        return RedirectToAction("BooksReview");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("New Book", "Books Review - Error on Saving Data.");
        //        return View();
        //    }
        //}

        //[AcceptVerbs(HttpVerbs.Get)]
        //public ActionResult UpdateBookReview(int bookReviewid)
        //{
        //    BooksReviewModels mdlBooksReview = new BooksReviewModels();
        //    MagazineRepository mdlMagazine = new MagazineRepository();
        //    List<MagazineModels> lstMagazine = new List<MagazineModels>();
        //    lstMagazine = mdlMagazine.GetAllMagazines();
        //    mdlBooksReview = GetBooksReviewList(1).Find(p => p.BooksReviewID == bookReviewid);
        //    mdlBooksReview.LstMagazine = new SelectList(lstMagazine.AsEnumerable(), "MagazineID", "Name", 0);

        //    return View(mdlBooksReview);
        //}

        //POST:Books / EditBooks/8
        //[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateBookReview(BooksReviewModels mdlBookReview)
        {
            BooksReviewRepository model = new BooksReviewRepository();
            string result = model.UpdateBookReview(mdlBookReview);
            if (result.Equals("Success"))
            {
                System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_BOOKSREVIEW);
                return RedirectToAction("BooksReview");
            }
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetAllBookReviewsByBookID(int? bookId)
        {
            List<BooksReviewModels> lstBooksReview = new List<BooksReviewModels>();
            if (bookId.HasValue && bookId.Value > 0)
            {   
                BooksReviewRepository repoBooksReview = new BooksReviewRepository();
                lstBooksReview = repoBooksReview.GetAllBookReviewsByBookID(bookId.Value);                
            }
            return Json(lstBooksReview, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult DeleteBookReview(int bookReviewid)
        {
            BooksReviewRepository model = new BooksReviewRepository();
            bool flag = model.DeleteBookReview(bookReviewid);
            if (flag)
            {
                System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_BOOKSREVIEW);
                return RedirectToAction("BooksReview");
            }
            return View();
        }
    }
}