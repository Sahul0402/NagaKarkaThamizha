using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace KarkaThamizha.Object.Models
{
    public class BooksReviewModels
    {
        public int BooksReviewID { get; set; }        
        public int BookID { get; set; }
        public string BookName { get; set; }
        [AllowHtml]
        public string Header { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        [Display(Name = "Magazine")]
        public Int16 MagazineID { get; set; }
        public string MagazineName { get; set; }
        public SelectList LstMagazine { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime SourceDate { get; set; }
        public Int16 ParentID { get; set; }
        public Int16 BookCategoryID { get; set; }
        public string BookCategory { get; set; }
        public Byte UserType { get; set; }
        public UserModels Users { get; set; }
        public UserDetailsModels UserDetail { get; set; }
        public BooksDetailsModels BookDetail { get; set; }        
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public CategoryModels Category { get; set; }
    }
}
