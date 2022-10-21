    using KarkaThamizha.Common.Utility;
using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.CacheData;
using KarkaThamizha.Repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static KarkaThamizha.Object.Models.MasterModels;
using log4net;


namespace KarkaThamizha.Admin.Controllers
{
    //[CustomException]
    public class HomeController : Controller
    {   
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {   
            if (Session["UserID"] == null)
                return RedirectToAction(FormsAuthentication.LoginUrl);

            AdminModels mdlHome = new AdminModels();
            DataCaching cache = new DataCaching();
            mdlHome.AuthorCount = cache.GetAllUsers().Where(x => x.UserType.UserTypeID == (Byte)(EnumCode.UserType.Author)).Count();
            mdlHome.UserCount = cache.GetAllUsers().Where(x => x.UserType.UserTypeID == (Byte)(EnumCode.UserType.User)).Count();
            mdlHome.BookCount = cache.GetBooksList().ToList().Count();
            return View("Index", mdlHome);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Dashboard()
        {
            AdminModels mdlHome = new AdminModels();
            return View("Dashboard", mdlHome);
        }        
    }
}