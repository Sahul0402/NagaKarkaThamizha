using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace KarkaThamizha.Object.Models
{
    public class EventsModels
    {
        public string alertMsg { get; set; }
        public int EventID { get; set; }
        [Display(Name = "Author")]
        public int? AuthorID { get; set; }
        
        [Required(ErrorMessage = "Header is required.")]
        public string Header { get; set; }
        [AllowHtml]
        public string Description { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image")]
        public string ImgPath { get; set; }
        [Required(ErrorMessage = "Event Date is required.")]
        [Display(Name = "Event/Start Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EventsDate { get; set; }
        public string EventsType { get; set; }

        public Int16 EventsTypeID { get; set; }
        public string VideoURL { get; set; }
        public SelectList EventsTypeList { get; set; }
        public UserModels UserList { get; set; }
        public UserDetailsModels UserDetailList { get; set; }
        //public PublishersModels PublisherList { get; set; }
        public List<UserDetailsModels> UserDetails { get; set; }
        public List<EventsModels> EventList { get; set; }
        //For Book Fair
        public Int16? CityId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> EndDate { get; set; }
    }
}
