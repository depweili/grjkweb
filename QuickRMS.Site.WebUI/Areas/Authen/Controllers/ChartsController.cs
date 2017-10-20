using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuickRMS.Site.WebUI.Common;
using QuickRMS.Site.WebUI.Extension.Filters;

namespace QuickRMS.Site.WebUI.Areas.Authen.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ChartsController : AdminController
    {
         [AdminLayout]
        public ActionResult Index()
        {
            return View();
        }
	}
}