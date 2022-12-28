using KarkaThamizha.Controller;
using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarkaThamizha.Controllers
{
    public class AccountController : KarkaThamizhaBaseController
    {
        public PartialViewResult LoginPartial()
        {
            return PartialView();
        }
        public ActionResult UserLogin(string email, string password, bool isRating = false)
        {
            UserRepository repoLogin = null;
            AccountModels mdlUser = null;
            string message = "";
            Session["isRating"] = isRating;
            if (string.IsNullOrEmpty(email.Trim()) && string.IsNullOrEmpty(password.Trim()))
                return null;

            try
            {
                repoLogin = new UserRepository();
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

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Register()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ForgetPassword()
        {
            return View("ForgetPassword");
        }
    }
}