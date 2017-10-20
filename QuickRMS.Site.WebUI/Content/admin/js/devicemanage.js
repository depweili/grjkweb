function initGlobe(win) {
    var day;
    win.tick = function () {
        if (!day) {
            day = new Date(ServerTime);
        }
        day.setSeconds(day.getSeconds() + 1);
        //console.log(day);
        document.getElementById("currentTime").innerHTML = showLocale(day);
        window.setTimeout("tick()", 1000);
    }
    win.tick();

    $('#topPeriod,#bottomPeriod').timepicker({
        defaultTime: "04:00",
        maxHours: 24,
        showMeridian: false,
        minuteStep: 1
    });
}

//系统状态
function initSystemState() {

     showWeather("海淀");

    QR.cPageFunction = 1;

    Qrms.updatePageData = function (node) {
        var city = node.areaName;
        showWeather(city);

        if (node.isOnline) {
            enableButton("btnSyncTime");
            enableButton("btnUpdateData");
        } else {
            disableButton("btnUpdateData");
            disableButton("btnSyncTime");
        }

        if (node.deviceCode && node.isOnline) {
            $.isLoading({ text: "正在处理..." });
            var id = node.id;
            var dataUrl = "/BaseDataApi/GetDeviceDatas?rand=" + Math.random();

            AjaxGet(dataUrl, { id: id }, function (data) {
                $.isLoading("hide");
                console.log(data);
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
                    var cct = Qrms.GetEnumPropName(Qrms.ValveControlChannel, node.ctrlNumber);
                    if (cct) {
                        $("#lblControlChannel").text(cct);
                    }


                }



            }, function () {
                $.isLoading("hide");
            });
        }
    }
}


//时间段设置
function initTimePeriodSetting() {
    Qrms.updatePageData = function (node) {

        if (node.isOnline) {
            enableButton("btnAddSpan");
            enableButton("btnUpdateData");
            enableButton("btnEditSpan");
            enableButton("btnDeleteSpan");
            enableButton("btnSaveSpan");
        } else {

            disableButton("btnAddSpan");
            disableButton("btnUpdateData");
            disableButton("btnEditSpan");
            disableButton("btnDeleteSpan");
            disableButton("btnSaveSpan");
        }

        if (node.deviceCode && node.isOnline) {
            $.isLoading({ text: "正在处理..." });
            var id = node.id;
            var dataUrl = "/BaseDataApi/GetDeviceCurLibraryList?rand=" + Math.random();



            AjaxGet(dataUrl, { id: id }, function (res) {
                $.isLoading("hide");

                var data = res || [];
                console.log(data);
                $("#DeviceCurLibraryList").empty();
                $("#DeviceCurLibraryList").append("<option value='0'>-请选择-</option>");
                for (var i = 0; i < data.length; i++) {
                    var ele = data[i];
                    var opt = $("<option value='" + ele.Code + "'>" + ele.Name + "</option>");
                    opt.data("dclData", ele);
                    $("#DeviceCurLibraryList").append(opt);
                }


            }, function () {
                $.isLoading("hide");
            });

            getTimeSpanList(id, 1);//默认曲线模式：1


        }
    }


    var option = {
        title: {
            text: '曲线图'
        },
        tooltip: {
            trigger: 'axis'
        },
        legend: {

        },
        toolbox: {
            show: false,
            feature: {
                dataZoom: {
                    yAxisIndex: 'none'
                },
                dataView: { readOnly: false },
                magicType: { type: ['line', 'bar'] },
                restore: {},
                saveAsImage: {}
            }
        },
        xAxis: {
            type: 'category',
            boundaryGap: false,
            data: [0, 0.2, 0.4, 0.6, 0.8, 1.0, 1.2]
        },
        yAxis: {
            type: 'value',
            data: [0, 0.2, 0.4, 0.5, 0.6, 0.8, 1.0, 1.2],
            axisLabel: {
                formatter: '{value}'
            }
        },
        series: [
            {
                name: '最高气温',
                type: 'line'


            }
        ]
    };

    // 基于准备好的dom，初始化echarts实例
    var myChart = echarts.init(document.getElementById('qxchart'));
    myChart.setOption(option);

    $("#DeviceCurLibraryList").on("change", function () {
        var $opt = $(this).find("option:selected");
        var data = $opt.data("dclData") || {};
        var start = -40;
        var dataX = [], dataY = [];
        for (var i = 1; i <= 121; i++) {
            dataX.push(start);
            dataY.push(data["Column" + i]);
            start += 0.5;
        }
        myChart.setOption({
            xAxis: {
                data: dataX
            },

            series: [
                {
                    data: dataY
                }
            ]
        });
    });
}

//模式设置
function initModeSetting() {
    Qrms.updatePageData = null;
}
//阀门设置
function initValveSetting() {
    Qrms.updatePageData = null;
}
//温度修正
function initTemperatureSetting() {
    Qrms.updatePageData = null;
}



