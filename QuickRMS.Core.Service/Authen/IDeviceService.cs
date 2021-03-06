﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.Framework.Tool;
using QuickRMS.Domain.Models.DeviceInfo;
using QuickRMS.Domain.Models.SysConfig;
using QuickRMS.Site.Models.Device;
using RemoteHelper;
using QuickRMS.Site.Models.Authen.Device;
using System.Data;

namespace QuickRMS.Core.Service.Authen
{
    public interface IDeviceService
    {
        #region 属性

        IQueryable<Device> Devices { get; }

        IQueryable<DeviceData> DeviceDatas { get; }

        IQueryable<DeviceCureLibrary> DeviceCureLibraries { get; }

        IQueryable<History> Histories { get; }

        IQueryable<DeviceMaintenance> DeviceMaintenances { get; }

        IQueryable<CurveLibrary> CurveLibraries { get; }

        IQueryable<TimeSpanSetting> TimeSpanSettings { get; }

        IQueryable<Valves> Valveses { get; }
        #endregion


        OperationResult Insert(DeviceViewModel model);

        OperationResult Update(DeviceViewModel model);

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        OperationResult Delete(int Id);


        dynamic DealCommandResult(RemoteResult res, dynamic clientdata=null);

        byte[] SaveDataAndGetCommand(DeviceParaDto data);

        List<CurveLibrary> GetCurveLibraries();

        List<TimeSpanSetting> GetTimeSpanList(int deviceId, int timeSpanId);
        List<DeviceMaintenanceModel> GetDeviceMaintenanceModels(int deviceId);

        int UpdateOrCreateDeviceMaintenance(int deviceId, string memo,int mid,DateTime date);

        int DeleteDeviceMaintenance(string[] mid);

        DeviceDataModel GetLastestData(int deviceId);

        List<DeviceCureLibraryModel> GetDeviceCureLibraryList(int deviceId);
        List<DeviceCureLibraryDataModel> GetDeviceCureLibraryDataList(int deviceId);
        List<HistoryModel> GetHistoryModels(int deviceId, HistoryTypeCollect type);


        Valves GetValvesModel(int deviceId, string name);
        int UpdateValvesModel(Valves model);

        double[,] GetDeviceCureData(int curid);

        string AddTimeSpan(int deviceid, int timespantype, string StartTime, string EndTime, int CurveCode);

        string SaveTimeSpan(int timespanid, string StartTime, string EndTime, int CurveCode);

        string DeleteTimeSpan(int timespanid);

        OperationResult UploadDeviceCurveLibraries(DataTable dt,int deviceId);
    }
}
