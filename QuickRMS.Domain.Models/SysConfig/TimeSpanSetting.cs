using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Quick.Framework.Tool.Entity;

namespace QuickRMS.Domain.Models.SysConfig
{
    public class TimeSpanSetting : EntityBase<int>
    {
        public int DeviceId { get; set; }
        public int TimeSpanID { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int CurveCode { get; set; }

        public int? Flag { get; set; }
    }
}
    