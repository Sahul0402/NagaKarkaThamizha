using KarkaThamizha.Common.Utility;
using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarkaThamizha.Admin.Controllers
{
    [CustomException]
    public class UserDetailsController : Controller
    {
        public string AddUserDetails(UserDetailsModels userDetails)
        {
            string message = "";
            UserDetailsRepository repoUserDetail = new UserDetailsRepository();
            message = repoUserDetail.AddUserDetails(userDetails);
            return message;
        }

        public UserDetailsModels GetUsersDetailsByUserId(int userId)
        {
            UserDetailsModels details = new UserDetailsModels();
            UserDetailsRepository repoUserDetail = new UserDetailsRepository();
            details = repoUserDetail.GetUsersDetailsByUserId(userId);
            return details;
        }
    }
}