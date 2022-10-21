using System;
using System.Collections.Generic;
using System.Text;

namespace KarkaThamizha.Object.Models
{
    public class UserTrackingModels
    {
        public int PageViewCount { get; set; }
        public int PageLikeCount { get; set; }
        public int UserID { get; set; }
        public Int16 MasterPageID { get; set; }
        public int? ChildPageID { get; set; }
        public string PageView { get; set; }
        public string PageLike { get; set; }

    }
}
