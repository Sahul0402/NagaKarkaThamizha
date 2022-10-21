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
    public class CityController : Controller
    {
        public ActionResult FillCities(int stateid)
        {
            List<MasterModels.CityModels> lstCities = GetCity(stateid);
            var citylist = lstCities.Select(c => new { c.CityID, c.City });
            return Json(citylist, JsonRequestBehavior.AllowGet);
        }

        public List<MasterModels.CityModels> GetCity(int stateid)
        {
            MasterRepository city = new MasterRepository();
            List<MasterModels.CityModels> lstCity = city.GetCityByStateID(stateid);
            ViewBag.City = lstCity;
            return lstCity;
        }

        [AllowAnonymous]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult IsCityExists(string cityName)
        {
            List<MasterModels.CityModels> lstCities = GetCity(31);
            bool IsCityExists = lstCities.Exists(x => x.City.ToLower().Contains(cityName.ToLower()));
            return Json(new { message = IsCityExists }, JsonRequestBehavior.AllowGet);
        }
    }
}