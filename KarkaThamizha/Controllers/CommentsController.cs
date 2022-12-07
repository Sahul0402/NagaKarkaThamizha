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
    public class CommentsController : KarkaThamizhaBaseController
    {
        [OutputCache(Duration = 43200, VaryByParam = "none")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Comments()
        {
            return View("Comments");
        }

        public JsonResult AddComments(CommentsModels mdlComments)
        {
            string message = "";
            CommentsRepository repoComments = null;

            if (ModelState.IsValid)
            {
                repoComments = new CommentsRepository();

                if (Session["UserID"] != null)
                    mdlComments.UserID = Convert.ToInt32(Session["UserID"]);
                else
                    mdlComments.UserID = 0;

                mdlComments.Name = StringExtension.TitleCase(mdlComments.Name);
                mdlComments.ProjectID = (Int16)EnumCode.Projects.KarkaThamizha;

                message = repoComments.AddComments(mdlComments);                
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}