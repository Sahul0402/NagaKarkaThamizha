using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace KarkaThamizha.Object.Models
{
    public class MasterModels
    {
        public class CountryModels
        {
            public Byte CountryID { get; set; }
            public string Country { get; set; }
            public SelectList LstCountry { get; set; }
        }

        public class StateModels
        {
            public Int16 StateID { get; set; }
            public string State { get; set; }
            public SelectList LstState { get; set; }
        }

        public class CityModels
        {
            public Int16 CityID { get; set; }

            [Remote("IsCityExists", "City", HttpMethod = "POST", ErrorMessage = "City already exists")]
            public string City { get; set; }
            public SelectList LstCity { get; set; }
        }

        public class SpecialNameModels
        {
            [Display(Name = "Special Name")]
            public Byte? SpecialNameID { get; set; }
            public string SpecialName { get; set; }
            public SelectList lstSpecialName { get; set; }
        }

        public class ProfessionModels
        {
            //[Display(Name = "Profession")]
            public Byte ProfessionID { get; set; }
            public string Profession { get; set; }
            public SelectList lstProfession { get; set; }
        }

        public class CacheModels
        {
            public Byte CacheID { get; set; }
            public string Code { get; set; }
            public string CacheName { get; set; }
        }

        public class SeriesTypeModels
        {
            [Display(Name = "Series Type")]
            public Byte SeriesTypeID { get; set; }
            public string SeriesType { get; set; }
            public SelectList SelectLstSeriesType { get; set; }
        }

        public class ToDoModels
        {
            public int ToDoListID { get; set; }
            public string Description { get; set; }
            public DateTime CreatedDate { get; set; }
            public string TimeInterval { get; set; }
        }

        public class BreakingNewsModels
        {
            public int BreakingNewsID { get; set; }
            public string Description { get; set; }
            public DateTime CreatedDate { get; set; }
            public string Type { get; set; }
            public List<BreakingNewsModels> NewsList { get; set; }
            public UserModels UserList { get; set; }
            public int UserID { get; set; }
            public string UserName { get; set; }
        }
    }
}
