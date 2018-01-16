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
using QuickRMS.Site.Models.Device;
using Newtonsoft.Json;

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
        public ActionResult GetDeviceCurLibraryDataList()
        {
            var id = Request["id"].GetInt(0);
            var list = DeviceService.GetDeviceCureLibraryDataList(id);

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
        public ActionResult GetDeviceCurveData(int curid)
        {
            //var id = Request["id"].GetInt(0);

            var dd = DeviceService.GetDeviceCureData(curid);
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


        //new
        /// <summary>
        /// 更新整体参数
        /// </summary>
        /// <param name="scode"></param>
        /// <param name="clientdata"></param>
        /// <returns></returns>
        [AdminPermission(PermissionCustomMode.Ignore)]
        [HttpPost]
        public ActionResult UpdateData(string scode, dynamic clientdata = null)
        {
            //var scode = Request["scode"];

            var dd = DeviceApiService.UpdateData(scode, clientdata);
            return Json(dd);
        }

        /// <summary>
        /// 保存下发整体参数
        /// </summary>
        /// <param name="scode"></param>
        /// <param name="clientdata"></param>
        /// <returns></returns>
        [AdminPermission(PermissionCustomMode.Ignore)]
        [HttpPost]
        public ActionResult SaveAndSendData(string scode, DeviceParaDto clientdata = null)
        {
            //var scode = Request["scode"];
            var dd = DeviceApiService.SaveAndSendData(scode, clientdata);
            return Json(dd);
        }

        /// <summary>
        /// 更新时间段设置
        /// </summary>
        /// <param name="scode"></param>
        /// <param name="clientdata"></param>
        /// <returns></returns>
        [AdminPermission(PermissionCustomMode.Ignore)]
        [HttpPost]
        public ActionResult UpdateTimeSpan(string scode, int timespantype)
        {
            //var scode = Request["scode"];
            var dd = DeviceApiService.UpdateTimeSpan(scode, timespantype);
            return Json(dd);
        }


        [AdminPermission(PermissionCustomMode.Ignore)]
        [HttpPost]
        public ActionResult UpdateHistory(string scode, int cmbHistoryType, int nuRowNumber)
        //public ActionResult UpdateHistory(dynamic clientdata)
        {
            //var scode = Request["scode"];
            //var scode = "88888888";

            //var strName = Convert.ToString(clientdata.NAME);
            //var ss = clientdata.NAME;
            //dynamic data = JsonConvert.DeserializeObject(clientdata);
            var dd = DeviceApiService.UpdateHistory(scode, cmbHistoryType, nuRowNumber);
            return Json(dd);
        }


        [AdminPermission(PermissionCustomMode.Ignore)]
        [HttpPost]
        public ActionResult AddTimeSpan(int deviceid, int timespantype, string StartTime, string EndTime, int CurveCode)
        {
            var dd = DeviceApiService.AddTimeSpan(deviceid, timespantype, StartTime, EndTime, CurveCode);
            return Json(dd);
        }

        [AdminPermission(PermissionCustomMode.Ignore)]
        [HttpPost]
        public ActionResult SaveTimeSpan(int timespanid, string StartTime, string EndTime, int CurveCode)
        {
            var dd = DeviceApiService.SaveTimeSpan(timespanid, StartTime, EndTime, CurveCode);
            return Json(dd);
        }

        [AdminPermission(PermissionCustomMode.Ignore)]
        [HttpPost]
        public ActionResult DeleteTimeSpan(int timespanid)
        {
            var dd = DeviceApiService.DeleteTimeSpan(timespanid);
            return Json(dd);
        }


        [AdminPermission(PermissionCustomMode.Ignore)]
        [HttpPost]
        public ActionResult UpdateDeviceCurveLibrary(string scode)
        {
            var dd = DeviceApiService.UpdateDeviceCurveLibrary(scode);
            return Json(dd);
        }


        [AdminPermission(PermissionCustomMode.Ignore)]
        [HttpPost]
        public ActionResult SaveAndSendDeviceCurveLibrary(string scode, int deviceId)
        {
            var dd = DeviceApiService.SaveAndSendDeviceCurveLibrary(scode, deviceId);
            return Json(dd);
        }

    }
}