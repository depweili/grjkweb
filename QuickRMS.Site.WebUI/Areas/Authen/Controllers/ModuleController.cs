using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using QuickRMS.Core.Service.Authen;
using QuickRMS.Site.Models.Authen.Module;
using Quick.Framework.Tool;
using System.Linq.Expressions;
using QuickRMS.Domain.Models.Authen;
using Quick.Site.Common.Models;
using System.ComponentModel;
using QuickRMS.Site.Models.Authen.Permission;
using QuickRMS.Site.WebUI.Common;
using QuickRMS.Site.WebUI.Extension.Filters;

namespace QuickRMS.Site.WebUI.Areas.Authen.Controllers
{
	[Export]
	[PartCreationPolicy(CreationPolicy.NonShared)]
    public class ModuleController : AdminController
    {
        //
		// GET: /Authen/Module/

		#region 属性
		[Import]
		public IModuleService ModuleService { get; set; }

		[Import]
		public IPermissionService PermissionService { get; set; }

		[Import]
		public IModulePermissionService ModulePermissionService { get; set; }
		#endregion

		[AdminLayout]
        public ActionResult Index()
        {
			var model = new ModuleModel();

			#region 父菜单列表
			var parentModuleData = ModuleService.Modules.Where(t => t.ParentId == null && t.IsMenu == true && t.Enabled == true && t.IsDeleted == false)
			.Select(t => new ModuleModel
			{
				Id = t.Id,
				Name = t.Name
			});
			foreach (var item in parentModuleData)
			{
				model.Search.ParentModuleItems.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
			}
			#endregion

            return View(model);
        }

		[AdminPermission(PermissionCustomMode.Ignore)]
		public ActionResult List(DataTableParameter param)
		{
			int total = ModuleService.Modules.Count(t => t.IsDeleted == false);

			//构建查询表达式
			var expr = BuildSearchCriteria();

			var filterResult = ModuleService.Modules.Where(expr).Select(t => new ModuleModel
			                    {
				                    Id = t.Id,
									Name = "<i class='" + t.Icon + "'></i> " + t.Name,
				                    Code = t.Code,
									ParentName = t.ParentModule != null ? t.ParentModule.Name : "",
				                    LinkUrl = t.LinkUrl,
				                    OrderSort = t.OrderSort,
									IsMenu = t.IsMenu,
				                    Enabled = t.Enabled
			                    }).OrderBy(t => t.Code).Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();

			int sortId = param.iDisplayStart + 1;

			var result = from c in filterResult
						 select new[]
                             {
                                 sortId++.ToString(), 
                                 c.Name,
                                 c.Code,
                                 c.ParentName,                            
                                 c.LinkUrl, 
                                 c.OrderSort.ToString(),
								 c.MenuText,
                                 c.EnabledText, 
                                 c.Id.ToString()
                             };

			return Json(new
			{
				sEcho = param.sEcho,
				iDisplayStart = param.iDisplayStart,
				iTotalRecords = total,
				iTotalDisplayRecords = total,
				aaData = result
			}, JsonRequestBehavior.AllowGet);
		}

		#region 构建查询表达式
		/// <summary>
		/// 构建查询表达式
		/// </summary>
		/// <returns></returns>
		private Expression<Func<Module, Boolean>> BuildSearchCriteria()
		{
			DynamicLambda<Module> bulider = new DynamicLambda<Module>();
			Expression<Func<Module, Boolean>> expr = null;
			if (!string.IsNullOrEmpty(Request["Name"]))
			{
				var data = Request["Name"].Trim();
				Expression<Func<Module, Boolean>> tmp = t => t.Name.Contains(data);
				expr = bulider.BuildQueryAnd(expr, tmp);
			}
			if (!string.IsNullOrEmpty(Request["LinkUrl"]))
			{
				var data = Request["LinkUrl"].Trim();
				Expression<Func<Module, Boolean>> tmp = t => t.LinkUrl.Contains(data);
				expr = bulider.BuildQueryAnd(expr, tmp);
			}
			if (!string.IsNullOrEmpty(Request["ParentId"]) && Request["ParentId"] != "0")
			{
				var data = Convert.ToInt32(Request["ParentId"]);
				Expression<Func<Module, Boolean>> tmp = t => (t.Id == data) || (t.ParentId == data);
				expr = bulider.BuildQueryAnd(expr, tmp);
			}
			if (Request["IsMenu"] == "0" || Request["IsMenu"] == "1")
			{
				var data = Request["IsMenu"] == "1" ? true : false;
				Expression<Func<Module, Boolean>> tmp = t => t.IsMenu == data;
				expr = bulider.BuildQueryAnd(expr, tmp);
			}
			if (Request["Enabled"] == "0" || Request["Enabled"] == "1")
			{
				var data = Request["Enabled"] == "1" ? true : false;
				Expression<Func<Module, Boolean>> tmp = t => t.Enabled == data;
				expr = bulider.BuildQueryAnd(expr, tmp);
			}

			Expression<Func<Module, Boolean>> tmpSolid = t => t.IsDeleted == false;
			expr = bulider.BuildQueryAnd(expr, tmpSolid);

			return expr;
		}

