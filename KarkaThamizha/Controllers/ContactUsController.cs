using KarkaThamizha.Common.Utility;
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
    [CustomErrorAttribute]
    public class ContactUsController : KarkaThamizhaBaseController
    {
        // GET: ContactUs
        [OutputCache(Duration = 43200, VaryByParam = "none")]
        [AcceptVerbs(HttpVerbs.Get)]
        
        public ActionResult ContactUs()
        {
            return View("ContactUs");
        }

        [OutputCache(Duration = 43200, VaryByParam = "none")]
        [AcceptVerbs(HttpVerbs.Get)]
        
        public ActionResult Feedback()
        {
            return View("Feedback");
        }

        public JsonResult Feedback(ContactUsModels mdlContact)
        {
            int userid = 0;
            string redirectUrl = "";
            bool isRedirect = false;
            ContactUsRepository ctrlContact = null;
            try
            {
                if (Session["UserID"] != null)
                {
                    ctrlContact = new ContactUsRepository();
                    mdlContact.ProjectID = (Byte)EnumCode.Projects.KarkaThamizha;

                    //userid = ctrlContact.Add(mdlContact);
                    if (userid > 0)
                    {
                        redirectUrl = Url.Action("UserRegistration", "User");
                        isRedirect = true;
                    }
                }
                else if (Session["UserID"] == null)
                {
                    if (mdlContact != null)
                    {
                        mdlContact.ProjectID = (Byte)EnumCode.Projects.KarkaThamizha;
                        ctrlContact = new ContactUsRepository();
                        //userid = ctrlContact.Add(mdlContact);
                        if (userid > 0)
                        {
                            Session["UserID"] = userid;
                            redirectUrl = Url.Action("UserRegistration", "User", new { @userID = userid });
                            isRedirect = true;
                        }
                    }
                    else
                    {
                        redirectUrl = "";
                        isRedirect = false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Json(new { Redirect = isRedirect, PageURL = redirectUrl }, JsonRequestBehavior.AllowGet);
        }
    }
}