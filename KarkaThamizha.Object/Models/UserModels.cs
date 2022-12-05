using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace KarkaThamizha.Object.Models
{
    public class UserModels
    {
        public int UserID { get; set; }
        public string Initial { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Name in English is required.")]
        [DisplayName("English")]
        public string Name { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [MaxLength(12), MinLength(8)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Mobile is required.")]
        public string Mobile { get; set; }
        public string UserCategory { get; set; }
        [Display(Name = "Author Name")]
        public int[] AuthorIds { get; set; }
        public int AuthorID { get; set; }
        public List<SelectListItem> lstAuthors { get; set; }
        public IEnumerable<SelectListItem> Authors { get; set; }
        public SelectList lstUsers { get; set; }
        public Int16 ProfessionID { get; set; }
        public MasterModels.ProfessionModels Profession { get; set; }
        public UserTypeModels UserType { get; set; }
        public MasterModels.SpecialNameModels SpecialName { get; set; }
        public string Header { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string ReviewedBy { get; set; }
        
        public Nullable<DateTime> SourceDate { get; set; }
        public MasterModels.CountryModels Country { get; set; }
        public MasterModels.StateModels State { get; set; }
        public MasterModels.CityModels City { get; set; }
        public List<int> SelectedMultiAuthorId { get; set; }
        // Gets or sets choose multiple Authors property.                
        [Display(Name = "Text Writer Name")]
        public List<int> TextWriterAuthorId { get; set; }
        [Display(Name = "Collect Name")]
        public List<int> CollectAuthorId { get; set; }
        [Display(Name = "Editor Name")]
        public List<int> EditorAuthorId { get; set; }
        [Display(Name = "Translator Name")]
        public List<int> TranslatorAuthorId { get; set; }
        // Gets or sets selected Author property.
        public List<UserModels> SelectedAuthorsLst { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Reference { get; set; }
        public UserDetailsModels UserDetail { get; set; }

    }
}
