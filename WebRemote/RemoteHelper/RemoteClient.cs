using Common;
using IEHNCS.Common;
using Maticsoft.DBUtility;
using RemoteObjectLibrary;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
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
        RemoteClient()
        {
            try
            {
                Reachability.ServerSinkProvider.StartWaitRedirectedMsg();
                remoteCommand = new RemoteCommand();

                if (!CreateCommandClient())
                {
                    throw new Exception("创建远程命令对象失败");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static RemoteClient Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new RemoteClient();
                    }
                    return instance;
                }
            }
        }

        bool CreateCommandClient()
        {
            bool res = false;

            try
            {
                commandClient = new RemoteCommandClient();
                commandClient.Sender = "WebAdminZ2";
                commandClient.ReceiveCommandResultEvent += new ReceiveCommandResultEventHandler(client_ReceiveCommandResultEvent);
                commandClient.DeviceOfflineEvent += new DeviceOnlineOfflineEventHandler(client_DeviceOfflineEvent);
                commandClient.DeviceOnlineEvent += new DeviceOnlineOfflineEventHandler(client_DeviceOnlineEvent);
                commandClient.LogOffEvent += new EventHandler(commandClient_LogOffEvent);
                ObjRef or = RemotingServices.Marshal(commandClient as MarshalByRefObject);

                if (!remoteCommand.AttachClient(commandClient))
                {
                    res = false;
                }
                else
                {
                    res = true;
                }
            }
            catch (Exception ex)
            {
                res = false;
            }

            return res;
             
        }

        private string SendCommand(byte[] datas, CommandConst name, int returnLength, string devicecode, object extInfo)
        {
            string msg = string.Empty;
            if (remoteCommand != null)
            {
                //if (!CreateCommandClient())
                //{
                //    return "远程命令对象创建失败";
                //}
                try
                {
                    //bool result = remoteCommand.SendCommand(datas, name, returnLength, CurrentUser.UserCode, currentDevice.Code, null);
                    bool result = remoteCommand.SendCommand(datas, name, returnLength, commandClient.Sender, devicecode, extInfo);
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
                msg = "远程对象出错，请重试！";
            }

            return msg;
        }

        private RemoteResult GetRemoteResult1(string guid, string deviceCode)
        {
            RemoteResult res = null;
            for (int i = 0; i < 5; i++)
            {
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

        private RemoteResult GetRemoteResult(string guid)
        {
            var sql = string.Format("select cmd_datas from T_CommondResult where cmd_guid='{0}'", guid);
            Thread.Sleep(1000);
            RemoteResult res = null;
            for (int i = 0; i < 5; i++)
            {
                var obj=DbHelperSQL.GetSingle(sql);

                if (obj != null)
                {
                    res = new RemoteResult();
                    res.Datas = (byte[])obj;
                    res.tag = guid;
                    break;
                }
                else
                {
                    Thread.Sleep(500);
                }
            }
            return res;
        }


        void client_ReceiveCommandResultEvent(string senderUser, CommandConst name, string deviceCode, byte[] Datas, object tag)
        {
            _relist.TryAdd(tag.ToString(), new RemoteResult(senderUser, name, deviceCode, Datas, tag));

        }

        //这几个事件都是异步的触发，只能处理总体的业务，可以不管，我也可以委托出来几个
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

            if (res != null) {
                res.deviceCode = devicecode;
                res.name = name;
                res.senderUser = commandClient.Sender;
            }

            return msg;
        }

    }
}
