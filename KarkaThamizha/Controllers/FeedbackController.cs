using KarkaThamizha.Common.Utility;
using KarkaThamizha.Controller;
using KarkaThamizha.Object.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarkaThamizha.Controllers
{
    public class FeedbackController : KarkaThamizhaBaseController
    {
        // GET: Feedback
        public ActionResult Add(FeedbackModels mdlFeedback)
        {
            if (Session["UserID"] != null)
            {
                mdlFeedback.ProjectID = (Byte)EnumCode.Projects.KarkaThamizha;


            }
            return View();
        }

        public ActionResult GetFeedbackByPageID(Int16 MasterPageID, int ChildPageID)
        {
            return View();
        }
    }
}