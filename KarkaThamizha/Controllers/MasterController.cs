﻿using KarkaThamizha.Repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static KarkaThamizha.Object.Models.MasterModels;

namespace KarkaThamizha.Controllers
{
    public class MasterController : Controller
    {
        // GET: Master
        public ActionResult GetLatestNews1()
        {
            List<BreakingNewsModels> news = new List<BreakingNewsModels>();
            MasterRepository repoMaster = new MasterRepository();
            news = repoMaster.GetLatestNews();
            return Json(news, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLatestNews()
        {
            List<BreakingNewsModels> news = new List<BreakingNewsModels>();
            MasterRepository repoMaster = new MasterRepository();
            news = repoMaster.GetLatestNews();
            return PartialView("_LatestNews", news);
        }

        
    }
}