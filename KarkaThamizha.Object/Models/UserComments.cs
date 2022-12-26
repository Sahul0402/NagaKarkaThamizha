using System;
using System.Collections.Generic;
using System.Text;

namespace KarkaThamizha.Object.Models
{
    public class UserComments
    {
        public UserComments() {
            this.attachments = new List<attachments>();
            this.pings = new List<Pings>();
            //this.modified = DateTime.Now;
        }
        public int id { get; set; }
        public string parent { get; set; }
        public string created { get; set; }
        public string modified { get; set; }
        public string content { get; set; }
        public  List<attachments> attachments { get; set; }
        public List<Pings> pings { get; set; }
        public int creator { get; set; }
        public string fullname { get; set; }
        public string profile_picture_url { get; set; }
        public bool created_by_admin { get; set; }
        public bool created_by_current_user { get; set; }
        public int upvote_count { get; set; }
        public bool user_has_upvoted { get; set; }
        public bool is_new { get; set; }
        public int BookId { get; set; }
        public int AuthorId { get; set; }
    }

    public class Pings
    {
        public int id { get; set; }
        public string userName { get; set; }
    }
    public class attachments { 
        public int id { get; set; }
        public string file { get; set; }
        public string mime_type { get; set; }
    }
}
