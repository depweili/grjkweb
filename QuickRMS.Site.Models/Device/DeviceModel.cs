using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.Site.Common.Models;
using QuickRMS.Domain.Models.DeviceInfo;

namespace QuickRMS.Site.Models.Device
{
    public class DeviceModel : EntityCommon
    {
        public int Id { get; set; }
        public string DeviceCode { get; set; }
        public string DeviceName { get; set; }
        public string IP { get; set; }
        public int? Port { get; set; }
        public int CtrlMode { get; set; }
        public int? AreaId { get; set; }
        public DateTime? InstallTime { get; set; }
        public string Memo { get; set; }
        public int? WorkMode { get; set; }
        public int? SaveInterval { get; set; }
        public int? CtrlNumber { get; set; }
        public bool IsInited { get; set; }
        public decimal? OutdoorFix { get; set; }
        public decimal? SupplyWaterFix1 { get; set; }
        public decimal? SupplyWaterFix2 { get; set; }
        public decimal? BackFix1 { get; set; }
        public decimal? BackFix2 { get; set; }
        public decimal? FixWater1 { get; set; }
        public decimal? FixWater2 { get; set; }
        public bool IsOnline { get; set; }

    }
}
