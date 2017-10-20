using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quick.Site.Common.Models;
using QuickRMS.Site.WebUI.Common;
using QuickRMS.Site.WebUI.Extension.Filters;

namespace QuickRMS.Site.WebUI.Areas.Common.Controllers
{

    [PartCreationPolicy(CreationPolicy.NonShared)]
    [AdminPermission(PermissionCustomMode.Ignore)]
    public class DashboardController : AdminController
    {
        //
        // GET: /Common/Dashboard/
        public ActionResult Index()
        {
            return View();
        }
	}
}