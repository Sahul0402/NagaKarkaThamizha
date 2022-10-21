using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace KarkaThamizha.Common.Utility
{
    public class CustomException : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                filterContext.ExceptionHandled = true;

                var model = new HandleErrorInfo(filterContext.Exception, "Controller", "Action");

                ExceptionUtility log = new ExceptionUtility();
                log.ExceptionMessage = filterContext.Exception.Message;
                log.StackTrace = Convert.ToString(filterContext.Exception.StackTrace);
                //log.Controller = Convert.ToString(filterContext.RouteData.Values["controller"]);
                //log.Action = Convert.ToString(filterContext.RouteData.Values["action"]);

                log.WriteErroLog(log);

                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error",
                    ViewData = new ViewDataDictionary(model)
                };

                filterContext.ExceptionHandled = true;
                //filterContext.HttpContext.Response.Clear();
                //filterContext.HttpContext.Response.StatusCode = 500;

                //filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            }
        }
    }
}
