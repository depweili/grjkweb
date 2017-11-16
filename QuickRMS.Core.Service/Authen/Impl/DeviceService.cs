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

        public List<DeviceCureLibraryDataModel> GetDeviceCureLibraryDataList(int deviceId)
        {
            List<DeviceCureLibraryDataModel> result = new List<DeviceCureLibraryDataModel>();
            var data = DeviceCureLibraries.Where(r => r.DeviceId == deviceId).OrderBy(r => r.Code).ToList();
            foreach (var d in data)
            {
                DeviceCureLibraryDataModel model = new DeviceCureLibraryDataModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    DeviceId = d.DeviceId,
                    Code = d.Code,
                    Column1 = d.Column1,
                    Column2 = d.Column2,
                    Column3 = d.Column3,
                    Column4 = d.Column4,
                    Column5 = d.Column5,
                    Column6 = d.Column6,
                    Column7 = d.Column7,
                    Column8 = d.Column8,
                    Column9 = d.Column9,
                    Column10 = d.Column10,
                    Column11 = d.Column11,
                    Column12 = d.Column12,
                    Column13 = d.Column13,
                    Column14 = d.Column14,
                    Column15 = d.Column15,
                    Column16 = d.Column16,
                    Column17 = d.Column17,
                    Column18 = d.Column18,
                    Column19 = d.Column19,
                    Column20 = d.Column20,
                    Column21 = d.Column21,
                    Column22 = d.Column22,
                    Column23 = d.Column23,
                    Column24 = d.Column24,
                    Column25 = d.Column25,
                    Column26 = d.Column26,
                    Column27 = d.Column27,
                    Column28 = d.Column28,
                    Column29 = d.Column29,
                    Column30 = d.Column30,
                    Column31 = d.Column31,
                    Column32 = d.Column32,
                    Column33 = d.Column33,
                    Column34 = d.Column34,
                    Column35 = d.Column35,
                    Column36 = d.Column36,
                    Column37 = d.Column37,
                    Column38 = d.Column38,
                    Column39 = d.Column39,
                    Column40 = d.Column40,
                    Column41 = d.Column41,
                    Column42 = d.Column42,
                    Column43 = d.Column43,
                    Column44 = d.Column44,
                    Column45 = d.Column45,
                    Column46 = d.Column46,
                    Column47 = d.Column47,
                    Column48 = d.Column48,
                    Column49 = d.Column49,
                    Column50 = d.Column50,
                    Column51 = d.Column51,
                    Column52 = d.Column52,
                    Column53 = d.Column53,
                    Column54 = d.Column54,
                    Column55 = d.Column55,
                    Column56 = d.Column56,
                    Column57 = d.Column57,
                    Column58 = d.Column58,
                    Column59 = d.Column59,
                    Column60 = d.Column60,
                    Column61 = d.Column61,
                    Column62 = d.Column62,
                    Column63 = d.Column63,
                    Column64 = d.Column64,
                    Column65 = d.Column65,
                    Column66 = d.Column66,
                    Column67 = d.Column67,
                    Column68 = d.Column68,
                    Column69 = d.Column69,
                    Column70 = d.Column70,
                    Column71 = d.Column71,
                    Column72 = d.Column72,
                    Column73 = d.Column73,
                    Column74 = d.Column74,
                    Column75 = d.Column75,
                    Column76 = d.Column76,
                    Column77 = d.Column77,
                    Column78 = d.Column78,
                    Column79 = d.Column79,
                    Column80 = d.Column80,
                    Column81 = d.Column81,
                    Column82 = d.Column82,
                    Column83 = d.Column83,
                    Column84 = d.Column84,
                    Column85 = d.Column85,
                    Column86 = d.Column86,
                    Column87 = d.Column87,
                    Column88 = d.Column88,
                    Column89 = d.Column89,
                    Column90 = d.Column90,
                    Column91 = d.Column91,
                    Column92 = d.Column92,
                    Column93 = d.Column93,
                    Column94 = d.Column94,
                    Column95 = d.Column95,
                    Column96 = d.Column96,
                    Column97 = d.Column97,
                    Column98 = d.Column98,
                    Column99 = d.Column99,
                    Column100 = d.Column100,
                    Column101 = d.Column101,
                    Column102 = d.Column102,
                    Column103 = d.Column103,
                    Column104 = d.Column104,
                    Column105 = d.Column105,
                    Column106 = d.Column106,
                    Column107 = d.Column107,
                    Column108 = d.Column108,
                    Column109 = d.Column109,
                    Column110 = d.Column110,
                    Column111 = d.Column111,
                    Column112 = d.Column112,
                    Column113 = d.Column113,
                    Column114 = d.Column114,
                    Column115 = d.Column115,
                    Column116 = d.Column116,
                    Column117 = d.Column117,
                    Column118 = d.Column118,
                    Column119 = d.Column119,
                    Column120 = d.Column120,
                    Column121 = d.Column121

                };
                result.Add(model);
            }
            return result;
        }

        public double[,] GetDeviceCureData(int curid)
        {
            double[,] result = new double[121,2];
            var data = DeviceCureLibraries.Single(r => r.Id == curid);

            Type cur=data.GetType();

            double start = -40;

            for (int i = 0; i < 121; i++)
            {
                result[i,0]=start;
                result[i, 1] = Convert.ToDouble(data.GetValue("Column" + (i + 1).ToString()));

                //lst.Add(start, Convert.ToDouble(data.GetValue("Column" + (i + 1).ToString()));
                start = start + 0.5;
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
                    MaintenanceDate=d.MaintenanceDate.ToString("yyyy-MM-dd"), //hh:mm:ss
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


        public int UpdateOrCreateDeviceMaintenance(int deviceId, string memo,int mid,DateTime date)
        {
            DeviceMaintenance model = new DeviceMaintenance
            {
                DeviceId = deviceId,
                Memo = memo,
                MaintenanceDate = date,//DateTime.Now,
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
                            resobj = DealCommand_cssd(res, clientdata);
                            break;
                        case CommandConst.ARM控制指令:
                            resobj = DealCommand_arm(res);
                            break;
                        case CommandConst.读取历史数据:
                            resobj = DealCommand_dqls(res, clientdata);
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

        private dynamic DealCommand_dqls(RemoteResult res, dynamic clientdata)
        {
            try
            {
                dynamic resobj = new ExpandoObject();

                var Datas = res.Datas;

                if (Datas[1] == 255)
                {
                    resobj.Message = "没有该条记录！";
                    return resobj;
                }

                Device device = Devices.Single(t => t.DeviceCode == res.deviceCode);

                var nuRowNumber = (int)clientdata.nuRowNumber;

                byte[] array = new byte[46];
                History his = null;
                Array.Copy(Datas, 1, array, 0, 46);

                //var historyType = (Common.HistoryType)Datas[0];

                HistoryTypeCollect historyType = (HistoryTypeCollect)Datas[0];

                his = Histories.SingleOrDefault(t => t.DeviceId == device.Id && t.HistoryType == historyType && t.RowNumber == nuRowNumber);

                if (his == null)
                {
                    his = new History()
                    {
                        CreateTime = DateTime.Now,
                        Data = array,
                        DeviceId = device.Id,
                        RowNumber = nuRowNumber,
                        HistoryType = historyType

                    };

                    HistoryRepository.Insert(his);
                }
                else
                {
                    his.Data = array;
                    HistoryRepository.Update(his);
                }

                UnitOfWork.Commit();

                return resobj;
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        private dynamic DealCommand_cssd(RemoteResult res, dynamic clientdata)
        {
            try
            {
                var Datas = res.Datas;

                string msg = "返回命令";

                if ((byte)Datas[0] >= (byte)Common.SettingType.曲线起始参数 && (byte)Datas[0] <= (byte)Common.SettingType.曲线终止参数)
                {
                    if (((int)Datas[0] - 1) == 30)
                    {
                        msg = "保存成功！";
                    }
                }
                else
                {
                    switch ((Common.SettingType)Datas[0])
                    {
                        case Common.SettingType.整体参数:
                        case Common.SettingType.工作模式曲线设置:
                        case Common.SettingType.周六模式曲线设置:
                        case Common.SettingType.周日模式曲线设置:
                        case Common.SettingType.假日模式曲线设置:
                            msg = "保存成功！";
                            break;
                        default:
                            break;
                    }
                }

                return msg;
                
            }
            catch (Exception ex)
            {
                
                throw;
            }
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


                var d29 = data.Data29[0];
                var d30 = data.Data30[0];
                Common.CheckValveState valveState = (Common.CheckValveState)data.Data29[0];

                byte value = data.Data28[0];  //低4位A阀，高4位B阀			每个的最高位为1为时间控制，为0为曲线控制				
                //0手动控制，1周六日、工作日自动控制，2周日、工作日控制，3全部工作日控制						
                byte B = Utility.Hi4Bit(value);    //0000 1011
                byte A = Utility.Low4Bit(value);    //0000 1011

                int bita1 = Utility.GetIntegerSomeBit(A, 3);
                int bitb1 = Utility.GetIntegerSomeBit(B, 3);
                


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
                        /*
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
                        */
                        #endregion

                        #region 温度修正
                        /*
                        resobj.nuOutdoor = device.OutdoorFix.Value;
                        resobj.nuSup1 = device.SupplyWaterFix1.Value;
                        resobj.nuSup2 = device.SupplyWaterFix2.Value;
                        resobj.nuBack1 = device.BackFix1.Value;
                        resobj.nuBack2 = device.BackFix2.Value;
                        resobj.nuFix1 = device.FixWater1.Value;
                        resobj.nuFix2 = device.FixWater2.Value;
                        resobj.nuSaveInterval = device.SaveInterval.Value;
                        */
                        #endregion


                        UnitOfWork.Commit();

                        //IsProcessing = false;
                        resobj.ValveA = ValveA;
                        resobj.ValveB = ValveB;
                        resobj.Device = device;

                        #endregion

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
                            //int timeSpanID = GetTimeSpanID(clientdata);
                            int timeSpanID = clientdata.timespantype;
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
                                //resobj.IsdgvData = true;
                                //resobj.dgvDataDataSource = dt;

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

                        UnitOfWork.Commit();

                        break;
                    default:
                        break;
                }

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


        public byte[] SaveDataAndGetCommand(DeviceParaDto data)
        {
            try
            {
                var currentDevice = Devices.Single(r => r.Id == data.Device.Id);
                var ValveA = Valveses.Single(r => r.Id == data.ValveA.Id);
                var ValveB = Valveses.Single(r => r.Id == data.ValveB.Id);

                /////////////////////////////////////

                ValveA.WorkBy = data.ValveA.WorkBy;
                ValveA.WorkMode = data.ValveA.WorkMode;
                ValveA.SetValue = data.ValveA.SetValue;

                ValveB.WorkBy = data.ValveB.WorkBy;
                ValveB.WorkMode = data.ValveB.WorkMode;
                ValveB.SetValue = data.ValveB.SetValue;

                ///////////////////////////

                ValveA.MinValue = data.ValveA.MinValue;
                ValveA.MaxValue = data.ValveA.MaxValue;
                ValveA.CtrlInterval = data.ValveA.CtrlInterval;
                ValveA.Step1 = data.ValveA.Step1;
                ValveA.Step2 = data.ValveA.Step2;
                ValveA.Step3 = data.ValveA.Step3;
                ValveA.Step4 = data.ValveA.Step4;
                ValveA.Step5 = data.ValveA.Step5;
                ValveA.MaxStep = data.ValveA.MaxStep;
                ValveA.Steering = data.ValveA.Steering;

                ValveB.MinValue = data.ValveB.MinValue;
                ValveB.MaxValue = data.ValveB.MaxValue;
                ValveB.CtrlInterval = data.ValveB.CtrlInterval;
                ValveB.Step1 = data.ValveB.Step1;
                ValveB.Step2 = data.ValveB.Step2;
                ValveB.Step3 = data.ValveB.Step3;
                ValveB.Step4 = data.ValveB.Step4;
                ValveB.Step5 = data.ValveB.Step5;
                ValveB.MaxStep = data.ValveB.MaxStep;
                ValveB.Steering = data.ValveB.Steering;

                currentDevice.CtrlNumber = data.Device.CtrlNumber;
                ///////////////////////////////////////////////

                currentDevice.BackFix1 = data.Device.BackFix1;
                currentDevice.BackFix2 = data.Device.BackFix2;
                currentDevice.FixWater1 = data.Device.FixWater1;
                currentDevice.FixWater2 = data.Device.FixWater2;
                currentDevice.OutdoorFix = data.Device.OutdoorFix;
                currentDevice.SaveInterval = data.Device.SaveInterval;
                currentDevice.SupplyWaterFix1 = data.Device.SupplyWaterFix1;
                currentDevice.SupplyWaterFix2 = data.Device.SupplyWaterFix2;


                ValvesRepository.Update(ValveA);
                ValvesRepository.Update(ValveB);
                DeviceRepository.Update(currentDevice);

                UnitOfWork.Commit();

                dynamic currentDevicePara = new
                {
                    Device = currentDevice,
                    ValveA = ValveA,
                    ValveB = ValveB
                };

                return BuildCommondDatas(currentDevicePara);

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public byte[] BuildCommondDatas(dynamic currentDevice)
        {
            try
            {
                byte a = (byte)currentDevice.ValveA.WorkMode;
                byte b = (byte)currentDevice.ValveB.WorkMode;
                bool aWorkBy = currentDevice.ValveA.WorkBy == 0 ? false : true;
                bool bWorkBy = currentDevice.ValveB.WorkBy == 0 ? false : true;
                b = (byte)(b << 4); //0011 0000
                byte result = (byte)(a | b);    //1011 1011
                result = (byte)IEHNCS.Common.Utility.SetIntegerSomeBit(3, result, aWorkBy);
                result = (byte)IEHNCS.Common.Utility.SetIntegerSomeBit(7, result, bWorkBy);
                //result is workmode
                byte[] datas = new byte[44];
                datas[0] = (byte)Common.SettingType.整体参数;
                datas[1] = result;
                datas[2] = (byte)currentDevice.ValveA.MinValue;
                datas[3] = (byte)currentDevice.ValveA.MaxValue;
                datas[4] = (byte)currentDevice.ValveB.MinValue;
                datas[5] = (byte)currentDevice.ValveB.MaxValue;
                ushort tmpValue = (ushort)currentDevice.ValveA.CtrlInterval;
                datas[6] = Utility.HiByte(tmpValue);
                datas[7] = Utility.LoByte(tmpValue);
                tmpValue = (ushort)currentDevice.ValveB.CtrlInterval;
                datas[8] = Utility.HiByte(tmpValue);
                datas[9] = Utility.LoByte(tmpValue);
                datas[10] = (byte)currentDevice.ValveA.Step1;
                datas[11] = (byte)currentDevice.ValveA.Step2;
                datas[12] = (byte)currentDevice.ValveA.Step3;
                datas[13] = (byte)currentDevice.ValveA.Step4;
                datas[14] = (byte)currentDevice.ValveA.Step5;
                datas[15] = (byte)currentDevice.ValveB.Step1;
                datas[16] = (byte)currentDevice.ValveB.Step2;
                datas[17] = (byte)currentDevice.ValveB.Step3;
                datas[18] = (byte)currentDevice.ValveB.Step4;
                datas[19] = (byte)currentDevice.ValveB.Step5;
                datas[20] = (byte)currentDevice.ValveA.MaxStep;
                datas[21] = (byte)currentDevice.ValveB.MaxStep;
                datas[22] = (byte)currentDevice.ValveA.Steering;
                datas[23] = (byte)currentDevice.ValveB.Steering;
                datas[24] = 0x0;
                string strTmp = "";
                short tmp = 0;
                if (currentDevice.Device.OutdoorFix == 0)
                {
                    datas[25] = 0;
                    datas[26] = 0;
                }
                else
                {
                    strTmp = currentDevice.Device.OutdoorFix.ToString("0.00");
                    strTmp = strTmp.Replace(".", "");
                    tmp = short.Parse(strTmp);
                    datas[25] = (byte)(tmp >> 8);
                    datas[26] = (byte)(tmp & 0xFF);
                }
                if (currentDevice.Device.SupplyWaterFix1 == 0)
                {
                    datas[27] = 0;
                    datas[28] = 0;
                }
                else
                {
                    strTmp = currentDevice.Device.SupplyWaterFix1.ToString("0.00");
                    strTmp = strTmp.Replace(".", "");
                    tmp = short.Parse(strTmp);
                    datas[27] = (byte)(tmp >> 8);
                    datas[28] = (byte)(tmp & 0xFF);
                }
                if (currentDevice.Device.SupplyWaterFix2 == 0)
                {
                    datas[29] = 0;
                    datas[30] = 0;
                }
                else
                {
                    strTmp = currentDevice.Device.SupplyWaterFix2.ToString("0.00");
                    strTmp = strTmp.Replace(".", "");
                    tmp = short.Parse(strTmp);
                    datas[29] = (byte)(tmp >> 8);
                    datas[30] = (byte)(tmp & 0xFF);
                }
                if (currentDevice.Device.BackFix1 == 0)
                {
                    datas[31] = 0;
                    datas[32] = 0;
                }
                else
                {
                    strTmp = currentDevice.Device.BackFix1.ToString("0.00");
                    strTmp = strTmp.Replace(".", "");
                    tmp = short.Parse(strTmp);
                    datas[31] = (byte)(tmp >> 8);
                    datas[32] = (byte)(tmp & 0xFF);
                }
                if (currentDevice.Device.BackFix2 == 0)
                {
                    datas[33] = 0;
                    datas[34] = 0;
                }
                else
                {
                    strTmp = currentDevice.Device.BackFix2.ToString("0.00");
                    strTmp = strTmp.Replace(".", "");
                    tmp = short.Parse(strTmp);
                    datas[33] = (byte)(tmp >> 8);
                    datas[34] = (byte)(tmp & 0xFF);
                }
                if (currentDevice.Device.FixWater1 == 0)
                {
                    datas[35] = 0;
                    datas[36] = 0;
                }
                else
                {
                    strTmp = currentDevice.Device.FixWater1.ToString("0.00");
                    strTmp = strTmp.Replace(".", "");
                    tmp = short.Parse(strTmp);
                    datas[35] = (byte)(tmp >> 8);
                    datas[36] = (byte)(tmp & 0xFF);
                }
                if (currentDevice.Device.FixWater2 == 0)
                {
                    datas[37] = 0;
                    datas[38] = 0;
                }
                else
                {
                    strTmp = currentDevice.Device.FixWater2.ToString("0.00");
                    strTmp = strTmp.Replace(".", "");
                    tmp = short.Parse(strTmp);
                    datas[37] = (byte)(tmp >> 8);
                    datas[38] = (byte)(tmp & 0xFF);
                }
                tmpValue = (ushort)currentDevice.Device.SaveInterval;
                datas[39] = Utility.HiByte(tmpValue);
                datas[40] = Utility.LoByte(tmpValue);
                datas[41] = (byte)currentDevice.Device.CtrlNumber;
                datas[42] = 0xFF;
                datas[43] = 0xFF;

                return datas;

            }
            catch (Exception ex)
            {
                
                throw;
            }
            
        }
    }
}
