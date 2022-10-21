using KarkaThamizha.Common.Utility;
using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.CacheData;
using KarkaThamizha.Repository.DAL;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarkaThamizha.Admin.Controllers
{
    [CustomException]
    public class BooksDetailsController : Controller
    {
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult BooksDetails(string sortOrder, string currentFilter, string txtSearch, int? page)
        {
            BooksDetailsRepository book = new BooksDetailsRepository();
            List<BooksDetailsModels> lstBooksDetails = new List<BooksDetailsModels>();

            int pageSize = 1;
            int pageNumber = 1;

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DescriptionSortParm = sortOrder == "Description" ? "Description_Desc" : "Description";

            if (txtSearch != null)
                page = 1;
            else
                txtSearch = currentFilter;

            ViewBag.CurrentFilter = txtSearch;

            if (!String.IsNullOrEmpty(txtSearch))
            {
                ViewBag.CurrentFilter = txtSearch.Trim().Replace(";", "").Trim();
            }

            lstBooksDetails = book.GetAllBookDetails().ToList();
            pageSize = 10;
            pageNumber = page.HasValue ? Convert.ToInt32(page) : 1;
            return View(lstBooksDetails.ToPagedList(pageNumber, pageSize));
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult AddBooksDetails(int? bookID, int? CategoryID)
        {
            BooksDetailsModels mdlBookdDetails = new BooksDetailsModels();
            DataCaching caching = new DataCaching();
            BooksModels mdlBooks = null;

            //Bind Book Format List in dropdown list             
            MasterController ctrlBookFormat = new MasterController();
            BookFormatModels mdlBookFormat = ctrlBookFormat.GetBookFormat();
            
            //List<PublishersModels> mdlPublisher = caching.GetAllPublishers();

            //mdlBookdDetails.PublisherList = new SelectList(mdlPublisher.AsEnumerable().OrderBy(x => x.Publisher), "PublisherID", "Publisher", 0);
            mdlBookdDetails.BookFormat = mdlBookFormat;

            if (bookID > 0)
            {
                mdlBooks = new BooksModels();
                mdlBooks = caching.GetBooksList().Find(p => p.BookID == bookID);
                if (mdlBooks != null)
                    mdlBookdDetails.Books = mdlBooks;
                else
                {
                    BooksRepository repoBook = new BooksRepository();
                    bool flag = repoBook.UpdateBookStatus(bookID.Value);
                    mdlBookdDetails.Books.BookID = bookID.Value;
                }
            }
            return View(mdlBookdDetails);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddBooksDetails(BooksDetailsModels objBookDetails, IEnumerable<HttpPostedFileBase> imgFile)
        {
            try
            {
                DataCaching caching = new DataCaching();
                //                List<UserModels> mdlAuthor = caching.GetUserByAuthorType();
                List<UserModels> mdlAuthor = null;
                                var lstAuthor = mdlAuthor.Select(a => new { a.UserID, a.Country });
                ViewBag.Author = new SelectList(lstAuthor.AsEnumerable(), "AuthorID", "User");
                int i = 0;
                string imgPath = "";
                bool value = false;

                ModelState.Remove("Books.Name");
                ModelState.Remove("Books.BookDescription");

                var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors }).ToArray();

                var errors1 = ModelState.Values.SelectMany(v => v.Errors);

                if (ModelState.IsValid)
                {
                    BooksDetailsRepository model = new BooksDetailsRepository();

                    foreach (HttpPostedFileBase file in imgFile)
                    {
                        //Checking file is available to save.  
                        if (file != null && file.ContentLength > 0)
                        {
                            var supportedTypes = new[] { "jpg", "jpeg", "png" };
                            var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);

                            if (!supportedTypes.Contains(fileExt))
                            {
                                ModelState.AddModelError("photo", "Invalid type. Only the following types (jpg, jpeg, png) are supported.");
                            }

                            var InputFileName = Path.GetFileName(file.FileName);
                            bool isLocal = System.Web.HttpContext.Current.Request.IsLocal;

                            imgPath = ConfigurationManager.AppSettings["ImgBook"] + InputFileName;

                            file.SaveAs(Path.Combine(imgPath));

                            AddBookImage(i, InputFileName, ref objBookDetails);
                            //assigning file uploaded status to ViewBag for showing message to user.  
                            ViewBag.UploadStatus = imgFile.Count().ToString() + " files uploaded successfully.";
                            value = true;
                        }
                        i++;
                    }

                    var result = model.AddBooksDetails(objBookDetails);
                    System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_BOOKDETAILS);
                    ViewData["Result"] = result;
                    ModelState.Clear();
                    return RedirectToAction("BooksDetails", "BooksDetails");
                }
                else
                {
                    ModelState.AddModelError("New Book", "Books Details - Error on Saving Data.");
                    return View();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void AddBookImage(int i, string inputFileName, ref BooksDetailsModels objBookDetails)
        {
            if (i == 0)
            { objBookDetails.ImgBookSmallFS = inputFileName; }
            else if (i == 1)
            { objBookDetails.ImgBookSmallBS = inputFileName; }
            else if (i == 2)
            { objBookDetails.ImgBookLarge = inputFileName; }
        }

        //GET:Books / EditBooks/17
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult UpdateBooksDetails(int bookDetailsId)
        {
            BooksDetailsRepository repoBookDetail = new BooksDetailsRepository();
            BooksDetailsModels mdlBooksDetails = new BooksDetailsModels();
            MasterController ctrlBookFormat = new MasterController();
            DataCaching caching = new DataCaching();

            //List<PublishersModels> mdlPublisher = caching.GetAllPublishers();
            mdlBooksDetails = repoBookDetail.GetAllBookDetails().Find(p => p.BookDetailsID == bookDetailsId);
            BookFormatModels mdlBookFormat = ctrlBookFormat.GetBookFormat();
            mdlBooksDetails.BookFormat.lstBookFormat = new SelectList(mdlBookFormat.lstBookFormat.AsEnumerable(), "Value", "Text");

            if (mdlBooksDetails.BookFormat.BookFormatID > 0)
                mdlBooksDetails.BookFormatID = mdlBooksDetails.BookFormat.BookFormatID;
            else
                mdlBooksDetails.BookFormatID = 0;

            //if (mdlBooksDetails.Publisher.PublisherID > 0)
            //{ mdlBooksDetails.PublisherID = mdlBooksDetails.Publisher.PublisherID; }
            //else
            //    mdlBooksDetails.PublisherID = 0;
            //mdlBooksDetails.PublisherList = new SelectList(mdlPublisher.AsEnumerable(), "PublisherID", "Publisher");

            return View(mdlBooksDetails);
        }

        //POST:Books / EditBooks/8
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateBooksDetails(int bookDetailsId, BooksDetailsModels objBookDetails, IEnumerable<HttpPostedFileBase> imgFile)
        {
            objBookDetails.BookDetailsID = bookDetailsId;
            AddBooksDetails(objBookDetails, imgFile);
            return View();
        }
        
        public ActionResult BestBooks()
        {
            return View("BestBooks");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult GetBookDetailsByBookID(int bookId)
        {
            List<BooksDetailsModels> lstBooksReview = new List<BooksDetailsModels>();
            BooksDetailsRepository repoBookDetails = new BooksDetailsRepository();
            lstBooksReview = repoBookDetails.GetAllBooksDetailsByBookID(bookId);

            //var lstPublisher = from pub in lstBooksReview
            //                   select new { PublisherName = pub.Publisher.Publisher, Price = pub.Price, NoOfCopy = pub.NoofCopy };
            //return Json(lstPublisher.ToList(), JsonRequestBehavior.AllowGet);
            return null;
        }
    }
}