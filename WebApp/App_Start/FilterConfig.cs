using System.Web.Mvc;

namespace WebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // Remove this comment, if you want to have the default redirect for 5xx errors
            //filters.Add(new HandleErrorAttribute());
        }
    }
}