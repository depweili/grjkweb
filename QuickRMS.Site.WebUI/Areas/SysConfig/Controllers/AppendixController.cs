using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Quick.Framework.Tool;
using QuickRMS.Site.WebUI.Common;
using QuickRMS.Site.WebUI.Extension.Filters;
using Quick.Site.Common.Models;

namespace QuickRMS.Site.WebUI.Areas.SysConfig.Controllers
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
	[AdminPermission(PermissionCustomMode.Ignore)]
	public class AppendixController : AdminController
    {
        //
		// GET: /SysConfig/Config/

		[AdminLayout]
		public ActionResult Icon()
		{
			return View();
		}
	}
}