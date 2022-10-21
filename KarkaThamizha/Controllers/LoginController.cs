using KarkaThamizha.Common.Utility;
using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarkaThamizha.Controllers
{
    public class LoginController : Controller
    {        
        public ActionResult UserLogin(string email, string password)
        {
            LoginRepository repoLogin = null;
            LoginModels mdlUser = null;

            if (string.IsNullOrEmpty(email.Trim()) && string.IsNullOrEmpty(password.Trim()))
                return null;

            try
            {
                repoLogin = new LoginRepository();
                mdlUser = repoLogin.UserLogin(email, password);
                if (mdlUser !=null)
                {
                    Session["UserID"] = mdlUser.LoginID;
                    Session["Name"] = mdlUser.Name;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(mdlUser.LoginID, JsonRequestBehavior.AllowGet);
            //return Json(new { Message = message, JsonRequestBehavior.AllowGet });
        }

        public void Logout()
        {
            // Clear all session variables.
            //HttpContext.Current.Session.Clear();
            //HttpContext.Current.Session.Abandon();

            //contextInfo.CurrentUser = null;
            //UserState userState = new UserState();
            //userState.Logout();
        }


        public ActionResult Register()
        {
            return View();
        }

        public ActionResult UserProfile()
        {
            return View("UserProfile");
        }
    }
}