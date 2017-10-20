
using System.ComponentModel.DataAnnotations;

namespace QuickRMS.Site.Models.Device
{
    public class HistoryInfo1Model
    {
        [Display(Name = "时间")]
        public string Time { get; set; }

        [Display(Name = "工作模式")]
        public string WorkMode { get; set; }

        [Display(Name = "最小开度1")]
        public string Min1 { get; set; }

        [Display(Name = "最大开度1")]
        public string Max1 { get; set; }

        [Display(Name = "最小开度2")]
        public string Min2 { get; set; }

        [Display(Name = "最大开度2")]
        public string Max2 { get; set; }

        [Display(Name = "A阀门控制间隔")]
        public string AInterval { get; set; }

        [Display(Name = "B阀门控制间隔")]
        public string BInterval { get; set; }

        [Display(Name = "A阀门微调1")]
        public string AStep1 { get; set; }

        [Display(Name = "A阀门微调1")]
        public string AStep2 { get; set; }

        [Display(Name = "A阀门微调1")]
        public string AStep3 { get; set; }

        [Display(Name = "A阀门微调1")]
        public string AStep4 { get; set; }

        [Display(Name = "A阀门微调1")]
        public string AStep5 { get; set; }

        [Display(Name = "B阀门微调1")]
        public string BStep1 { get; set; }

        [Display(Name = "B阀门微调1")]
        public string BStep2 { get; set; }

        [Display(Name = "B阀门微调1")]
        public string BStep3 { get; set; }

        [Display(Name = "B阀门微调1")]
        public string BStep4 { get; set; }

        [Display(Name = "B阀门微调1")]
        public string BStep5 { get; set; }

        [Display(Name = "A阀门最大步距")]
        public string AMaxStep { get; set; }

        [Display(Name = "B阀门最大步距")]
        public string BMaxStep { get; set; }

        [Display(Name = "A阀门转向")]
        public string ASteer { get; set; }

        [Display(Name = "B阀门转向")]
        public string BSteer { get; set; }

        [Display(Name = "无线开关状态")]
        public string Wire { get; set; }

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

        [Display(Name = "定时")]
        public string Save { get; set; }

        [Display(Name = "阀门控制路数")]
        public string CtrlNumber { get; set; }
    }
}
