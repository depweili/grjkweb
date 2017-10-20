using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using QuickRMS.Core.Service.Authen;
using QuickRMS.Site.Models.Authen.Permission;
using Quick.Framework.Tool;
using System.Linq.Expressions;
using QuickRMS.Domain.Models.Authen;
using Quick.Site.Common.Models;
using QuickRMS.Site.WebUI.Common;
using QuickRMS.Site.WebUI.Extension.Filters;

namespace QuickRMS.Site.WebUI.Areas.Authen.Controllers
{
	[Export]
	[PartCreationPolicy(CreationPolicy.NonShared)]
    public class PermissionController : AdminController
    {
        //
		// GET: /Authen/Permission/

		#region 属性
		[Import]
		public IPermissionService PermissionService { get; set; }
		#endregion

		[AdminLayout]
        public ActionResult Index()
        {
			var model = new PermissionModel();
            return View(model);
        }

		[AdminPermission(PermissionCustomMode.Ignore)]
		public ActionResult List(DataTableParameter param)
		{
			int total = PermissionService.Permissions.Count(t => t.IsDeleted == false);

			//构建查询表达式
			var expr = BuildSearchCriteria();

			var filterResult = PermissionService.Permissions.Where(expr).Select(t => new PermissionModel
			{
				Id = t.Id,
				Name = t.Name,
				Code = t.Code,			
				Icon = t.Icon,
				OrderSort = t.OrderSort,
				Description = t.Description,
				Enabled = t.Enabled
			}).OrderBy(t => t.OrderSort).Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();

			int sortId = param.iDisplayStart + 1;

			var result = from c in filterResult
						 select new[]
                             {
                                 sortId++.ToString(), 
								 c.Name,
                                 c.Code,								                                                
                                 c.Icon, 
                                 c.OrderSort.ToString(),
								 c.Description,
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
		private Expression<Func<Permission, Boolean>> BuildSearchCriteria()
		{
			DynamicLambda<Permission> bulider = new DynamicLambda<Permission>();
			Expression<Func<Permission, Boolean>> expr = null;
			if (!string.IsNullOrEmpty(Request["Name"]))
			{
				var data = Request["Name"].Trim();
				Expression<Func<Permission, Boolean>> tmp = t => t.Name.Contains(data);
				expr = bulider.BuildQueryAnd(expr, tmp);
			}

			Expression<Func<Permission, Boolean>> tmpSolid = t => t.IsDeleted == false;
			expr = bulider.BuildQueryAnd(expr, tmpSolid);

			return expr;
		}

		#endregion

        public ActionResult Create()
        {
            var model = new PermissionModel();
            return PartialView(model);
        }

        [HttpPost]
		[AdminOperateLog]
        public ActionResult Create(PermissionModel model)
        {
            if (ModelState.IsValid)
            {
                OperationResult result = PermissionService.Insert(model);
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
            var model = new PermissionModel();
            var entity = PermissionService.Permissions.FirstOrDefault(t => t.Id == Id);
            if (null != entity)
            {
                model = new PermissionModel
                {
                    Id = entity.Id,
                    Code = entity.Code,
                    Icon = entity.Icon,
                    Name = entity.Name,
                    Description = entity.Description,
                    OrderSort = entity.OrderSort,
                    Enabled = entity.Enabled
                };
            }
            return PartialView(model);
        }

        [HttpPost]
		[AdminOperateLog]
        public ActionResult Edit(PermissionModel model)
        {
            if (ModelState.IsValid)
            {
                OperationResult result = PermissionService.Update(model);
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
            OperationResult result = PermissionService.Delete(Id);
            return Json(result);
        }
	}
}