using KarkaThamizha.Common.Utility;
using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.WebPages;

namespace KarkaThamizha.Admin.Controllers
{
    //[CustomException]
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            LoginModels mdlLogin = new LoginModels();

            if (Session["UserName"] == null)
            {
                return View("Login");
            }
            else
                return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(LoginModels mdlLogin)
        {
            LoginRepository repoLogin = new LoginRepository();
            if (string.IsNullOrWhiteSpace(mdlLogin.Password) || string.IsNullOrWhiteSpace(mdlLogin.Name))
                return View("Login");

            try
            {                
                int id = repoLogin.CheckUserExists(mdlLogin);
                LoginStatus status = new LoginStatus();

                switch (id)
                {
                    case 1:
                        FormsAuthentication.SetAuthCookie(mdlLogin.Name, mdlLogin.Rememberme);
                        status.Success = true;
                        status.TargetURL = FormsAuthentication.GetRedirectUrl(mdlLogin.Name, mdlLogin.Rememberme);
                        Session["UserID"] = id;
                        Session["UserName"] = StringExtension.TitleCase(mdlLogin.Name);
                        if (string.IsNullOrWhiteSpace(status.TargetURL))
                        {
                            status.TargetURL = FormsAuthentication.DefaultUrl;
                        }
                        return RedirectToAction("Index", "Home");

                    case 2:
                        status.Success = false;
                        status.Message = "Invalid Username and/or password.";
                        status.TargetURL = FormsAuthentication.LoginUrl;
                        return RedirectToAction(status.TargetURL);
                    case 3:
                        status.Message = "Failed";
                        return RedirectToAction(FormsAuthentication.LoginUrl);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction(FormsAuthentication.LoginUrl);
            }
            return View(FormsAuthentication.LoginUrl);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            return RedirectToAction("Login");
        }
    }
}