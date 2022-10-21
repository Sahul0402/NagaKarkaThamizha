using KarkaThamizha.Common.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace KarkaThamizha
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            BundleTable.EnableOptimizations = true;
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Remove all Razor Engine
            ViewEngines.Engines.Clear();
            //Add Razor engine
            ViewEngines.Engines.Add(new CustomViewEngine());
        }

        public class CustomViewEngine : RazorViewEngine
        {
            public CustomViewEngine()
            {
                base.AreaViewLocationFormats = new string[] { "~/Areas/{2}/Views/{1}/{0}.cshtml", "~/Areas/{2}/Views/Shared/{0}.cshtml" };
                base.AreaMasterLocationFormats = new string[] { "~/Areas/{2}/Views/{1}/{0}.cshtml", "~/Areas/{2}/Views/Shared/{0}.cshtml" };
                base.AreaPartialViewLocationFormats = new string[] { "~/Areas/{2}/Views/{1}/{0}.cshtml", "~/Areas/{2}/Views/Shared/{0}.cshtml" };
                base.ViewLocationFormats = new string[] { "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml" };
                base.MasterLocationFormats = new string[] { "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml" };
                base.PartialViewLocationFormats = new string[] { "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml" };
                base.FileExtensions = new string[] { "cshtml" };
            }
        }

        public void Applicatoin_Error(object sender, EventArgs e)
        {
            Response.Filter = null;

            var exception = Server.GetLastError();
            if (exception == null)
                return;

            //string msg = Convert.ToString(exception.InnerException);
            string msg = exception.Message.ToString();
            if (!string.IsNullOrEmpty(msg))
            {
                //bool flag = ExceptionUtility.SendMail(Convert.ToString(exception.InnerException), "support@karkathamizha.com", "KarkaThamizha Error");
                ExceptionUtility.LogException(exception, null);

                // Clear the error from the server
                Server.ClearError();
                Response.Redirect("Error", true);
            }
        }

        protected void Application_BeginRequest()
        {
            CultureInfo cInf = new CultureInfo("en-IN", false);
            // NOTE: change the culture name en-ZA to whatever culture suits your needs

            cInf.DateTimeFormat.DateSeparator = "/";
            cInf.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";

            System.Threading.Thread.CurrentThread.CurrentCulture = cInf;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cInf;
        }

    }
}
