using Common;
using IEHNCS.Common;
using RemoteObjectLibrary;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;

namespace RemoteHelper
{
    public class RemoteClient
    {
        private static RemoteClient instance = null;
        private static readonly object padlock = new object();
        private static ConcurrentDictionary<string, RemoteResult> _relist = new ConcurrentDictionary<string, RemoteResult>();


        private RemoteCommand remoteCommand;
        private RemoteCommandClient commandClient;
        private bool status;

        RemoteClient()
        {
            try
            {
                Reachability.ServerSinkProvider.StartWaitRedirectedMsg();
                remoteCommand = new RemoteCommand();
                commandClient = new RemoteCommandClient();
                commandClient.Sender = "WebAdminX1";
                commandClient.ReceiveCommandResultEvent += new ReceiveCommandResultEventHandler(client_ReceiveCommandResultEvent);
                commandClient.DeviceOfflineEvent += new DeviceOnlineOfflineEventHandler(client_DeviceOfflineEvent);
                commandClient.DeviceOnlineEvent += new DeviceOnlineOfflineEventHandler(client_DeviceOnlineEvent);
                commandClient.LogOffEvent += new EventHandler(commandClient_LogOffEvent);
                ObjRef or = RemotingServices.Marshal(commandClient as MarshalByRefObject);
                if (!remoteCommand.AttachClient(commandClient))
                {
                    status = false;
                    throw new Exception("登录失败,请查看日志！");
                }
                else
                {
                    status = true;
                }
            }
            catch (Exception ex)
            {
                status = false;
                throw new Exception("连接服务器时出现异常，请重新尝试！异常信息" + ex.Message);
            }
        }

        public static RemoteClient Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null || !instance.status)
                    {
                        instance = new RemoteClient();
                    }
                    return instance;
                }
            }
        }

        public static void Close()
        {
            try
            {
                if (instance != null)
                {
                    instance.ClientClose();
                    instance = null;
                }

                Reachability.ServerSinkProvider.StopWaitRedirectedMsg();
            }
            catch (Exception)
            {
                try
                {
                    Reachability.ServerSinkProvider.StopWaitRedirectedMsg();
                }
                catch
                {
                }
            }
        }

        public void ClientClose()
        {
            if (commandClient != null)
            {
                commandClient.DeviceOfflineEvent -= client_DeviceOfflineEvent;
                commandClient.DeviceOnlineEvent -= client_DeviceOnlineEvent;
                commandClient.ReceiveCommandResultEvent -= client_ReceiveCommandResultEvent;
                commandClient.LogOffEvent -= commandClient_LogOffEvent;
                if (status)
                {
                    remoteCommand.DetachClient(commandClient);
                }
            }
        }

        void client_ReceiveCommandResultEvent(string senderUser, CommandConst name, string deviceCode, byte[] Datas, object tag)
        {
            _relist.TryAdd(tag.ToString(), new RemoteResult(senderUser, name, deviceCode, Datas, tag));
        }

        //这几个事件都是异步的触发，只能处理总体的业务，可以不管，我也可以委托出来几个，谢谢
        private void commandClient_LogOffEvent(object sender, EventArgs e)
        {
            //表示本客户端下线了
            //delegateLogOff()
        }

        void client_DeviceOfflineEvent(string deviceCode)
        {
            //写入数据库 deviceCode 设备下线
            //delegateDeviceOffline(deviceCode)
        }
        private void client_DeviceOnlineEvent(string deviceCode)
        {
            //写入数据库 deviceCode 设备上线
            //delegateDeviceOnline(deviceCode)
        }

        public string SendCommandAndGetResult(byte[] datas, CommandConst name, int returnLength, string devicecode, out RemoteResult res)
        {
            res = null;
            string msg = string.Empty;

            if (string.IsNullOrEmpty(devicecode))
            {
                return "设备号不可为空";
            }
            var guid = Guid.NewGuid().ToString();

            msg = SendCommand(datas, name, returnLength, devicecode, guid);

            if (string.IsNullOrEmpty(msg))
            {
                res = GetRemoteResult(guid);
            }

            return msg;
        }

        private string SendCommand(byte[] datas, CommandConst name, int returnLength,string devicecode,object extInfo)
        {
            string msg = string.Empty;
            if (remoteCommand != null)
            {
                try
                {
                    //bool result = remoteCommand.SendCommand(datas, name, returnLength, CurrentUser.UserCode, currentDevice.Code, null);
                    bool result = remoteCommand.SendCommand(datas, name, returnLength, "WebAdminX1", devicecode, extInfo);
                    if (!result)
                    {
                        msg = "请求失败！";

                    }
                }
                catch (Exception ex)
                {
                    msg = "请求异常：" + ex.Message;
                }
            }
            else
            {
                this.status = false;
                msg = "远程对象出错，请重试！";
            }

            return msg;
        }

        private RemoteResult GetRemoteResult(string guid)
        {
            RemoteResult res = null;
            for (int i = 0; i < 5; i++) {
                //if (_relist.ContainsKey(guid) && _relist.TryRemove(guid, out res))
                if (_relist.TryRemove(guid, out res))
                {
                    break;
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }

            return res;
        }


    }
}
