using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using QuickRMS.Core.Service.SysConfig;
using Quick.Framework.Tool;
using System.Linq.Expressions;
using QuickRMS.Domain.Models.SysConfig;
using Quick.Site.Common.Models;
using QuickRMS.Site.WebUI.Common;
using QuickRMS.Site.WebUI.Extension.Filters;
using QuickRMS.Site.Models.SysConfig;
using QuickRMS.Site.Models.SysConfig.OperateLog;

namespace QuickRMS.Site.WebUI.Areas.SysConfig.Controllers
{
	[Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class OperateLogController : AdminController
    {
        //
		// GET: /SysConfig/OperateLog/

		#region 属性
		[Import]
		public IOperateLogService OperateLogService { get; set; }
		#endregion

		[AdminLayout]
		public ActionResult Index()
		{
			var model = new OperateLogModel();
			return View(model);
		}

		[AdminPermission(PermissionCustomMode.Ignore)]
		public ActionResult List(DataTableParameter param)
		{
			int total = OperateLogService.OperateLogs.Count(t => t.IsDeleted == false);

			//构建查询表达式
			var expr = BuildSearchCriteria();

			var filterResult = OperateLogService.OperateLogs.Where(expr).Select(t => new OperateLogModel
			{
				LogTime = t.LogTime,
				Area = t.Area,
				Controller = t.Controller,
				Action = t.Action,
				Description = t.Description,
				IPAddress = t.IPAddress,
				LoginName = t.LoginName
				
			}).OrderByDescending(t => t.LogTime).Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();

			int sortId = param.iDisplayStart + 1;

			var result = from c in filterResult
						 select new[]
                             {
                                 sortId++.ToString(), 
								 c.LogTime.ToString(),
								 c.Area,
								 c.Controller,
								 c.Action,
								 c.Description,
								 c.IPAddress,
								 c.LoginName
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
		private Expression<Func<OperateLog, Boolean>> BuildSearchCriteria()
		{
			DynamicLambda<OperateLog> bulider = new DynamicLambda<OperateLog>();
			Expression<Func<OperateLog, Boolean>> expr = null;
			if (!string.IsNullOrEmpty(Request["Area"]))
			{
				var data = Request["Area"].Trim();
				Expression<Func<OperateLog, Boolean>> tmp = t => t.Area.Contains(data);
				expr = bulider.BuildQueryAnd(expr, tmp);
			}
			if (!string.IsNullOrEmpty(Request["Controller"]))
			{
				var data = Request["Controller"].Trim();
				Expression<Func<OperateLog, Boolean>> tmp = t => t.Controller.Contains(data);
				expr = bulider.BuildQueryAnd(expr, tmp);
			}
			if (!string.IsNullOrEmpty(Request["Action"]))
			{
				var data = Request["Action"].Trim();
				Expression<Func<OperateLog, Boolean>> tmp = t => t.Action.Contains(data);
				expr = bulider.BuildQueryAnd(expr, tmp);
			}
			if (!string.IsNullOrEmpty(Request["IPAddress"]))
			{
				var data = Request["IPAddress"].Trim();
				Expression<Func<OperateLog, Boolean>> tmp = t => t.IPAddress.Contains(data);
				expr = bulider.BuildQueryAnd(expr, tmp);
			}
			if (!string.IsNullOrEmpty(Request["LoginName"]))
			{
				var data = Request["LoginName"].Trim();
				Expression<Func<OperateLog, Boolean>> tmp = t => t.LoginName.Contains(data);
				expr = bulider.BuildQueryAnd(expr, tmp);
			}
			if (!string.IsNullOrEmpty(Request["StartTime"]))
			{
				var data = Convert.ToDateTime(Request["StartTime"].Trim());
				Expression<Func<OperateLog, Boolean>> tmp = t => t.LogTime >= data;
				expr = bulider.BuildQueryAnd(expr, tmp);
			}
			if (!string.IsNullOrEmpty(Request["EndTime"]))
			{
				var data = Convert.ToDateTime(Request["EndTime"].Trim()).AddDays(1);
				Expression<Func<OperateLog, Boolean>> tmp = t => t.LogTime <= data;
				expr = bulider.BuildQueryAnd(expr, tmp);
			}
			Expression<Func<OperateLog, Boolean>> tmpSolid = t => t.IsDeleted == false;
			expr = bulider.BuildQueryAnd(expr, tmpSolid);

			return expr;
		}

		#endregion

		public ActionResult DeleteAll()
		{
			OperationResult result = OperateLogService.Delete();
			return Json(result);
		}
	}
}