using KarkaThamizha.Common.Utility;
using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.CacheData;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarkaThamizha.Controllers
{
    [CustomErrorAttribute]
    public class BookPreReleaseController : Controller
    {
        #region BookPreRelease
        // GET: BookPreRelease
        [AcceptVerbs(HttpVerbs.Get)]
        [Route("BookPreRelease")]
        public ActionResult BookPreRelease()
        {
            return View("BookPreRelease");
        }

        [HandleError]
        [HttpGet]
        public ActionResult GetAllBookRelease(int? page)
        {
            int pageNumber = 1;
            int pageSize = 10;
            List<EventsModels> lstEvents = null;
            DataCaching caching = new DataCaching();

            lstEvents = caching.GetAllEvents().Where(x => x.EventsTypeID == Convert.ToInt16(EnumCode.EventsType.BookPreRelease)).OrderByDescending(o => o.EndDate).ToList();
            if (lstEvents != null && lstEvents.Count > 0)
            {
                pageNumber = (page ?? 1);
            }
            return PartialView("_BookPreRelease", lstEvents.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        [Route("PreRelease/{eventId}")]
        public ActionResult PreRelease(int eventId)
        {
            ViewBag.MetaKeywords = "Book Reivew,Tamil Book Reivew,Novel Review,Tamil Novel Review";
            ViewBag.MetaDescription = "Book Reivew,Tamil Book Reivew,Novel Review,Tamil Novel Review";

            EventsController ctrlEvents = new EventsController();
            EventsModels mdlEvents = null;
            mdlEvents = PreReleaseList().Where(x => x.EventID == eventId).FirstOrDefault();

            return View("PreRelease", mdlEvents);
        }

        [HttpGet]
        public List<EventsModels> PreReleaseList()
        {
            DataCaching caching = new DataCaching();
            List<EventsModels> mdlEvents = null;
            mdlEvents = caching.GetAllEvents().Where(x => x.EventsTypeID == Convert.ToInt16(EnumCode.EventsType.BookPreRelease)).OrderByDescending(o => o.EndDate).ToList();

            return mdlEvents;
        }
        #endregion

        #region Offer

        [AcceptVerbs(HttpVerbs.Get)]
        [Route("Offer")]
        public ActionResult Offer()
        {
            return View("Offer");
        }

        [HandleError]
        [HttpGet]
        public ActionResult GetAllBookOffer(int? page)
        {
            int pageNumber = 1;
            int pageSize = 10;
            List<EventsModels> lstEvents = null;
            DataCaching caching = new DataCaching();

            lstEvents = caching.GetAllEvents().Where(x => x.EventsTypeID == Convert.ToInt16(EnumCode.EventsType.SpecialOffer)).OrderByDescending(o => o.EndDate).ToList();
            if (lstEvents != null && lstEvents.Count > 0)
            {
                pageNumber = (page ?? 1);
            }
            return PartialView("_Offer", lstEvents.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public List<EventsModels> OfferList()
        {
            DataCaching caching = new DataCaching();
            List<EventsModels> mdlEvents = null;
            mdlEvents = caching.GetAllEvents().Where(x => x.EventsTypeID == Convert.ToInt16(EnumCode.EventsType.SpecialOffer)).OrderByDescending(o => o.EndDate).ToList();

            return mdlEvents;
        }

        [HttpGet]
        [Route("OfferBook/{eventId}")]
        public ActionResult OfferBook(int eventId)
        {
            ViewBag.MetaKeywords = "Book Reivew,Tamil Book Reivew,Novel Review,Tamil Novel Review";
            ViewBag.MetaDescription = "Book Reivew,Tamil Book Reivew,Novel Review,Tamil Novel Review";
            DataCaching caching = new DataCaching();
            EventsModels mdlEvents = null;
            mdlEvents = caching.GetAllEvents().Where(x => x.EventID == eventId).FirstOrDefault();

            return View("OfferBook", mdlEvents);
        }
        #endregion

        #region Clear Cache
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ClearEventsCache()
        {
            string actionName = "";
            string url = Request.UrlReferrer.AbsolutePath.ToString();
            System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_EVENTS);

            switch (url)
            {
                case "/Offer":
                    actionName = "Offer";
                    break;
                case "/BookPreRelease":
                    actionName = "BookPreRelease";
                    break;
                default:
                    actionName = "BookPreRelease";
                    break;
            }

            System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_EVENTS);
            return RedirectToAction(actionName, "BookPreRelease");
        }
        #endregion
    }
}