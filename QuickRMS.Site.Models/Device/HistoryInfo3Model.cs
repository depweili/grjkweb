using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickRMS.Site.Models.Device
{
    public class HistoryInfo3Model
    {
        [Display(Name = "时间")]
        public string Time { get; set; }

         [Display(Name = "曲线号码")]
        public string Num { get; set; }

        public List<HistoryInfo3> HistoryInfo3List { get; set; }
    }

    public class HistoryInfo3
    {
        public decimal? Value { get; set; }

    }
}
