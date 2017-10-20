using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using IEHNCS.Common;
using Quick.Framework.Tool;
using QuickRMS.Core.Repository.Authen.Impl;
using QuickRMS.Core.Repository.SysConfig;
using QuickRMS.Domain.Models.SysConfig;
using RemoteHelper;

namespace QuickRMS.Core.Service.Authen.Impl
{
    [Export(typeof(ITimeSpanSettingsService))]
    public class TimeSpanSettingsService : CoreServiceBase, ITimeSpanSettingsService
    {
        [Import]
        public ITimeSpanSettingRepository  TimeSpanSettingRepository { get; set; }

        public IQueryable<TimeSpanSetting> TimeSpanSettings
        {
            get { return TimeSpanSettingRepository.Entities; }
        }

        public  OperationResult SetTimeSpan(string scode, int sid, int timeSpanType)
        {
            OperationResult result = new OperationResult(OperationResultType.Success, "成功执行！");
            try
            {

                RemoteResult res = null;
                var _deviceService = new DeviceService();
                var timespans = TimeSpanSettings.Where(r => r.DeviceId == sid).ToList();


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
                    arr[i * 3 + 3] = (byte)Utility.SetIntegerSomeBit(6, byte.Parse(row1.CurveCode.ToString()), row1.Flag.GetInt(0) == 1);
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
    }
}
