using System;
using System.Collections.Generic;
using System.Text;
using IEHNCS.Common;
using Common;
using log4net;
using System.Collections;
using Maticsoft.DBUtility;


namespace RemoteObjectLibrary
{
    public delegate void UserLoginOutEventHandler(string senderUser);
    public delegate void SendCommandEventHandler(byte[] parameter, Common.CommandConst name, int returnLength, string senderUser, string deviceCode, object tag);
    public class RemoteCommand : MarshalByRefObject
    {
        private readonly object lockObject = new object();
        Dictionary<string, IRemoteCommand> dicClients = new Dictionary<string, IRemoteCommand>();

        

        public event UserLoginOutEventHandler UserLogin;
        public event UserLoginOutEventHandler UserLogout;
        public event SendCommandEventHandler SendCommandEvent;
        public ILog log;

        public RemoteCommand()
        {
            log = log4net.LogManager.GetLogger("myLooger");
        }
        public int SendCommandResult(string senderUser, CommandConst name, string deviceCode, byte[] datas, object tag)
        {
            try
            {
                if (dicClients.ContainsKey(senderUser))
                {
                    log.Info("[SendCommandResult]返回消息给：" + senderUser + "[name]:" + name.ToString() + "[datas]:" + bytetostring(datas));
                    var sql = string.Format("insert into T_CommondResult (cmd_guid,cmd_datas) values ('{0}',@fs)", tag.ToString());
                    DbHelperSQL.ExecuteSqlInsertImg(sql, datas);
                    dicClients[senderUser].ReceiveCommandResult(senderUser, name, deviceCode, datas, tag);
                }
                else
                {
                    log.Info("[SendCommandResult]未找到：" + senderUser);
                }
                    
                return 1;
            }
            catch(Exception ex)
            {
                log.Info("[SendCommandResult]" + ex.Message);
                return 0;
            }
        }

        string bytetostring(byte[] Datas)
        {
            var msg = "";
            for (int i = 0; i < Datas.Length; i++)
            {
                msg += Datas[i].ToString("X2") + " ";
            }
            return msg;
        }

        public bool AttachClient(IRemoteCommand iClient)
        { 
            if (iClient == null)
                return false;
            IRemoteCommand iPreviousClient = null;
            try
            {
                lock (lockObject)
                {

                    if (dicClients.ContainsKey(iClient.Sender))
                    {
                        iPreviousClient = dicClients[iClient.Sender];
                        dicClients.Remove(iClient.Sender);
                    }
                    dicClients.Add(iClient.Sender, iClient);
                }
                if (UserLogin != null)
                {
                    UserLogin(iClient.Sender);
                }

            }
            catch (Exception ex)
            {
                log.Info(iClient.Sender + "登录时发生错误", ex);
                return false;
            }
            try
            {
                if (iPreviousClient != null)
                {
                    iPreviousClient.LogOff();
                }
            }
            catch
            {

            }
            return true;
        }
        public void DetachClient(IRemoteCommand iClient)
        {
            if (iClient == null)
                return;

            try
            {
                lock (lockObject)
                {
                    if (dicClients.ContainsKey(iClient.Sender))
                    {
                        dicClients.Remove(iClient.Sender);
                    }
                }
                if (UserLogout != null)
                {
                    UserLogout(iClient.Sender);
                }

            }
            catch (Exception ex)
            {
                log.Info(iClient.Sender + "DetachError" + ex.Message);
            }
            finally
            {
            }

        }

        public int DeviceOnline(string deviceCode)
        {
            string[] keys = new string[dicClients.Count];
            lock (lockObject)
            {
                dicClients.Keys.CopyTo(keys, 0);
            }
            try
            {
                foreach (string key in keys)
                {
                    if (dicClients.ContainsKey(key))
                    {
                        dicClients[key].DeviceOnline(deviceCode);
                    }
                }
                return 1;

            }
            catch
            {
                //有可能通知失败，不做处理
                return 0;
            }
        }
        public int DeviceOffline(string deviceCode)
        {
            string[] keys = new string[dicClients.Count];
            lock (lockObject)
            {
                dicClients.Keys.CopyTo(keys, 0);
            }
            try
            {
                foreach (string key in keys)
                {
                    if (dicClients.ContainsKey(key))
                    {
                        dicClients[key].DeviceOffline(deviceCode);
                    }
                }
                return 1;
            }
            catch
            {
                //有可能通知失败，不做处理
                return 0;
            }
        }
        public bool SendCommand(byte[] parameter, Common.CommandConst name, int returnLength, string senderUser, string deviceCode, object tag)
        {
            if (dicClients.ContainsKey(senderUser))
            {
                if (SendCommandEvent != null)
                {
                    SendCommandEvent(parameter, name, returnLength, senderUser, deviceCode, tag);
                }
                return true;
            }
            else
                return false;
        }
        public DateTime Ping(IRemoteCommand iClient)
        {
            if (iClient == null)
                return DateTime.MinValue;
            string s = iClient.Sender;
            if (s != null)
                return DateTime.Now;
            else
                return DateTime.MinValue;

        }
        public override object InitializeLifetimeService() { return (null); }

    }
}
