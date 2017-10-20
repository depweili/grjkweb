using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Quick.Site.Common.Models;
using QuickRMS.Core.Service;
using QuickRMS.Core.Service.Authen;
using QuickRMS.Domain.Models;
using QuickRMS.Domain.Models.DeviceInfo;
using QuickRMS.Site.Models.Device;
using QuickRMS.Site.WebUI.Common;
using QuickRMS.Site.WebUI.Extension.Filters;

namespace QuickRMS.Site.WebUI.Areas.Authen.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DeviceController : AdminController
    {

        [Import]
        public IAreaService AreaService { get; set; }

        [Import]
        public IDeviceService DeviceService { get; set; }

        [Import]
        public IHistoryService HistoryService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        [AdminLayout]
        public ActionResult DeviceManage()
        {
            var now = DateTime.Now;
            ViewBag.Now = now.ToString("yyyy-MM-dd HH:mm:ss");
            return View();
        }

        [AdminLayout]
        public ActionResult SystemState()
        {
            var now = Json(DateTime.Now);
            ViewBag.Now = now.Data.GetString();
            return View();
        }
        [AdminLayout]
        public ActionResult TimePeriodSetting()
        {
           
            return View();
        }
        [AdminLayout]
        public ActionResult ModeSetting()
        {
            return View();
        }
        [AdminLayout]
        public ActionResult ValveSetting()
        {
            return View();
        }
        [AdminLayout]
        public ActionResult TemperatureSetting()
        {
            return View();
        }

        [AdminLayout]
        public ActionResult  TerminalSetting()
        {
            return View();
        }

        [AdminLayout]
        public ActionResult DeviceMap()
        {
            return View();
        }

         [AdminPermission(PermissionCustomMode.Ignore)]
        public ActionResult DeviceInfo(int id)
        {
             var model=new Device();
             model = DeviceService.Devices.FirstOrDefault(r => r.Id == id);
             return PartialView(model);
        }

        [AdminPermission(PermissionCustomMode.Ignore)]
        public ActionResult DeviceHistoryInfo(int id)
        {
            var hd = HistoryService.Histories.FirstOrDefault(r => r.Id == id);
            var Data = hd.Data;
            var type = hd.HistoryType;
            #region h0

            if (type == HistoryTypeCollect.正常记录)
            {
                var model = new HistoryInfo0Model();
                if (Data.Length > 0)
                {
                    model.Type = ((HistoryNormalType) Data[0]).ToString();
                    model.Time = Utilities.GetDateTimeValue(Data, 1).ToString("yyyy-MM-dd HH:mm:ss");
                    model.Out = Utilities.GetDecimalValue(Data, 9, 8).ToString();
                    model.Sup1 = Utilities.GetDecimalValue(Data, 11, 10).ToString();
                    model.Sup2 = Utilities.GetDecimalValue(Data, 13, 12).ToString();
                    model.Back1 = Utilities.GetDecimalValue(Data, 15, 14).ToString();
                    model.Back2 = Utilities.GetDecimalValue(Data, 17, 16).ToString();
                    model.Fix1 = Utilities.GetDecimalValue(Data, 19, 18).ToString();
                    model.Fix2 = Utilities.GetDecimalValue(Data, 21, 20).ToString();
                    model.ValveA = Data[22].ToString();
                    model.ValveB = Data[23].ToString();
                    model.Wire = ((WirelessState) Data[24]).ToString();
                    model.WorkMode = ((WorkMode) Data[25]).ToString();
                    model.Net1 = ((WaterNetStatus) Data[26]).ToString();
                    model.Net2 = ((WaterNetStatus) Data[27]).ToString();
                    model.Curve = Data[28].ToString();
                    model.Ref1 = Utilities.GetDecimalValue(Data, 30, 29).Value.ToString("0.00");
                    model.Ref2 = Utilities.GetDecimalValue(Data, 32, 31).Value.ToString("0.00");
                    model.Ref3 = Utilities.GetDecimalValue(Data, 34, 32).Value.ToString("0.00");
                    model.Ref4 = Utilities.GetDecimalValue(Data, 36, 35).Value.ToString("0.00");
                    model.Ref5 = Utilities.GetDecimalValue(Data, 38, 37).Value.ToString("0.00");
                    model.Ref6 = Utilities.GetDecimalValue(Data, 40, 39).Value.ToString("0.00");
                }

                 return PartialView("DeviceHistoryInfo0", model);
            }

            #endregion

            #region h1

            if (type == HistoryTypeCollect.参数修改记录)
            {
                var model1 = new HistoryInfo1Model();
                if (Data.Length > 0)
                {
                    model1.Time = Utilities.GetDateTimeValue(Data, 0).ToString("yyyy-MM-dd HH:mm:ss");
                    model1.WorkMode = ((WorkMode) Data[7]).ToString();
                    model1.Min1 = Data[8].ToString();
                    model1.Max1 = Data[9].ToString();
                    model1.Min2 = Data[10].ToString();
                    model1.Max2 = Data[11].ToString();
                    model1.AInterval = Utilities.GetIntValue(Data, 13, 12).ToString();
                    model1.BInterval = Utilities.GetIntValue(Data, 15, 14).ToString();
                    model1.AStep1 = Data[16].ToString();
                    model1.AStep2 = Data[17].ToString();
                    model1.AStep3 = Data[18].ToString();
                    model1.AStep4 = Data[19].ToString();
                    model1.AStep5 = Data[20].ToString();
                    model1.BStep1 = Data[21].ToString();
                    model1.BStep2 = Data[22].ToString();
                    model1.BStep3 = Data[23].ToString();
                    model1.BStep4 = Data[24].ToString();
                    model1.BStep5 = Data[25].ToString();
                    model1.AMaxStep = Data[26].ToString();
                    model1.BMaxStep = Data[27].ToString();
                    model1.ASteer = (Data[28] == 0 ? "正转" : "反转");
                    model1.BSteer = (Data[29] == 0 ? "正转" : "反转");
                    model1.Wire = ((WirelessState) Data[30]).ToString();
                    model1.Out = Utilities.GetDecimalValue(Data, 32, 31).ToString();
                    model1.Sup1 = Utilities.GetDecimalValue(Data, 34, 33).ToString();
                    model1.Sup2 = Utilities.GetDecimalValue(Data, 36, 35).ToString();
                    model1.Back1 = Utilities.GetDecimalValue(Data, 38, 37).ToString();
                    model1.Back2 = Utilities.GetDecimalValue(Data, 40, 39).ToString();
                    model1.Fix1 = Utilities.GetDecimalValue(Data, 42, 41).ToString();
                    model1.Fix2 = Utilities.GetDecimalValue(Data, 44, 43).ToString();
                    model1.Save = Utilities.GetIntValue(Data, 46, 45).ToString();
                    model1.CtrlNumber = Data[47].ToString();
                }
                  return PartialView("DeviceHistoryInfo1", model1);
            }

            #endregion

            #region h2

            if (type == HistoryTypeCollect.模式曲线修改记录)
            {
                var model2 = new HistoryInfo2Model();
                model2.HistoryInfo2List = new List<HistoryInfo2>();
                if (Data.Length > 0)
                {
                    model2.Time = Utilities.GetDateTimeValue(Data, 0).ToString("yyyy-MM-dd HH:mm:ss");
                    model2.Mode = ((TimeSpanType) Data[7]).ToString();
                    string startTime = "";
                    int curveCode = -1;
                    string endTime = "";

                    int timeSpanID = Data[7];

                    HistoryInfo2 info = new HistoryInfo2();

                    //先读取第一组
                  
                    info.TimeSpanID = timeSpanID;
                    info.DeviceID = 0;

                    curveCode = Data[10];
                    startTime = string.Format("{0:00}:{1:00}", Data[8], Data[9]);
                    endTime = string.Format("{0:00}:{1:00}", Data[11], Data[12]);

                    info.CurveCode = curveCode;
                    info.StartTime = startTime;
                    info.EndTime = endTime;

                    model2.HistoryInfo2List.Add(info);

                    string previousEndTime = endTime;
                    //读余下几组
                    for (int i = 13; i < 75; i = i + 3)
                    {
                        if (Data[i] == 255)
                            break;

                        var info1 = new HistoryInfo2();

                        info1.TimeSpanID = timeSpanID;
                        info1.DeviceID = 0;
                        curveCode = Data[i];
                        startTime = previousEndTime;

                        if (Data[i + 1] > 23 || Data[i + 2] > 59)
                        {
                            endTime = null;
                        }
                        else
                        {
                            endTime = string.Format("{0:00}:{1:00}", Data[i + 1], Data[i + 2]);
                        }

                        info1.CurveCode = curveCode;
                        info1.StartTime = startTime;
                        info1.EndTime = endTime;
                        previousEndTime = endTime;
                        model2.HistoryInfo2List.Add(info);
                    }
                }
                 return PartialView("DeviceHistoryInfo2", model2);
            }

            #endregion

            #region h3

            if (type == HistoryTypeCollect.温度曲线修改记录)
            {
                var model3 = new HistoryInfo3Model();
                model3.HistoryInfo3List = new List<HistoryInfo3>();
                if (Data.Length > 0)
                {
                    model3.Time = Utilities.GetDateTimeValue(Data, 0).ToString("yyyy-MM-dd HH:mm:ss");
                    model3.Num = Data[7].ToString();

                    for (int i = 0; i < 121; i++)
                    {
                        model3.HistoryInfo3List.Add(new HistoryInfo3
                        {
                            Value = Utilities.GetDecimalValue(Data, i*2 + 9, (i*2) + 8)
                        });
                    }
                }
                return PartialView("DeviceHistoryInfo3", model3);
            }

            #endregion

            return PartialView();
        }

        [AdminPermission(PermissionCustomMode.Ignore)]
        public ActionResult AreaDeviceTree()
        {
            var user = GetCurrentUser();
            var id = user == null ? 0 : user.Id;

            var nodes = AreaService.LoadAreaDeviceByUser(id);
            var treeData = JsonConvert.SerializeObject(nodes, Formatting.Indented,
                new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()});
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.Data = treeData;
            return result;

        }

        [AdminPermission(PermissionCustomMode.Ignore)]
        public ActionResult DeviceCureList(int id)
        {
            var data = DeviceService.GetDeviceCureLibraryList(id);
          return  PartialView(data);
        }


        [AdminPermission(PermissionCustomMode.Ignore)]
        public ActionResult DeviceMaintenances(int id)
        {
            var data = DeviceService.GetDeviceMaintenanceModels(id);         
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AdminPermission(PermissionCustomMode.Ignore)]
        public ActionResult UpdateOrCreateDeviceMaintenance(int id)
        {
            var mid = Request["mid"].GetInt(0);
            var memo = Request["memo"];
            
            var data = DeviceService.UpdateOrCreateDeviceMaintenance(id,memo,mid);
            
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AdminPermission(PermissionCustomMode.Ignore)]
        public ActionResult DeleteDeviceMaintenance()
        {
            var mid =  Request["mid"].Split(new char[]{','});


            var data = DeviceService.DeleteDeviceMaintenance(mid);
            
            return Json(data, JsonRequestBehavior.AllowGet);
        }




        [AdminPermission(PermissionCustomMode.Ignore)]
        public ActionResult GetValvesModel(int id)
        {
           
            var name = Request["name"];
            var data = DeviceService.GetValvesModel(id, name);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AdminPermission(PermissionCustomMode.Ignore)]
        public ActionResult UpdateValvesModel(int id)
        {
            var model = new Valves();
            var valve = Request["valve"];
            var workby = Request["workby"].GetInt(0);
            var workmodel = Request["workmodel"].GetInt(0);
            var ValveOpenValue = Request["ValveOpenValue"].GetInt(0);

            model.DeviceId = id;
            model.WorkBy = workby;
            model.Name = valve;
            model.WorkMode = workmodel;
            model.SetValue = ValveOpenValue;

            var data = DeviceService.UpdateValvesModel(model);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}