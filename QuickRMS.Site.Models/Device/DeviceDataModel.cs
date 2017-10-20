using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.Site.Common.Models;

namespace QuickRMS.Site.Models.Device
{
    public class DeviceDataModel : EntityCommon
    {
        public int DeviceId { get; set; }
        public DateTime DataTime { get; set; }
        public decimal? OutdoorTemp { get; set; }
        public decimal? SupplyWaterTemp1 { get; set; }
        public decimal? SupplyWaterTemp2 { get; set; }
        public decimal? BackWaterTemp1 { get; set; }
        public decimal? BackWaterTemp2 { get; set; }
        public decimal? FixWaterTemp1 { get; set; }
        public decimal? FixWaterTemp2 { get; set; }
        public int? Valve1 { get; set; }
        public int? Valve2 { get; set; }

        public int WirelessStatus { get; set; }
        public int WorkStatus { get; set; }
        public int? WaterNetStatus1 { get; set; }

        public string WaterNetStatus1Text { get; set; }

        public int? WaterNetStatus2 { get; set; }
        public string WaterNetStatus2Text { get; set; }

        public byte[] Data27 { get; set; }
        public byte[] Data28 { get; set; }
        public byte[] Data29 { get; set; }
        public byte[] Data30 { get; set; }


        public string ControlChannelText { get; set; }
    }
}
