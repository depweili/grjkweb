using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quick.Framework.Common.ToolsHelper;
using QuickRMS.Domain.Models.Authen;

namespace QuickRMS.Site.WebUI.Areas.Common.Controllers
{
    public class TestController : Controller
    {
        public ActionResult TestConn()
        {
            var user = SessionHelper.GetSession("CurrentUser") as User;
            var ret = new {state = (user == null ? 0 : 1)};
            return Json(ret, JsonRequestBehavior.AllowGet);
        }
	}
}