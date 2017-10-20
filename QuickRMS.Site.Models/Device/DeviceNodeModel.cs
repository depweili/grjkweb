using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickRMS.Site.Models.Device
{
    public class AreaDeviceNodeModel
    {
        public int Id { get; set; }
        public string DeviceCode { get; set; }
        public string DeviceName { get; set; }
        public bool IsOnline { get; set; }
        public int CtrlNumber { get; set; }

        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }

        public string Text { get; set; }
        public string Icon { get; set; }
        public List<string> Tags { get; set; }
        public string Color { get; set; }
        public int AreaId { get; set; }

        public string AreaName { get; set; }

        public int ParentId { get; set; }
        public List<AreaDeviceNodeModel> Nodes { get; set; }
    }
}