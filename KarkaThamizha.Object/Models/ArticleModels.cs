using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace KarkaThamizha.Object.Models
{
    public class ArticleModels
    {
        public int ArticleID { get; set; }
        public string Header { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string ImgPath { get; set; }
        public Nullable<DateTime> SourceDate { get; set; }
        public Nullable<DateTime> EndDate { get; set; }
        //[Display(Name = "Week Days")]
        //public Byte? WeekDaysID { get; set; }
        //public Byte SeriesTypeID { get; set; }        
        public MasterModels.SeriesTypeModels SeriesType { get; set; }        
        //public WeekDaysModels WeekDays { get; set; }
        [Display(Name = "Interview By")]
        public Int32? InterviewBy { get; set; }
        [Display(Name = "Admin User")]
        public Int32? BookDetailsID { get; set; }
        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }
        public SelectList SelectLstArticle { get; set; }        
        public ArticleTypeModels ArticleType { get; set; }
        public UserModels Author { get; set; }
        public UserDetailsModels UserDetail { get; set; }
        //public MagazineModels MagazineName { get; set; }
        public List<BooksModels> LstBooks { get; set; }
        public List<ArticleModels> LstArticle { get; set; }
        //public List<SeriesModels> LstAuthorSeries { get; set; }
    }
}
