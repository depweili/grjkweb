using QuickRMS.Site.WebUI.Extension.Filter;
using System.Web;
using System.Web.Mvc;

namespace QuickRMS.Site.WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
			filters.Add(new ElmahErrorAttribute());
        }
    }
}
