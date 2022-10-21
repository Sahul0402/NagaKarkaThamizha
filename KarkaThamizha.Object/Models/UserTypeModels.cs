using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.Mvc;

namespace KarkaThamizha.Object.Models
{
    public class UserTypeModels
    {
        [DisplayName("User Type")]
        public Byte UserTypeID { get; set; }
        public string UserType { get; set; }
        public string ShortCode { get; set; }
        public SelectList LstUserType { get; set; }
    }
}
