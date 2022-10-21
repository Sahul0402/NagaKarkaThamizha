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
    public class StateController : Controller
    {
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult FillStates(int countryId)
        {
            try
            {
                List<MasterModels.StateModels> lstStates = new List<MasterModels.StateModels>();
                MasterRepository state = new MasterRepository();
                lstStates = state.GetStatesByCountryID(countryId);
                return Json(lstStates, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            { throw; }
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public List<MasterModels.StateModels> GetStates(int countryId)
        {
            List<MasterModels.StateModels> lstStates = new List<MasterModels.StateModels>();
            MasterRepository state = new MasterRepository();
            lstStates = state.GetStatesByCountryID(countryId);
            return lstStates;
        }
    }
}