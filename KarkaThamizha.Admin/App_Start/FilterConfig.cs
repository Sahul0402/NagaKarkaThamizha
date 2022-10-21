using KarkaThamizha.Admin.Controllers;
using System.Web;
using System.Web.Mvc;

namespace KarkaThamizha.Admin
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ErrorLoggerAttribute());
        }
    }
}
