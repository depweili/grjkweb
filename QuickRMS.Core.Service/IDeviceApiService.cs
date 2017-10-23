using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.Framework.Tool;
using QuickRMS.Core.Service.Authen;

namespace QuickRMS.Core.Service
{
    public interface IDeviceApiService
    {
        IDeviceService DeviceService { get; }

        OperationResult SyncTime(string scode);

        OperationResult UpdateData(string scode, dynamic clientdata = null);

        OperationResult UpdateOutTemData(string scode);

        OperationResult UpdateWorkMode(string scode);


        OperationResult UpdateValve(string scode);

        OperationResult UpdateFix(string scode);

        OperationResult SetTimeSpan(string scode, int sid, int timeSpanType);


        OperationResult SetWorkMode(string scode, int sid, int timeSpanType);

        OperationResult SetDeviceCurveLibrary(string scode, int sid);

    }
}
