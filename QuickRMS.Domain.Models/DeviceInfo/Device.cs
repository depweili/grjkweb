using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.Framework.Tool.Entity;
using QuickRMS.Domain.Models.Authen;
using Newtonsoft.Json;

namespace QuickRMS.Domain.Models.DeviceInfo
{
    public class Device : EntityBase<int>
    {

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

        /// <summary>
        /// 经度
        /// </summary>
        public decimal? Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public decimal? Latitude { get; set; }

        public string Company { get; set; }
        public string Address { get; set; }

        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }

        [JsonIgnore]
        public virtual ICollection<DeviceCureLibrary> DeviceCureLibraries { get; set; }

        [JsonIgnore]
        public virtual ICollection<DeviceMaintenance> DeviceMaintenances { get; set; }

        [JsonIgnore]
        public virtual ICollection<History> Histories { get; set; }

        [JsonIgnore]
        public virtual ICollection<DeviceData> DeviceDatas { get; set; }

        [JsonIgnore]
        public virtual ICollection<Valves> Valveses { get; set; }

        [JsonIgnore]
        public virtual Area Area { get; set; }
    }
}
