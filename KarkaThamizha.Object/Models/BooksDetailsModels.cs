using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace KarkaThamizha.Object.Models
{
    public class BooksDetailsModels
    {
        public int BookDetailsID { get; set; }
        public BooksModels Books { get; set; }
        public string BookCode { get; set; }
        [Required]
        public Decimal Price { get; set; }
        [Required]
        public int Pages { get; set; }
        public Int16 PublisherID { get; set; }
        //public PublishersModels Publisher { get; set; }
        public System.Web.Mvc.SelectList PublisherList { get; set; }
        [Required]
        [DefaultValue(1)]
        public Byte NoofCopy { get; set; }
        [DisplayName("Book Format")]
        public Int16 BookFormatID { get; set; }

        [DisplayName("Small Image- FS")]
        public string ImgBookSmallFS { get; set; }

        [DisplayName("Small Image- BS")]
        public string ImgBookSmallBS { get; set; }

        [DisplayName("Large Image")]
        public string ImgBookLarge { get; set; }

        public string ISBN13 { get; set; }
        public string FirstEdition { get; set; }
        public string CurrentEdition { get; set; }
        public string Dimensions { get; set; }
        public UserModels Users { get; set; }
        public bool IsKarkaBook { get; set; }
        public BookFormatModels BookFormat { get; set; }
        public CategoryModels Category { get; set; }
        public BooksReviewModels BooksReview { get; set; }
        public int BooksReviewID { get; set; }
        public string CategoryName { get; set; }
        public Int16 CategoryID { get; set; }
        public List<BooksDetailsModels> authorBooks { get; set; }
        public List<ContactUsModels> Feedback { get; set; }
        public Byte UserTypeID { get; set; }
    }
}
