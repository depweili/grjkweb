using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Quick.Framework.Tool;
using QuickRMS.Site.WebUI.Extension.Filters;
using Quick.Site.Common.Models;
using QuickRMS.Site.WebUI.Common;
using QuickRMS.Site.Models.AdminCommon;
using QuickRMS.Site.Models.Authen.User;
using Quick.Framework.Common.ToolsHelper;
using QuickRMS.Domain.Models.Authen;
using Quick.Framework.Common.SecurityHelper;
using QuickRMS.Core.Service.Authen;

namespace QuickRMS.Site.WebUI.Areas.Common.Controllers
{	
	[Export]
	[PartCreationPolicy(CreationPolicy.NonShared)]
    [AdminPermission(PermissionCustomMode.Ignore)]
	public class ProfileController : AdminController
    {
        //
        // GET: /Common/Profile/
        #region 属性
        [Import]
		public IUserService UserService { get; set; }
        #endregion

        [AdminLayout]
        public ActionResult Index()
        {
			var entity = this.GetCurrentUser();
			var model = new ProfileModel { 
				Id = entity.Id,
				LoginName = entity.UserCode,
				Email = entity.Memo,
				FullName = entity.UserName,
				Phone = entity.Tel,
				LoginCount = entity.LoginCount,
				LastLoginTime = entity.LastLoginTime,
				RegisterTime = entity.RegisterTime
			};
			return View(model);
        }

		[AdminLayout]
		public ActionResult ChangePwd()
		{
			var entity = this.GetCurrentUser();
			var model = new ChangePwdModel { 
				Id = entity.Id,
				LoginName = entity.UserCode,
				Email = entity.Memo
			};
			return View(model);
		}

		[HttpPost]
		public ActionResult ChangePwd(ChangePwdModel model)
		{
			if (ModelState.IsValid)
			{
				OperationResult result = UserService.Update(model);
				if (result.ResultType == OperationResultType.Success)
				{
					return Json(result);
				}
				else
				{
					return PartialView(model);
				}
			}
			else
			{
				return PartialView(model);
			}
		}

		public ActionResult CheckPwd(string oldLoginPwd)
		{
			bool result = true;
			var user = SessionHelper.GetSession("CurrentUser") as User;
			if (DESProvider.DecryptString(user.UserPwd) != oldLoginPwd)
			{
				result = false;
			}
			return Json(result, JsonRequestBehavior.AllowGet);
		}
	}
}