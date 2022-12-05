using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace KarkaThamizha.Controllers
{
    public class UserRatingController : Controller
    {
        // GET: UserRating
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult AddOrUpdateUserRating(int userId, int bookId, int userRating)
        {
            UserRatingRepository userRatingRepository = new UserRatingRepository();
            try
            {
                userRatingRepository.AddOrUpdateUserRating(userId, bookId, userRating);
            }
            catch (Exception ex)
            {

            }
            return Json(new object(), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetUserRatingByUserAndBookID(int userId, int bookId)
        {
            UserRatingRepository userRatingRepository = new UserRatingRepository();
            UserRatingModels result = new UserRatingModels();
            try
            {
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