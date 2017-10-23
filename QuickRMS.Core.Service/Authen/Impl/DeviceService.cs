using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.Framework.Tool;
using QuickRMS.Core.Repository.DeviceInfo;
using QuickRMS.Core.Repository.DeviceInfo.Impl;
using QuickRMS.Core.Repository.SysConfig;
using QuickRMS.Domain.Models;
using QuickRMS.Domain.Models.DeviceInfo;
using QuickRMS.Domain.Models.SysConfig;
using QuickRMS.Site.Models.Device;
using RemoteHelper;
using Common;
using IEHNCS.Common;
using System.Reflection;
using System.Dynamic;
using System.Data;

namespace QuickRMS.Core.Service.Authen.Impl
{
    [Export(typeof (IDeviceService))]
    public class DeviceService : CoreServiceBase, IDeviceService
    {
        [Import]
        public IDeviceRepository DeviceRepository { get; set; }

        [Import]
        public IDeviceDataRepository DeviceDataRepository { get; set; }

        [Import]
        public IDeviceCureLibraryRepository DeviceCureLibraryRepository { get; set; }

        [Import]
        public IHistoryRepository HistoryRepository { get; set; }

        [Import]
        public IDeviceMaintenanceRepository DeviceMaintenanceRepository { get; set; }

        [Import]
        public ICurveLibraryRepository CurveLibraryRepository { get; set; }

        [Import]
        public ITimeSpanSettingRepository TimeSpanSettingRepository { get; set; }

        [Import]
        public IValvesRepository ValvesRepository { get; set; }

        public IQueryable<Device> Devices
        {
            get { return DeviceRepository.Entities; }
        }

        public IQueryable<DeviceData> DeviceDatas
        {
            get { return DeviceDataRepository.Entities; }
        }

        public IQueryable<DeviceCureLibrary> DeviceCureLibraries
        {
            get { return DeviceCureLibraryRepository.Entities; }
        }

        public IQueryable<History> Histories
        {
            get { return HistoryRepository.Entities; }
        }

        public IQueryable<TimeSpanSetting> TimeSpanSettings
        {
            get { return TimeSpanSettingRepository.Entities; }
        }
        public IQueryable<Valves> Valveses
        {
            get { return ValvesRepository.Entities; }
        }
        public IQueryable<DeviceMaintenance> DeviceMaintenances
        {
            get { return DeviceMaintenanceRepository.Entities; }
        }

        public IQueryable<CurveLibrary> CurveLibraries
        {
            get { return CurveLibraryRepository.Entities; }
        }
        
        public DeviceDataModel GetLastestData(int deviceId)
        {
            var data =
                DeviceDatas.Where(r => r.DeviceId == deviceId).OrderByDescending(r => r.DataTime).FirstOrDefault();
            var model = ConvertToModel(data);
            return model;
        }

        private DeviceDataModel ConvertToModel(DeviceData data)
        {
            if (data == null) return null;
            var device = data.Device;
            DeviceDataModel model = new DeviceDataModel
            {
                DeviceId = data.DeviceId,
                OutdoorTemp = data.OutdoorTemp,
                BackWaterTemp1 = data.BackWaterTemp1,
                BackWaterTemp2 = data.BackWaterTemp2,
                Data27 = data.Data27,
                Data28 = data.Data28,
                Data29 = data.Data29,
                Data30 = data.Data30,
                DataTime = data.DataTime,
                SupplyWaterTemp1 = data.SupplyWaterTemp1,
                SupplyWaterTemp2 = data.SupplyWaterTemp2,
                FixWaterTemp1 = data.FixWaterTemp1,
                FixWaterTemp2 = data.FixWaterTemp2,
                Valve1 = data.Valve1,
                Valve2 = data.Valve2,
                WirelessStatus = data.WirelessStatus,
                WorkStatus = data.WorkStatus,
                WaterNetStatus1 = data.WaterNetStatus1,
                WaterNetStatus2 = data.WaterNetStatus2,

                WaterNetStatus1Text = ((QuickRMS.Domain.Models.WaterNetStatus)data.WaterNetStatus1).ToString(),
                WaterNetStatus2Text = ((QuickRMS.Domain.Models.WaterNetStatus)data.WaterNetStatus2).ToString(),
              //  ControlChannelText = ((ValveControlChannel)device.CtrlNumber).ToString(),
            };

            return model;
        }





