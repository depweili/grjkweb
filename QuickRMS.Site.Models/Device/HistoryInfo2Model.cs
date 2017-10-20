using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickRMS.Site.Models.Device
{
    public class HistoryInfo2Model
    {
        [Display(Name = "时间")]
        public string Time { get; set; }

        [Display(Name = "曲线模式")]
        public string Mode { get; set; }

        public List<HistoryInfo2> HistoryInfo2List { get; set; }

    }

    public class HistoryInfo2
    {
        public int ID { get; set; }
        public int DeviceID { get; set; }
        public int TimeSpanID { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int CurveCode { get; set; }

    }
}
