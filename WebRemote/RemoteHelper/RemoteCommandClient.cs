using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IEHNCS.Common;
using System.Runtime.Remoting.Lifetime;

namespace RemoteHelper
{
    public class RemoteCommandClient : MarshalByRefObject, IRemoteCommand
    {
        public event ReceiveCommandResultEventHandler ReceiveCommandResultEvent;
        public event DeviceOnlineOfflineEventHandler DeviceOnlineEvent;
        public event DeviceOnlineOfflineEventHandler DeviceOfflineEvent;
        public event EventHandler LogOffEvent;

        public delegate void ReceiveEventHandler();

        //public override object InitializeLifetimeService() 
        //{
        //    ILease lease = (ILease)base.InitializeLifetimeService();
        //    if (lease.CurrentState == LeaseState.Initial)
        //    {
        //        lease.InitialLeaseTime = TimeSpan.FromMinutes(1);
        //        lease.RenewOnCallTime = TimeSpan.FromSeconds(30);
        //    }
        //    return lease;
        //}


        #region IRemoteCommand 成员

        public string Sender
        {
            get;
            set;
        }

        public void ReceiveCommandResult(string senderUser, global::Common.CommandConst name, string deviceCode, byte[] datas, object tag)
        {
            if (ReceiveCommandResultEvent != null)
                ReceiveCommandResultEvent(senderUser, name, deviceCode, datas, tag);
        }

        public void DeviceOnline(string deviceCode)
        {
            if (DeviceOnlineEvent != null)
                DeviceOnlineEvent(deviceCode);
        }

        public void DeviceOffline(string deviceCode)
        {
            if (DeviceOfflineEvent != null)
                DeviceOfflineEvent(deviceCode);
        }

        public void LogOff()
        {
            if (LogOffEvent != null)
                LogOffEvent(this, null);
        }
        #endregion
    }
}
