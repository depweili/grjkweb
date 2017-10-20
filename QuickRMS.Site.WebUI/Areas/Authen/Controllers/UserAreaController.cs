using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quick.Site.Common.Models;
using QuickRMS.Core.Service.Authen;
using QuickRMS.Site.Models.Authen.UserArea;
using QuickRMS.Site.WebUI.Common;
using QuickRMS.Site.WebUI.Extension.Filters;

namespace QuickRMS.Site.WebUI.Areas.Authen.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserAreaController : AdminController
    {
        #region 属性
       [Import]
        public IUserAreaService UserAreaService { get; set; }
        #endregion	

        [HttpPost]
        [AdminOperateLog]
        [AdminPermission(PermissionCustomMode.Ignore)]
        public ActionResult Create(UserAreaModel model)
        {
            var result = UserAreaService.SetUserArea(model);
            return Json(result);
        }

        [HttpPost]
        [AdminOperateLog]
        [AdminPermission(PermissionCustomMode.Ignore)]
        public ActionResult Delete(UserAreaModel model)
        {
            var result = UserAreaService.RemoveUserArea(model);
            return Json(result);
        }

       [AdminPermission(PermissionCustomMode.Ignore)]
        public ActionResult SelectedArea(int uId)
        {
            var result = UserAreaService.UserAreas.Where(r=>r.UserId==uId).Select(t=>new UserAreaModel
            {
                Id = t.Id,
                UserId = t.UserId,
                AreaId = t.AreaId,
                AreaName = t.Area.Name
            });
            return PartialView(result);
        }
	}
}