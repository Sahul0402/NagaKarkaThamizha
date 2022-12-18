using KarkaThamizha.Common.Utility;
using KarkaThamizha.Controller;
using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.CacheData;
using KarkaThamizha.Repository.DAL;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarkaThamizha.Controllers
{
    ////http://www.dotnetawesome.com/2014/11/how-to-implement-custom-paging-and-sorting-in-webgrid-using-jquery.html
    [CustomErrorAttribute]
    public class AuthorController : KarkaThamizhaBaseController
    {
        #region Show Authors
        // GET: Author
        [AcceptVerbs(HttpVerbs.Get)]
        [Route("Authors")]
        public ActionResult Authors()
        {
            return View("Authors");
        }

        public ActionResult GetAllAuthor(string search, int? page)
        {
            int pageSize = 8;
            int pageNumber = 1;
            object data;

            DataCaching repoUser = new DataCaching();
            List<UserModels> mdlUser = new List<UserModels>();

            if (TempData["Search"] != null)
            {
                if (!string.IsNullOrWhiteSpace(search))
                { TempData["Search"] = search; }
                else if (TempData["Search"].ToString() != search) { search = TempData["Search"] as string; }

                if (string.IsNullOrWhiteSpace(search))
                    mdlUser = repoUser.GetAllAuthorsProfile((Byte)EnumCode.UserType.Author).OrderBy(x => x.UserName).ToList();
                else
                {
                    data = search;
                    TempData["Search"] = search;
                    mdlUser = repoUser.GetAllAuthorsProfile((Byte)EnumCode.UserType.Author).Where(x => x.UserName.StartsWith(data.ToString())).OrderBy(x => x.UserName).ToList();
                    TempData.Keep();
                }
            }
            else if (!string.IsNullOrWhiteSpace(search))
            {
                mdlUser = repoUser.GetAllAuthorsProfile((Byte)EnumCode.UserType.Author).Where(x => x.UserName.StartsWith(search.ToString())).OrderBy(x => x.UserName).ToList(); TempData["Search"] = search;
            }
            else
                mdlUser = repoUser.GetAllAuthorsProfile((Byte)EnumCode.UserType.Author).OrderBy(x => x.UserName).ToList();

            if (search == "All")
            {
                TempData["Search"] = null;
                mdlUser = repoUser.GetAllAuthorsProfile((Byte)EnumCode.UserType.Author).OrderBy(x => x.UserName).ToList();
            }

            pageNumber = page.HasValue ? Convert.ToInt32(page) : 1;
            return PartialView("_Authors", mdlUser.ToPagedList(pageNumber, pageSize));
        }
        #endregion

        #region Author Details - Info & Books
        public ActionResult AuthorDetails(string Name, int? UserID)
        {
            if (UserID != null)
            {
                ViewData["Author"] = Name;
                ViewData["hdnUserID"] = UserID.Value;
                return View("AuthorDetails");
            }
            else
                return View("Authors");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult LoadUserFeedback(Int16 masterPageID, Int16 childPageID)
        {
            try
            {
                ContactUsRepository repo = null;
                List<ContactUsModels> feedback = null;
                if (masterPageID > 0 && childPageID > 0)
                {
                    repo = new ContactUsRepository();
                    //feedback = repo.GetAllFeedback(masterPageID, (int)EnumCode.Projects.KarkaThamizha).Where(x => x.ChildPageID == childPageID).ToList();
                }

                if (masterPageID == (Int16)EnumCode.Pages.BookFair || masterPageID == (Int16)EnumCode.Pages.BookRelease || masterPageID == (Int16)EnumCode.Pages.Award)
                {
                    BooksDetailsModels model = new BooksDetailsModels();  //Not possible to send booksdetails model to _UserFeedback page. it expects only ContactUsModels
                    model.Feedback = feedback;
                    return PartialView("_UserFeedback", model);
                }
                else
                    return PartialView("_UserFeedback", feedback);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetAuthorInfoByAuthorID(int? userID)
        {
            UserModels userDetails = GetUserDetails(userID.Value);
            return PartialView("_AuthorInfo", userDetails);
        }

        //public ActionResult GetUserDetailswithSocialMedia(string Name)
        //{
        //    int userID = 0;
        //    UserModels userDetails = GetUserDetails(userID);
        //    return PartialView("_SocialMedia", userDetails);
        //}

        private UserModels GetUserDetails(int userID)
        {
            UserModels userDetails = new UserModels();
            if (userID > 0)
            {
                UserRepository userInfo = new UserRepository();
                userDetails = userInfo.GetAuthorsProfileByID(userID);
            }
            return userDetails;
        }

        public ActionResult GetAuthorBooksDetailsByAuthorID(int? userID)
        {
            try
            {
                BooksModels mdlbook = new BooksModels();
                List<BooksDetailsModels> mdlBookDetails = new List<BooksDetailsModels>();
                List<BooksDetailsModels> books = new List<BooksDetailsModels>();

                if (userID.HasValue && userID > 0)
                {
                    DataCaching repoUser = new DataCaching();
                    BooksDetailsRepository repoBookDetail = new BooksDetailsRepository();

                    //Get Author Profile
                    //var users = (from user in repoUser.GetAllAuthorsProfile((Byte)EnumCode.UserType.Author).Where(x => x.UserID == userID.Value) select user).FirstOrDefault();
                    //if (users != null)
                    //{
                    //    mdlbook.Users = users;
                    //Get Author BookDetails
                    int? userId = null;
                    if (Session["UserID"] != null)
                    {
                        userId = int.Parse(Session["UserID"].ToString());
                    }

                    mdlBookDetails = repoBookDetail.GetAllBooksDetailsByAuthorID(userID.Value, userId).ToList();

                    var books1 = mdlBookDetails.GroupBy(x => x.CategoryID).Select(x => x.FirstOrDefault()).ToList();

                    foreach (BooksDetailsModels item in books1)
                    {
                        var booksList = mdlBookDetails.Where(x => x.CategoryID == item.CategoryID && x.Books.CategoryID == item.CategoryID).Select(b=>b.Books).ToList();
                        books.Add(new BooksDetailsModels
                        {
                            CategoryID = item.CategoryID,
                            CategoryName = item.CategoryName,
                            Price = item.Price,
                            FirstEdition = item.FirstEdition,
                            BooksReviewID = item.BooksReview.BooksReviewID,
                            BookList = booksList,
                            UserTypeID = item.UserTypeID
                        });
                    }
                    //}
                }
                return Json(books, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult GetAuthorBooksByAuthorID(int? userID)
        {
            try
            {
                BooksModels mdlbook = new BooksModels();
                List<BooksDetailsModels> mdlBookDetails = new List<BooksDetailsModels>();
                List<BooksDetailsModels> books = new List<BooksDetailsModels>();

                if (userID.HasValue && userID > 0)
                {
                    DataCaching repoUser = new DataCaching();
                    BooksDetailsRepository repoBookDetail = new BooksDetailsRepository();

                    //Get Author Profile
                    //var users = (from user in repoUser.GetAllAuthorsProfile((Byte)EnumCode.UserType.Author).Where(x => x.UserID == userID.Value) select user).FirstOrDefault();
                    //if (users != null)
                    //{
                    //    mdlbook.Users = users;
                    //Get Author BookDetails
                    mdlBookDetails = repoBookDetail.GetAllBooksDetailsByAuthorID(userID.Value, null).ToList();

                    var books1 = mdlBookDetails.GroupBy(x => x.CategoryID).Select(x => x.FirstOrDefault()).ToList();

                    foreach (BooksDetailsModels item in books1)
                    {
                        books.Add(new BooksDetailsModels
                        {
                            CategoryID = item.CategoryID,
                            CategoryName = item.CategoryName,
                            Price = item.Price,
                            FirstEdition = item.FirstEdition,
                            BooksReviewID = item.BooksReview.BooksReviewID,
                            authorBooks = mdlBookDetails.Where(x => x.CategoryID == item.CategoryID).ToList(),
                            UserTypeID = item.UserTypeID
                        });
                    }
                    //}
                }
                return PartialView("_AuthorBooks", books);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Author Series
        [HttpGet]
        public ActionResult AuthorSeries()
        {
            return View("AuthorSeries");
        }

        //[HttpGet]
        //public ActionResult GetAllAuthorSeries(int? page)
        //{
        //    int pageNumber = 1;
        //    int pageSize = 10;

        //    List<SeriesModels> mdlSeries = new List<SeriesModels>();
        //    AuthorRepository ctrlAuthor = new AuthorRepository();
        //    mdlSeries = ctrlAuthor.GetAllAuthorSeries().OrderByDescending(x => x.StartDate).ToList();
        //    if (mdlSeries != null && mdlSeries.Count > 0)
        //    {
        //        pageNumber = (page ?? 1);
        //    }
        //    return PartialView("_AuthorSeries", mdlSeries.ToPagedList(pageNumber, pageSize));
        //}

        #endregion

        #region Clear Cache
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ClearAuthorCache()
        {
            System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_AUTHORPROFILE);
            System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_USER);
            return RedirectToAction("Authors");
        }
        #endregion

        #region Author Interviews

        [AcceptVerbs(HttpVerbs.Get)]
        [ActionName("Interviews")]
        [Route("Interviews")]
        public ActionResult AuthorInterviews(string sortOrder, string currentFilter, string txtSearch, int? page, string type)
        {
            ViewBag.MetaKeywords = "Book Reivew,Tamil Book Reivew,Novel Review,Tamil Novel Review";
            ViewBag.MetaDescription = "Book Reivew,Tamil Book Reivew,Novel Review,Tamil Novel Review";

            ArticleRepository repoInterview = new ArticleRepository();
            IEnumerable<ArticleModels> mdlInterview;
            mdlInterview = repoInterview.GetAllInterviews().OrderByDescending(X => X.CreateDate).ToList();

            int pageSize = 12;
            int pageNumber = page.HasValue ? Convert.ToInt32(page) : 1;

            return View("AuthorInterviews", mdlInterview.ToPagedList(pageNumber, pageSize));
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetAllInterivews(int? page)
        {
            int pageNumber = 1;
            int pageSize = 10;

            ViewBag.MetaKeywords = "எழுத்தாளர் நேர்காணல்கள்,Writers Interview";
            ViewBag.MetaDescription = "";

            ArticleRepository repoInterview = new ArticleRepository();
            IEnumerable<ArticleModels> mdlInterview;

            mdlInterview = repoInterview.GetAllInterviews().OrderByDescending(X => X.CreateDate).ToList();
            if (mdlInterview != null && mdlInterview.Count() > 0)
            {
                pageNumber = (page ?? 1);
            }
            return PartialView("_AuthorInterviews", mdlInterview.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Interview(int id)
        {
            ViewBag.MetaKeywords = "எழுத்தாளர் நேர்காணல்கள்,Writers Interview";
            ViewBag.MetaDescription = "Book Reivew,Tamil Book Reivew,Novel Review,Tamil Novel Review";

            ArticleRepository repoInterview = new ArticleRepository();
            ArticleModels mdlInterview = new ArticleModels();
            mdlInterview = repoInterview.GetAllInterviews().Where(x => x.ArticleID == id).FirstOrDefault();

            return View("AuthorInterview", mdlInterview);
        }

        #endregion

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetAllTaggedWith(int? page)
        {
            return PartialView("_TaggedWith");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetAllShareOn(int? page)
        {
            return PartialView("_ShareOn");
        }

        //[HttpGet]
        //public JsonResult GetAuthorBooks()
        //{
        //    BooksDetailsRepository repoBookDetail = new BooksDetailsRepository();
        //    var mdlBookDetails1 = repoBookDetail.GetAllBooksDetailsByAuthorID(31).ToList();

        //    return Json(mdlBookDetails1, JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //public ActionResult GetAuthorInfoByUserID(int? authorID)
        //{
        //    DataCaching caching = new DataCaching();
        //    List<UserModels> authors = caching.GetAllUsers().Where(x => x.UserID == authorID.Value).ToList();
        //    return PartialView("_AuthorInfo", authors);
        //}
    }
}