using KarkaThamizha.Object.Models;
using KarkaThamizha.Repository.CacheData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarkaThamizha.Admin.Controllers
{
    public class UserTypeController : Controller
    {
        public UserTypeModels GetUserType()
        {
            DataCaching caching = new DataCaching();
            UserTypeModels mdlUserType = new UserTypeModels();

            List<UserTypeModels> lstUserType = caching.GetAllUserType().OrderBy(x => x.UserType).ToList();            
            mdlUserType.LstUserType = new SelectList(lstUserType.AsEnumerable(), "UserTypeID", "UserType", 0);
            return mdlUserType;
        }
    }
}