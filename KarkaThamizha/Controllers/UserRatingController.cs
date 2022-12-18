using KarkaThamizha.Controller;
using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.DAL;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace KarkaThamizha.Controllers
{
    public class UserRatingController : KarkaThamizhaBaseController
    {
        readonly UserRatingRepository userRatingRepository = null;
        public UserRatingController()
        {
            userRatingRepository = new UserRatingRepository();
        }

        // GET: UserRating
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult UserRating(UserRatingModels models)
        {
            UserRatingModels userRatingModels = new UserRatingModels();
            userRatingModels = userRatingRepository.GetUserRatingByUserAndBookID(models.UserID, models.BookID);
            userRatingModels.MaxRating = int.Parse(WebConfigurationManager.AppSettings["MaxUserRating"]);
            return PartialView(userRatingModels);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult AddOrUpdateUserRating(int bookId, int userRating)
        {
            if (Session["UserID"] == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            UserRatingRepository userRatingRepository = new UserRatingRepository();
            try
            {
                int userId = int.Parse(Session["UserID"].ToString());
                userRatingRepository.AddOrUpdateUserRating(userId, bookId, userRating);
                Session["isRating"] = false;
            }
            catch (Exception ex)
            {

            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetUserRatingByUserAndBookID(int bookId)
        {
            UserRatingModels result = new UserRatingModels();
            try
            {
                int? userId = null;
                if (Session["UserID"] != null)
                {
                    userId = int.Parse(Session["UserID"].ToString());
                }
                result = userRatingRepository.GetUserRatingByUserAndBookID(userId, bookId);
                result.MaxRating = int.Parse(WebConfigurationManager.AppSettings["MaxUserRating"]);
            }
            catch (Exception ex)
            {

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}