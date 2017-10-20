using System.Web.Mvc;

namespace QuickRMS.Site.WebUI.Areas.Authen
{
    public class AuthenAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Authen";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Authen_default",
                "Authen/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "QuickRMS.Site.WebUI.Areas.Authen.Controllers"}
            );
        }
    }
}