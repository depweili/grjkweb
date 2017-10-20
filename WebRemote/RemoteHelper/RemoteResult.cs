using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteHelper
{
    public class RemoteResult
    {
        public string senderUser;
        public CommandConst name;
        public string deviceCode;
        public byte[] Datas;
        public object tag;

        public RemoteResult() { }
        public RemoteResult(string senderUser, CommandConst name, string deviceCode, byte[] Datas, object tag)
        {
            // TODO: Complete member initialization
            this.senderUser = senderUser;
            this.name = name;
            this.deviceCode = deviceCode;
            this.Datas = Datas;
            this.tag = tag;
        }
        
    }
}