        public List<DeviceCureLibraryModel> GetDeviceCureLibraryList(int deviceId)
        {
            List<DeviceCureLibraryModel> result = new List<DeviceCureLibraryModel>();
            var data= DeviceCureLibraries.Where(r => r.DeviceId == deviceId).OrderBy(r => r.Code).ToList();
            foreach (var d in data)
            {
                DeviceCureLibraryModel model = new DeviceCureLibraryModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    DeviceId = d.DeviceId,
                    Code = d.Code

                };
                result.Add(model);
            }
            return result;
        }


        public List<HistoryModel> GetHistoryModels(int deviceId, HistoryTypeCollect type)
        {
            var data =
                Histories.Where(r => r.DeviceId == deviceId && r.HistoryType == type);
             List<HistoryModel> result=new List<HistoryModel>();
            foreach (var d in data.ToList())
            {
                var model = new HistoryModel
                {
                    Id = d.Id,
                    DeviceId = deviceId,
                    Data = d.Data,
                    HistoryTypeId = (int) d.HistoryType,
                    HistoryTypeName = d.HistoryType.GetString(),
                    RowNumber = d.RowNumber
                };
                result.Add(model);
            }
            return result;
        }








        public List<CurveLibrary> GetCurveLibraries()
        {
            throw new NotImplementedException();
        }

        public List<DeviceMaintenanceModel> GetDeviceMaintenanceModels(int deviceId)
        {
            List<DeviceMaintenanceModel> result = new List<DeviceMaintenanceModel>();
            var data = DeviceMaintenances.Where(r => r.DeviceId == deviceId).ToList();
            foreach (var d in data)
            {
                result.Add(new DeviceMaintenanceModel
                {
                    Id=d.Id,
                    MaintenanceDate=d.MaintenanceDate.ToString("yyyy-MM-dd hh:mm:ss"),
                    Memo=d.Memo,
                    DeviceId=d.DeviceId
                });
            }
            return result;
        }





        public List<TimeSpanSetting> GetTimeSpanList(int deviceId, int timeSpanId)
        {
            var data =
                TimeSpanSettings.Where(r => r.DeviceId == deviceId && r.TimeSpanID == timeSpanId);
            List<TimeSpanSetting> result = new List<TimeSpanSetting>();

            result = data.ToList();
            return result;
        }


