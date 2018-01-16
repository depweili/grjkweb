using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
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
using QuickRMS.Site.Models.Authen.Device;
using System.Linq.Expressions;
using Quick.Framework.Tool;
using System.Web;
using QuickRMS.Site.Models.File;
using NPOI.XSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;

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

         [AdminLayout]
        public ActionResult Index()
        {
            DeviceViewModel model = new DeviceViewModel();
            var areaList = AreaService.Areas.Where(r => r.IsDeleted == null || !r.IsDeleted.Value).ToList();

            foreach (var area in areaList)
            {
                var item = new SelectListItem
                {
                    Text = area.Name,
                    Value = area.Id.GetString()
                };
                model.Search.AreaList.Add(item);

            }
            return View(model);
        }



        [AdminPermission(PermissionCustomMode.Ignore)]
        public ActionResult List(DataTableParameter param)
        {
            int total = DeviceService.Devices.Count(t => t.IsDeleted == null || t.IsDeleted.Value == false);
          
            //构建查询表达式
            var expr = BuildSearchCriteria();

            var filterResult = DeviceService.Devices.Where(expr).Select(t => new DeviceViewModel
            {
                DeviceCode = t.DeviceCode,
                DeviceName = t.DeviceCode,
                Port = t.Port,
                AreaId = t.AreaId,
                AreaName=(t.Area==null?"":t.Area.Name),
                InstallTime = t.InstallTime,
                Longitude = t.Longitude,
                Latitude = t.Latitude,
                Address = t.Address,
                Company = t.Company,
                Id = t.Id
            }).OrderByDescending(t => t.Id).Skip(param.iDisplayStart).Take(param.iDisplayLength).ToList();

            int sortId = param.iDisplayStart + 1;

            var result = from c in filterResult
                         select new[]
                {
                    sortId++.ToString(),
                   c.DeviceCode,
                   c.DeviceName,
                    c.Port.GetString(),
                    c.AreaName.GetString(),
                    c.InstallTime.GetDateString(),
                    c.Longitude.GetString(),
                    c.Latitude.GetString(),
                    c.Address,
                    c.Company,
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
        private Expression<Func<Device, Boolean>> BuildSearchCriteria()
        {
            DynamicLambda<Device> bulider = new DynamicLambda<Device>();
            Expression<Func<Device, Boolean>> expr = null;

            if (!string.IsNullOrEmpty(Request["DeviceCode"]))
            {
                var data = Request["DeviceCode"].Trim();
                Expression<Func<Device, Boolean>> tmp = t => t.DeviceCode.Contains(data);
                expr = bulider.BuildQueryAnd(expr, tmp);
            }

            if (!string.IsNullOrEmpty(Request["Name"]))
            {
                var data = Request["Name"].Trim();
                Expression<Func<Device, Boolean>> tmp = t => t.DeviceName.Contains(data);
                expr = bulider.BuildQueryAnd(expr, tmp);
            }

            if (!string.IsNullOrEmpty(Request["AreaList"]))
            {
                var data = Request["AreaList"].GetInt(0);
                if (data > 0)
                {
                    Expression<Func<Device, Boolean>> tmp = t => t.AreaId==data;
                    expr = bulider.BuildQueryAnd(expr, tmp);
                }
            }

            Expression<Func<Device, Boolean>> tmpSolid = (t => t.IsDeleted == null || t.IsDeleted.Value == false);
            expr = bulider.BuildQueryAnd(expr, tmpSolid);

            return expr;
        }

        #endregion

        public ActionResult Create(int? pId)
        {
            var model = new DeviceViewModel();
            var areaList = AreaService.Areas.Where(r => r.IsDeleted == null || !r.IsDeleted.Value).ToList();
          
            foreach (var area in areaList)
            {
                var item = new SelectListItem
                {
                    Text = area.Name,
                    Value = area.Id.GetString()
                };
                model.AreaList.Add(item);
            }
           
            return PartialView(model);
        }

        [HttpPost]
        [AdminOperateLog]
        public ActionResult Create(DeviceViewModel model)
        {
            if (ModelState.IsValid)
            {

                OperationResult result = DeviceService.Insert(model);
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
            var model = new DeviceViewModel();
            var entity = DeviceService.Devices.FirstOrDefault(t => t.Id == Id);
            if (null != entity)
            {
                model = new DeviceViewModel
                {
                    Id = entity.Id,
                    DeviceName = entity.DeviceName,
                    DeviceCode = entity.DeviceCode,
                    IP = entity.IP,
                    Port = entity.Port,
                    AreaId = entity.AreaId,
                    InstallTime = entity.InstallTime,
                    InstallDate=entity.InstallTime.GetDateString(),
                    Memo = entity.Memo,
                    IsInited = entity.IsInited,
                    Longitude = entity.Longitude,
                    Latitude = entity.Latitude,
                    Company = entity.Company,
                    Address = entity.Address,
                };
               

            }

            var areaList = AreaService.Areas.Where(r => r.IsDeleted == null || !r.IsDeleted.Value).ToList();

            foreach (var area in areaList)
            {
                var item = new SelectListItem
                {
                    Text = area.Name,
                    Value = area.Id.GetString()
                };
                if (area.Id == model.AreaId)
                {
                    item.Selected = true;
                }
                model.AreaList.Add(item);
            }
            return PartialView(model);
        }

        [HttpPost]
        [AdminOperateLog]
        public ActionResult Edit(DeviceViewModel model)
        {
            if (ModelState.IsValid)
            {

                OperationResult result = DeviceService.Update(model);
               
                    return Json(result);
               
            }
            else
            {
              
                return PartialView(model);
            }
        }



        [AdminOperateLog]
        public ActionResult Delete(int Id)
        {
            OperationResult result = DeviceService.Delete(Id);
            return Json(result);
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
            var model = new DeviceMapViewModel();
            model.Device = DeviceService.Devices.FirstOrDefault(r => r.Id == id);
            model.DeviceData = DeviceService.DeviceDatas.Where(r => r.DeviceId == id).OrderByDescending(rr => rr.DataTime).FirstOrDefault();
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
            var data = DeviceService.GetDeviceCureLibraryDataList(id);
            return PartialView(data);
            //return Json(data, JsonRequestBehavior.AllowGet);
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
            var date = DateTime.Now;
            var mid = Request["mid"].GetInt(0);
            var memo = Request["memo"];
            var strdate = Request["maintenanceDate"];

            if (string.IsNullOrEmpty(strdate))
            {
                date = strdate.GetDate();
            }

            var data = DeviceService.UpdateOrCreateDeviceMaintenance(id, memo, mid, date);
            
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

          [AdminPermission(PermissionCustomMode.Ignore)]
        public ActionResult UpLoad()
        {
            return PartialView();
        }

        [HttpPost]
        [AdminPermission(PermissionCustomMode.Ignore)]
        public ActionResult UpLoad(HttpPostedFileBase fileData)
        {
            try
            {
                var result = new OperationResult(OperationResultType.Success);
                if (ModelState.IsValid)
                {
                    if (fileData != null && fileData.ContentLength > 0)
                    {
                        var insurance = new FileManageModel
                        {
                            FileName = System.IO.Path.GetFileName(fileData.FileName),
                            FileType = FileType.设备曲线库,
                            ContentType = fileData.ContentType
                        };
                       
                        ExcelHelper excelHelper = new ExcelHelper();
                        var dt = excelHelper.ImportExcelFileToDatatable(fileData.InputStream);
                        result = DeviceService.UploadDeviceCurveLibraries(dt, 0);//不传入设备id，取excel列
                        var fileName = Guid.NewGuid().ToString();
                        string filePath = string.Format(@"{0}Files/temp/{1}.xlsx",
                            AppDomain.CurrentDomain.BaseDirectory, fileName);

                        excelHelper.RenderDataTableToExcelFile(dt, filePath);
                        result.AppendData = string.Format(@"/Files/temp/{0}.xlsx", fileName);
                    }


                    return Json(result);
                }
                var error = new OperationResult(OperationResultType.Error);
                return Json(error);
            }
            catch (Exception ex)
            {
                var result = new OperationResult(OperationResultType.Error);
                result.Message = ex.Message;
                return Json(result);
            }

        }


        [AdminPermission(PermissionCustomMode.Ignore)]
        public FileResult GetMiFileStream()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "Files/templates/设备曲线库.xlsx";

            //构建查询表达式
            var deviceId = Request["deviceId"].GetInt(0);
            var device = DeviceService.Devices.FirstOrDefault(r => r.Id == deviceId);


            XSSFWorkbook workBook;
            using (var fs = new FileStream(filePath, FileMode.Open))
            {
                workBook = new XSSFWorkbook(fs);
            }

            var sheet = workBook.GetSheetAt(0);

            var rowStart = 1;
            if (device != null)
            {
                var deviceCode = device.DeviceCode;

                var filterResult = DeviceService.GetDeviceCureLibraryDataList(deviceId);


                foreach (var lp in filterResult)
                {
                    IRow row = sheet.CreateRow(rowStart);

                    #region 字段赋值
                    row.CreateCell(0).SetCellValue(rowStart.ToString());
                    row.CreateCell(1).SetCellValue(deviceCode);
                    row.CreateCell(2).SetCellValue(lp.Code);
                    row.CreateCell(3).SetCellValue(lp.Name);

                    row.CreateCell(4).SetCellValue(lp.Column1.GetString());
                    row.CreateCell(5).SetCellValue(lp.Column2.GetString());
                    row.CreateCell(6).SetCellValue(lp.Column3.GetString());
                    row.CreateCell(7).SetCellValue(lp.Column4.GetString());
                    row.CreateCell(8).SetCellValue(lp.Column5.GetString());
                    row.CreateCell(9).SetCellValue(lp.Column6.GetString());
                    row.CreateCell(10).SetCellValue(lp.Column7.GetString());
                    row.CreateCell(11).SetCellValue(lp.Column8.GetString());
                    row.CreateCell(12).SetCellValue(lp.Column9.GetString());
                    row.CreateCell(13).SetCellValue(lp.Column10.GetString());
                    row.CreateCell(14).SetCellValue(lp.Column11.GetString());
                    row.CreateCell(15).SetCellValue(lp.Column12.GetString());
                    row.CreateCell(16).SetCellValue(lp.Column13.GetString());
                    row.CreateCell(17).SetCellValue(lp.Column14.GetString());
                    row.CreateCell(18).SetCellValue(lp.Column15.GetString());
                    row.CreateCell(19).SetCellValue(lp.Column16.GetString());
                    row.CreateCell(20).SetCellValue(lp.Column17.GetString());
                    row.CreateCell(21).SetCellValue(lp.Column18.GetString());
                    row.CreateCell(22).SetCellValue(lp.Column19.GetString());
                    row.CreateCell(23).SetCellValue(lp.Column20.GetString());
                    row.CreateCell(24).SetCellValue(lp.Column21.GetString());
                    row.CreateCell(25).SetCellValue(lp.Column22.GetString());
                    row.CreateCell(26).SetCellValue(lp.Column23.GetString());
                    row.CreateCell(27).SetCellValue(lp.Column24.GetString());
                    row.CreateCell(28).SetCellValue(lp.Column25.GetString());
                    row.CreateCell(29).SetCellValue(lp.Column26.GetString());
                    row.CreateCell(30).SetCellValue(lp.Column27.GetString());
                    row.CreateCell(31).SetCellValue(lp.Column28.GetString());
                    row.CreateCell(32).SetCellValue(lp.Column29.GetString());
                    row.CreateCell(33).SetCellValue(lp.Column30.GetString());
                    row.CreateCell(34).SetCellValue(lp.Column31.GetString());
                    row.CreateCell(35).SetCellValue(lp.Column32.GetString());
                    row.CreateCell(36).SetCellValue(lp.Column33.GetString());
                    row.CreateCell(37).SetCellValue(lp.Column34.GetString());
                    row.CreateCell(38).SetCellValue(lp.Column35.GetString());
                    row.CreateCell(39).SetCellValue(lp.Column36.GetString());
                    row.CreateCell(40).SetCellValue(lp.Column37.GetString());
                    row.CreateCell(41).SetCellValue(lp.Column38.GetString());
                    row.CreateCell(42).SetCellValue(lp.Column39.GetString());
                    row.CreateCell(43).SetCellValue(lp.Column40.GetString());
                    row.CreateCell(44).SetCellValue(lp.Column41.GetString());
                    row.CreateCell(45).SetCellValue(lp.Column42.GetString());
                    row.CreateCell(46).SetCellValue(lp.Column43.GetString());
                    row.CreateCell(47).SetCellValue(lp.Column44.GetString());
                    row.CreateCell(48).SetCellValue(lp.Column45.GetString());
                    row.CreateCell(49).SetCellValue(lp.Column46.GetString());
                    row.CreateCell(50).SetCellValue(lp.Column47.GetString());
                    row.CreateCell(51).SetCellValue(lp.Column48.GetString());
                    row.CreateCell(52).SetCellValue(lp.Column49.GetString());
                    row.CreateCell(53).SetCellValue(lp.Column50.GetString());
                    row.CreateCell(54).SetCellValue(lp.Column51.GetString());
                    row.CreateCell(55).SetCellValue(lp.Column52.GetString());
                    row.CreateCell(56).SetCellValue(lp.Column53.GetString());
                    row.CreateCell(57).SetCellValue(lp.Column54.GetString());
                    row.CreateCell(58).SetCellValue(lp.Column55.GetString());
                    row.CreateCell(59).SetCellValue(lp.Column56.GetString());
                    row.CreateCell(60).SetCellValue(lp.Column57.GetString());
                    row.CreateCell(61).SetCellValue(lp.Column58.GetString());
                    row.CreateCell(62).SetCellValue(lp.Column59.GetString());
                    row.CreateCell(63).SetCellValue(lp.Column60.GetString());
                    row.CreateCell(64).SetCellValue(lp.Column61.GetString());
                    row.CreateCell(65).SetCellValue(lp.Column62.GetString());
                    row.CreateCell(66).SetCellValue(lp.Column63.GetString());
                    row.CreateCell(67).SetCellValue(lp.Column64.GetString());
                    row.CreateCell(68).SetCellValue(lp.Column65.GetString());
                    row.CreateCell(69).SetCellValue(lp.Column66.GetString());
                    row.CreateCell(70).SetCellValue(lp.Column67.GetString());
                    row.CreateCell(71).SetCellValue(lp.Column68.GetString());
                    row.CreateCell(72).SetCellValue(lp.Column69.GetString());
                    row.CreateCell(73).SetCellValue(lp.Column70.GetString());
                    row.CreateCell(74).SetCellValue(lp.Column71.GetString());
                    row.CreateCell(75).SetCellValue(lp.Column72.GetString());
                    row.CreateCell(76).SetCellValue(lp.Column73.GetString());
                    row.CreateCell(77).SetCellValue(lp.Column74.GetString());
                    row.CreateCell(78).SetCellValue(lp.Column75.GetString());
                    row.CreateCell(79).SetCellValue(lp.Column76.GetString());
                    row.CreateCell(80).SetCellValue(lp.Column77.GetString());
                    row.CreateCell(81).SetCellValue(lp.Column78.GetString());
                    row.CreateCell(82).SetCellValue(lp.Column79.GetString());
                    row.CreateCell(83).SetCellValue(lp.Column80.GetString());
                    row.CreateCell(84).SetCellValue(lp.Column81.GetString());
                    row.CreateCell(85).SetCellValue(lp.Column82.GetString());
                    row.CreateCell(86).SetCellValue(lp.Column83.GetString());
                    row.CreateCell(87).SetCellValue(lp.Column84.GetString());
                    row.CreateCell(88).SetCellValue(lp.Column85.GetString());
                    row.CreateCell(89).SetCellValue(lp.Column86.GetString());
                    row.CreateCell(90).SetCellValue(lp.Column87.GetString());
                    row.CreateCell(91).SetCellValue(lp.Column88.GetString());
                    row.CreateCell(92).SetCellValue(lp.Column89.GetString());
                    row.CreateCell(93).SetCellValue(lp.Column90.GetString());
                    row.CreateCell(94).SetCellValue(lp.Column91.GetString());
                    row.CreateCell(95).SetCellValue(lp.Column92.GetString());
                    row.CreateCell(96).SetCellValue(lp.Column93.GetString());
                    row.CreateCell(97).SetCellValue(lp.Column94.GetString());
                    row.CreateCell(98).SetCellValue(lp.Column95.GetString());
                    row.CreateCell(99).SetCellValue(lp.Column96.GetString());
                    row.CreateCell(100).SetCellValue(lp.Column97.GetString());
                    row.CreateCell(101).SetCellValue(lp.Column98.GetString());
                    row.CreateCell(102).SetCellValue(lp.Column99.GetString());
                    row.CreateCell(103).SetCellValue(lp.Column100.GetString());
                    row.CreateCell(104).SetCellValue(lp.Column101.GetString());
                    row.CreateCell(105).SetCellValue(lp.Column102.GetString());
                    row.CreateCell(106).SetCellValue(lp.Column103.GetString());
                    row.CreateCell(107).SetCellValue(lp.Column104.GetString());
                    row.CreateCell(108).SetCellValue(lp.Column105.GetString());
                    row.CreateCell(109).SetCellValue(lp.Column106.GetString());
                    row.CreateCell(110).SetCellValue(lp.Column107.GetString());
                    row.CreateCell(111).SetCellValue(lp.Column108.GetString());
                    row.CreateCell(112).SetCellValue(lp.Column109.GetString());
                    row.CreateCell(113).SetCellValue(lp.Column110.GetString());
                    row.CreateCell(114).SetCellValue(lp.Column111.GetString());
                    row.CreateCell(115).SetCellValue(lp.Column112.GetString());
                    row.CreateCell(116).SetCellValue(lp.Column113.GetString());
                    row.CreateCell(117).SetCellValue(lp.Column114.GetString());
                    row.CreateCell(118).SetCellValue(lp.Column115.GetString());
                    row.CreateCell(119).SetCellValue(lp.Column116.GetString());
                    row.CreateCell(120).SetCellValue(lp.Column117.GetString());
                    row.CreateCell(121).SetCellValue(lp.Column118.GetString());
                    row.CreateCell(122).SetCellValue(lp.Column119.GetString());
                    row.CreateCell(123).SetCellValue(lp.Column120.GetString());
                    row.CreateCell(124).SetCellValue(lp.Column121.GetString());



                    #endregion

                    rowStart++;
                }
            }

            var ms = new MemoryStream();

            //ms.Seek(0, SeekOrigin.Begin);
            using (var exportData = new MemoryStream())
            {
                workBook.Write(exportData);
                string strdate = DateTime.Now.ToString("yyyyMMddhhmmss");//
                byte[] bytes = exportData.ToArray();
                return File(bytes, "application/vnd.ms-excel", strdate + "设备曲线库.xlsx");
            }




        }
    }
}