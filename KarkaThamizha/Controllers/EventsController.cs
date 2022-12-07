using KarkaThamizha.Common.Utility;
using KarkaThamizha.Controller;
using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.CacheData;
using KarkaThamizha.Repository.DAL;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarkaThamizha.Controllers
{
    [RoutePrefix("Events")]
    [CustomErrorAttribute]
    public class EventsController : KarkaThamizhaBaseController
    {
        [AcceptVerbs(HttpVerbs.Get)]        
        [Route("Events/{Page}")]
        public ActionResult Events(string Page)
        {
            ViewData["EventsType"] = "";
            ViewData["hdnPage"] = "";

            if (Page == "BookFair")
            {
                ViewData["EventsType"] = "புத்தகக் கண்காட்சிகள்";
                ViewData["hdnPage"] = (int)EnumCode.Pages.BookFair;
            }
            else if (Page == "BookRelease")
            {
                ViewData["EventsType"] = "நூல் வெளியீட்டு விழா";
                ViewData["hdnPage"] = (Int16)EnumCode.Pages.BookRelease;
            }
            else if (Page == "Award")
            {
                ViewData["EventsType"] = "விருது வழங்கும் விழா";
                ViewData["hdnPage"] = (Int16)EnumCode.Pages.Award;
            }
            return View();
        }

        [HandleError]        
        public ActionResult ShowEventsType(int? pagesize, Int16 viewpage)
        {
            int pageNumber = 1;
            int pageSize = 10;
            List<EventsModels> lstEvents = null;
            DataCaching caching = null;

            if (viewpage > 0)
            {
                caching = new DataCaching();
                if (viewpage == (int)EnumCode.Pages.BookFair)
                {
                    lstEvents = caching.GetAllEvents().Where(x => x.EventsTypeID.Equals(Convert.ToInt16(EnumCode.EventsType.BookFair))).ToList();
                }
                else if (viewpage == ((Int16)EnumCode.Pages.BookRelease))
                {
                    lstEvents = caching.GetAllEvents().Where(x => x.EventsTypeID.Equals(Convert.ToInt16(EnumCode.EventsType.BookRelease))).ToList();
                }
                else if (viewpage == ((Int16)EnumCode.Pages.Award))
                {
                    lstEvents = caching.GetAllEvents().Where(x => x.EventsTypeID.Equals(Convert.ToInt16(EnumCode.EventsType.AwardCeremony))).ToList();
                }
                else if (viewpage == ((Int16)EnumCode.Pages.BookPreRelease))
                {
                    lstEvents = caching.GetAllEvents().Where(x => x.EventsTypeID.Equals(Convert.ToInt16(EnumCode.EventsType.BookPreRelease))).ToList();
                }
            }
            if (lstEvents != null && lstEvents.Count > 0)
            {
                pageNumber = (pagesize ?? 1);
                pageSize = 10;
            }
            return PartialView("_Events", lstEvents.ToPagedList(pageNumber, pageSize));
        }

        //This method is used due to not able to pass dynamic action name(BookFair/Award) to EventsList.cshtml
        [HttpGet]
        [Route("EventName/{eventId:int?}")]
        public ActionResult EventName(int? eventId)
        {
            EventsModels mdlEvent = new EventsModels();
            DataCaching caching = new DataCaching();

            if (eventId != null && eventId.Value > 0)
                mdlEvent = caching.GetAllEvents().Where(x => x.EventID == eventId.Value).FirstOrDefault();
            return View("ShowEvents", mdlEvent);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public EventsModels GetEventsList()
        {
            EventsModels mdlEvents = new EventsModels();
            List<UserDetailsModels> mdlUserDetails = new List<UserDetailsModels>();
            UserDetailsRepository repoUserDetails = new UserDetailsRepository();
            List<EventsModels> lstEvents = null;
            DataCaching caching = new DataCaching();

            lstEvents = caching.GetAllEvents().OrderByDescending(x => x.EventsDate).ToList();
            mdlEvents.EventList = lstEvents;
            string arr = string.Join(",", lstEvents.Select(x => x.UserList.UserID).Distinct());
            mdlUserDetails = repoUserDetails.GetUsersDetails(arr);
            mdlEvents.UserDetails = mdlUserDetails;
            
            return mdlEvents;
        }        

        [AcceptVerbs(HttpVerbs.Get)]
        public EventsModels ViewEvents(string header)
        {
            EventsModels mdlEvent = new EventsModels();
            DataCaching caching = new DataCaching();
            if (!string.IsNullOrEmpty(header))
            {
                mdlEvent = caching.GetAllEvents().Where(x => x.Header.Equals(header.Trim())).FirstOrDefault();
            }
            return mdlEvent;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ShowEvents(int eventId)
        {
            ViewBag.MetaKeywords = "Book Reivew,Tamil Book Reivew,Novel Review,Tamil Novel Review";
            ViewBag.MetaDescription = "Book Reivew,Tamil Book Reivew,Novel Review,Tamil Novel Review";
            EventsModels mdlEvent = new EventsModels();
            DataCaching caching = new DataCaching();

            mdlEvent = caching.GetAllEvents().Where(x => x.EventID == eventId).FirstOrDefault();
            return View("ShowEvents", mdlEvent);
        }

        [HttpGet]
        public ActionResult BookRelease(int eventId)
        {
            ViewBag.MetaKeywords = "Book Reivew,Tamil Book Reivew,Novel Review,Tamil Novel Review";
            ViewBag.MetaDescription = "Book Reivew,Tamil Book Reivew,Novel Review,Tamil Novel Review";
            EventsModels mdlEvent = new EventsModels();
            DataCaching caching = new DataCaching();

            mdlEvent = caching.GetAllEvents().Where(x => x.EventID == eventId).FirstOrDefault();
            return View("ShowEvents", mdlEvent);
        }

        [HttpGet]
        public ActionResult Award(int eventId)
        {
            ViewBag.MetaKeywords = "விருது வழங்கும் விழா,Award";
            ViewBag.MetaDescription = "Book Reivew,Tamil Book Reivew,Novel Review,Tamil Novel Review";
            EventsModels mdlEvent = new EventsModels();
            DataCaching caching = new DataCaching();

            mdlEvent = caching.GetAllEvents().Where(x => x.EventID == eventId).FirstOrDefault();
            return View("ShowEvents", mdlEvent);
        }

        public ActionResult BookFair(int eventId)
        {
            ViewBag.MetaKeywords = "புத்தகக் கண்காட்சி,Book Fair";
            ViewBag.MetaDescription = ",BookFair in Chennai,Chennai Book Fair";
            EventsModels mdlEvent = new EventsModels();
            DataCaching caching = new DataCaching();

            mdlEvent = caching.GetAllEvents().Where(x => x.EventID == eventId).FirstOrDefault();
            return View("ShowEvents", mdlEvent);
        }

        public ActionResult EventsCalendar()
        {
            return View("EventsCalendar");
        }

        public JsonResult LiteratureEvents()
        {
            DataCaching caching = new DataCaching();
            var events = caching.GetAllEvents().Take(10).ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        #region Clear Cache
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ClearEventsCache()
        {
            string rediectPage = "";
            string url = Request.UrlReferrer.Query.ToString();
            System.Web.HttpContext.Current.Cache.Remove(CacheConstants.CACHE_ALL_EVENTS);

            switch (url)
            {
                case "?Page=BookFair":
                    rediectPage = "BookFair";
                    break;
                case "?Page=BookRelease":
                    rediectPage = "BookRelease";
                    break;
                case "?Page=Award":
                    rediectPage = "Award";
                    break;
                default:
                    rediectPage = "BookRelease";
                    break;
            }
                
            return RedirectToAction("Events", "Events", new { Page = rediectPage });
        }
        #endregion
    }
}