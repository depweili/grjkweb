using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickRMS.Domain.Models.DeviceInfo;

namespace QuickRMS.Core.Service.Authen
{
   public interface IDeviceCureLibraryService
    {
        #region 属性

        IQueryable<DeviceCureLibrary> DeviceCureLibraries { get; }

        #endregion

        #region 公共方法

        List<DeviceCureLibrary> GetDeviceCureLibraries(int deviceId);

        #endregion
    }
}
