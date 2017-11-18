using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Quick.Framework.Tool;
using Quick.Site.Common.Models;
using QuickRMS.Core.Service.Authen;
using QuickRMS.Core.Service.Authen.Impl;
using QuickRMS.Domain.Models.Authen;
using QuickRMS.Site.Models.Authen.Area;
using QuickRMS.Site.Models.Authen.Permission;
using QuickRMS.Site.WebUI.Common;
using QuickRMS.Site.WebUI.Extension.Filters;

namespace QuickRMS.Site.WebUI.Areas.Authen.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AreaController : AdminController
    {

        //
        // GET: /Authen/Area/

        #region 属性

        [Import]
        public IAreaService AreaService { get; set; }

        #endregion

        [AdminLayout]
        public ActionResult Index()
        {
            var model = new AreaModel();

            #region 父菜单列表

            var parentAreaData = AreaService.Areas.Where(
                t => t.ParentId == null  && t.IsDeleted == false)
                .Select(t => new AreaModel
                {
                    Id = t.Id,
                    Name = t.Name
                });

            #endregion

            return View(model);
        }

        [AdminPermission(PermissionCustomMode.Ignore)]
        public ActionResult List(DataTableParameter param)
        {
            int total = AreaService.Areas.Count(t => t.IsDeleted ==null||t.IsDeleted.Value == false);

            //构建查询表达式
            var expr = BuildSearchCriteria();

            var filterResult = AreaService.Areas.Where(expr).Select(t => new AreaModel
            {
                Id = t.Id,
                ParentId=t.ParentId,
                Description = t.Description,
                Name = t.Name,
                ParentName = t.ParentArea != null ? t.ParentArea.Name : ""
            }).OrderBy(t => (t.ParentId??0)).Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();

            int sortId = param.iDisplayStart + 1;

            var result = from c in filterResult
                select new[]
                {
                    sortId++.ToString(),
                    c.Name,
                    c.Description,
                    c.ParentName,
                    c.OrderSort.ToString(),
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
        private Expression<Func<Area, Boolean>> BuildSearchCriteria()
        {
            DynamicLambda<Area> bulider = new DynamicLambda<Area>();
            Expression<Func<Area, Boolean>> expr = null;

            if (!string.IsNullOrEmpty(Request["Name"]))
            {
                var data = Request["Name"].Trim();
                Expression<Func<Area, Boolean>> tmp = t => t.Name.Contains(data);
                expr = bulider.BuildQueryAnd(expr, tmp);
            }


            Expression<Func<Area, Boolean>> tmpSolid = (t => t.IsDeleted == null || t.IsDeleted.Value == false);
            expr = bulider.BuildQueryAnd(expr, tmpSolid);

            return expr;
        }

        #endregion

        public ActionResult Create(int? pId)
        {
            var model = new AreaModel();
            model.ParentName = "无";
            if (pId != null)
            {
                var po = AreaService.Areas.First(r => r.Id == pId.Value);
                model.ParentId = po.Id;
                model.ParentName = po.Name;
            }
            InitParentArea(model);

            return PartialView(model);
        }

        [HttpPost]
        [AdminOperateLog]
        public ActionResult Create(AreaModel model)
        {
            if (ModelState.IsValid)
            {

                OperationResult result = AreaService.Insert(model);
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
            var model = new AreaModel();
            var entity = AreaService.Areas.FirstOrDefault(t => t.Id == Id);
            if (null != entity)
            {
                model = new AreaModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    ParentId = entity.ParentId,
                    Description = entity.Description,
                    ParentName = (entity.ParentArea == null ? "" : (entity.ParentArea.Name))
                };
                InitParentArea(model);

            }
            return PartialView(model);
        }

        [HttpPost]
        [AdminOperateLog]
        public ActionResult Edit(AreaModel model)
        {
            if (ModelState.IsValid)
            {

                OperationResult result = AreaService.Update(model);
                if (result.ResultType == OperationResultType.Success)
                {
                    return Json(result);
                }
                else
                {
                    InitParentArea(model);
                    return PartialView(model);
                }
            }
            else
            {
                InitParentArea(model);
                return PartialView(model);
            }
        }



        [AdminOperateLog]
        public ActionResult Delete(int Id)
        {
            OperationResult result = AreaService.Delete(Id);
            return Json(result);
        }

        /// <summary>
        /// 父菜单列表
        /// </summary>
        /// <param name="model"></param>
        private void InitParentArea(AreaModel model)
        {
            var parentAreaData = AreaService.Areas.Where(
                t => t.ParentId == null && (t.IsDeleted == null || t.IsDeleted.Value == false))
                .Select(t => new AreaModel
                {
                    Id = t.Id,
                    Name = t.Name
                });

        }

       

    }
}