//终端维护
function initTerminalSetting() {
    var currentdNode;
    $("#cmbHistoryType").on("change", function () {
        if (!currentdNode) return;

        var id = currentdNode.id;
        var type = $(this).find("option:selected").val();

        if (!id || type == -1) {

            $("#showinfo1").html("");
            $("#historyList tbody").empty();
            return;
        }
        $.isLoading({ text: "正在获取数据..." });
        var dataUrl = "/BaseDataApi/GetDeviceHistories?rand=" + Math.random();
        AjaxGet(dataUrl, { id: id, type: type }, function (res) {
            $.isLoading("hide");
            var data = res || [];
            console.log(data);
            $("#historyList tbody").empty();

            for (var i = 0; i < data.length; i++) {
                var ele = data[i];
                var opt = "<tr onclick='onShowInfo(" + ele.Id + ")'>";
                opt += "<td>" + ele.RowNumber + "</td>";
                opt += "<td>" + ele.HistoryTypeName + "</td>";
                opt += "</tr>";
                $("#historyList tbody").append(opt);
            }


        }, function () {
            $.isLoading("hide");
        });
    });


    $("#btnSaveDeviceMaintenance").on("click", function () {
        var data = $("#DeviceMaintenanceData").serialize();
        if (!currentdNode) return;

        var id = currentdNode.id;
        //保存故障维护
        var infoUrl = "UpdateOrCreateDeviceMaintenance" + "/" + id;
        AjaxGet(infoUrl,
            data,
            function (res) {
                Qrms.updatePageData(currentdNode);
            });
    });

    function onShowInfo(id) {
        $("#infocontainer1").isLoading({
            text: "正在加载...",
            position: "overlay"
        });
        var infoUrl = "DeviceHistoryInfo" + "/" + id;
        AjaxHtml(infoUrl, {}, function (res) {
            $("#infocontainer1").isLoading("hide");
            $("#showinfo1").html(res);
        });
    }

    Qrms.updatePageData = function (node) {
        currentdNode = node;
        if (node.deviceCode) {
            enableButton("btnUpdateHistory");
        } else {
            disableButton("btnUpdateHistory");
        }
        var id = currentdNode.id;
        if (!id) {

            $("#showinfo1").html("");
            $("#historyList tbody").empty();
            return;
        }
        $("#TerminalSetting").isLoading({
            text: "正在加载...",
            position: "overlay"
        });

        //故障维护
        var infoUrl = "DeviceMaintenances" + "/" + id;
        AjaxGet(infoUrl,
            {},
            function (res) {
                $("#TerminalSetting").isLoading("hide");
                $("#historyInfo tbody").empty();
                var data = res || [];
                console.log(data);
                for (var i = 0; i < data.length; i++) {
                    var ele = data[i];
                    var opt = "<tr data-mid='" + ele.Id + "'>";
                    opt += "<td><input type='checkbox' /></td>";
                    opt += "<td>" + ele.MaintenanceDate + "</td>";
                    opt += "<td>" + ele.Memo + "</td>";
                    opt += "</tr>";
                    $("#historyInfo tbody").append(opt);
                }
            });

        //曲线库
        var infoUrl1 = "DeviceCureList" + "/" + id;
        AjaxHtml(infoUrl1,
            {},
            function (res) {
                $("#TerminalSetting").isLoading("hide");
                $("#dcl").html(res);
            });

    };




}



/************系统状态*****/

//同步时间
function btnSyncTimeFunc(btn) {
    var currentdNode = Qrms.currentdNode;

    if (!currentdNode) return;
    var code = currentdNode.deviceCode;
    $(btn).isLoading({
        text: "正在加载...",
        position: "overlay"
    });
    var dataUrl = "/BaseDataApi/SyncTime?rand=" + Math.random();
    AjaxPost(dataUrl,
        { scode: code },
        function () {
            $(btn).isLoading("hide");
            bootbox.alert('同步成功！');
        },
        function () {

        });


}

//室外温度 立即更新
function btnUpdateOutTemDataFunc(btn) {
    var currentdNode = Qrms.currentdNode;

    if (!currentdNode) return;
    var code = currentdNode.deviceCode;
    var id = currentdNode.id;
    $(btn).isLoading({
        text: "正在加载...",
        position: "overlay"
    });
    var dataUrl = "/BaseDataApi/UpdateOutTemData?rand=" + Math.random();
    AjaxPost(dataUrl,
        { scode: code, sid: id },
        function () {
            bootbox.alert('更新成功！');
            $.isLoading("hide");
        },
        function () {
            $.isLoading("hide");
        });
}

//模式设置 保存下发
function btnSetWorkModeFunc(btn) {
    var currentdNode = Qrms.currentdNode;

    if (!currentdNode) return;
    var code = currentdNode.deviceCode;
    var id = currentdNode.id;
    $(btn).isLoading({
        text: "正在执行...",
        position: "overlay"
    });
    var dataUrl = "/BaseDataApi/SetWorkMode?rand=" + Math.random();
    AjaxPost(dataUrl,
        { scode: code, sid: id },
        function () {
            $.isLoading("hide");
            bootbox.alert('更新成功！');
        },
        function () {
            $.isLoading("hide");
        });
}

