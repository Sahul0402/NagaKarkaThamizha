using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace KarkaThamizha.Object.Models
{
    public class BooksModels
    {
        public int BookID { get; set; }

        [DisplayName("Book Name")]        
        [StringLength(100, ErrorMessage = "Can not insert more than 100 characters.")]
        
        public string Book { get; set; }

        [DataType(DataType.MultilineText)]
        public string BookDescription { get; set; }

        [StringLength(60, ErrorMessage = "Can not insert more than 60 characters.")]
        public string Name { get; set; }

        [StringLength(60, ErrorMessage = "Can not insert more than 60 characters.")]
        public string Tanglish { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [DisplayName("Created")]        
        public DateTime CreatedDate { get; set; }
        public UserModels Users { get; set; }
        public BooksDetailsModels BookDetails { get; set; }
        public CategoryModels Categories { get; set; }
        public string Status { get; set; }
        public CategoryModels Category { get; set; }
        public List<CategoryModels> lstSubCategories { get; set; }
        public string FileUploadType { get; set; }
    }
}
