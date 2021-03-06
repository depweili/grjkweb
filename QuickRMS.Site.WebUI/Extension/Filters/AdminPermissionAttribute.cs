﻿using Quick.Site.Common.Models;
using QuickRMS.Core.Service.Authen;
using QuickRMS.Domain.Models.Authen;
using QuickRMS.Site.Models.AdminCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace QuickRMS.Site.WebUI.Extension.Filters
{
	/// <summary>
	/// 后台权限验证
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple= false)]
    public class AdminPermissionAttribute : System.Web.Mvc.AuthorizeAttribute
    {
		private PermissionCustomMode CustomMode;

        private IUserService UserService { get; set; }
		private IRoleService RoleService { get; set; }
		private IUserRoleService UserRoleService { get; set; }
		private IRoleModulePermissionService RoleModulePermissionService { get; set; }
		private IModuleService ModuleService { get; set; }
		private IModulePermissionService ModulePermissionService { get; set; }
		private IPermissionService PermissionService { get; set; }

		public AdminPermissionAttribute(PermissionCustomMode mode)
		{
			var container = HttpContext.Current.Application["Container"] as CompositionContainer;
			UserService = container.GetExportedValueOrDefault<IUserService>();
			RoleService = container.GetExportedValueOrDefault<IRoleService>();
			UserRoleService = container.GetExportedValueOrDefault<IUserRoleService>();
			RoleModulePermissionService = container.GetExportedValueOrDefault<IRoleModulePermissionService>();
			ModuleService = container.GetExportedValueOrDefault<IModuleService>();
			ModulePermissionService = container.GetExportedValueOrDefault<IModulePermissionService>();
			PermissionService = container.GetExportedValueOrDefault<IPermissionService>();

			CustomMode = mode;
		}

		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			//权限拦截是否忽略
			if (CustomMode == PermissionCustomMode.Ignore)
			{
				return;
			}

			//验证用户是否登录
			//TODO: Test
            //var userRole = new List<UserRole> { new UserRole { Id = 1, UserId = 1, RoleId = 1 } };
            //var user = new User { Id = 1, LoginName = "admin", LoginPwd = "8wdJLK8mokI=", UserRole = userRole };
			var user = filterContext.HttpContext.Session["CurrentUser"] as User;
			if (user == null)
			{
				//跳转到登录页面
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "Common", controller = "Login", action = "SignOut" }));
			}
			else
			{
				// 权限拦截与验证
				var area = filterContext.RouteData.DataTokens.ContainsKey("area") ? filterContext.RouteData.DataTokens["area"].ToString() : string.Empty;
				var controller = filterContext.RouteData.Values["controller"].ToString().ToLower();
				var action = filterContext.RouteData.Values["action"].ToString().ToLower();

				var isAllowed = this.IsAllowed(user, controller, action);

				if (!isAllowed)
				{
					filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "Common", controller = "Error", action = "Page400" }));
				}
			}
		}

        public bool IsAllowed(User user, string controller, string action)
        {
			var roleIdList = UserRoleService.UserRoles.Where(t => t.UserId == user.Id && t.IsDeleted == false).Select(t => t.RoleId);
			var modules = ModuleService.Modules.Where(t => t.Controller.ToLower() == controller).Select(r=>r.Id);
			var permission = PermissionService.Permissions.FirstOrDefault(t => t.Code.ToLower() == action);

			if (modules.Any() && permission != null)
			{
				var roleModulePermisssion = RoleModulePermissionService.RoleModulePermissions.Where(t => roleIdList.Contains(t.RoleId)
												&& modules.Contains(t.ModuleId)
												&& t.PermissionId == permission.Id
												&& t.IsDeleted == false);
				if (roleModulePermisssion.Any())
				{
					return true;
				}
			}

			return false;
        }

		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			//if (filterContext.HttpContext.User.Identity.IsAuthenticated)
			//{
			//	base.HandleUnauthorizedRequest();
			//}
			//else
			//{
			//	filterContext.Result = new RedirectToRouteResult(new
			//	RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
			//}
		}
    
		
	}
}