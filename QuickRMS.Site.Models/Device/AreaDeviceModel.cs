using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.Site.Common.Models;

namespace QuickRMS.Site.Models.Device
{
    public class AreaDeviceModel 
    {
        public int AreaId { get; set; }
        public string AreaCode { get; set; }
        public string AreaName { get; set; }
        public int ParentId { get; set; }

        public List<AreaDeviceModel> ChildAreaDeviceModels { get; set; }

     
    }
}
