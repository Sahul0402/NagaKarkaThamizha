using KarkaThamizha.Common.Utility;
using KarkaThamizha.Controller;
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
    public class LoginController : KarkaThamizhaBaseController
    {
        public PartialViewResult LoginPartial()
        {

            return PartialView();
        }
        public ActionResult UserLogin(string email, string password, bool isRating = false)
        {
            LoginRepository repoLogin = null;
            LoginModels mdlUser = null;
            string message = "";
            Session["isRating"] = isRating;
            if (string.IsNullOrEmpty(email.Trim()) && string.IsNullOrEmpty(password.Trim()))
                return null;

            try
            {
                repoLogin = new LoginRepository();
                mdlUser = repoLogin.UserLogin(email, password);
                if (mdlUser != null && !string.IsNullOrEmpty(mdlUser.LoginID.ToString()) && !string.IsNullOrEmpty(mdlUser.Name))
                {
                    Session["UserID"] = mdlUser.LoginID;
                    Session["Name"] = mdlUser.Name;
                    message = Convert.ToString(mdlUser.LoginID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(new { message = message, isRating = isRating }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            //Clear all session variables.
            Session.Remove("UserID");
            Session.Remove("Name");
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserProfile()
        {
            LoginRepository repoLogin = null;
            LoginModels mdlUser = null;
            int userId = Convert.ToInt16(Session["UserID"]);
            if (userId > 0)
            {
                repoLogin = new LoginRepository();
                mdlUser = new LoginModels();

                mdlUser = repoLogin.GetUserProfileByUserID(userId);
            }
            return View("UserProfile", mdlUser);
        }

        [HttpPost]
        public ActionResult UserProfile(UserModels mdlUsers)
        {
            LoginRepository repoLogin = null;
            if (Session["UserID"] !=null)
            {
                repoLogin = new LoginRepository();

                var returnValue = repoLogin.UpdateUserProfileByUserID(mdlUsers);
            }
            return RedirectToAction("UserProfile", "Login");
        }
    }
}