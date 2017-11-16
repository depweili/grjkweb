function LoadDataFromDeviceData(data)
{
    var cct = Qrms.GetEnumPropName(Qrms.ValveControlChannel, Qrms.currentdNode.ctrlNumber);
    if (cct) {
        $("#lblControlChannel").text(cct);
    }

    //if (rdoModeA.Checked)
    //    lblCurrentValveValue.Text = data.Valve1.ToString();
    //else
    //    lblCurrentValveValue.Text = data.Valve2.ToString();
    //lblTime.Text = GetDateTime(data.DataTime);

    $("#lblOutdoor").text(data.OutdoorTemp || "0.00");


    $("#lblSup1").text(data.SupplyWaterTemp1 || "0.00");
    $("#lblBack1").text(data.BackWaterTemp1 || "0.00");
    $("#lblFix1").text(data.FixWaterTemp1 || "0.00");
    $("#lblValve1").text(data.Valve1);
    $("#lblState1").text(data.WaterNetStatus1Text);
    $("#lblSup2").text(data.SupplyWaterTemp2 || "0.00");

    $("#lblBack2").text(data.BackWaterTemp2 || "0.00");
    $("#lblFix2").text(data.FixWaterTemp2 || "0.00");
    $("#lblValve2").text(data.Valve2 || "0.00");
    $("#lblState2").text(data.WaterNetStatus2Text);
    if (data.Data28.length>0) {

        var arr28 = parseInt(base64ToBytes(data.Data28), 2);
        var arr29 = parseInt(base64ToBytes(data.Data29), 2);
        var arr30 = parseInt(base64ToBytes(data.Data30), 2);

        //var arr29 = base64ToBytes(data.Data29);
        //var arr30 = base64ToBytes(data.Data30);

        var valveState = arr29;

        if (valveState != Qrms.CheckValveState.无校阀) {
            $("#lblCheckValve").text("(" + arr30 + ")");
        }

        switch (data.WorkStatus) {
            case Qrms.WorkStatus.假日模式:
                enableLabel("lblWorkState2");
                disableLabel("lblWorkState1");

                break;
            case Qrms.WorkStatus.工作模式:
                enableLabel("lblWorkState1");
                disableLabel("lblWorkState2");
                break;
            default:
                break;
        }

        var value = arr28;  //低4位A阀，高4位B阀			每个的最高位为1为时间控制，为0为曲线控制				
        //0手动控制，1周六日、工作日自动控制，2周日、工作日控制，3全部工作日控制						
        var B = Utility.Hi4Bit(value);    //0000 1011
        var A = Utility.Low4Bit(value);    //0000 1011

        var bita1 = Utility.GetIntegerSomeBit(A, 3);
        var bitb1 = Utility.GetIntegerSomeBit(B, 3);
        
        if (bita1 == 1)
        {
            //A阀为时间控制
            enableLabel("lblTime1");
            disableLabel("lblCycle1");

            //currentDevice.ValveA.WorkBy = (int)WorkBy.时间;
            //将该位置为0
            var workmode = Utility.SetIntegerSomeBit(3, A, false);
            //currentDevice.ValveA.WorkMode = workmode;
            if (workmode == 0)
            {
                //A阀为手动控制
                enableLabel("lblMaunalWorkMode1");
                disableLabel("lblAutoWorkMode1");

            }
            else
            {
                enableLabel("lblAutoWorkMode1");
                disableLabel("lblMaunalWorkMode1");
            }
        }
        else
        {
            //currentDevice.ValveA.WorkBy = (int)WorkBy.曲线;
            //曲线控制
            enableLabel("lblCycle1");
            disableLabel("lblTime1");
            //currentDevice.ValveA.WorkMode = A;
            if (A == 0)
            {
                //A阀为手动控制
                enableLabel("lblMaunalWorkMode1");
                disableLabel("lblAutoWorkMode1");
            }
            else
            {
                enableLabel("lblAutoWorkMode1");
                disableLabel("lblMaunalWorkMode1");
            }
        }
        if (bitb1 == 1)
        {
            //currentDevice.ValveB.WorkBy = (int)WorkBy.时间;
            //B阀为时间控制
            enableLabel("lblTime2");
            disableLabel("lblCycle2");
            //将该位置为0

            var workmode = Utility.SetIntegerSomeBit(3, B, false);
            //currentDevice.ValveB.WorkMode = workmode;
            if (workmode == 0)
            {
                //A阀为手动控制
                enableLabel("lblManualWorkMode2");
                disableLabel("lblAutoWorkMode2");
            }
            else
            {
                enableLabel("lblAutoWorkMode2");
                disableLabel("lblManualWorkMode2");
            }
        }
        else
        {
            //currentDevice.ValveB.WorkBy = (int)WorkBy.曲线;
            //曲线控制
            enableLabel("lblCycle2");
            disableLabel("lblTime2");
            //currentDevice.ValveB.WorkMode = B;
            if (B == 0)
            {
                //A阀为手动控制
                enableLabel("lblManualWorkMode2");
                disableLabel("lblAutoWorkMode2");
            }
            else
            {
                enableLabel("lblAutoWorkMode2");
                disableLabel("lblManualWorkMode2");
            }
        }
    }

    /*
    currentDevice.ValveA.CurrentValue = int.Parse(lblValve1.Text);
    currentDevice.ValveB.CurrentValue = int.Parse(lblValve2.Text);

    byte value = data.Data28[0];  //低4位A阀，高4位B阀			每个的最高位为1为时间控制，为0为曲线控制				
    //0手动控制，1周六日、工作日自动控制，2周日、工作日控制，3全部工作日控制						
    byte B = Utility.Hi4Bit(value);    //0000 1011
    byte A = Utility.Low4Bit(value);    //0000 1011

    int bita1 = Utility.GetIntegerSomeBit(A, 3);
    int bitb1 = Utility.GetIntegerSomeBit(B, 3);
    if (bita1 == 1)
    {
        //A阀为时间控制
        lblTime1.Enabled = true;
        lblCycle1.Enabled = false;
        currentDevice.ValveA.WorkBy = (int)WorkBy.时间;
        //将该位置为0
        int workmode = Utility.SetIntegerSomeBit(3, A, false);
        currentDevice.ValveA.WorkMode = workmode;
        if (workmode == 0)
        {
            //A阀为手动控制
            lblAutoWorkMode1.Enabled = false;
            lblMaunalWorkMode1.Enabled = true;

        }
        else
        {
            lblAutoWorkMode1.Enabled = true;
            lblMaunalWorkMode1.Enabled = false;
        }
    }
    else
    {
        currentDevice.ValveA.WorkBy = (int)WorkBy.曲线;
        //曲线控制
        lblTime1.Enabled = false;
        lblCycle1.Enabled = true;
        currentDevice.ValveA.WorkMode = A;
        if (A == 0)
        {
            //A阀为手动控制
            lblAutoWorkMode1.Enabled = false;
            lblMaunalWorkMode1.Enabled = true;
        }
        else
        {
            lblAutoWorkMode1.Enabled = true;
            lblMaunalWorkMode1.Enabled = false;
        }
    }
    if (bitb1 == 1)
    {
        currentDevice.ValveB.WorkBy = (int)WorkBy.时间;
        //B阀为时间控制
        lblTime2.Enabled = true;
        lblCycle2.Enabled = false;
        //将该位置为0

        int workmode = Utility.SetIntegerSomeBit(3, B, false);
        currentDevice.ValveB.WorkMode = workmode;
        if (workmode == 0)
        {
            //A阀为手动控制
            lblAutoWorkMode2.Enabled = false;
            lblManualWorkMode2.Enabled = true;
        }
        else
        {
            lblAutoWorkMode2.Enabled = true;
            lblManualWorkMode2.Enabled = false;
        }
    }
    else
    {
        currentDevice.ValveB.WorkBy = (int)WorkBy.曲线;
        //曲线控制
        lblTime2.Enabled = false;
        lblCycle2.Enabled = true;
        currentDevice.ValveB.WorkMode = B;
        if (B == 0)
        {
            //A阀为手动控制
            lblAutoWorkMode2.Enabled = false;
            lblManualWorkMode2.Enabled = true;
        }
        else
        {
            lblAutoWorkMode2.Enabled = true;
            lblManualWorkMode2.Enabled = false;
        }
    }
    */
}


