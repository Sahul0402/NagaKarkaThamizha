using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace KarkaThamizha.Object.Models
{
    public class ArticleTypeModels
    {
        public Byte ArticleTypeID { get; set; }
        public string ArticleType { get; set; }
        public SelectList LstArticleType { get; set; }
    }
}
