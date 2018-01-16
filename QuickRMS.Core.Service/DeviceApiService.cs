using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using IEHNCS.Common;
using Quick.Framework.Tool;
using QuickRMS.Core.Repository.SysConfig;
using QuickRMS.Core.Service.Authen;
using QuickRMS.Core.Service.Authen.Impl;
using QuickRMS.Domain.Models.SysConfig;
using RemoteHelper;
using ARMControl = QuickRMS.Domain.Models.ARMControl;
using QuickRMS.Domain.Models.DeviceInfo;
using System.Threading;
using System.Reflection;

namespace QuickRMS.Core.Service
{
    [Export(typeof(IDeviceApiService))]
    public class DeviceApiService : CoreServiceBase, IDeviceApiService
    {
        [Import]
        public IDeviceService DeviceService { get; set; }

        public dynamic DealCommandResult(RemoteResult res, dynamic clientdata=null)
        {
            dynamic resobj = null;

            //var service = new DeviceService();

            resobj = DeviceService.DealCommandResult(res, clientdata);

            return resobj;
        }

        public OperationResult SyncTime(string scode)
        {
            OperationResult result = new OperationResult(OperationResultType.Success, "成功执行！");
            try
            {

                RemoteResult res = null;
                string msg = RemoteClient.Instance.SendCommandAndGetResult(new byte[] { (byte)ARMControl.修改时间,(byte)(DateTime.Now.Year-2000),
                    (byte)DateTime.Now.Month,
                    (byte)DateTime.Now.Day,
                    (byte)DateTime.Now.DayOfWeek,
                    (byte)DateTime.Now.Hour,
                    (byte)DateTime.Now.Minute,
                    (byte)DateTime.Now.Second
                }, CommandConst.ARM控制指令, 12, scode, out res);

                if (!string.IsNullOrEmpty(msg))
                {
                    result.ResultType = OperationResultType.Error;
                    result.Message = msg;
                }
                else
                {
                    result.AppendData = DealCommandResult(res);
                }

                return result;

            }
            catch (Exception ex)
            {
                result.ResultType = OperationResultType.Error;
                result.Message = ex.Message;
                return result;
            }
        }

        public OperationResult UpdateOutTemData(string scode)
        {
            OperationResult result = new OperationResult(OperationResultType.Success, "成功执行！");
            try
            {

                RemoteResult res = null;
                string msg = RemoteClient.Instance.SendCommandAndGetResult(null, CommandConst.读取测量值, 35, scode, out res);
                //result.AppendData = DealCommandResult(res);

                if (!string.IsNullOrEmpty(msg))
                {
                    result.ResultType = OperationResultType.Error;
                    result.Message = msg;
                }
                else
                {
                    result.AppendData = DealCommandResult(res);
                }

                return result;

            }
            catch (Exception ex)
            {
                result.ResultType = OperationResultType.Error;
                result.Message = ex.Message;
                return result;
            }
        }

        public OperationResult UpdateData(string scode, dynamic clientdata = null)
        {
            OperationResult result = new OperationResult(OperationResultType.Success, "成功执行！");
            try
            {

                RemoteResult res = null;
                string msg = RemoteClient.Instance.SendCommandAndGetResult(new byte[] { (byte)SettingType.整体参数 }, CommandConst.读取参数, 48, scode, out res);
                
                //result.AppendData = DealCommandResult(res, clientdata);

                if (!string.IsNullOrEmpty(msg))
                {
                    result.ResultType = OperationResultType.Error;
                    result.Message = msg;
                }
                else
                {
                    result.AppendData = DealCommandResult(res, clientdata);
                }

                return result;

            }
            catch (Exception ex)
            {
                result.ResultType = OperationResultType.Error;
                result.Message = ex.Message;
                return result;
            }
        }