function DoAfterUpdateData()
{
    if (Qrms.currentParaData == null) return;

    var data = Qrms.currentParaData;

    rdoValveCheck();

    UpdateModeSetting(data);
    UpdateValveSetting(data);
    UpdateTemperatureSetting(data);
    
}

function rdoValveCheck()
{
    if (Qrms.currentParaData == null) return;

    var data = Qrms.currentParaData;
    
    var rdovalve = $("input[name='valve']:checked").val();
    if (rdovalve == "A") {
        $("input[name='workby'][value='" + data.ValveA.WorkBy + "']").prop("checked", "checked");
        $("input[name='workmodel'][value='" + data.ValveA.WorkMode + "']").prop("checked", "checked");
        $("#lblCurrentValveValue").text(data.ValveA.CurrentValue);
        $("#txtValveOpenValue").val(data.ValveA.SetValue);
    }
    else {
        $("input[name='workby'][value='" + data.ValveB.WorkBy + "']").prop("checked", "checked");
        $("input[name='workmodel'][value='" + data.ValveB.WorkMode + "']").prop("checked", "checked");
        $("#lblCurrentValveValue").text(data.ValveB.CurrentValue);
        $("#txtValveOpenValue").val(data.ValveB.SetValue);
    }
}

function UpdateModeSetting(data)
{

}

