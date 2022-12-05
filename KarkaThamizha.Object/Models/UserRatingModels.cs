using System;
using System.Collections.Generic;
using System.Text;

namespace KarkaThamizha.Object.Models
{
    public class UserRatingModels
    {
        public string Book { get; set; }
        public int MaxRating { get; set; }
        public int UserRatingID { get; set; }
        public int BookID { get; set; }
        public int UserID { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string BookName { get; set; }
        public string AvgUserRating { get; set; }

    }
}
