using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace KarkaThamizha.Object.Models
{
    public class BookFormatModels
    {
        public Byte BookFormatID { get; set; }
        public string BookFormat { get; set; }
        public SelectList lstBookFormat { get; set; }
    }
}
