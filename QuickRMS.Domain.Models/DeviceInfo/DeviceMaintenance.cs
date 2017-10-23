using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.Framework.Tool.Entity;
using Newtonsoft.Json;

namespace QuickRMS.Domain.Models.DeviceInfo
{
   public class DeviceMaintenance : EntityBase<int>
    {
       public int DeviceId { get; set; }
       public DateTime MaintenanceDate { get; set; }
       public string Memo { get; set; }

       [JsonIgnore]
       public virtual Device Device { get; set; }
    }
}
