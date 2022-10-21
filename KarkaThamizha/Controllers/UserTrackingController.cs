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
    public class UserTrackingController : Controller
    {
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult SetGetPageViewCount(Int16 viewPageID, int? articleID)
        {
            try
            {
                UserTrackingModels mdlUserTrack = new UserTrackingModels();
                UserTrackingRepository repoUserTrack = new UserTrackingRepository();

                mdlUserTrack.UserID = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : 0;
                mdlUserTrack.MasterPageID = viewPageID;
                mdlUserTrack.ChildPageID = articleID ?? 0;
                mdlUserTrack.PageView = "Y";
                mdlUserTrack.PageViewCount = repoUserTrack.SetGetPageViewCount(mdlUserTrack);
                return Json(new { count = mdlUserTrack.PageViewCount }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { count = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult SetGetPageLikeCount(Int16 viewPageID, int? articleID)
        {
            try
            {
                UserTrackingModels mdlUserTrack = new UserTrackingModels();
                UserTrackingRepository repoUserTrack = new UserTrackingRepository();

                mdlUserTrack.UserID = Session["UserID"] != null ? Convert.ToInt32(Session["UserID"]) : 0;
                mdlUserTrack.MasterPageID = viewPageID;
                mdlUserTrack.ChildPageID = articleID ?? 0;
                mdlUserTrack.PageLike = "Y";
                mdlUserTrack.PageLikeCount = repoUserTrack.SetGetPageLikeCount(mdlUserTrack);
                return Json(new { count = mdlUserTrack.PageLikeCount }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { count = 0 }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}

//https://www.codeproject.com/Articles/7514/Page-Tracking-in-ASP-NET