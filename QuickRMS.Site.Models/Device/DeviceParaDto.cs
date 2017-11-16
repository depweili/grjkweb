using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickRMS.Site.Models.Device
{
    public class DeviceParaDto
    {

        public ValvesModel ValveA { get; set; }

        public ValvesModel ValveB { get; set; }

        public DeviceModel Device { get; set; }
    }
}
