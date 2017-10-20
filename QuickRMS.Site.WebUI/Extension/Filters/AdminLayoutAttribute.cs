using Quick.Framework.Common.ToolsHelper;
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
	/// 页面布局
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class AdminLayoutAttribute : ActionFilterAttribute 
    {
		public IUserService UserService { get; set; }
		public IRoleService RoleService { get; set; }
		public IModuleService ModuleService { get; set; }
		public IPermissionService PermissionService { get; set; }
		public IModulePermissionService ModulePermissionService { get; set; }
		public IRoleModulePermissionService RoleModulePermissionService { get; set; }

		public AdminLayoutAttribute()
		{
			//TODO: Test
            //var userRole = new List<UserRole> { new UserRole { Id = 1, UserId = 1, RoleId = 1 } };
            //var user = new User { Id = 1, LoginName = "admin", LoginPwd = "8wdJLK8mokI=", UserRole = userRole };
            //SessionHelper.SetSession("CurrentUser", user);
			var user = SessionHelper.GetSession("CurrentUser") as User;
			if (user != null)
			{
			    InitService();
			}
		}

	    private void InitService()
	    {
            var container = System.Web.HttpContext.Current.Application["Container"] as CompositionContainer;
            UserService = container.GetExportedValueOrDefault<IUserService>();
            RoleService = container.GetExportedValueOrDefault<IRoleService>();
            RoleModulePermissionService = container.GetExportedValueOrDefault<IRoleModulePermissionService>();
            ModuleService = container.GetExportedValueOrDefault<IModuleService>();
            ModulePermissionService = container.GetExportedValueOrDefault<IModulePermissionService>();
            PermissionService = container.GetExportedValueOrDefault<IPermissionService>();
	    }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = SessionHelper.GetSession("CurrentUser") as User;
            // 如果用户没有登陆
            if (user == null)
            {

                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "Login", action = "SignOut" }));
                return;
            }
            if (UserService == null)
            {
                InitService();
            }
        }

		public override void OnResultExecuting(ResultExecutingContext filterContext)
		{
			var user = SessionHelper.GetSession("CurrentUser") as User;

			if (user != null)
			{
				//顶部菜单
				((ViewResult)filterContext.Result).ViewBag.LoginName = user.UserCode;

				//左侧菜单
				((ViewResult)filterContext.Result).ViewBag.SidebarMenuModel = InitSidebarMenu(user);

				//面包屑
				((ViewResult)filterContext.Result).ViewBag.BreadCrumbModel = InitBreadCrumb(filterContext);
			
				//按钮
				InitButton(user, filterContext);
			}
		}

		private List<SidebarMenuModel> InitSidebarMenu(User user)
		{
			var entity = user.UserRoles.Select(t => t.RoleId);

			List<int> RoleIds = entity.ToList();

			var model = new List<SidebarMenuModel>();

			//取出所有选中的节点
            var parentModuleIdList =
                RoleModulePermissionService.RoleModulePermissions.OrderBy(r => r.Module.OrderSort)
                    .Where(t => RoleIds.Contains(t.RoleId) && t.PermissionId == null && t.IsDeleted == false)
                    .Select(t => t.ModuleId)
                    .Distinct()
                    .ToList();

            //var parentModuleIdList = (from rmp in
            //    RoleModulePermissionService.RoleModulePermissions
            //    join m in ModuleService.Modules on rmp.ModuleId equals m.Id
            //    where RoleIds.Contains(rmp.RoleId) && rmp.PermissionId == null && rmp.IsDeleted == false
            //    orderby m.OrderSort
            //   select m.Id).ToList().Distinct();


            var childModuleIdList =
                RoleModulePermissionService.RoleModulePermissions.OrderBy(r => r.Module.OrderSort)
                    .Where(t => RoleIds.Contains(t.RoleId) && t.PermissionId != null && t.IsDeleted == false)
                    .Select(t => t.ModuleId)
                    .Distinct()
                    .ToList();

            //var childModuleIdList = (from rmp in
            //                              RoleModulePermissionService.RoleModulePermissions
            //                          join m in ModuleService.Modules on rmp.ModuleId equals m.Id
            //                          where RoleIds.Contains(rmp.RoleId) && rmp.PermissionId != null && rmp.IsDeleted == false
            //                          orderby m.OrderSort
            //                          select m.Id).ToList().Distinct();

			foreach (var pmId in parentModuleIdList)
			{
				//取出父菜单
				var parentModule = ModuleService.Modules.FirstOrDefault(t => t.Id == pmId&&t.Enabled);
				if (parentModule != null)
				{
					var sideBarMenu = new SidebarMenuModel
					{
						Id = parentModule.Id,
						ParentId = parentModule.ParentId,
						Name = parentModule.Name,
						Code = parentModule.Code,
						Icon = parentModule.Icon,
						LinkUrl = parentModule.LinkUrl,
                        OrderSort = parentModule.OrderSort
					};

					//取出子菜单
					foreach (var cmId in childModuleIdList)
					{
						var childModule = ModuleService.Modules.FirstOrDefault(t => t.Id == cmId);
						if (childModule != null && childModule.ParentId == sideBarMenu.Id)
						{
							var childSideBarMenu = new SidebarMenuModel
							{
								Id = childModule.Id,
								ParentId = childModule.ParentId,
								Name = childModule.Name,
								Code = childModule.Code,
								Icon = childModule.Icon,
								Area = childModule.Area,
								Controller = childModule.Controller,
								Action = childModule.Action,
                                LinkUrl = childModule.LinkUrl,
                                OrderSort = childModule.OrderSort
							};
							sideBarMenu.ChildMenuList.Add(childSideBarMenu);
						}
					}

					//子菜单排序
                    sideBarMenu.ChildMenuList = sideBarMenu.ChildMenuList.OrderBy(t => t.OrderSort).ToList();
					model.Add(sideBarMenu);
				}
				//父菜单排序
                model = model.OrderBy(t => t.OrderSort).ToList();
			}

			return model;
		}

		private BreadCrumbNavModel InitBreadCrumb(ResultExecutingContext filterContext)
		{
			var area = filterContext.RouteData.DataTokens.ContainsKey("area") ? filterContext.RouteData.DataTokens["area"].ToString().ToLower() : string.Empty;
			var controller = filterContext.RouteData.Values["controller"].ToString().ToLower();
			var action = filterContext.RouteData.Values["action"].ToString().ToLower();
            var id = filterContext.RouteData.Values["id"];
			string linkUrl = string.Format("{0}/{1}/{2}", area, controller, action);
		    if (id!=null)
		    {
                linkUrl = string.Format("{0}/{1}", linkUrl, id.ToString());
		    }
			var model = new BreadCrumbNavModel();

			var indexModel = new BreadCrumbModel { 
				Name = "首页",
				Icon = "icon-home",
				IsParent = false,
				IsIndex = true
			};

			if (area == "common" && controller == "home" && action == "index")
			{
				model.CurrentName = "首页";
			}

			model.BreadCrumbList.Add(indexModel);

			var module = ModuleService.Modules.FirstOrDefault(t => t.LinkUrl.ToLower()==linkUrl && t.IsDeleted == false && t.Enabled == true);

			if (module != null)
			{			
				//有父菜单
				if (module.ParentModule != null)
				{
					var parentModel = new BreadCrumbModel
					{
						IsParent = true,
						Name = module.ParentModule.Name,
						Icon = module.ParentModule.Icon
					};
					model.BreadCrumbList.Add(parentModel);
				}
				
				var currentModel = new BreadCrumbModel {
 					IsParent = false,
					Name = module.Name,
					Icon = ""
				};

				model.CurrentName = currentModel.Name;
				model.BreadCrumbList.Add(currentModel);

				((ViewResult)filterContext.Result).ViewBag.CurrentTitle = module.Name;
			}
			return model;
		}

		private void InitButton(User user, ResultExecutingContext filterContext)
		{
			var roleIds = user.UserRoles.Select(t => t.RoleId);
			var controller = filterContext.RouteData.Values["controller"].ToString().ToLower();
			var action = filterContext.RouteData.Values["action"].ToString().ToLower();
			var module = ModuleService.Modules.FirstOrDefault(t => t.Controller.ToLower() == controller);
			if (module != null)
			{
				var permissionIds = RoleModulePermissionService.RoleModulePermissions.Where(t => roleIds.Contains(t.RoleId) && t.ModuleId == module.Id).Select(t => t.PermissionId).Distinct();
				foreach (var permissionId in permissionIds)
				{
					var entity = PermissionService.Permissions.FirstOrDefault(t => t.Id == permissionId && t.Enabled == true && t.IsDeleted == false);
					if (entity != null)
					{
						var btnButton = new ButtonModel
						{
							Icon = entity.Icon,
							Text = entity.Name
						};
						if (entity.Code.ToLower() == "create")
						{
							((ViewResult)filterContext.Result).ViewBag.Create = btnButton;
						}
						else if (entity.Code.ToLower() == "edit")
						{
							((ViewResult)filterContext.Result).ViewBag.Edit = btnButton;
						}
						else if (entity.Code.ToLower() == "delete")
						{
							((ViewResult)filterContext.Result).ViewBag.Delete = btnButton;
						}
						else if (entity.Code.ToLower() == "setbutton")
						{
							((ViewResult)filterContext.Result).ViewBag.SetButton = btnButton;
						}
						else if (entity.Code.ToLower() == "setpermission")
						{
							((ViewResult)filterContext.Result).ViewBag.SetPermission = btnButton;
						}
						else if (entity.Code.ToLower() == "changepwd")
						{
							((ViewResult)filterContext.Result).ViewBag.ChangePwd = btnButton;
						}
						else if (entity.Code.ToLower() == "deleteall")
						{
							((ViewResult)filterContext.Result).ViewBag.DeleteAll = btnButton;
						}
					}
				}
			}
		}
	}
}