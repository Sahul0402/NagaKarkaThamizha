using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace KarkaThamizha.Object.Models
{
    public class UserDetailsModels
    {
        public int UserDetailsID { get; set; }
        public int UserID { get; set; }
        [AllowHtml]
        public string Profile { get; set; }
        public string Protocol { get; set; }
        public string Website { get; set; }
        public string Blog { get; set; }
        public string BlogType { get; set; }
        public string FaceBook { get; set; }
        public string Twitter { get; set; }
        public string YouTube { get; set; }
        public string Pinterest { get; set; }
        public string Instagram { get; set; }
        public string Wikipedia { get; set; }
        public string Address { get; set; }
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> DOB { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> DOD { get; set; }
        public bool OldAuthorDOD { get; set; }
        public string WriterImage { get; set; }
        public string ImgProfile { get; set; }
        public string ImgComments { get; set; }
        public Int16 CountryID { get; set; }
        public Int32 StateID { get; set; }
        public Int32 CityID { get; set; }
        public string OtherImage { get; set; }
        public bool IsShownInMenu { get; set; }
        public bool IsMailSubscription { get; set; }
        public string Reference { get; set; }
    }
}