function UpdateValveSetting(data)
{
    //A
    $("#nuAStep1").val(data.ValveA.Step1);
    $("#nuAStep2").val(data.ValveA.Step2);
    $("#nuAStep3").val(data.ValveA.Step3);
    $("#nuAStep4").val(data.ValveA.Step4);
    $("#nuAStep5").val(data.ValveA.Step5);
    $("#nuAMaxStep").val(data.ValveA.MaxStep);
    $("#nuAInterval").val(data.ValveA.CtrlInterval);
    $("#nuAMaxValue").val(data.ValveA.MaxValue);
    $("#nuAMinValue").val(data.ValveA.MinValue);
    $("#cmbACtrlNumber").val(data.Device.CtrlNumber);

    $("input[name='rdoAsteering'][value='" + data.ValveA.Steering + "']").prop("checked", "checked");

    //if (data.ValveA.Steering == 0) {
    //    $("input[name='rdoAsteering'][value='zheng']").prop("checked", "checked");
    //}
    //else {
    //    $("input[name='rdoAsteering'][value='fan']").prop("checked", "checked");
    //}

    //B
    $("#nuBStep1").val(data.ValveB.Step1);
    $("#nuBStep2").val(data.ValveB.Step2);
    $("#nuBStep3").val(data.ValveB.Step3);
    $("#nuBStep4").val(data.ValveB.Step4);
    $("#nuBStep5").val(data.ValveB.Step5);
    $("#nuBMaxStep").val(data.ValveB.MaxStep);
    $("#nuBInterval").val(data.ValveB.CtrlInterval);
    $("#nuBMaxValue").val(data.ValveB.MaxValue);
    $("#nuBMinValue").val(data.ValveB.MinValue);
    $("#cmbBCtrlNumber").val(data.Device.CtrlNumber);

    $("input[name='rdoBsteering'][value='" + data.ValveB.Steering + "']").prop("checked", "checked");

    //if (data.ValveB.Steering == 0) {
    //    $("input[name='rdoBsteering'][value='zheng']").prop("checked", "checked");
    //}
    //else {
    //    $("input[name='rdoBsteering'][value='fan']").prop("checked", "checked");
    //}
}