        public OperationResult SaveAndSendData(string scode, dynamic clientdata = null)
        {
            OperationResult result = new OperationResult(OperationResultType.Success, "成功执行！");
            try
            {

                RemoteResult res = null;

                byte[] datas = DeviceService.SaveDataAndGetCommand(clientdata);
                string msg = RemoteClient.Instance.SendCommandAndGetResult(datas, CommandConst.参数设定, 48, scode, out res);
                //result.AppendData = DealCommandResult(res, clientdata);

                if (!string.IsNullOrEmpty(msg))
                {
                    result.ResultType = OperationResultType.Error;
                    result.Message = msg;
                }
                else
                {
                    result.AppendData = DealCommandResult(res, clientdata);
                }

                return result;

            }
            catch (Exception ex)
            {
                result.ResultType = OperationResultType.Error;
                result.Message = ex.Message;
                return result;
            }
        }

        public OperationResult UpdateTimeSpan(string scode, int timespantype)
        {
            OperationResult result = new OperationResult(OperationResultType.Success, "成功执行！");
            try
            {

                RemoteResult res = null;
                TimeSpanType spanType = (TimeSpanType)timespantype;

                string msg = string.Empty;

                switch (spanType)
                {
                    case TimeSpanType.假日:
                        msg = RemoteClient.Instance.SendCommandAndGetResult(new byte[] { (byte)SettingType.假日模式曲线设置 }, CommandConst.读取参数, 79, scode, out res);
                        //SendCommand(new byte[] { (byte)SettingType.假日模式曲线设置 }, CommandConst.读取参数, 79);
                        break;
                    case TimeSpanType.周六:
                        msg = RemoteClient.Instance.SendCommandAndGetResult(new byte[] { (byte)SettingType.周六模式曲线设置 }, CommandConst.读取参数, 79, scode, out res);
                        break;
                    case TimeSpanType.周日:
                        msg = RemoteClient.Instance.SendCommandAndGetResult(new byte[] { (byte)SettingType.周日模式曲线设置 }, CommandConst.读取参数, 79, scode, out res);
                        break;
                    case TimeSpanType.工作日:
                        msg = RemoteClient.Instance.SendCommandAndGetResult(new byte[] { (byte)SettingType.工作模式曲线设置 }, CommandConst.读取参数, 79, scode, out res);
                        break;
                    default:
                        break;
                }

                if (!string.IsNullOrEmpty(msg))
                {
                    result.ResultType = OperationResultType.Error;
                    result.Message = msg;
                }
                else
                {
                    result.AppendData = DealCommandResult(res, new { timespantype = timespantype });
                }

                return result;

            }
            catch (Exception ex)
            {
                result.ResultType = OperationResultType.Error;
                result.Message = ex.Message;
                return result;
            }
        }


        public OperationResult AddTimeSpan(int deviceid, int timespantype, string StartTime, string EndTime, int CurveCode)
        {
            OperationResult result = new OperationResult(OperationResultType.Success, "成功执行！");
            try
            {
                string msg = string.Empty;

                msg = DeviceService.AddTimeSpan(deviceid, timespantype, StartTime, EndTime, CurveCode);

                if (!string.IsNullOrEmpty(msg))
                {
                    result.ResultType = OperationResultType.Error;
                    result.Message = msg;
                }

                return result;

            }
            catch (Exception ex)
            {
                result.ResultType = OperationResultType.Error;
                result.Message = ex.Message;
                return result;
            }
        }

        public OperationResult SaveTimeSpan(int timespanid, string StartTime, string EndTime, int CurveCode)
        {
            OperationResult result = new OperationResult(OperationResultType.Success, "成功执行！");
            try
            {
                string msg = string.Empty;

                msg = DeviceService.SaveTimeSpan(timespanid, StartTime, EndTime, CurveCode);

                if (!string.IsNullOrEmpty(msg))
                {
                    result.ResultType = OperationResultType.Error;
                    result.Message = msg;
                }

                return result;

            }
            catch (Exception ex)
            {
                result.ResultType = OperationResultType.Error;
                result.Message = ex.Message;
                return result;
            }
        }

        public OperationResult DeleteTimeSpan(int timespanid)
        {
            OperationResult result = new OperationResult(OperationResultType.Success, "成功执行！");
            try
            {
                string msg = string.Empty;

                msg = DeviceService.DeleteTimeSpan(timespanid);

                if (!string.IsNullOrEmpty(msg))
                {
                    result.ResultType = OperationResultType.Error;
                    result.Message = msg;
                }

                return result;

            }
            catch (Exception ex)
            {
                result.ResultType = OperationResultType.Error;
                result.Message = ex.Message;
                return result;
            }
        }



