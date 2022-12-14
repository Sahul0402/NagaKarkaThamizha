using System;
using System.ComponentModel.DataAnnotations;

namespace KarkaThamizha.Object.Models
{
    public class FeedbackModels
    {
        public int FeedbackId { get; set; }

        public int UserId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Name")]
        [MaxLength(35), MinLength(3)]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only Alphabets allowed.")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter Email ID"), MaxLength(40)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "Email is not valid")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "Please enter Mobile.")]
        [MaxLength(20), MinLength(10)]
        public string Mobile { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter your Comments.")]
        public string Feedback { get; set; }
        public Byte ProjectID { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
