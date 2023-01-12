using KarkaThamizha.Controller;
using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.CacheData;
using KarkaThamizha.Repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarkaThamizha.Controllers
{
    public class UserAccountController : KarkaThamizhaBaseController
    {
        #region User Account Information

        [AllowAnonymous]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult IsMailExists(string mailId)
        {
            UserRepository repoUser = new UserRepository();
            bool flag = repoUser.CheckEmailExists(mailId);
            return Json(new { message = flag }, JsonRequestBehavior.AllowGet);
        }

        #region Update User Profile
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

        public IEnumerable<SelectListItem> GetAuthorList()
        {
            DataCaching caching = new DataCaching();
            SelectList lstObj = null;
            var list = caching.GetUserByAuthorType().Select(p => new SelectListItem { Value = p.UserID.ToString(), Text = p.UserName }).OrderBy(x => x.Text).ToList();

            //setting
            lstObj = new SelectList(list, "Value", "Text");
            return lstObj;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UserProfile(UserModels mdlUsers)
        {
            UserRepository repoUser = null;
            if (Session["UserID"] != null)
            {
                int userID = Convert.ToInt32(Session["UserID"]);
                repoUser = new UserRepository();
                mdlUsers.UserID = userID;
                var returnValue = "Success";//repoUser.UpdateUser(mdlUsers);

                if (returnValue == "Success")
                {
                    
                }
            }
            return RedirectToAction("UserProfile", "UserAccount");
        }
        
        #endregion

        [AcceptVerbs(HttpVerbs.Post)]
        #region New User Registration

        public ActionResult UserRegister(UserModels mdlUsers)
        {
            UserRepository repoUser = null;
            mdlUsers.Profession = new MasterModels.ProfessionModels();
            mdlUsers.UserType = new UserTypeModels();
            if (mdlUsers != null)
            {
                repoUser = new UserRepository();

                //Set default value else error through in SP
                mdlUsers.Profession.ProfessionID = 16;
                mdlUsers.UserType.UserTypeID = 6;
                mdlUsers.UserName = "";
                var returnValue = repoUser.AddUser(mdlUsers);

                if (returnValue > 0)
                {

                }
            }
            return RedirectToAction("UserProfile", "UserAccount");
        }
        #endregion

        #region ChangePassword
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ChangePassword()
        {
            return View("ChangePassword");
        }

        public ActionResult ChangePassword(string password, string confirmpwd)
        {
            string message = "";
            if (Session["UserId"] != null)
            {
                int loginId = Convert.ToInt32(Session["UserId"]);
                LoginRepository repoLogin = new LoginRepository();
                message = repoLogin.ChangePassword(loginId, password);
            }
            else
            {
                ClearSession();
                message = "Expired";
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region LogOut
        public ActionResult Logout()
        {
            //Clear all session variables.
            return ClearSession();
        }

        private ActionResult ClearSession()
        {
            Session.Remove("UserID");
            Session.Remove("Name");
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        #endregion 
        #endregion
    }
}