using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickRMS.Site.Models.Device
{
    public class DeviceMaintenanceModel
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public string MaintenanceDate { get; set; }
        public string Memo { get; set; }

    }
}
