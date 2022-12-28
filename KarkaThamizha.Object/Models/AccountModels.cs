using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KarkaThamizha.Object.Models
{
    public class AccountModels
    {
        public Int32 LoginID { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Name")]
        [MaxLength(35), MinLength(3)]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only Alphabets allowed.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter Mobile.")]
        [MaxLength(10), MinLength(10)]
        public string Mobile { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter Email ID"), MaxLength(35)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "Email is not valid")]
        public string EMail { get; set; }

        [DataType(DataType.Password)]
        [MaxLength(12), MinLength(8)]
        [Required(ErrorMessage = "Please enter Password.")]
        public string Password { get; set; }

        [DisplayName("Remember me")]
        public bool Rememberme { get; set; }

        public DateTime DOB { get; set; }
        public string Profile { get; set; }

        #region Change Password
        public class ChangePasswordModels
        {
            [Required]
            [DataType(DataType.Password)]
            public string CurrentPassword { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [MaxLength(12), MinLength(8)]
            [StringLength(12, ErrorMessage = "{0} cannot be longer than {1} characters and less than {8} characters", MinimumLength = 6)]
            public string NewPassword { get; set; }
        }
        #endregion

        public class ForgetPasswordModels
        {
            public bool EMail { get; set; }
        }

        public class LoginStatus
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public string TargetURL { get; set; }
        }
    }
}