function UpdateTemperatureSetting(data)
{
    $("#nuOutdoor").val(data.Device.OutdoorFix);
    $("#nuSup1").val(data.Device.SupplyWaterFix1);
    $("#nuSup2").val(data.Device.SupplyWaterFix2);
    $("#nuBack1").val(data.Device.BackFix1);
    $("#nuBack2").val(data.Device.BackFix2);
    $("#nuFix1").val(data.Device.FixWater1);
    $("#nuFix2").val(data.Device.FixWater2);
    $("#nuSaveInterval").val(data.Device.SaveInterval);
}

function UpdateTerminalSetting(data)
{

}


function BuildCurrentParaData()
{
    if (Qrms.currentParaData == null) return;

    var data = Qrms.currentParaData;

    BuildDataModeSetting(data);
    BuildDataValveSetting(data);
    BuildDataTemperatureSetting(data);
    BuildDataTerminalSetting(data);
}


function BuildDataModeSetting(data)
{
    var rdovalve = $("input[name='valve']:checked").val();

    var rdoworkmodel = $("input[name='workmodel']:checked").val();
    var rdoworkby = $("input[name='workby']:checked").val();
    var txtValveOpenValue = $("#txtValveOpenValue").val();

    if (rdovalve == "A") {
        data.ValveA.WorkBy = rdoworkby;
        data.ValveA.WorkMode = rdoworkmodel;
        data.ValveA.SetValue = txtValveOpenValue;
    }
    else {
        data.ValveB.WorkBy = rdoworkby;
        data.ValveB.WorkMode = rdoworkmodel;
        data.ValveB.SetValue = txtValveOpenValue;
    }

}

function BuildDataValveSetting(data)
{
    //A
    data.ValveA.Step1=$("#nuAStep1").val();
    data.ValveA.Step2=$("#nuAStep2").val();
    data.ValveA.Step3=$("#nuAStep3").val();
    data.ValveA.Step4=$("#nuAStep4").val();
    data.ValveA.Step5=$("#nuAStep5").val();
    data.ValveA.MaxStep=$("#nuAMaxStep").val();
    data.ValveA.CtrlInterval=$("#nuAInterval").val();
    data.ValveA.MaxValue=$("#nuAMaxValue").val();
    data.ValveA.MinValue=$("#nuAMinValue").val();
    data.Device.CtrlNumber = $("#cmbACtrlNumber").val();

    data.ValveA.Steering = $("input[name='rdoAsteering']:checked").val();


    //B
    data.ValveB.Step1=$("#nuBStep1").val();
    data.ValveB.Step2=$("#nuBStep2").val();
    data.ValveB.Step3=$("#nuBStep3").val();
    data.ValveB.Step4=$("#nuBStep4").val();
    data.ValveB.Step5=$("#nuBStep5").val();
    data.ValveB.MaxStep=$("#nuBMaxStep").val();
    data.ValveB.CtrlInterval=$("#nuBInterval").val();
    data.ValveB.MaxValue=$("#nuBMaxValue").val();
    data.ValveB.MinValue=$("#nuBMinValue").val();
    data.Device.CtrlNumber=$("#cmbBCtrlNumber").val();

    data.ValveB.Steering = $("input[name='rdoBsteering']:checked").val();
}

function BuildDataTemperatureSetting(data)
{
    data.Device.OutdoorFix=$("#nuOutdoor").val();
    data.Device.SupplyWaterFix1=$("#nuSup1").val();
    data.Device.SupplyWaterFix2=$("#nuSup2").val();
    data.Device.BackFix1=$("#nuBack1").val();
    data.Device.BackFix2=$("#nuBack2").val();
    data.Device.FixWater1=$("#nuFix1").val();
    data.Device.FixWater2=$("#nuFix2").val();
    data.Device.SaveInterval=$("#nuSaveInterval").val();
}

function BuildDataTerminalSetting(data)
{

}