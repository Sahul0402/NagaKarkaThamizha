using System;
using System.Collections.Generic;
using System.Text;

namespace KarkaThamizha.Object.Models
{
    public class FeedbackModels
    {
        public int FeedbackID { get; set; }
        public int UserId { get; set; }
        public string Feedback { get; set; }
        public Int16 ProjectID { get; set; }
        public Int16 MasterPageID { get; set; }
        public int ChildPageID { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
