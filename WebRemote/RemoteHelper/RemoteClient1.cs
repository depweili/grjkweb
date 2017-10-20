using Common;
using IEHNCS.Common;
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
    public class RemoteClient1
    {
        private static RemoteClient1 instance = null;
        private static readonly object padlock = new object();
        private static ConcurrentDictionary<string, RemoteResult> _relist = new ConcurrentDictionary<string, RemoteResult>();
        private static ConcurrentBag<string> _offdevlist = new ConcurrentBag<string>();

        private static RemoteCommandClient commandClient;

        private RemoteCommand remoteCommand;
        //private RemoteCommandClient commandClient;
        private bool status;

        RemoteClient1()
        {
            try
            {
                //Reachability.ServerSinkProvider.StartWaitRedirectedMsg();
                remoteCommand = new RemoteCommand();
                commandClient = new RemoteCommandClient();
                commandClient.Sender = "WebAdminZ2";
                commandClient.ReceiveCommandResultEvent += new ReceiveCommandResultEventHandler(client_ReceiveCommandResultEvent);
                commandClient.DeviceOfflineEvent += new DeviceOnlineOfflineEventHandler(client_DeviceOfflineEvent);
                commandClient.DeviceOnlineEvent += new DeviceOnlineOfflineEventHandler(client_DeviceOnlineEvent);
                commandClient.LogOffEvent += new EventHandler(commandClient_LogOffEvent);
                //ObjRef or = RemotingServices.Marshal(commandClient as MarshalByRefObject);
                
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
                //throw new Exception("连接服务器时出现异常，请重新尝试！异常信息" + ex.Message);
            }
        }

        public static RemoteClient1 Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null || !instance.status)
                    {
                        instance = new RemoteClient1();
                    }
                    //instance = new RemoteClient();
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
                }

                Reachability.ServerSinkProvider.StopWaitRedirectedMsg();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<KeyValuePair<string, RemoteResult>> GetResultList()
        {
            return _relist.ToList();
        }

        public bool ClientReAttach()
        {
            if (commandClient != null)
            {
                try
                {
                    //RemotingServices.Disconnect(commandClient);
                }
                catch (Exception ex)
                {
                }
            }

            commandClient = new RemoteCommandClient();
            commandClient.Sender = "WebAdminZ2";
            commandClient.ReceiveCommandResultEvent += new ReceiveCommandResultEventHandler(client_ReceiveCommandResultEvent);
            commandClient.DeviceOfflineEvent += new DeviceOnlineOfflineEventHandler(client_DeviceOfflineEvent);
            commandClient.DeviceOnlineEvent += new DeviceOnlineOfflineEventHandler(client_DeviceOnlineEvent);
            commandClient.LogOffEvent += new EventHandler(commandClient_LogOffEvent);
            //ObjRef or = RemotingServices.Marshal(commandClient as MarshalByRefObject);

            if (!remoteCommand.AttachClient(commandClient))
            {
                status = false;
                throw new Exception("重新自动连接失败！");
            }
            else
            {
                status = true;

            }

            return status;
        }

        public void ClientClose()
        {
            //if (commandClient != null)
            //{
            //    commandClient.DeviceOfflineEvent -= client_DeviceOfflineEvent;
            //    commandClient.DeviceOnlineEvent -= client_DeviceOnlineEvent;
            //    commandClient.ReceiveCommandResultEvent -= client_ReceiveCommandResultEvent;
            //    commandClient.LogOffEvent -= commandClient_LogOffEvent;
            //    if (status)
            //    {
            //        remoteCommand.DetachClient(commandClient);
            //    }
            //}

            if (commandClient != null)
            {
                //RemotingServices.Disconnect(commandClient);
            }

            //RemotingServices.Disconnect(remoteCommand);
        }

        public static void ReRegister()
        {
            Close();
            UnRegisterChannels();

            String configfilename = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            RemotingConfiguration.Configure(configfilename, false);
        }

        public static void UnRegisterChannels()
        {

            IChannel[] channels = ChannelServices.RegisteredChannels;

            foreach (IChannel channel in channels)
            {

                if (channel.ChannelName.ToLower().IndexOf("http") >= 0) // http
                {

                    HttpChannel httpChannel = (HttpChannel)channel;

                    //Close listening

                    httpChannel.StopListening(null);

                    //Unregister channel

                    ChannelServices.UnregisterChannel(httpChannel);

                }

                if (channel.ChannelName.ToLower().IndexOf("tcp") >= 0) // tcp
                {

                    TcpChannel tcpChannel = (TcpChannel)channel;

                    //Close listening

                    tcpChannel.StopListening(null);

                    //Unregister channel

                    ChannelServices.UnregisterChannel(tcpChannel);

                }

            }

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
            if (!_offdevlist.Contains(deviceCode))
            {
                _offdevlist.Add(deviceCode);
            }
            
        }
        private void client_DeviceOnlineEvent(string deviceCode)
        {
            //写入数据库 deviceCode 设备上线
            //delegateDeviceOnline(deviceCode)
            _offdevlist.TryTake(out deviceCode);
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
                res = GetRemoteResult(guid, devicecode);
            }

            //重试一次
            //if (res == null)
            //{
            //    if (ClientReAttach())
            //    {
            //        msg = SendCommand(datas, name, returnLength, devicecode, guid);
            //        if (string.IsNullOrEmpty(msg))
            //        {
            //            res = GetRemoteResult(guid, devicecode);
            //        }
            //    }
            //}

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
                    bool result = remoteCommand.SendCommand(datas, name, returnLength, "WebAdminZ2", devicecode, extInfo);
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

        private RemoteResult GetRemoteResult(string guid, string deviceCode)
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

            if (res == null)
            {
                if (_offdevlist.Contains(deviceCode))
                {
                    string ss = "下线";
                    //res = new RemoteResult("", "", deviceCode + "下线", null, null);
                }
            }

            return res;
        }


    }
}
