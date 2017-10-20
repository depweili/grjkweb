using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.Framework.Tool.Entity;

namespace QuickRMS.Domain.Models.SysConfig
{
    public class SysSetting : EntityBase<int>
    {
        public string SettingCode { get; set; }
        public string SettingValue { get; set; }
        public string Memo { get; set; }
    }
}
