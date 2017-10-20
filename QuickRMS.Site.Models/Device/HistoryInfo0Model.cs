using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickRMS.Site.Models.Device
{
    public class HistoryInfo0Model
    {
        [Display(Name = "类别")]
        public string Type { get; set; }

        [Display(Name = "时间")]
        public string Time { get; set; }

        [Display(Name = "室外")]
        public string Out { get; set; }

        [Display(Name = "供水1")]
        public string Sup1 { get; set; }

        [Display(Name = "供水2")]
        public string Sup2 { get; set; }

        [Display(Name = "回水1")]
        public string Back1 { get; set; }

        [Display(Name = "回水2")]
        public string Back2 { get; set; }

        [Display(Name = "混水1")]
        public string Fix1 { get; set; }

        [Display(Name = "混水2")]
        public string Fix2 { get; set; }

        [Display(Name = "阀门开度A")]
        public string ValveA { get; set; }

        [Display(Name = "阀门开度B")]
        public string ValveB { get; set; }

        [Display(Name = "无线开关状态")]
        public string Wire { get; set; }

        [Display(Name = "工作模式")]
        public string WorkMode { get; set; }

        [Display(Name = "水网1状态")]
        public string Net1 { get; set; }

        [Display(Name = "水网2状态")]
        public string Net2 { get; set; }

        [Display(Name = "工作模式曲线号")]
        public string Curve { get; set; }

        [Display(Name = "外部参考温度1")]
        public string Ref1 { get; set; }

        [Display(Name = "外部参考温度2")]
        public string Ref2 { get; set; }

        [Display(Name = "外部参考温度3")]
        public string Ref3 { get; set; }

        [Display(Name = "外部参考温度4")]
        public string Ref4 { get; set; }

        [Display(Name = "外部参考温度5")]
        public string Ref5 { get; set; }

        [Display(Name = "外部参考温度6")]
        public string Ref6 { get; set; }
    }
}
