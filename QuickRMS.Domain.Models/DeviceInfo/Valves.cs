using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.Framework.Tool.Entity;
using Newtonsoft.Json;

namespace QuickRMS.Domain.Models.DeviceInfo
{
   public class Valves : EntityBase<int>
    {
        public int DeviceId { get; set; }
        public string Name { get; set; }

       [JsonIgnore]
        public virtual Device Device { get; set; }

        public int Step1 { get; set; }
        public int Step2 { get; set; }
        public int Step3 { get; set; }
        public int Step4 { get; set; }
        public int Step5 { get; set; }

        public int Steering { get; set; }
        public int MaxStep { get; set; }
        public int CtrlInterval { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }

        public int CurrentValue { get; set; }
        public int SetValue { get; set; }
        public int WorkMode { get; set; }
        public int WorkBy { get; set; }
    }
}
