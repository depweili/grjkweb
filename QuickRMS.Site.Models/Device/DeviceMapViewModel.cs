using QuickRMS.Domain.Models.DeviceInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickRMS.Site.Models.Device
{
   public class DeviceMapViewModel
    {
       public QuickRMS.Domain.Models.DeviceInfo.Device Device { get; set; }
       public QuickRMS.Domain.Models.DeviceInfo.DeviceData DeviceData { get; set; }
    }
}
