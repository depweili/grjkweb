using Quick.Framework.Common.ToolsHelper;
using Quick.Site.Common.Models;
using QuickRMS.Domain.Models.Authen;
using QuickRMS.Site.Models.AdminCommon;
using QuickRMS.Site.Models.Authen.Module;
using QuickRMS.Site.WebUI.Extension.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace QuickRMS.Site.WebUI.Common
{
	[AdminPermission(PermissionCustomMode.Enforce)]
    public class AdminController : Controller
    {
		public AdminController()
		{
			//TODO: Test
            //var userRole = new List<UserRole> { new UserRole { Id = 1, UserId = 1, RoleId = 1 } };
            //var user = new User { Id = 1, LoginName = "admin", LoginPwd = "8wdJLK8mokI=", UserRole = userRole };
            //SessionHelper.SetSession("CurrentUser", user);
		}

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            #region 

            var user = GetCurrentUser();

            //检验用户是否已经登录，如果登录则不执行，否则则执行下面的跳转代码
            if (user == null)
            {

                //filterContext.Result = RedirectToAction("SignOut", "Login", new RouteValueDictionary(new { area = "Admin" }));
                //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "Common", controller = "Login", action = "SignOut" }));
                
                //暂时不进行用户检查
                //filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.GatewayTimeout);
                //return;
            }
            #endregion
            

        }

		protected User GetCurrentUser()
		{
			var user = SessionHelper.GetSession("CurrentUser") as User;
			return user;
		}

		protected void CreateBaseData<T>(T model) where T : EntityCommon
		{
			var user = SessionHelper.GetSession("CurrentUser") as User;
			if (user != null)
			{
				model.CreateId = user.Id;
				model.CreateBy = user.UserCode;
				model.CreateTime = DateTime.Now;
				model.ModifyId = user.Id;
				model.ModifyBy = user.UserCode;
				model.ModifyTime = DateTime.Now;
			}
		}

		protected void UpdateBaseData<T>(T model) where T : EntityCommon
		{
			var user = SessionHelper.GetSession("CurrentUser") as User;
			if (user != null)
			{
				model.ModifyId = user.Id;
				model.ModifyBy = user.UserCode;
				model.ModifyTime = DateTime.Now;
			}
		}

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
	}
}