        public OperationResult UpdateHistory(string scode,int cmbHistoryType,int nuRowNumber)
        {
            OperationResult result = new OperationResult(OperationResultType.Success, "成功执行！");
            try
            {
                RemoteResult res = null;

                string msg = string.Empty;

                ushort tmpValue = (ushort)nuRowNumber;
                byte hiByte = Utility.HiByte(tmpValue);
                byte loByte = Utility.LoByte(tmpValue);
                switch ((HistoryType)cmbHistoryType)
                {
                    case HistoryType.读取正常记录:
                        msg = RemoteClient.Instance.SendCommandAndGetResult(new byte[] { (byte)HistoryType.读取正常记录, hiByte, loByte }, CommandConst.读取历史数据, 51, scode, out res);
                        break;
                    case HistoryType.读取参数修改记录:
                        msg = RemoteClient.Instance.SendCommandAndGetResult(new byte[] { (byte)HistoryType.读取参数修改记录, hiByte, loByte }, CommandConst.读取历史数据, 65, scode, out res);
                        break;
                    case HistoryType.模式曲线修改记录:
                        msg = RemoteClient.Instance.SendCommandAndGetResult(new byte[] { (byte)HistoryType.模式曲线修改记录, hiByte, loByte }, CommandConst.读取历史数据, 101, scode, out res);
                        break;
                    case HistoryType.温度曲线修改记录:
                        msg = RemoteClient.Instance.SendCommandAndGetResult(new byte[] { (byte)HistoryType.温度曲线修改记录, hiByte, loByte }, CommandConst.读取历史数据, 255, scode, out res);
                        break;
                    default:
                        break;
                }

                if (!string.IsNullOrEmpty(msg))
                {
                    result.ResultType = OperationResultType.Error;
                    result.Message = msg;
                }
                else
                {
                    result.AppendData = DealCommandResult(res, new { cmbHistoryType = cmbHistoryType, nuRowNumber = nuRowNumber });
                }

                return result;

            }
            catch (Exception ex)
            {
                result.ResultType = OperationResultType.Error;
                result.Message = ex.Message;
                return result;
            }
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scode"></param>
        /// <returns></returns>
        public OperationResult UpdateWorkMode(string scode)
        {
            OperationResult result = new OperationResult(OperationResultType.Success, "成功执行！");
            try
            {

                RemoteResult res = null;
                string msg = RemoteClient.Instance.SendCommandAndGetResult(new byte[] { (byte)SettingType.整体参数 }, CommandConst.读取测量值, 48, scode, out res);
                result.AppendData = res;

                return result;

            }
            catch (Exception ex)
            {
                result.ResultType = OperationResultType.Error;
                result.Message = ex.Message;
                return result;
            }
        }

        public OperationResult UpdateValve(string scode)
        {
            OperationResult result = new OperationResult(OperationResultType.Success, "成功执行！");
            try
            {

                RemoteResult res = null;
                string msg = RemoteClient.Instance.SendCommandAndGetResult(new byte[] { (byte)SettingType.整体参数 }, CommandConst.读取测量值, 48, scode, out res);
                result.AppendData = res;

                return result;

            }
            catch (Exception ex)
            {
                result.ResultType = OperationResultType.Error;
                result.Message = ex.Message;
                return result;
            }
        }

        public OperationResult UpdateFix(string scode)
        {
            OperationResult result = new OperationResult(OperationResultType.Success, "成功执行！");
            try
            {

                RemoteResult res = null;
                string msg = RemoteClient.Instance.SendCommandAndGetResult(new byte[] { (byte)SettingType.整体参数 }, CommandConst.读取测量值, 48, scode, out res);
                result.AppendData = res;

                return result;

            }
            catch (Exception ex)
            {
                result.ResultType = OperationResultType.Error;
                result.Message = ex.Message;
                return result;
            }
        }

        public OperationResult SetTimeSpan(string scode, int sid, int timeSpanType)
        {
            OperationResult result = new OperationResult(OperationResultType.Success, "成功执行！");
            try
            {

                RemoteResult res = null;

                var timespans = DeviceService.TimeSpanSettings
                    .Where(r => r.DeviceId == sid && r.TimeSpanID == timeSpanType).OrderBy(r=>r.Id)
                    .ToList();


                int i = 0;
                byte[] arr = new byte[75];
                string startTime = "";
                string endTime = "";
                byte hour1 = 0;
                byte mintute1 = 0;
                byte hour2 = 0;
                byte mintute2 = 0;
                //初始化
                for (i = 0; i < 75; i++)
                {
                    arr[i] = 0xFF;
                }
                //先设置第一组
                var row1 = timespans[0];
                startTime = row1.StartTime;
                endTime = row1.EndTime;
                hour1 = byte.Parse(startTime.Substring(0, 2));
                mintute1 = byte.Parse(startTime.Substring(3, 2));
                hour2 = byte.Parse(endTime.Substring(0, 2));
                mintute2 = byte.Parse(endTime.Substring(3, 2));
                arr[1] = hour1;
                arr[2] = mintute1;
                arr[3] = (byte)Utility.SetIntegerSomeBit(6, byte.Parse(row1.CurveCode.ToString()),
                    row1.Flag.GetInt(0) == 1);
                arr[4] = hour2;
                arr[5] = mintute2;

                for (i = 1; i < timespans.Count; i++)
                {
                    var rowi = timespans[i];
                    endTime = rowi.EndTime;
                    // hour1 = byte.Parse(startTime.Substring(0, 2));
                    // mintute1 = byte.Parse(startTime.Substring(3, 2));
                    hour2 = byte.Parse(endTime.Substring(0, 2));
                    mintute2 = byte.Parse(endTime.Substring(3, 2));
                    //arr[i * 5 + 1] = hour1;
                    //arr[i * 5 + 2] = mintute1;
                    arr[i * 3 + 3] = (byte)Utility.SetIntegerSomeBit(6, byte.Parse(rowi.CurveCode.ToString()), rowi.Flag.GetInt(0) == 1);
                    arr[i * 3 + 4] = hour2;
                    arr[i * 3 + 5] = mintute2;
                }
                TimeSpanType spanType = (TimeSpanType)timeSpanType;
                switch (spanType)
                {
                    case TimeSpanType.假日:
                        arr[0] = 0x23;
                        break;
                    case TimeSpanType.周六:
                        arr[0] = 0x21;
                        break;
                    case TimeSpanType.周日:
                        arr[0] = 0x22;
                        break;
                    case TimeSpanType.工作日:
                        arr[0] = 0x20;
                        break;
                    default:
                        break;
                }

                string msg = RemoteClient.Instance.SendCommandAndGetResult(arr, CommandConst.参数设定, 79, scode, out res);
                result.AppendData = res;

                return result;

            }
            catch (Exception ex)
            {
                result.ResultType = OperationResultType.Error;
                result.Message = ex.Message;
                return result;
            }
        }


        public OperationResult SetWorkMode(string scode, int sid, int timeSpanType)
        {
            OperationResult returnData = new OperationResult(OperationResultType.Success, "成功执行！");
            try
            {

                //RemoteResult res = null;
                //var _deviceService = new DeviceService();
                //var valves = _deviceService.Valveses.FirstOrDefault(r => r.DeviceId == sid);


                //byte a = (byte)valveA.WorkMode.Value;
                //byte b = (byte)valveB.WorkMode.Value;
                //bool aWorkBy = valveA.WorkBy.Value == 0 ? false : true;
                //bool bWorkBy = valveB.WorkBy.Value == 0 ? false : true;
                //b = (byte)(b << 4); //0011 0000
                //byte result = (byte)(a | b);    //1011 1011
                //result = (byte)IEHNCS.Common.Utility.SetIntegerSomeBit(3, result, aWorkBy);
                //result = (byte)IEHNCS.Common.Utility.SetIntegerSomeBit(7, result, bWorkBy);
                ////result is workmode
                //byte[] datas = new byte[44];
                //datas[0] = (byte)SettingType.整体参数;
                //datas[1] = result;
                //datas[2] = (byte)valveA.MinValue.Value;
                //datas[3] = (byte)valveA.MaxValue.Value;
                //datas[4] = (byte)valveB.MinValue.Value;
                //datas[5] = (byte)valveB.MaxValue.Value;
                //ushort tmpValue = (ushort)valveA.CtrlInterval.Value;
                //datas[6] = Utility.HiByte(tmpValue);
                //datas[7] = Utility.LoByte(tmpValue);
                //tmpValue = (ushort)valveB.CtrlInterval.Value;
                //datas[8] = Utility.HiByte(tmpValue);
                //datas[9] = Utility.LoByte(tmpValue);
                //datas[10] = (byte)valveA.Step1.Value;
                //datas[11] = (byte)valveA.Step2.Value;
                //datas[12] = (byte)valveA.Step3.Value;
                //datas[13] = (byte)valveA.Step4.Value;
                //datas[14] = (byte)valveA.Step5.Value;
                //datas[15] = (byte)valveB.Step1.Value;
                //datas[16] = (byte)valveB.Step2.Value;
                //datas[17] = (byte)valveB.Step3.Value;
                //datas[18] = (byte)valveB.Step4.Value;
                //datas[19] = (byte)valveB.Step5.Value;
                //datas[20] = (byte)valveA.MaxStep.Value;
                //datas[21] = (byte)valveB.MaxStep.Value;
                //datas[22] = (byte)valveA.Steering.Value;
                //datas[23] = (byte)valveB.Steering.Value;
                //datas[24] = 0x0;
                //string strTmp = "";
                //short tmp = 0;
                //if (device.OutdoorFix.Value == 0)
                //{
                //    datas[25] = 0;
                //    datas[26] = 0;
                //}
                //else
                //{
                //    strTmp = device.OutdoorFix.Value.ToString("0.00");
                //    strTmp = strTmp.Replace(".", "");
                //    tmp = short.Parse(strTmp);
                //    datas[25] = (byte)(tmp >> 8);
                //    datas[26] = (byte)(tmp & 0xFF);
                //}
                //if (device.SupplyWaterFix1.Value == 0)
                //{
                //    datas[27] = 0;
                //    datas[28] = 0;
                //}
                //else
                //{
                //    strTmp = device.SupplyWaterFix1.Value.ToString("0.00");
                //    strTmp = strTmp.Replace(".", "");
                //    tmp = short.Parse(strTmp);
                //    datas[27] = (byte)(tmp >> 8);
                //    datas[28] = (byte)(tmp & 0xFF);
                //}
                //if (device.SupplyWaterFix2.Value == 0)
                //{
                //    datas[29] = 0;
                //    datas[30] = 0;
                //}
                //else
                //{
                //    strTmp = device.SupplyWaterFix2.Value.ToString("0.00");
                //    strTmp = strTmp.Replace(".", "");
                //    tmp = short.Parse(strTmp);
                //    datas[29] = (byte)(tmp >> 8);
                //    datas[30] = (byte)(tmp & 0xFF);
                //}
                //if (device.BackFix1.Value == 0)
                //{
                //    datas[31] = 0;
                //    datas[32] = 0;
                //}
                //else
                //{
                //    strTmp = device.BackFix1.Value.ToString("0.00");
                //    strTmp = strTmp.Replace(".", "");
                //    tmp = short.Parse(strTmp);
                //    datas[31] = (byte)(tmp >> 8);
                //    datas[32] = (byte)(tmp & 0xFF);
                //}
                //if (device.BackFix2.Value == 0)
                //{
                //    datas[33] = 0;
                //    datas[34] = 0;
                //}
                //else
                //{
                //    strTmp = device.BackFix2.Value.ToString("0.00");
                //    strTmp = strTmp.Replace(".", "");
                //    tmp = short.Parse(strTmp);
                //    datas[33] = (byte)(tmp >> 8);
                //    datas[34] = (byte)(tmp & 0xFF);
                //}
                //if (device.FixWater1.Value == 0)
                //{
                //    datas[35] = 0;
                //    datas[36] = 0;
                //}
                //else
                //{
                //    strTmp = device.FixWater1.Value.ToString("0.00");
                //    strTmp = strTmp.Replace(".", "");
                //    tmp = short.Parse(strTmp);
                //    datas[35] = (byte)(tmp >> 8);
                //    datas[36] = (byte)(tmp & 0xFF);
                //}
                //if (device.FixWater2.Value == 0)
                //{
                //    datas[37] = 0;
                //    datas[38] = 0;
                //}
                //else
                //{
                //    strTmp = device.FixWater2.Value.ToString("0.00");
                //    strTmp = strTmp.Replace(".", "");
                //    tmp = short.Parse(strTmp);
                //    datas[37] = (byte)(tmp >> 8);
                //    datas[38] = (byte)(tmp & 0xFF);
                //}
                //tmpValue = (ushort)device.SaveInterval.Value;
                //datas[39] = Utility.HiByte(tmpValue);
                //datas[40] = Utility.LoByte(tmpValue);
                //datas[41] = (byte)device.CtrlNumber.Value;
                //datas[42] = 0xFF;
                //datas[43] = 0xFF;

                //string msg = RemoteClient.Instance.SendCommandAndGetResult(datas, CommandConst.参数设定, 48, scode, out res);
                //returnData.AppendData = res;

                return returnData;

            }
            catch (Exception ex)
            {
                returnData.ResultType = OperationResultType.Error;
                returnData.Message = ex.Message;
                return returnData;
            }
        }

        private OperationResult GetFirstParaTypeAndSend(int deviceId)
        {
            var valveA = DeviceService.Valveses.FirstOrDefault(r => r.DeviceId == deviceId && r.Name == "A");
            var valveB = DeviceService.Valveses.FirstOrDefault(r => r.DeviceId == deviceId && r.Name == "B");

            var device = DeviceService.Devices.FirstOrDefault(r => r.Id == deviceId);
            if (device == null) return null;

            RemoteResult res = null;
            OperationResult result1 = new OperationResult(OperationResultType.Success, "成功执行！");

            try
            {
                valveA = (valveA == null) ? (new Valves()) : valveA;
                valveB = (valveB == null) ? (new Valves()) : valveB;

                byte a = (byte)valveA.WorkMode;
                byte b = (byte)valveB.WorkMode;
                bool aWorkBy = valveA.WorkBy == 0 ? false : true;
                bool bWorkBy = valveB.WorkBy == 0 ? false : true;
                b = (byte)(b << 4); //0011 0000
                byte result = (byte)(a | b);    //1011 1011
                result = (byte)IEHNCS.Common.Utility.SetIntegerSomeBit(3, result, aWorkBy);
                result = (byte)IEHNCS.Common.Utility.SetIntegerSomeBit(7, result, bWorkBy);
                //result is workmode
                byte[] datas = new byte[44];
                datas[0] = (byte)SettingType.整体参数;
                datas[1] = result;
                datas[2] = (byte)valveA.MinValue;
                datas[3] = (byte)valveA.MaxValue;
                datas[4] = (byte)valveB.MinValue;
                datas[5] = (byte)valveB.MaxValue;
                ushort tmpValue = (ushort)valveA.CtrlInterval;
                datas[6] = Utility.HiByte(tmpValue);
                datas[7] = Utility.LoByte(tmpValue);
                tmpValue = (ushort)valveB.CtrlInterval;
                datas[8] = Utility.HiByte(tmpValue);
                datas[9] = Utility.LoByte(tmpValue);
                datas[10] = (byte)valveA.Step1;
                datas[11] = (byte)valveA.Step2;
                datas[12] = (byte)valveA.Step3;
                datas[13] = (byte)valveA.Step4;
                datas[14] = (byte)valveA.Step5;
                datas[15] = (byte)valveB.Step1;
                datas[16] = (byte)valveB.Step2;
                datas[17] = (byte)valveB.Step3;
                datas[18] = (byte)valveB.Step4;
                datas[19] = (byte)valveB.Step5;
                datas[20] = (byte)valveA.MaxStep;
                datas[21] = (byte)valveB.MaxStep;
                datas[22] = (byte)valveA.Steering;
                datas[23] = (byte)valveB.Steering;
                datas[24] = 0x0;
                string strTmp = "";
                short tmp = 0;
                if (device.OutdoorFix.Value == 0)
                {
                    datas[25] = 0;
                    datas[26] = 0;
                }
                else
                {
                    strTmp = device.OutdoorFix.Value.ToString("0.00");
                    strTmp = strTmp.Replace(".", "");
                    tmp = short.Parse(strTmp);
                    datas[25] = (byte)(tmp >> 8);
                    datas[26] = (byte)(tmp & 0xFF);
                }
                if (device.SupplyWaterFix1.Value == 0)
                {
                    datas[27] = 0;
                    datas[28] = 0;
                }
                else
                {
                    strTmp = device.SupplyWaterFix1.Value.ToString("0.00");
                    strTmp = strTmp.Replace(".", "");
                    tmp = short.Parse(strTmp);
                    datas[27] = (byte)(tmp >> 8);
                    datas[28] = (byte)(tmp & 0xFF);
                }
                if (device.SupplyWaterFix2.Value == 0)
                {
                    datas[29] = 0;
                    datas[30] = 0;
                }
                else
                {
                    strTmp = device.SupplyWaterFix2.Value.ToString("0.00");
                    strTmp = strTmp.Replace(".", "");
                    tmp = short.Parse(strTmp);
                    datas[29] = (byte)(tmp >> 8);
                    datas[30] = (byte)(tmp & 0xFF);
                }
                if (device.BackFix1.Value == 0)
                {
                    datas[31] = 0;
                    datas[32] = 0;
                }
                else
                {
                    strTmp = device.BackFix1.Value.ToString("0.00");
                    strTmp = strTmp.Replace(".", "");
                    tmp = short.Parse(strTmp);
                    datas[31] = (byte)(tmp >> 8);
                    datas[32] = (byte)(tmp & 0xFF);
                }
                if (device.BackFix2.Value == 0)
                {
                    datas[33] = 0;
                    datas[34] = 0;
                }
                else
                {
                    strTmp = device.BackFix2.Value.ToString("0.00");
                    strTmp = strTmp.Replace(".", "");
                    tmp = short.Parse(strTmp);
                    datas[33] = (byte)(tmp >> 8);
                    datas[34] = (byte)(tmp & 0xFF);
                }
                if (device.FixWater1.Value == 0)
                {
                    datas[35] = 0;
                    datas[36] = 0;
                }
                else
                {
                    strTmp = device.FixWater1.Value.ToString("0.00");
                    strTmp = strTmp.Replace(".", "");
                    tmp = short.Parse(strTmp);
                    datas[35] = (byte)(tmp >> 8);
                    datas[36] = (byte)(tmp & 0xFF);
                }
                if (device.FixWater2.Value == 0)
                {
                    datas[37] = 0;
                    datas[38] = 0;
                }
                else
                {
                    strTmp = device.FixWater2.Value.ToString("0.00");
                    strTmp = strTmp.Replace(".", "");
                    tmp = short.Parse(strTmp);
                    datas[37] = (byte)(tmp >> 8);
                    datas[38] = (byte)(tmp & 0xFF);
                }
                tmpValue = (ushort)device.SaveInterval.Value;
                datas[39] = Utility.HiByte(tmpValue);
                datas[40] = Utility.LoByte(tmpValue);
                datas[41] = (byte)device.CtrlNumber.Value;
                datas[42] = 0xFF;
                datas[43] = 0xFF;

                string msg = RemoteClient.Instance.SendCommandAndGetResult(datas, CommandConst.参数设定, 48, device.DeviceCode, out res);
                result1.AppendData = res;
                return result1;
            }
            catch (Exception ex)
            {
                result1.ResultType = OperationResultType.Error;
                result1.Message = ex.Message;
                return result1;
            }

        }




        public OperationResult SetDeviceCurveLibrary(string scode, int sid)
        {
            var curvelist = DeviceService.DeviceCureLibraries.Where(r => r.DeviceId == sid).ToList();

            //if (dt.Rows.Count != 30)
            //{
            //    ShowMessage("曲线库数量不正确，必须为30条时才能下发！");
            //    return;
            //} 
            RemoteResult res = null;
            OperationResult result = new OperationResult(OperationResultType.Success, "成功执行！");
            try
            {
                if (curvelist.Count > 0)
                {
                    int rowCount = curvelist.Count;
                    string strTmp = "";
                    short tmp = 0;
                    byte[] arr = new byte[243];
                    int colCount = curvelist.Count;
                    for (int j = 0; j < rowCount; j++)
                    {
                        var cur = curvelist[j];

                        arr[0] = (byte)(cur.Code + 1);    //type
                        for (int i = 1; i <= 121; i++)
                        {
                            var temp = cur.GetValue("Column" + i.ToString()).GetString();
                            strTmp = Convert.ToDecimal(temp).ToString("0.00");
                            strTmp = strTmp.Replace(".", "");
                            tmp = short.Parse(strTmp);
                            arr[((i - 4) * 2 + 1)] = (byte)(tmp >> 8);
                            arr[((i - 4) * 2 + 2)] = (byte)(tmp & 0xFF);
                        }


                        // SendCommand(arr, CommandConst.参数设定, 247);
                        //Thread.Sleep(600);
                    }
                    string msg = RemoteClient.Instance.SendCommandAndGetResult(arr, CommandConst.参数设定, 247, scode, out res);
                    result.AppendData = res;
                   
                }
                result.ResultType=OperationResultType.Error;
                result.Message = "没有曲线库";
                return result;
            }
            catch (Exception ex)
            {
                result.ResultType = OperationResultType.Error;
                result.Message = ex.Message;
                return result;
            }


        }

        public OperationResult UpdateDeviceCurveLibrary(string scode)
        {
            OperationResult result = new OperationResult(OperationResultType.Success, "成功执行！");
            try
            {
                RemoteResult res = null;
                for (byte i = (byte)SettingType.曲线起始参数; i <= (byte)SettingType.曲线终止参数; i++)
                {
                    //SendCommand(new byte[] { i }, CommandConst.读取参数, 247);

                    string msg = RemoteClient.Instance.SendCommandAndGetResult(new byte[] { i }, CommandConst.读取参数, 247, scode, out res);

                    if (!string.IsNullOrEmpty(msg))
                    {

                        result.ResultType = OperationResultType.Error;
                        result.Message = msg;

                        break;
                    }
                    else
                    {
                        DealCommandResult(res);
                    }

                    Thread.Sleep(600);
                }

                return result;

            }
            catch (Exception ex)
            {
                result.ResultType = OperationResultType.Error;
                result.Message = ex.Message;
                return result;
            }
            
        }

        public OperationResult SaveAndSendDeviceCurveLibrary(string scode,int deviceId)
        {
            OperationResult result = new OperationResult(OperationResultType.Success, "成功执行！");
            try
            {
                RemoteResult res = null;


                var curs = DeviceService.DeviceCureLibraries
                    .Where(r => r.DeviceId == deviceId).OrderBy(r => r.Id)
                    .ToList();

                int rowCount = curs.Count;
                string strTmp = "";
                short tmp = 0;
                byte[] arr = new byte[243];
                int colCount = 121;//dt.Columns.Count;
                for (int j = 0; j < rowCount; j++)
                {
                    var row = curs[j];
                    arr[0] = (byte)(Convert.ToInt32(row.Code) + 1);    //type
                    for (int i = 1; i <=121; i++)
                    {
                        strTmp = Convert.ToDecimal(row.GetValue("Column" + i.ToString())).ToString("0.00");
                        strTmp = strTmp.Replace(".", "");
                        tmp = short.Parse(strTmp);
                        arr[((i - 1) * 2 + 1)] = (byte)(tmp >> 8);
                        arr[((i - 1) * 2 + 2)] = (byte)(tmp & 0xFF);
                    }

                    string msg = RemoteClient.Instance.SendCommandAndGetResult(arr, CommandConst.参数设定, 247, scode, out res);

                    if (!string.IsNullOrEmpty(msg))
                    {

                        result.ResultType = OperationResultType.Error;
                        result.Message = msg;

                        break;
                    }
                    else
                    {
                        DealCommandResult(res);
                    }

                    Thread.Sleep(600);
                }

                return result;

            }
            catch (Exception ex)
            {
                result.ResultType = OperationResultType.Error;
                result.Message = ex.Message;
                return result;
            }
        }





    }//
}//
