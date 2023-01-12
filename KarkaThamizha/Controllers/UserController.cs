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
    //[RoutePrefix("User")]
    public class UserController : KarkaThamizhaBaseController
    {
        [AllowAnonymous]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult IsMailExists(string mailId)
        {
            UserRepository repoUser = new UserRepository();
            bool flag = repoUser.CheckEmailExists(mailId);
            return Json(new { message = flag }, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult User()
        {
            //ViewData["ReviewBy"] = "UserByID";
            return View("User");
        }

        [AcceptVerbs(HttpVerbs.Get)]

        //[ActionName("UserReview")]
        public ActionResult UserInfoByUserId(int? userId)
        {
            UserRepository repoUser = null;
            UserModels mdlUser = null;
            //UserBooksReviewModels bookReivew = null;

            if (userId != null)
            {
                repoUser = new UserRepository();
                mdlUser = repoUser.GetAuthorsProfileByID(userId.Value);
                //bookReivew = new UserBooksReviewModels();
                //bookReivew.User = mdlUser;

                return PartialView("_UserInfo", mdlUser);
                //return View("User", bookReivew);
            }
            return RedirectToActionPermanent("Index", "Home");
        }

        public IEnumerable<SelectListItem> GetAuthorList()
        {
            DataCaching caching = new DataCaching();
            SelectList lstObj = null;
            var list = caching.GetUserByAuthorType().Select(p => new SelectListItem { Value = p.UserID.ToString(), Text = p.UserName }).OrderBy(x => x.Text).ToList();

            //setting
            lstObj = new SelectList(list, "Value", "Text");
            return lstObj;
        }

        public ActionResult UserReviews(int? userId, int? page, string viewType)
        {
            int pageNumber = 1;
            int pageSize = 10;
            string url = "";
            UserBookReview reviewBooks = new UserBookReview();


            List<BooksReviewModels> lstUserBookReview = new List<BooksReviewModels>();
            BooksReviewRepository repoBookReview = new BooksReviewRepository();
            if (userId != null && userId.Value > 0)
            {
                lstUserBookReview = repoBookReview.GetAllBookReviewsByUserID(userId.Value).ToList();
                url = HttpUtility.UrlDecode("_UserReview");

                if (lstUserBookReview != null && lstUserBookReview.Count > 0)
                {
                    pageNumber = (page ?? 1);
                }
                reviewBooks.ListBooks = lstUserBookReview;
                reviewBooks.ViewType = viewType;
                //return PartialView(url, lstUserBookReview.ToPagedList(pageNumber, pageSize));
                return PartialView(url, reviewBooks);
            }
            else
            {
                return RedirectToActionPermanent("Index", "Home");
            }
        }

        #region User Profile
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult UserProfile()
        {
            UserRepository repoUser = null;
            UserModels mdlUser = null;
            MasterModels.ProfessionModels mdlProfession = null;
            MasterRepository repoMaster = null;
            int userId = Convert.ToInt16(Session["UserID"]);
            if (userId > 0)
            {
                repoUser = new UserRepository();
                mdlUser = new UserModels();
                mdlProfession = new MasterModels.ProfessionModels();
                repoMaster = new MasterRepository();

                mdlUser = repoUser.GetUserByUserID(userId);

                mdlProfession.lstProfession = new SelectList(repoMaster.GetAllProfession().AsEnumerable(), "ProfessionID", "Profession");
                mdlProfession.ProfessionID = mdlUser.Profession.ProfessionID;
                mdlUser.Profession = mdlProfession;

                var mdlAuthor = GetAuthorList();
                mdlUser.Authors = mdlAuthor;

                return View("UserProfile", mdlUser);
            }
            else
                return RedirectToAction("Index", "Home");

        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult UserProfile(UserModels mdlUsers)
        //{
        //    UserRepository repoUser = null;
        //    if (Session["UserID"] != null)
        //    {
        //        int userID = Convert.ToInt32(Session["UserID"]);
        //        repoUser = new UserRepository();
        //        mdlUsers.UserID = userID;
        //        var returnValue = repoUser.UpdateUser(mdlUsers);

        //        if (returnValue == "Success")
        //        {
        //            InsertFavouriteAuthor(userID);
        //            InsertFavouriteCategory(userID);
        //        }
        //    }
        //    return RedirectToAction("UserProfile", "User");
        //}

        private void InsertFavouriteAuthor(int userID)
        {

        }

        private void InsertFavouriteCategory(int userID)
        {

        }
        #endregion

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddBooksReviewByUser(BooksReviewModels booksReviewModels)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                if (Session["UserId"] != null)
                {
                    BooksReviewRepository repoBooksReview = null;
                    int loginId = Convert.ToInt32(Session["UserId"]);
                    booksReviewModels.UserID = loginId;
                    message = repoBooksReview.AddBooksReviewByUser(booksReviewModels);
                }
                else
                {
                    message = "Expired";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}