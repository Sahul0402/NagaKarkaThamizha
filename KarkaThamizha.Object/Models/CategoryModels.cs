using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace KarkaThamizha.Object.Models
{
    public class CategoryModels
    {
        public Int16 CategoryID { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }    
        public List<SelectListItem> lstCategories { get; set; }
        public Int16 ParentID { get; set; }
        public bool ShowInMenu { get; set; }
        public List<CategoryModels> CategoryModelsList { get; set; }
        public CategoryModels Categories { get; set; }
        public List<SelectListItem> sliCategory { get; set; }
        public List<int> SelectedMultiCategoryId { get; set; }
        public int SubCategoryID { get; set; }
        public bool IsChecked { get; set; }
        
        public CategoryModels()
        {
            this.sliCategory = new List<SelectListItem>();
        }
    }
}
