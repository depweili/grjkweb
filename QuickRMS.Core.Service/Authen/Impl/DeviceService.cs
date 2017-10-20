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

                WaterNetStatus1Text = ((WaterNetStatus)data.WaterNetStatus1).ToString(),
                WaterNetStatus2Text = ((WaterNetStatus)data.WaterNetStatus2).ToString(),
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
    }
}