		#endregion

        public ActionResult Create()
        {
            var model = new ModuleModel();
            InitParentModule(model);

            return PartialView(model);
        }

        [HttpPost]
		[AdminOperateLog]
        public ActionResult Create(ModuleModel model)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.LinkUrl) && model.LinkUrl.Split('/').Length == 3)
                {
                    string[] link = model.LinkUrl.Split('/');
                    model.Area = link[0];
                    model.Controller = link[1];
                    model.Action = link[2];
                } 
                OperationResult result = ModuleService.Insert(model);
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

        public ActionResult Edit(int Id)
        {
            var model = new ModuleModel();
            var entity = ModuleService.Modules.FirstOrDefault(t => t.Id == Id);
            if (null != entity)
            {
                model = new ModuleModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Code = entity.Code,
                    Icon = entity.Icon,
                    ParentId = entity.ParentId,
                    LinkUrl = entity.LinkUrl,
                    OrderSort = entity.OrderSort,
					IsMenu = entity.IsMenu,
                    Enabled = entity.Enabled
                };
               InitParentModule(model);

            }
            return PartialView(model);
        }

        [HttpPost]
		[AdminOperateLog]
        public ActionResult Edit(ModuleModel model)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.LinkUrl) && model.LinkUrl.Split('/').Length == 3)
                {
                    string[] link = model.LinkUrl.Split('/');
                    model.Area = link[0];
                    model.Controller = link[1];
                    model.Action = link[2];
                }
                OperationResult result = ModuleService.Update(model);
                if (result.ResultType == OperationResultType.Success)
                {
                    return Json(result);
                }
                else
                {
                    InitParentModule(model);
                    return PartialView(model);
                }
            }
            else
            {
				InitParentModule(model);
                return PartialView(model);
            }
        }

		public ActionResult SetButton(int Id)
		{
			var module = ModuleService.Modules.FirstOrDefault(t => t.Id == Id);
			var model = new ButtonModel
			{
				ModuleId = module.Id,
				ModuleName = module.Name
			};
			model.ButtonList = PermissionService.GetKeyValueList();
			//Selected Button 
			foreach (var item in module.ModulePermission.Where(t => t.IsDeleted == false))
			{
				model.SelectedButtonList.Add(item.PermissionId);
			}
			return PartialView(model);
		}

		[HttpPost]
		[AdminOperateLog]
		public ActionResult SetButton(ButtonModel model)
		{
			if (ModelState.IsValid)
			{
				OperationResult result = ModulePermissionService.SetButton(model);
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

		[AdminOperateLog]
        public ActionResult Delete(int Id)
        {
            OperationResult result = ModuleService.Delete(Id);
            return Json(result);
        }

		/// <summary>
		/// 父菜单列表
		/// </summary>
		/// <param name="model"></param>
		private void InitParentModule(ModuleModel model)
		{
			var parentModuleData = ModuleService.Modules.Where(t => t.ParentId == null && t.IsMenu == true && t.Enabled == true && t.IsDeleted == false)
				.Select(t => new ModuleModel
				{
					Id = t.Id,
					Name = t.Name
				});
			foreach (var item in parentModuleData)
			{
				model.ParentModuleItems.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
			}
		}


        [HttpGet]
        [AdminPermission(PermissionCustomMode.Ignore)]
        public ActionResult GetModuleLinkUrl(string term)
        {
            var model = ConfigSettingHelper.GetAllModuleLinkUrl().Where(t => t.LinkUrl.ToLower().Contains(term.ToLower())).ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
	}
}