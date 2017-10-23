function LoadDataFromDeviceData(data)
{
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
    if (data.Data28) {

        var valveState = data.Data29[0];

        if (valveState != Qrms.CheckValveState.无校阀) {
            $("#lblCheckValve").text("(" + data.Data30[0] + ")");
        }

        switch (data.WorkStatus) {
            case Qrms.WorkStatus.假日模式:
                enableButton("lblWorkState2");
                disableButton("lblWorkState1");

                break;
            case Qrms.WorkStatus.工作模式:
                enableButton("lblWorkState1");
                disableButton("lblWorkState2");
                break;
            default:
                break;
        }
        var cct = Qrms.GetEnumPropName(Qrms.ValveControlChannel, Qrms.currentdNode.ctrlNumber);
        if (cct) {
            $("#lblControlChannel").text(cct);
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

    $("#nuAStep1").val(data.ValveA.Step1);
    $("#nuAStep2").val(data.ValveA.Step2);
    $("#nuAStep3").val(data.ValveA.Step3);
    $("#nuAStep4").val(data.ValveA.Step4);
    $("#nuAStep5").val(data.ValveA.Step5);

    
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