//时间段设置 保存下发
function btnSetTimeSpanFunc(btn) {
    var currentdNode = Qrms.currentdNode;

    if (!currentdNode) return;
    var code = currentdNode.deviceCode;
    var id = currentdNode.id;
    var tst = $("input[name='rdoTimePeriodMode']:checked").val();
    $(btn).isLoading({
        text: "正在执行...",
        position: "overlay"
    });
    var dataUrl = "/BaseDataApi/SetTimeSpan?rand=" + Math.random();
    AjaxPost(dataUrl,
        { scode: code, sid: id, tst: tst },
        function () {
            $.isLoading("hide");
            bootbox.alert('更新成功！');
        },
        function () {
            $.isLoading("hide");
        });
}

function getTimeSpanList(deviceId, spanTimeId) {
    $.isLoading({
        text: "正在执行...",
        position: "overlay"
    });

    var dataUrl = "/BaseDataApi/GetTimeSpanList?rand=" + Math.random();
    AjaxGet(dataUrl, { id: deviceId, tsid: spanTimeId }, function (res) {
        $.isLoading("hide");

        var data = res || [];
        console.log(data);
        $("#timeSpanTable tbody").empty();

        for (var i = 0; i < data.length; i++) {
            var ele = data[i];
            var tr = "<tr>" +
                "<td>" + ele.StartTime + "</td>" +
                "<td>" + ele.EndTime + "</td>" +
                "<td>" + ele.CurveCode + "</td>" +
                "<td>" + "<a class='btn btn-primary btn-xs'>编辑</a>" + " <a class='btn btn-warning btn-xs'>删除</a>" + "</td>" +
                + "</tr>";

            $("#timeSpanTable tbody").append(tr);
        }


    }, function () {
        $.isLoading("hide");
    });
}


//模式设置 保存下发
function btnSetWorkModeFunc(btn) {
    var currentdNode = Qrms.currentdNode;

    if (!currentdNode) return;
    var code = currentdNode.deviceCode;
    var id = currentdNode.id;
    var tst = $("input[name='rdoTimePeriodMode']:checked").val();
    $(btn).isLoading({
        text: "正在执行...",
        position: "overlay"
    });
    var dataUrl = "/BaseDataApi/SetWorkMode?rand=" + Math.random();
    AjaxPost(dataUrl,
        { scode: code, sid: id, tst: tst },
        function () {
            $.isLoading("hide");
            bootbox.alert('更新成功！');
        },
        function () {
            $.isLoading("hide");
        });
}

//删除故障信息
function btnDeleteDMFunc(btn) {

    var $tr = $("#historyInfo tbody tr");
    var ids = [];
    $tr.each(function (i, e) {
        var checked = $(this).find("input[type='checkbox']").prop("checked");
        if (checked) {

            var id = $(this).attr("data-mid");
            ids.push(id);
        }
    });


    var dataUrl = "DeleteDeviceMaintenance?rand=" + Math.random();
    AjaxPost(dataUrl,
        { mid:ids.join(",") },
        function () {            
            var currentdNode = Qrms.currentdNode;
            if (!currentdNode) return;
            Qrms.updatePageData(currentdNode);
        },
        function () {
            
        });
}

//更新模式
function btnUpdateWorkModeFunc(btn) {

    var currentdNode = Qrms.currentdNode;

    if (!currentdNode) return;
    var code = currentdNode.deviceCode;
    var id = currentdNode.id;
    var name = $("input[name='valve']:checked").val();
    $.isLoading({ text: "正在获取数据..." });
    var dataUrl = "GetValvesModel/" + id + "?rand=" + Math.random();
    AjaxPost(dataUrl,
        { scode: code, sid: id, name: name },
        function (res) {
            $.isLoading("hide");
            var data = res || {};
            var workby = data.WorkBy, workmodel = data.WorkMode, setvalue = data.SetValue;

            $("input[name='workmodel'][value='" + workmodel + "']").prop("checked", true);
            $("input[name='workby'][value='" + workby + "']").prop("checked", true);
            $("input[name='ValveOpenValue'][value='" + setvalue + "']").prop("checked", true);

        },
        function () {
            $.isLoading("hide");
        });
}

//保存模式
function btnSaveWorkModeFunc(btn) {

    var currentdNode = Qrms.currentdNode;

    if (!currentdNode) return;
    var code = currentdNode.deviceCode;
    var id = currentdNode.id; 
    $.isLoading({ text: "正在保存..." });
    var dataUrl = "UpdateValvesModel/" + id + "?rand=" + Math.random() + "&" + $("#ModeSettingForm").serialize();
    AjaxPost(dataUrl,
        {},
        function (res) {
            $.isLoading("hide");
            bootbox.alert('保存成功！');

        },
        function () {
            $.isLoading("hide");
        });
}
/******系统状态************/
