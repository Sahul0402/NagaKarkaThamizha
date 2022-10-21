using KarkaThamizha.Common.Utility;
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
    [CustomErrorAttribute]
    public class ArticleController : Controller
    {
        [AcceptVerbs(HttpVerbs.Get)]
        public List<ArticleModels> GetGeneralArticles()
        {
            DataCaching caching = new DataCaching();
            List<ArticleModels> articles = caching.GetGeneralArticles();
            return articles;
        }
    }
}