        public int UpdateOrCreateDeviceMaintenance(int deviceId, string memo,int mid)
        {
            DeviceMaintenance model = new DeviceMaintenance
            {
                DeviceId = deviceId,
                Memo = memo,
                MaintenanceDate = DateTime.Now,
                IsDeleted = false,
                Id = mid
            };
            try
            {
                if (mid > 0)
                {
                    model = DeviceMaintenances.FirstOrDefault(r => r.Id == mid);
                    model.Memo = memo;
                    DeviceMaintenanceRepository.Update(model);
                }
                else
                {
                    DeviceMaintenanceRepository.Insert(model);
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
           
        }


        public int DeleteDeviceMaintenance(string[] mid)
        {
            try
            {
                for (var i = 0; i < mid.Length; i++)
                {
                    var id = mid[i].GetInt(0);
                    DeviceMaintenanceRepository.Delete(id);
                }
                    return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public Valves GetValvesModel(int deviceId, string name)
        {
            var data = Valveses.FirstOrDefault(r => r.DeviceId == deviceId && r.Name == name);
            if (data != null)
            {
                data.Device = null;
            }
            else
            {
                data = new Valves();
            }
            return data;
        }

        public int UpdateValvesModel(Valves model)
        {
            try
            {
                var entity = Valveses.FirstOrDefault(r => r.DeviceId == model.DeviceId && r.Name == model.Name);
                if (entity == null)
                {
                    ValvesRepository.Insert(model);
                }
                else
                {
                    entity.SetValue = model.SetValue;
                    entity.WorkMode = model.WorkMode;
                    entity.WorkBy = model.WorkBy;

                    var data = ValvesRepository.Update(entity);
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public dynamic DealCommandResult(RemoteResult res, dynamic clientdata=null)
        {
            dynamic resobj = null;
            try
            {
                if (res != null)
                {
                    CommandConst command = res.name;
                    switch (command)
                    {
                        case CommandConst.读取测量值:
                            resobj = DealCommand_dqclz(res);
                            break;
                        case CommandConst.读取参数:
                            resobj = DealCommand_dqcs(res, clientdata);
                            break;
                        case CommandConst.默认参数设定:
                            break;
                        case CommandConst.参数设定:
                            break;
                        case CommandConst.ARM控制指令:
                            resobj = DealCommand_arm(res);
                            break;
                        case CommandConst.读取历史数据:
                            break;
                        case CommandConst.FLASH测试:
                            break;
                        case CommandConst.读取终端ID:
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }

            return resobj;
        }

        /// <summary>
        /// 读取测量值
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        private dynamic DealCommand_dqclz(RemoteResult res)
        {
            try
            {
                var Datas = res.Datas;
                Device device = Devices.Single(t => t.DeviceCode == res.deviceCode);
                DeviceData data = new DeviceData();
                data.CreateTime = DateTime.Now;
                data.DeviceId = device.Id;

                int year = int.Parse("20" + Datas[0]);
                int month = Datas[1];
                int day = Datas[2];
                int week = Datas[3];
                int hour = Datas[4];
                int minutes = Datas[5];
                int second = Datas[6];
                DateTime dataTime = new DateTime(year, month, day, hour, minutes, second);
                data.DataTime = dataTime;

                data.OutdoorTemp = Utility.GetDecimalValue(Datas, 8, 7);
                data.BackWaterTemp1 = Utility.GetDecimalValue(Datas, 14, 13);
                data.BackWaterTemp2 = Utility.GetDecimalValue(Datas, 16, 15);
                data.FixWaterTemp1 = Utility.GetDecimalValue(Datas, 18, 17);
                data.FixWaterTemp2 = Utility.GetDecimalValue(Datas, 20, 19);
                data.SupplyWaterTemp1 = Utility.GetDecimalValue(Datas, 10, 9);
                data.SupplyWaterTemp2 = Utility.GetDecimalValue(Datas, 12, 11);
                data.Valve1 = Datas[21];
                data.Valve2 = Datas[22];
                data.WirelessStatus = Datas[23];
                data.WorkStatus = Datas[24];
                data.WaterNetStatus1 = Datas[25];
                data.WaterNetStatus2 = Datas[26];
                data.Data27 = new byte[] { Datas[27] };
                data.Data28 = new byte[] { Datas[28] };
                data.Data29 = new byte[] { Datas[29] };
                data.Data30 = new byte[] { Datas[30] };

                DeviceDataRepository.Insert(data);

                UnitOfWork.Commit();

                return ConvertToModel(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

        }

        /// <summary>
        /// 读取参数
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        private dynamic DealCommand_dqcs(RemoteResult res,dynamic clientdata)
        {
            try
            {
                dynamic resobj = new ExpandoObject();

                var Datas = res.Datas;

                Device device = Devices.Single(t => t.DeviceCode == res.deviceCode);
                Valves ValveA = null;
                Valves ValveB = null;

                if ((byte)Datas[0] >= (byte)Common.SettingType.曲线起始参数 && (byte)Datas[0] <= (byte)Common.SettingType.曲线终止参数)
                {
                    decimal? tmp = 0;
                    DeviceCureLibrary curveModel = null;
                    PropertyInfo prop = null;
                    Type type = typeof(DeviceCureLibrary);
                    int code = ((int)Datas[0] - 1);

                    DeviceCureLibraryRepository.Delete(DeviceCureLibraryRepository.Entities.Where(t=>t.Code==code&&t.DeviceId==device.Id));
                    curveModel = new DeviceCureLibrary()
                    {
                        Code = code,
                        Name = "曲线" + code.ToString(),
                        DeviceId = device.Id,
                    };
                    for (int i = 1; i <= 121; i++)
                    {
                        tmp = Utility.GetDecimalValue(Datas, (i * 2), (i * 2 - 1));
                        prop = type.GetProperty("Column" + i.ToString());
                        prop.SetValue(curveModel, tmp, null);
                    }
                    DeviceCureLibraryRepository.Insert(curveModel);
                    if (code == 30)
                    {
                        //treeView1.Enabled = true;
                        //ShowMessage("更新成功！");
                        //BindDevCurves();
                        resobj.IsBindDevCurves = true;
                        resobj.DevCurvesData = DeviceCureLibraries.Where(t => t.DeviceId == device.Id);
                    }
                    else {
                        resobj.IsBindDevCurves = false;
                    }
                }

                //工作模式及曲线和时间控制
                switch ((Common.SettingType)((int)Datas[0]))
                {
                    case Common.SettingType.整体参数:
                        #region 整体参数
                        //currentDevice.GotParameters = true;
                        //阀门控制路数
                        Common.ValveControlChannel vc = (Common.ValveControlChannel)Datas[41];
                        //lblControlChannel.Text = vc.ToString();
                        resobj.lblControlChannel = vc.ToString();
                        if (vc == Common.ValveControlChannel.A和B通路)
                        {
                            resobj.grpA = true;
                            resobj.grpB = true;
                        }
                        else if (vc == Common.ValveControlChannel.A通路)
                        {
                            resobj.grpA = true;
                            resobj.grpB = false;
                        }
                        else
                        {
                            resobj.grpA = false;
                            resobj.grpB = true;
                        }

                        ValveA = Valveses.FirstOrDefault(r => r.DeviceId == device.Id && r.Name == "A");
                        ValveB = Valveses.FirstOrDefault(r => r.DeviceId == device.Id && r.Name == "B");

                        ValveA.MinValue = Datas[2];
                        ValveA.MaxValue = Datas[3];
                        ValveA.CtrlInterval = Utility.GetIntValue(Datas, 7, 6);
                        ValveA.Step1 = Datas[10];
                        ValveA.Step2 = Datas[11];
                        ValveA.Step3 = Datas[12];
                        ValveA.Step4 = Datas[13];
                        ValveA.Step5 = Datas[14];
                        ValveA.MaxStep = Datas[20];
                        ValveA.Steering = Datas[22];


                        ValveB.MinValue = Datas[4];
                        ValveB.MaxValue = Datas[5];
                        ValveB.CtrlInterval = Utility.GetIntValue(Datas, 9, 8);
                        ValveB.Step1 = Datas[15];
                        ValveB.Step2 = Datas[16];
                        ValveB.Step3 = Datas[17];
                        ValveB.Step4 = Datas[18];
                        ValveB.Step5 = Datas[19];
                        ValveB.MaxStep = Datas[21];
                        ValveB.Steering = Datas[23];

                        ValvesRepository.Update(ValveA);
                        ValvesRepository.Update(ValveB);
                        //valveBO.Update(currentDevice.ValveA);
                        //valveBO.Update(currentDevice.ValveB);

                        

                        device.BackFix1 = Utility.GetDecimalValue(Datas, 32, 31);
                        device.BackFix2 = Utility.GetDecimalValue(Datas, 34, 33);
                        device.CtrlNumber = Datas[41];
                        device.FixWater1 = Utility.GetDecimalValue(Datas, 36, 35);
                        device.FixWater2 = Utility.GetDecimalValue(Datas, 38, 37);
                        device.OutdoorFix = Utility.GetDecimalValue(Datas, 26, 25);
                        device.SaveInterval = Utility.GetIntValue(Datas, 40, 39);
                        device.SupplyWaterFix1 = Utility.GetDecimalValue(Datas, 28, 27);
                        device.SupplyWaterFix2 = Utility.GetDecimalValue(Datas, 30, 29);

                        DeviceRepository.Update(device);
                        //DeviceBO.Update(currentDevice.Device);


                        //treeView1.Enabled = true;
                        //IsProcessing = false;

                        //if (resobj.rdoModeA)
                        //    rdoModeA_CheckedChanged(null, null);
                        //else
                        //    resobj.rdoModeA = true;

                        #region 阀门相关
                        resobj.rdoAZhengZhuan = (ValveA.Steering == 0);
                        resobj.rdoAFanZhuan = !resobj.rdoAZhengZhuan;
                        resobj.nuAStep1 = ValveA.Step1;
                        resobj.nuAStep2 = ValveA.Step2;
                        resobj.nuAStep3 = ValveA.Step3;
                        resobj.nuAStep4 = ValveA.Step4;
                        resobj.nuAStep5 = ValveA.Step5;
                        resobj.nuAMaxStep = ValveA.MaxStep;
                        resobj.nuAInterval = ValveA.CtrlInterval;
                        resobj.btnAMaxRolateDelataAngle = ValveA.MaxValue / 100 * 360;
                        resobj.btnAMinRolateDelataAngle = ValveA.MinValue / 100 * 360;
                        resobj.nuAMaxValue = ValveA.MaxValue;
                        resobj.nuAMinValue = ValveA.MinValue;

                        resobj.cmbACtrlNumber = device.CtrlNumber - 1;

                        resobj.rdoBZhengZhuan = (ValveB.Steering == 0);
                        resobj.rdoBFanZhuan = !resobj.rdoBZhengZhuan;
                        resobj.nuBStep1 = ValveB.Step1;
                        resobj.nuBStep2 = ValveB.Step2;
                        resobj.nuBStep3 = ValveB.Step3;
                        resobj.nuBStep4 = ValveB.Step4;
                        resobj.nuBStep5 = ValveB.Step5;
                        resobj.nuBMaxStep = ValveB.MaxStep;
                        resobj.nuBInterval = ValveB.CtrlInterval;
                        resobj.nuBMaxValue = ValveB.MaxValue;
                        resobj.nuBMinValue = ValveB.MinValue;
                        resobj.btnBMaxRolateDelataAngle = ValveB.MaxValue / 100 * 360;
                        resobj.btnBMinRolateDelataAngle = ValveB.MinValue / 100 * 360;
                        resobj.cmbBCtrlNumber = device.CtrlNumber.Value - 1;
                        #endregion

                        #region 温度修正
                        resobj.nuOutdoor = device.OutdoorFix.Value;
                        resobj.nuSup1 = device.SupplyWaterFix1.Value;
                        resobj.nuSup2 = device.SupplyWaterFix2.Value;
                        resobj.nuBack1 = device.BackFix1.Value;
                        resobj.nuBack2 = device.BackFix2.Value;
                        resobj.nuFix1 = device.FixWater1.Value;
                        resobj.nuFix2 = device.FixWater2.Value;
                        resobj.nuSaveInterval = device.SaveInterval.Value;

                        #endregion
                        #endregion
                        //IsProcessing = false;
                        break;
                    case Common.SettingType.工作模式曲线设置:
                    case Common.SettingType.周六模式曲线设置:
                    case Common.SettingType.周日模式曲线设置:
                    case Common.SettingType.假日模式曲线设置:
                        #region 曲线设置
                        {
                            string startTime = "";
                            int curveCode = -1;
                            int flag = 0;
                            string endTime = "";
                            DataTable dt = new DataTable();
                            dt.Columns.Add("ID", typeof(int));
                            dt.Columns.Add("TimeSpanID", typeof(int));
                            dt.Columns.Add("StartTime", typeof(string));
                            dt.Columns.Add("EndTime", typeof(string));
                            dt.Columns.Add("CurveCode", typeof(int));
                            dt.Columns.Add("DeviceID", typeof(int));
                            dt.Columns.Add("Flag", typeof(int));
                            int timeSpanID = GetTimeSpanID(clientdata);
                            DataRow dr = null;
                            //先读取第一组
                            dr = dt.NewRow();
                            dr["TimeSpanID"] = timeSpanID;
                            dr["DeviceID"] = device.Id;
                            curveCode = Utility.SetIntegerSomeBit(6, Datas[3], false);
                            flag = Utility.GetIntegerSomeBit(Datas[3], 6);
                            startTime = string.Format("{0:00}:{1:00}", Datas[1], Datas[2]);
                            endTime = string.Format("{0:00}:{1:00}", Datas[4], Datas[5]);

                            dr["StartTime"] = startTime;
                            dr["EndTime"] = endTime;
                            dr["CurveCode"] = curveCode;
                            dr["Flag"] = flag;
                            dt.Rows.Add(dr);
                            string previousEndTime = endTime;
                            //读余下几组
                            for (int i = 6; i < 75; i = i + 3)
                            {
                                if (Datas[i] == 255)
                                    break;
                                dr = dt.NewRow();
                                dr["TimeSpanID"] = timeSpanID;
                                dr["DeviceID"] = device.Id;
                                curveCode = Utility.SetIntegerSomeBit(6, Datas[i], false);
                                flag = Utility.GetIntegerSomeBit(Datas[i], 6);
                                startTime = previousEndTime;
                                if (Datas[i + 1] > 23 || Datas[i + 2] > 59)
                                {
                                    endTime = null;
                                }
                                else
                                {
                                    endTime = string.Format("{0:00}:{1:00}", Datas[i + 1], Datas[i + 2]);
                                }

                                dr["StartTime"] = startTime;
                                dr["EndTime"] = endTime;
                                dr["CurveCode"] = curveCode;
                                dr["Flag"] = flag;
                                previousEndTime = endTime;
                                dt.Rows.Add(dr);
                            }

                            if (dt.Rows.Count > 0)
                            {
                                resobj.IsdgvData = true;
                                resobj.dgvDataDataSource = dt;

                                //timeSpanBO.DeleteByDeviceIDAndTimeSpanID(device.Id, timeSpanID);
                                TimeSpanSettingRepository.Delete(TimeSpanSettings.Where(t => t.DeviceId == device.Id && t.TimeSpanID == timeSpanID));

                                TimeSpanSetting model = null;
                                foreach (DataRow dr2 in dt.Rows)
                                {
                                    model = new TimeSpanSetting();
                                    model.TimeSpanID = timeSpanID;
                                    model.DeviceId = device.Id;
                                    model.CurveCode = (int)dr2["CurveCode"];
                                    model.EndTime = dr2["EndTime"].ToString();
                                    model.StartTime = dr2["StartTime"].ToString();
                                    model.Flag = (int)dr2["Flag"];
                                    TimeSpanSettingRepository.Insert(model);
                                }
                            }
                            //IsProcessing = false;
                        }
                        #endregion
                        break;
                    default:
                        break;
                }

                resobj.ValveA = ValveA;
                resobj.ValveB = ValveB;
                resobj.Device = device;

                return resobj;

            }
            catch (Exception ex)
            {
                
                throw;
            }
        }


        private dynamic DealCommand_arm(RemoteResult res)
        {
            try
            {
                var Datas = res.Datas;

                string msg = "返回命令";

                switch ((Common.ARMControl)Datas[0])
                {
                    case Common.ARMControl.手动控阀:
                        msg = "命令成功执行！";
                        break;
                    case Common.ARMControl.修改时间:
                        msg = "时间同步成功！";
                        break;
                    case Common.ARMControl.校阀:
                        msg = "校阀";
                        break;
                    default:
                        break;
                }

                return msg;
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }




        private int GetTimeSpanID(dynamic obj)
        {
            if (obj.rdoMode1)
                return (int)Common.TimeSpanType.工作日;
            if (obj.rdoMode2)
                return (int)Common.TimeSpanType.周六;
            if (obj.rdoMode3)
                return (int)Common.TimeSpanType.周日;
            return (int)Common.TimeSpanType.假日;
        }


    }
}
