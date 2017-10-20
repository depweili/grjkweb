using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IEHNCS.Model;

namespace RemoteHelper
{
    public class DeviceParaHelper
    {
        public bool IsOnline
        {
            get
            {
                if (Device.IsOnline.HasValue)
                    return Device.IsOnline.Value;
                return false;
            }
            set
            {
                Device.IsOnline = value;
            }
        }
        public string Code
        {
            get;
            set;
        }

        //public TreeNode Node
        //{
        //    get;
        //    set;
        //}
        public Valves ValveA
        {
            get;
            set;
        }

        public Valves ValveB
        {
            get;
            set;
        }

        public Devices Device
        {
            get;
            set;
        }

        public bool GotParameters
        {
            get;
            set;
        }
    }
}
