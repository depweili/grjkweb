using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quick.Site.Common.Models;
using QuickRMS.Core.Service;
using QuickRMS.Core.Service.Authen;

using QuickRMS.Domain.Models.DeviceInfo;
using QuickRMS.Site.WebUI.Common;
using QuickRMS.Site.WebUI.Extension.Filters;

namespace QuickRMS.Site.WebUI.Areas.Authen.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BaseDataApiController : AdminController
    {
        [Import]
        public IDeviceService DeviceService { get; set; }

        [Import]
        public IDeviceCureLibraryService DeviceCureLibraryService { get; set; }

        [Import]
        public IDeviceApiService DeviceApiService { get; set; }

        [AdminPermission(PermissionCustomMode.Ignore)]
        public ActionResult GetDeviceDatas()
        {
            var id = Request["id"].GetInt(0);
            var dd = DeviceService.GetLastestData(id);
            return Json(dd, JsonRequestBehavior.AllowGet);
        }

        [AdminPermission(PermissionCustomMode.Ignore)]
        public ActionResult GetDeviceCurLibraryList()
        {
            var id = Request["id"].GetInt(0);
            var list = DeviceService.GetDeviceCureLibraryList(id);

             //list.ForEach(r=> { r.Device = null; });
                 //var query = from q in list
                 //    select new
                 //    {
                 //        Text=q.Name,
                 //        Value=q.Code
                 //    };
                 return Json(list, JsonRequestBehavior.AllowGet);
        }


        [AdminPermission(PermissionCustomMode.Ignore)]
        public ActionResult GetTimeSpanList()
        {
            var id = Request["id"].GetInt(0);
            var tsid = Request["tsid"].GetInt(0);
            var list = DeviceService.GetTimeSpanList(id, tsid);

            //var query = from q in list
            //    select new
            //    {
            //        Text=q.Name,
            //        Value=q.Code
            //    };
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [AdminPermission(PermissionCustomMode.Ignore)]
        public ActionResult GetDeviceHistories()
        {
            var id = Request["id"].GetInt(0);
            var type = (HistoryTypeCollect)Request["type"].GetInt(0);
            var dd = DeviceService.GetHistoryModels(id, type);
            return Json(dd, JsonRequestBehavior.AllowGet);
        }

        [AdminPermission(PermissionCustomMode.Ignore)]
        public ActionResult GetDeviceCureLibraries()
        {
            var id = Request["id"].GetInt(0);

            var dd = DeviceCureLibraryService.GetDeviceCureLibraries(id);
            return Json(dd, JsonRequestBehavior.AllowGet);
        }


        [AdminPermission(PermissionCustomMode.Ignore)]
        [HttpPost]
        public ActionResult SyncTime()
        {
            var scode = Request["scode"];

            var dd = DeviceApiService.SyncTime(scode);
            return Json(dd);
        }

        [AdminPermission(PermissionCustomMode.Ignore)]
        [HttpPost]
        public ActionResult UpdateOutTemData()
        {
            var scode = Request["scode"];

            var dd = DeviceApiService.UpdateOutTemData(scode);
            return Json(dd);
        }

        [AdminPermission(PermissionCustomMode.Ignore)]
        [HttpPost]
        public ActionResult SetTimeSpan()
        {
            var scode = Request["scode"];
            var sid = Request["sid"].GetInt(0);
            var tsp = (Request["tst"].GetInt(0));
            var dd = DeviceApiService.SetTimeSpan(scode, sid, tsp);
            return Json(dd);
        }

        [AdminPermission(PermissionCustomMode.Ignore)]
        [HttpPost]
        public ActionResult SetWorkMode()
        {
            var scode = Request["scode"];
            var sid = Request["sid"].GetInt(0);
            var tsp = (Request["tst"].GetInt(0));
            var dd = DeviceApiService.SetWorkMode(scode, sid, tsp);
            return Json(dd);
        }



        [AdminPermission(PermissionCustomMode.Ignore)]
        [HttpPost]
        public ActionResult SetDeviceCurveLibrary()
        {
            var scode = Request["scode"];
            var sid = Request["sid"].GetInt(0);
           
            var dd = DeviceApiService.SetDeviceCurveLibrary(scode, sid);
            return Json(dd);
        }

        [AdminPermission(PermissionCustomMode.Ignore)]
        [HttpPost]
        public ActionResult UpdateWorkMode()
        {
            var scode = Request["scode"];

            var dd = DeviceApiService.UpdateWorkMode(scode);
            return Json(dd);
        }

        [AdminPermission(PermissionCustomMode.Ignore)]
        [HttpPost]
        public ActionResult UpdateValve()
        {
            var scode = Request["scode"];

            var dd = DeviceApiService.UpdateValve(scode);
            return Json(dd);
        }

        [AdminPermission(PermissionCustomMode.Ignore)]
        [HttpPost]
        public ActionResult UpdateFix()
        {
            var scode = Request["scode"];

            var dd = DeviceApiService.UpdateFix(scode);
            return Json(dd);
        }

    }
}