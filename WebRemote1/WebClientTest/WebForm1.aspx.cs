using Common;
using RemoteHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClientTest
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSyncTime_Click(object sender, EventArgs e)
        {
            //发送命令并且获得返回结果
            //RemoteResult res=null;
            //RemoteClient.Instance.SendCommandAndGetResult(...out res)
            

            RemoteResult res=null;
            string msg = RemoteClient.Instance.SendCommandAndGetResult(new byte[] { (byte)ARMControl.修改时间,(byte)(DateTime.Now.Year-2000),
            (byte)DateTime.Now.Month,
            (byte)DateTime.Now.Day,
            (byte)DateTime.Now.DayOfWeek,
            (byte)DateTime.Now.Hour,
            (byte)DateTime.Now.Minute,
            (byte)DateTime.Now.Second
            }, CommandConst.ARM控制指令, 12, "88888888", out res);

            if (!string.IsNullOrEmpty(msg))
            {
                this.Message.Text = msg;
            }

            if (res!=null)
            {
                this.Message.Text = "返回：" + res.tag.ToString();


                //这里直接就是收到返回过后的业务处理
                client_ReceiveCommandResult(res.senderUser,res.name,res.deviceCode,res.Datas,res.tag);

            }
        }

        private void client_ReceiveCommandResult(string senderUser, CommandConst name, string deviceCode, byte[] Datas, object tag)
        {

            var resstr = this.Message.Text;

            try
            {

                CommandConst command = name;
                switch (command)
                {
                    case CommandConst.读取测量值:
                        break;
                    case CommandConst.ARM控制指令:
                        switch ((ARMControl)Datas[0])
                        {
                            case ARMControl.手动控阀:
                                this.Message.Text = resstr + "命令成功执行！";
                                break;
                            case ARMControl.修改时间:
                                this.Message.Text = resstr + "时间同步成功！";
                                break;
                            case ARMControl.校阀:
                                break;
                            default:
                                break;
                        }

                        break;
                   default:
                        break;
                }

            }
            catch (Exception)
            {
                
                throw;
            }
            /*
            try
            {
                CommandConst command = name;
                switch (command)
                {
                    case CommandConst.读取测量值:
                        #region 读取测量值
                        {
                            IEHNCS.Model.Devices device = DeviceBO.GetModel(deviceCode, 0);
                            IEHNCS.Model.Datas data = new Model.Datas();
                            data.CreateTime = DateTime.Now;
                            data.DeviceID = device.ID;
                            int year = int.Parse("20" + Datas[0]);
                            int month = Datas[1];
                            int day = Datas[2];
                            int week = Datas[3];
                            int hour = Datas[4];
                            int minutes = Datas[5];
                            int second = Datas[6];
                            DateTime dataTime = new DateTime(year, month, day, hour, minutes, second);
                            data.DataTime = dataTime;
                            data.OutdoorTemp = Utility.GetDecimalValue(Datas, 8, 7);
                            data.BackWaterTemp1 = Utility.GetDecimalValue(Datas, 14, 13);
                            data.BackWaterTemp2 = Utility.GetDecimalValue(Datas, 16, 15);
                            data.FixWaterTemp1 = Utility.GetDecimalValue(Datas, 18, 17);
                            data.FixWaterTemp2 = Utility.GetDecimalValue(Datas, 20, 19);
                            data.SupplyWaterTemp1 = Utility.GetDecimalValue(Datas, 10, 9);
                            data.SupplyWaterTemp2 = Utility.GetDecimalValue(Datas, 12, 11);
                            data.Valve1 = Datas[21];
                            data.Valve2 = Datas[22];
                            data.WirelessStatus = Datas[23];
                            data.WorkStatus = Datas[24];
                            data.WaterNetStatus1 = Datas[25];
                            data.WaterNetStatus2 = Datas[26];
                            data.Data27 = new byte[] { Datas[27] };
                            data.Data28 = new byte[] { Datas[28] };
                            data.Data29 = new byte[] { Datas[29] };
                            data.Data30 = new byte[] { Datas[30] };
                            dataBO.Add(data);
                            LoadDataFromDeviceData(data);
                        }
                        btnSyncTime.Enabled = true;
                        treeView1.Enabled = true;
                        btnUpdateData.Enabled = true;
                        IsProcessing = false;
                        #endregion
                        break;
                    case CommandConst.读取参数:
                        {
                            if ((byte)Datas[0] >= (byte)SettingType.曲线起始参数 && (byte)Datas[0] <= (byte)SettingType.曲线终止参数)
                            {
                                decimal? tmp = 0;
                                DeviceCurveLibrary curveModel = null;
                                PropertyInfo prop = null;
                                Type type = typeof(IEHNCS.Model.DeviceCurveLibrary);
                                int code = ((int)Datas[0] - 1);
                                devCurveBO.DeleteOfDevice(selectedDeviceID, code);
                                curveModel = new Model.DeviceCurveLibrary()
                                {
                                    Code = code,
                                    Name = "曲线" + code.ToString(),
                                    DeviceID = selectedDeviceID,
                                };
                                for (int i = 1; i <= 121; i++)
                                {
                                    tmp = Utility.GetDecimalValue(Datas, (i * 2), (i * 2 - 1));
                                    prop = type.GetProperty("Column" + i.ToString());
                                    prop.SetValue(curveModel, tmp, null);
                                }
                                devCurveBO.Add(curveModel);
                                if (code == 30)
                                {
                                    IsProcessing = false;
                                    treeView1.Enabled = true;
                                    ShowMessage("更新成功！");
                                    BindDevCurves();

                                }
                                break;
                            }

                            //工作模式及曲线和时间控制
                            switch ((SettingType)((int)Datas[0]))
                            {
                                case SettingType.整体参数:
                                    #region 整体参数
                                    currentDevice.GotParameters = true;
                                    //阀门控制路数
                                    ValveControlChannel vc = (ValveControlChannel)Datas[41];
                                    lblControlChannel.Text = vc.ToString();
                                    if (vc == ValveControlChannel.A和B通路)
                                    {
                                        grpA.Enabled = true;
                                        grpB.Enabled = true;
                                    }
                                    else if (vc == ValveControlChannel.A通路)
                                    {
                                        grpA.Enabled = true;
                                        grpB.Enabled = false;
                                    }
                                    else
                                    {
                                        grpA.Enabled = false;
                                        grpB.Enabled = true;
                                    }
                                    currentDevice.ValveA.MinValue = Datas[2];
                                    currentDevice.ValveA.MaxValue = Datas[3];
                                    currentDevice.ValveA.CtrlInterval = Utility.GetIntValue(Datas, 7, 6);
                                    currentDevice.ValveA.Step1 = Datas[10];
                                    currentDevice.ValveA.Step2 = Datas[11];
                                    currentDevice.ValveA.Step3 = Datas[12];
                                    currentDevice.ValveA.Step4 = Datas[13];
                                    currentDevice.ValveA.Step5 = Datas[14];
                                    currentDevice.ValveA.MaxStep = Datas[20];
                                    currentDevice.ValveA.Steering = Datas[22];


                                    currentDevice.ValveB.MinValue = Datas[4];
                                    currentDevice.ValveB.MaxValue = Datas[5];
                                    currentDevice.ValveB.CtrlInterval = Utility.GetIntValue(Datas, 9, 8);
                                    currentDevice.ValveB.Step1 = Datas[15];
                                    currentDevice.ValveB.Step2 = Datas[16];
                                    currentDevice.ValveB.Step3 = Datas[17];
                                    currentDevice.ValveB.Step4 = Datas[18];
                                    currentDevice.ValveB.Step5 = Datas[19];
                                    currentDevice.ValveB.MaxStep = Datas[21];
                                    currentDevice.ValveB.Steering = Datas[23];

                                    valveBO.Update(currentDevice.ValveA);
                                    valveBO.Update(currentDevice.ValveB);

                                    currentDevice.Device.BackFix1 = Utility.GetDecimalValue(Datas, 32, 31);
                                    currentDevice.Device.BackFix2 = Utility.GetDecimalValue(Datas, 34, 33);
                                    currentDevice.Device.CtrlNumber = Datas[41];
                                    currentDevice.Device.FixWater1 = Utility.GetDecimalValue(Datas, 36, 35);
                                    currentDevice.Device.FixWater2 = Utility.GetDecimalValue(Datas, 38, 37);
                                    currentDevice.Device.OutdoorFix = Utility.GetDecimalValue(Datas, 26, 25);
                                    currentDevice.Device.SaveInterval = Utility.GetIntValue(Datas, 40, 39);
                                    currentDevice.Device.SupplyWaterFix1 = Utility.GetDecimalValue(Datas, 28, 27);
                                    currentDevice.Device.SupplyWaterFix2 = Utility.GetDecimalValue(Datas, 30, 29);
                                    DeviceBO.Update(currentDevice.Device);
                                    treeView1.Enabled = true;
                                    IsProcessing = false;
                                    if (rdoModeA.Checked)
                                        rdoModeA_CheckedChanged(null, null);
                                    else
                                        rdoModeA.Checked = true;

                                    #region 阀门相关
                                    rdoAZhengZhuan.Checked = (currentDevice.ValveA.Steering.Value == 0);
                                    rdoAFanZhuan.Checked = !rdoAZhengZhuan.Checked;
                                    nuAStep1.Value = currentDevice.ValveA.Step1.Value;
                                    nuAStep2.Value = currentDevice.ValveA.Step2.Value;
                                    nuAStep3.Value = currentDevice.ValveA.Step3.Value;
                                    nuAStep4.Value = currentDevice.ValveA.Step4.Value;
                                    nuAStep5.Value = currentDevice.ValveA.Step5.Value;
                                    nuAMaxStep.Value = currentDevice.ValveA.MaxStep.Value;
                                    nuAInterval.Value = currentDevice.ValveA.CtrlInterval.Value;
                                    btnAMaxRolate.DelataAngle = currentDevice.ValveA.MaxValue.Value / 100 * 360;
                                    btnAMinRolate.DelataAngle = currentDevice.ValveA.MinValue.Value / 100 * 360;
                                    nuAMaxValue.Value = currentDevice.ValveA.MaxValue.Value;
                                    nuAMinValue.Value = currentDevice.ValveA.MinValue.Value;

                                    cmbACtrlNumber.SelectedIndex = currentDevice.Device.CtrlNumber.Value - 1;

                                    rdoBZhengZhuan.Checked = (currentDevice.ValveB.Steering.Value == 0);
                                    rdoBFanZhuan.Checked = !rdoBZhengZhuan.Checked;
                                    nuBStep1.Value = currentDevice.ValveB.Step1.Value;
                                    nuBStep2.Value = currentDevice.ValveB.Step2.Value;
                                    nuBStep3.Value = currentDevice.ValveB.Step3.Value;
                                    nuBStep4.Value = currentDevice.ValveB.Step4.Value;
                                    nuBStep5.Value = currentDevice.ValveB.Step5.Value;
                                    nuBMaxStep.Value = currentDevice.ValveB.MaxStep.Value;
                                    nuBInterval.Value = currentDevice.ValveB.CtrlInterval.Value;
                                    nuBMaxValue.Value = currentDevice.ValveB.MaxValue.Value;
                                    nuBMinValue.Value = currentDevice.ValveB.MinValue.Value;
                                    btnBMaxRolate.DelataAngle = currentDevice.ValveB.MaxValue.Value / 100 * 360;
                                    btnBMinRolate.DelataAngle = currentDevice.ValveB.MinValue.Value / 100 * 360;
                                    cmbBCtrlNumber.SelectedIndex = currentDevice.Device.CtrlNumber.Value - 1;
                                    #endregion

                                    #region 温度修正
                                    nuOutdoor.Value = currentDevice.Device.OutdoorFix.Value;
                                    nuSup1.Value = currentDevice.Device.SupplyWaterFix1.Value;
                                    nuSup2.Value = currentDevice.Device.SupplyWaterFix2.Value;
                                    nuBack1.Value = currentDevice.Device.BackFix1.Value;
                                    nuBack2.Value = currentDevice.Device.BackFix2.Value;
                                    nuFix1.Value = currentDevice.Device.FixWater1.Value;
                                    nuFix2.Value = currentDevice.Device.FixWater2.Value;
                                    nuSaveInterval.Value = currentDevice.Device.SaveInterval.Value;

                                    #endregion
                                    #endregion
                                    IsProcessing = false;
                                    break;
                                case SettingType.工作模式曲线设置:
                                case SettingType.周六模式曲线设置:
                                case SettingType.周日模式曲线设置:
                                case SettingType.假日模式曲线设置:
                                    #region 曲线设置
                                    {
                                        string startTime = "";
                                        int curveCode = -1;
                                        int flag = 0;
                                        string endTime = "";
                                        DataTable dt = new DataTable();
                                        dt.Columns.Add("ID", typeof(int));
                                        dt.Columns.Add("TimeSpanID", typeof(int));
                                        dt.Columns.Add("StartTime", typeof(string));
                                        dt.Columns.Add("EndTime", typeof(string));
                                        dt.Columns.Add("CurveCode", typeof(int));
                                        dt.Columns.Add("DeviceID", typeof(int));
                                        dt.Columns.Add("Flag", typeof(int));
                                        int timeSpanID = GetTimeSpanID();
                                        DataRow dr = null;
                                        //先读取第一组
                                        dr = dt.NewRow();
                                        dr["TimeSpanID"] = timeSpanID;
                                        dr["DeviceID"] = selectedDeviceID;
                                        curveCode = Utility.SetIntegerSomeBit(6, Datas[3], false);
                                        flag = Utility.GetIntegerSomeBit(Datas[3], 6);
                                        startTime = string.Format("{0:00}:{1:00}", Datas[1], Datas[2]);
                                        endTime = string.Format("{0:00}:{1:00}", Datas[4], Datas[5]);

                                        dr["StartTime"] = startTime;
                                        dr["EndTime"] = endTime;
                                        dr["CurveCode"] = curveCode;
                                        dr["Flag"] = flag;
                                        dt.Rows.Add(dr);
                                        string previousEndTime = endTime;
                                        //读余下几组
                                        for (int i = 6; i < 75; i = i + 3)
                                        {
                                            if (Datas[i] == 255)
                                                break;
                                            dr = dt.NewRow();
                                            dr["TimeSpanID"] = timeSpanID;
                                            dr["DeviceID"] = selectedDeviceID;
                                            curveCode = Utility.SetIntegerSomeBit(6, Datas[i], false);
                                            flag = Utility.GetIntegerSomeBit(Datas[i], 6);
                                            startTime = previousEndTime;
                                            if (Datas[i + 1] > 23 || Datas[i + 2] > 59)
                                            {
                                                endTime = null;
                                            }
                                            else
                                            {
                                                endTime = string.Format("{0:00}:{1:00}", Datas[i + 1], Datas[i + 2]);
                                            }

                                            dr["StartTime"] = startTime;
                                            dr["EndTime"] = endTime;
                                            dr["CurveCode"] = curveCode;
                                            dr["Flag"] = flag;
                                            previousEndTime = endTime;
                                            dt.Rows.Add(dr);
                                        }

                                        if (dt.Rows.Count > 0)
                                        {
                                            dgvData.DataSource = dt;

                                            timeSpanBO.DeleteByDeviceIDAndTimeSpanID(selectedDeviceID, timeSpanID);
                                            TimeSpanSettings model = null;
                                            foreach (DataRow dr2 in dt.Rows)
                                            {
                                                model = new TimeSpanSettings();
                                                model.TimeSpanID = timeSpanID;
                                                model.DeviceID = selectedDeviceID;
                                                model.CurveCode = (int)dr2["CurveCode"];
                                                model.EndTime = dr2["EndTime"].ToString();
                                                model.StartTime = dr2["StartTime"].ToString();
                                                model.Flag = (int)dr2["Flag"];
                                                timeSpanBO.Add(model);
                                            }
                                        }
                                        IsProcessing = false;
                                    }
                                    #endregion
                                    break;
                                default:
                                    break;
                            }



                        }

                        break;
                    case CommandConst.默认参数设定:
                        break;
                    case CommandConst.参数设定:
                        {

                            if ((byte)Datas[0] >= (byte)SettingType.曲线起始参数 && (byte)Datas[0] <= (byte)SettingType.曲线终止参数)
                            {
                                if (((int)Datas[0] - 1) == 30)
                                {
                                    IsProcessing = false;
                                    ShowMessage("保存成功！");
                                }
                                break;
                            }
                            switch ((SettingType)Datas[0])
                            {
                                case SettingType.整体参数:
                                case SettingType.工作模式曲线设置:
                                case SettingType.周六模式曲线设置:
                                case SettingType.周日模式曲线设置:
                                case SettingType.假日模式曲线设置:
                                    btnSaveSpan.Enabled = true;
                                    ShowMessage("保存成功！");
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case CommandConst.ARM控制指令:
                        switch ((ARMControl)Datas[0])
                        {
                            case ARMControl.手动控阀:
                                ShowMessage("命令成功执行！");
                                btnRun.Enabled = true;
                                break;
                            case ARMControl.修改时间:
                                ShowMessage("时间同步成功！");
                                btnRun.Enabled = true;
                                break;
                            case ARMControl.校阀:
                                break;
                            default:
                                break;
                        }

                        break;
                    case CommandConst.读取历史数据:
                        switch (((HistoryType)Datas[0]))
                        {
                            case HistoryType.读取正常记录:
                                byte[] array = new byte[46];
                                int id = 0;
                                History his = null;
                                if (Datas[1] != 255)
                                {
                                    Array.Copy(Datas, 1, array, 0, 46);
                                    id = historyBO.Exists(selectedDeviceID, (int)HistoryType.读取正常记录, (int)nuRowNumber.Value);

                                    if (id > 0)
                                    {
                                        his = historyBO.GetModel(id);
                                        his.Data = array;
                                    }
                                    else
                                        his = new History()
                                        {
                                            CreateTime = DateTime.Now,
                                            Data = array,
                                            DeviceID = selectedDeviceID,
                                            RowNumber = (int)nuRowNumber.Value,
                                            HistoryType = (int)HistoryType.读取正常记录

                                        };
                                    if (id > 0)
                                        historyBO.Update(his);
                                    else
                                        historyBO.Add(his);
                                    dgvHistory.DataSource = historyBO.GetList("DeviceID=" + selectedDeviceID + " And HistoryType=" + his.HistoryType).Tables[0];
                                    dgvHistory.Rows[0].Selected = true;
                                    ShowMessage("更新成功！");
                                }
                                else
                                {
                                    ShowMessage("没有该条记录！");
                                }
                                break;
                            case HistoryType.读取参数修改记录:
                                if (Datas[1] != 255)
                                {
                                    array = new byte[60];
                                    Array.Copy(Datas, 1, array, 0, 60);
                                    id = historyBO.Exists(selectedDeviceID, (int)HistoryType.读取参数修改记录, (int)nuRowNumber.Value);
                                    his = null;
                                    if (id > 0)
                                    {
                                        his = historyBO.GetModel(id);
                                        his.Data = array;
                                    }
                                    else
                                        his = new History()
                                        {
                                            CreateTime = DateTime.Now,
                                            Data = array,
                                            DeviceID = selectedDeviceID,
                                            RowNumber = (int)nuRowNumber.Value,
                                            HistoryType = (int)HistoryType.读取参数修改记录

                                        };
                                    if (id > 0)
                                        historyBO.Update(his);
                                    else
                                        historyBO.Add(his);
                                    dgvHistory.DataSource = historyBO.GetList("DeviceID=" + selectedDeviceID + " And HistoryType=" + his.HistoryType).Tables[0];
                                    dgvHistory.Rows[0].Selected = true;
                                    ShowMessage("更新成功！");
                                }
                                else
                                {
                                    ShowMessage("没有该条记录！");
                                }
                                break;
                            case HistoryType.模式曲线修改记录:
                                if (Datas[1] != 255)
                                {
                                    array = new byte[96];
                                    Array.Copy(Datas, 1, array, 0, 96);
                                    id = historyBO.Exists(selectedDeviceID, (int)HistoryType.模式曲线修改记录, (int)nuRowNumber.Value);
                                    his = null;
                                    if (id > 0)
                                    {
                                        his = historyBO.GetModel(id);
                                        his.Data = array;
                                    }
                                    else
                                        his = new History()
                                        {
                                            CreateTime = DateTime.Now,
                                            Data = array,
                                            DeviceID = selectedDeviceID,
                                            RowNumber = (int)nuRowNumber.Value,
                                            HistoryType = (int)HistoryType.模式曲线修改记录

                                        };
                                    if (id > 0)
                                        historyBO.Update(his);
                                    else
                                        historyBO.Add(his);
                                    dgvHistory.DataSource = historyBO.GetList("DeviceID=" + selectedDeviceID + " And HistoryType=" + his.HistoryType).Tables[0];
                                    dgvHistory.Rows[0].Selected = true;
                                    ShowMessage("更新成功！");
                                }
                                else
                                {
                                    ShowMessage("没有该条记录！");
                                }
                                break;
                            case HistoryType.温度曲线修改记录:
                                if (Datas[1] != 255)
                                {
                                    array = new byte[250];
                                    Array.Copy(Datas, 1, array, 0, 250);
                                    id = historyBO.Exists(selectedDeviceID, (int)HistoryType.温度曲线修改记录, (int)nuRowNumber.Value);
                                    his = null;
                                    if (id > 0)
                                    {
                                        his = historyBO.GetModel(id);
                                        his.Data = array;
                                    }
                                    else
                                        his = new History()
                                        {
                                            CreateTime = DateTime.Now,
                                            Data = array,
                                            DeviceID = selectedDeviceID,
                                            RowNumber = (int)nuRowNumber.Value,
                                            HistoryType = (int)HistoryType.温度曲线修改记录

                                        };
                                    if (id > 0)
                                        historyBO.Update(his);
                                    else
                                        historyBO.Add(his);
                                    dgvHistory.DataSource = historyBO.GetList("DeviceID=" + selectedDeviceID + " And HistoryType=" + his.HistoryType).Tables[0];
                                    dgvHistory.Rows[0].Selected = true;
                                    ShowMessage("更新成功！");
                                }
                                else
                                {
                                    ShowMessage("没有该条记录！");
                                }
                                break;
                            default:
                                break;
                        }
                        IsProcessing = false;
                        break;
                    case CommandConst.FLASH测试:
                        break;
                    case CommandConst.读取终端ID:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ShowLog(ex.Message);
            }
            */
        }
    }
}