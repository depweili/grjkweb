function initGlobe(win) {
    var day;
    win.tick = function () {
        if (!day) {
            var date = ServerTime.replace(new RegExp(/-/gm), "/")
            day = new Date(date);
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

    //$("#DeviceMaintenanceData input[name='maintenanceDate']").datepicker();
}

function initEvent() {
    //阀门单选事件
    $("input[name='valve']").on("change", function () {
        rdoValveCheck();
    });

    //阀门全开
    $("#btnOpenFull").on("click", function () {
        $("#txtValveOpenValue").val(100);
    });
    $("#btnCloseFull").on("click", function () {
        $("#txtValveOpenValue").val(0);
    });

    //阀门设置
    //$("input[tag='spinner']").spinner({ min: 0, max: 100 });


    //时间段模式设置
    $("input[name='rdoTimePeriodMode']").on("change", function () {
        var currentdNode = Qrms.currentdNode;
        if (!currentdNode) return;
        var id = currentdNode.id;
        var tst = $("input[name='rdoTimePeriodMode']:checked").val();
        getTimeSpanList(id, tst);
    });

    //导入曲线库
    $("#uploadDeviceCurveLibrary").on("click",function(){
        var actionUrl = "UpLoad";
        var param = {};
        ShowFileForm(actionUrl, param, "导入设备曲线库", true);
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
                LoadDataFromDeviceData(data);
                /*
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
                    var cct = Qrms.GetEnumPropName(Qrms.ValveControlChannel, node.ctrlNumber);
                    if (cct) {
                        $("#lblControlChannel").text(cct);
                    }
                }
                */


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
            name: "外界温度",
            nameLocation: "center",
            nameGap:30,
            type: 'value',
            min: -50,
            max: 30,
            //boundaryGap: false,
            //data: [0, 0.2, 0.4, 0.6, 0.8, 1.0, 1.2]
        },
        yAxis: {
            name: "预设温度",
            nameLocation: "center",
            nameRotate: 90,
            nameGap: 30,
            type: 'value',
            //data: [0, 0.2, 0.4, 0.5, 0.6, 0.8, 1.0, 1.2],
            axisLabel: {
                formatter: '{value}'
            }
        },
        series: [
            {
                name: '温度',
                type: 'line',
                //data: [[-45, 20], [-40, 10], [-36.5, 20], [16.5, 30], [22.1, 40]]
            }
        ]
    };

    // 基于准备好的dom，初始化echarts实例
    var myChart = echarts.init(document.getElementById('qxchart'));
    myChart.setOption(option);

    //$("#DeviceCurLibraryList").on("change", function () {
    //    var $opt = $(this).find("option:selected");
    //    var data = $opt.data("dclData") || {};
    //    var start = -40;
    //    var dataX = [], dataY = [];
    //    for (var i = 1; i <= 121; i++) {
    //        dataX.push(start);
    //        dataY.push(data["Column" + i]);
    //        start += 0.5;
    //    }
    //    myChart.setOption({
    //        xAxis: {
    //            data: dataX
    //        },

    //        series: [
    //            {
    //                data: dataY
    //            }
    //        ]
    //    });
    //});

    $("#DeviceCurLibraryList").bind("input propertychange", function () {
        var $opt = $(this).find("option:selected");
        var ele = $opt.data("dclData") || {};
        if (ele.Id && ele.Id != null)
        {
            var dataUrl = "/BaseDataApi/GetDeviceCurveData?rand=" + Math.random();

            AjaxGet(dataUrl, { curid: ele.Id }, function (res) {
                $.isLoading("hide");

                var data = res || [];
                console.log(data);

                myChart.setOption({
                    series: [
                        {
                            data: data
                        }
                    ]
                });

            }, function () {
                $.isLoading("hide");
            });
        }
        
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

//设备地图
function iniDeviceMap() {
    // 百度地图API功能
    Qrms.Map = new BMap.Map("devicemap");
    var point = new BMap.Point(116.404, 39.915);
    Qrms.Map.centerAndZoom(point, 5);


    var top_left_control = new BMap.ScaleControl({ anchor: BMAP_ANCHOR_TOP_LEFT }); // 左上角，添加比例尺
    var top_left_navigation = new BMap.NavigationControl(); //左上角，添加默认缩放平移控件
    var top_right_navigation = new BMap.NavigationControl({ anchor: BMAP_ANCHOR_TOP_RIGHT, type: BMAP_NAVIGATION_CONTROL_SMALL });

    Qrms.Map.addControl(top_left_control);
    Qrms.Map.addControl(top_left_navigation);
    Qrms.Map.addControl(top_right_navigation);


    Qrms.updateMap = function () {
        var map = Qrms.Map;
        var dos = Qrms.DeviceObjs || [];

        Qrms.DeviceMapObjs = [];
        map.clearOverlays();

        var sbofflin = new BMap.Icon("/content/admin/images/map/sboffline.png", new BMap.Size(32, 32));
        var sbonline = new BMap.Icon("/content/admin/images/map/sbonline.png", new BMap.Size(32, 32));

        for (var i = 0; i < dos.length; i++) {
            var lng = dos[i].longitude;
            var lat = dos[i].latitude;
            if (lng && lat) {
                var pt = new BMap.Point(lng, lat);
                var myIcon = dos[i].isOnline ? sbonline : sbofflin;

                var marker = new BMap.Marker(pt, { icon: myIcon });
                marker.did = dos[i].id;
                Qrms.DeviceMapObjs.push(marker);


                marker.addEventListener("click", openDeviceInfoWindow);

                map.addOverlay(marker);
            }
        }


    };


    Qrms.updateMap();

    var currentNode;
    Qrms.updatePageData = function (node) {
        currentNode = node;

        if (node.deviceCode) {
            openDeviceInfoWindow();

        } else {
            var lng = node.longitude;
            var lat = node.latitude;
            if (lng && lat) {
                var point = new BMap.Point(lng, lat);
                Qrms.Map.centerAndZoom(point, 13);
            }
        }
    }

    function getCurrentMarker() {
        if (!currentNode) return null;
        for (var i = 0; i < Qrms.DeviceMapObjs.length; i++) {
            var o = Qrms.DeviceMapObjs[i];
            if (o.did == currentNode.id) {
                return o;
            }
        }
    }

    function openDeviceInfoWindow(m) {
        var cn = m ? m.target : getCurrentMarker();
        var $this = cn;
        console.log(m);
        var lng = cn.longitude;
        var lat = cn.latitude;
        var point = new BMap.Point(lng, lat);
        var path = "DeviceInfo/" + (cn.did || 0);
        var infoUrl = path + "?rand=" + Math.random();
        AjaxHtml(infoUrl, {}, function (res) {
            var sContent = res;
            var infoWindow = new BMap.InfoWindow(sContent);
            $this.openInfoWindow(infoWindow);
        });
    }
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

    $("#btnAddDM").on("click", function () {
        if (!currentdNode) return;

        var id = currentdNode.id;

        $("#DeviceMaintenanceData input[name='mid']").val(null);
        $("#DeviceMaintenanceData input[name='maintenanceDate']").val(null);
        $("#DeviceMaintenanceData textarea[name='memo']").val(null);

    });

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
                //$("#historyInfo tbody").empty();
                //var data = res || [];
                //console.log(data);
                //for (var i = 0; i < data.length; i++) {
                //    var ele = data[i];
                //    var opt = "<tr data-mid='" + ele.Id + "'>";
                //    opt += "<td><input type='checkbox' /></td>";
                //    opt += "<td>" + ele.MaintenanceDate + "</td>";
                //    opt += "<td>" + ele.Memo + "</td>";
                //    opt += "</tr>";
                //    $("#historyInfo tbody").append(opt);
                //}
                var tshistoryInfo = $('#historyInfo').DataTable({
                    destroy: true,
                    bPaginate: false,
                    bLengthChange: false,
                    bFilter: false,
                    bInfo: false,

                    data: res,
                    columns: [
                        {
                            "sClass": "text-center",
                            "data": "Id",
                            "render": function (data, type, full, meta) {
                                return '<input type="checkbox" value="' + data + '" />';
                            },
                            "bSortable": false
                        },
                        //{data:"Id"},
                    { data: "MaintenanceDate" },
                    { data: "Memo" }
                    ]
                });

                $('#historyInfo tbody tr').on('click', function () {

                    if ($(this).hasClass('selected')) {
                        $(this).removeClass('selected');
                    }
                    else {
                        tshistoryInfo.$('tr.selected').removeClass('selected');
                        $(this).addClass('selected');
                    }

                    var rowdata = tshistoryInfo.row('.selected').data();

                    console.log(rowdata);

                    //alert(rowdata.StartTime);

                    $("#DeviceMaintenanceData input[name='mid']").val(rowdata.Id);
                    $("#DeviceMaintenanceData input[name='maintenanceDate']").val(rowdata.MaintenanceDate);
                    $("#DeviceMaintenanceData textarea[name='memo']").val(rowdata.Memo);
                    

                });

            },
            function () {
                $.isLoading("hide");
            });

        //曲线库
        //var infoUrl1 = "DeviceCureList" + "/" + id;   AjaxHtml
        var infoUrl1 = "/BaseDataApi/GetDeviceCurLibraryDataList?rand=" + Math.random();
        AjaxGet(infoUrl1,
            { id: id },
            function (res) {
                $("#TerminalSetting").isLoading("hide");
                //$("#dcl").html(res);

                var data = res || [];

                var qxkTable = $('#qxkTable').DataTable({
                    destroy: true,
                    bPaginate: false,
                    bLengthChange: false,
                    bFilter: false,
                    bInfo: false,
                    ordering: false,
                    AutoWidth: true,
                    scrollY: 400,
                    scrollX: true,

                    data: data,
                    columns: [
                    { data: "Code", Width: "10%" },
                    { data: "Name", Width: "10%" },
                    { data: "Column1" },
                    { data: "Column2" },
                    { data: "Column3" },
                    { data: "Column4" },
                    { data: "Column5" },
                    { data: "Column6" },
                    { data: "Column7" },
                    { data: "Column8" },
                    { data: "Column9" },
                    { data: "Column10" },
                    { data: "Column11" },
                    { data: "Column12" },
                    { data: "Column13" },
                    { data: "Column14" },
                    { data: "Column15" },
                    { data: "Column16" },
                    { data: "Column17" },
                    { data: "Column18" },
                    { data: "Column19" },
                    { data: "Column20" },
                    { data: "Column21" },
                    { data: "Column22" },
                    { data: "Column23" },
                    { data: "Column24" },
                    { data: "Column25" },
                    { data: "Column26" },
                    { data: "Column27" },
                    { data: "Column28" },
                    { data: "Column29" },
                    { data: "Column30" },
                    { data: "Column31" },
                    { data: "Column32" },
                    { data: "Column33" },
                    { data: "Column34" },
                    { data: "Column35" },
                    { data: "Column36" },
                    { data: "Column37" },
                    { data: "Column38" },
                    { data: "Column39" },
                    { data: "Column40" },
                    { data: "Column41" },
                    { data: "Column42" },
                    { data: "Column43" },
                    { data: "Column44" },
                    { data: "Column45" },
                    { data: "Column46" },
                    { data: "Column47" },
                    { data: "Column48" },
                    { data: "Column49" },
                    { data: "Column50" },
                    { data: "Column51" },
                    { data: "Column52" },
                    { data: "Column53" },
                    { data: "Column54" },
                    { data: "Column55" },
                    { data: "Column56" },
                    { data: "Column57" },
                    { data: "Column58" },
                    { data: "Column59" },
                    { data: "Column60" },
                    { data: "Column61" },
                    { data: "Column62" },
                    { data: "Column63" },
                    { data: "Column64" },
                    { data: "Column65" },
                    { data: "Column66" },
                    { data: "Column67" },
                    { data: "Column68" },
                    { data: "Column69" },
                    { data: "Column70" },
                    { data: "Column71" },
                    { data: "Column72" },
                    { data: "Column73" },
                    { data: "Column74" },
                    { data: "Column75" },
                    { data: "Column76" },
                    { data: "Column77" },
                    { data: "Column78" },
                    { data: "Column79" },
                    { data: "Column80" },
                    { data: "Column81" },
                    { data: "Column82" },
                    { data: "Column83" },
                    { data: "Column84" },
                    { data: "Column85" },
                    { data: "Column86" },
                    { data: "Column87" },
                    { data: "Column88" },
                    { data: "Column89" },
                    { data: "Column90" },
                    { data: "Column91" },
                    { data: "Column92" },
                    { data: "Column93" },
                    { data: "Column94" },
                    { data: "Column95" },
                    { data: "Column96" },
                    { data: "Column97" },
                    { data: "Column98" },
                    { data: "Column99" },
                    { data: "Column100" },
                    { data: "Column101" },
                    { data: "Column102" },
                    { data: "Column103" },
                    { data: "Column104" },
                    { data: "Column105" },
                    { data: "Column106" },
                    { data: "Column107" },
                    { data: "Column108" },
                    { data: "Column109" },
                    { data: "Column110" },
                    { data: "Column111" },
                    { data: "Column112" },
                    { data: "Column113" },
                    { data: "Column114" },
                    { data: "Column115" },
                    { data: "Column116" },
                    { data: "Column117" },
                    { data: "Column118" },
                    { data: "Column119" },
                    { data: "Column120" },
                    { data: "Column121" }

                    ]
                });
            });
    };
}

function onShowInfo(id) {
    $("#infocontainer1").isLoading({
        text: "正在加载...",
        position: "overlay"
    });
    var infoUrl = "DeviceHistoryInfo" + "/" + id;
    AjaxHtml(infoUrl, {}, function (res) {
        $("#infocontainer1").isLoading("hide");
        $("#showinfo1").html(res);
    }, function () {
        $.isLoading("hide");
    });
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
        function (data) {
            $(btn).isLoading("hide");
            if (data.ResultType == 0) {
                bootbox.alert('同步成功！');
            }
            else {
                bootbox.alert('失败:' + data.Message);
            }
            
        },
        function () {
            $.isLoading("hide");
            bootbox.alert('出错！');
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
        function (data) {
            $.isLoading("hide");

            if (data.ResultType == 0) {
                LoadDataFromDeviceData(data.AppendData);

                //btnUpdateCS
                if (Qrms.currentParaData == null) {
                    $("#btnUpdateCS").trigger("click");
                }
                else {
                    bootbox.alert('更新成功！');
                }
            }
            else {
                bootbox.alert('失败:' + data.Message);
            }
            
        },
        function () {
            $.isLoading("hide");
        });
}


/////////////////////////////////////////////////////////////////////////////////////////

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
            $(btn).isLoading("hide");
        },
        function () {
            $.isLoading("hide");
            $(btn).isLoading("hide");
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
        //$("#timeSpanTable tbody").empty();

        //for (var i = 0; i < data.length; i++) {
        //    var ele = data[i];
        //    var tr = "<tr>" +
        //        "<td>" + ele.StartTime + "</td>" +
        //        "<td>" + ele.EndTime + "</td>" +
        //        "<td>" + ele.CurveCode + "</td>" +
        //        "<td>" + "<a class='btn btn-primary btn-xs'>编辑</a>" + " <a class='btn btn-warning btn-xs'>删除</a>" + "</td>" +
        //        + "</tr>";

        //    $("#timeSpanTable tbody").append(tr);
        //}


        //DataTable api
        //dataTable  jquery 对象 需要$( selector ).dataTable().api(); 否者下面的row() 无法使用
        var tstable = $('#timeSpanTable').DataTable({
            destroy: true,
            bPaginate: false,
            bLengthChange: false,
            bFilter: false,
            bInfo: false,

            data: res,
            columns: [
            { data: "StartTime" },
            { data: "EndTime" },
            { data: "CurveCode" }
            ]
        });

        //$('#timeSpanTable tbody').on('click', 'tr', function () {

        $('#timeSpanTable tbody tr').on('click', function () {

            if ($(this).hasClass('selected')) {
                $(this).removeClass('selected');
            }
            else {
                tstable.$('tr.selected').removeClass('selected');
                $(this).addClass('selected');
            }

            var rowdata = tstable.row('.selected').data();

            console.log(rowdata);

            //alert(rowdata.StartTime);

            $("#topPeriod").val(rowdata.StartTime);
            $("#bottomPeriod").val(rowdata.EndTime);
            $("#DeviceCurLibraryList").val(rowdata.CurveCode);
            $("#DeviceCurLibraryList").trigger("input");

        });


    }, function () {
        $.isLoading("hide");
    });
}

//新增时间段
function btnAddSpanFunc(btn) {
    var currentdNode = Qrms.currentdNode;

    if (!currentdNode) return;
    var code = currentdNode.deviceCode;
    var id = currentdNode.id;
    var tst = $("input[name='rdoTimePeriodMode']:checked").val();
    //$(btn).isLoading({
    //    text: "正在加载...",
    //    position: "overlay"
    //});
    var dataUrl = "/BaseDataApi/AddTimeSpan?rand=" + Math.random();

    AjaxPost(dataUrl,
        { deviceid: id, timespantype: tst, StartTime: $("#topPeriod").val(), EndTime: $("#bottomPeriod").val(), CurveCode: $("#DeviceCurLibraryList").val() },
        function (data) {
            //LoadDataFromDeviceData(data.AppendData);
            console.log(data);
            if (data.ResultType == 0) {
                //Qrms.currentParaData = data.AppendData;
                //DoAfterUpdateData();
                getTimeSpanList(id, tst);
                bootbox.alert('添加成功！');
            }
            else {
                bootbox.alert('添加失败！');
            }

            $.isLoading("hide");
        },
        function () {
            bootbox.alert('执行失败！');
            $.isLoading("hide");
        });
}

function btnSaveSpanFunc(btn) {
    var currentdNode = Qrms.currentdNode;

    if (!currentdNode) return;
    var code = currentdNode.deviceCode;
    var id = currentdNode.id;
    var tstable = $('#timeSpanTable').DataTable();
    var rowdata = tstable.row('.selected').data();
    var tsid = rowdata.Id;
    var tst = $("input[name='rdoTimePeriodMode']:checked").val();
    //$(btn).isLoading({
    //    text: "正在加载...",
    //    position: "overlay"
    //});
    var dataUrl = "/BaseDataApi/SaveTimeSpan?rand=" + Math.random();

    AjaxPost(dataUrl,
        { timespanid: tsid, StartTime: $("#topPeriod").val(), EndTime: $("#bottomPeriod").val(), CurveCode: $("#DeviceCurLibraryList").val() },
        function (data) {
            //LoadDataFromDeviceData(data.AppendData);
            console.log(data);
            if (data.ResultType == 0) {
                //Qrms.currentParaData = data.AppendData;
                //DoAfterUpdateData();
                getTimeSpanList(id, tst);
                bootbox.alert('修改成功！');
            }
            else {
                bootbox.alert('修改失败！');
            }

            $.isLoading("hide");
        },
        function () {
            bootbox.alert('执行失败！');
            $.isLoading("hide");
        });
}

function btnDeleteSpanFunc(btn) {
    var currentdNode = Qrms.currentdNode;

    if (!currentdNode) return;
    var code = currentdNode.deviceCode;
    var id = currentdNode.id;
    var tst = $("input[name='rdoTimePeriodMode']:checked").val();

    var tstable = $('#timeSpanTable').DataTable();
    var rowdata = tstable.row('.selected').data();
    var tsid = rowdata.Id;

    var dataUrl = "/BaseDataApi/DeleteTimeSpan?rand=" + Math.random();

    AjaxPost(dataUrl,
        { timespanid: tsid },
        function (data) {
            //LoadDataFromDeviceData(data.AppendData);
            console.log(data);
            if (data.ResultType == 0) {
                //Qrms.currentParaData = data.AppendData;
                //DoAfterUpdateData();
                getTimeSpanList(id, tst);
                bootbox.alert('修改成功！');
            }
            else {
                bootbox.alert('修改失败！');
            }

            $.isLoading("hide");
        },
        function () {
            bootbox.alert('执行失败！');
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
            //var id = $(this).attr("data-mid");
            var id = $(this).find("input[type='checkbox']").val();
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




//v1统一更新*********************************************************************************************************
function btnUpdateDataFunc(btn) {
    var currentdNode = Qrms.currentdNode;

    if (!currentdNode) return;
    var code = currentdNode.deviceCode;
    var id = currentdNode.id;
    //$(btn).isLoading({
    //    text: "正在加载...",
    //    position: "overlay"
    //});
    var dataUrl = "/BaseDataApi/UpdateData?rand=" + Math.random();
    AjaxPost(dataUrl,
        { scode: code, clientdata: { } },
        function (data) {
            //LoadDataFromDeviceData(data.AppendData);
            console.log(data);
            if (data.AppendData && data.AppendData != null) {
                Qrms.currentParaData = data.AppendData;
                DoAfterUpdateData();
                bootbox.alert('更新成功！');
            }
            else {
                bootbox.alert('更新失败！');
            }

            $.isLoading("hide");
        },
        function () {
            $.isLoading("hide");
        });
}

//保存下发
function btnSaveAndSendFunc(btn) {
    var currentdNode = Qrms.currentdNode;

    if (!currentdNode) return;
    var code = currentdNode.deviceCode;
    var id = currentdNode.id;
    //$(btn).isLoading({
    //    text: "正在加载...",
    //    position: "overlay"
    //});
    var dataUrl = "/BaseDataApi/SaveAndSendData?rand=" + Math.random();

    BuildCurrentParaData();

    AjaxPost(dataUrl,
        { scode: code, clientdata: Qrms.currentParaData },
        function (data) {
            //LoadDataFromDeviceData(data.AppendData);
            console.log(data);
            if (data.AppendData && data.AppendData != null) {
                //Qrms.currentParaData = data.AppendData;
                //DoAfterUpdateData();
                bootbox.alert('保存成功！');
            }
            else {
                bootbox.alert('更新失败！');
            }

            $.isLoading("hide");
        },
        function () {
            $.isLoading("hide");
        });
}

//更新时间段
function btnUpdateTimeSpanFunc(btn) {
    var currentdNode = Qrms.currentdNode;

    if (!currentdNode) return;
    var code = currentdNode.deviceCode;
    var id = currentdNode.id;
    var tst = $("input[name='rdoTimePeriodMode']:checked").val();
    //$(btn).isLoading({
    //    text: "正在加载...",
    //    position: "overlay"
    //});
    var dataUrl = "/BaseDataApi/UpdateTimeSpan?rand=" + Math.random();

    AjaxPost(dataUrl,
        { scode: code, timespantype: tst },
        function (data) {
            //LoadDataFromDeviceData(data.AppendData);
            console.log(data);
            if (data.AppendData && data.AppendData != null) {
                //Qrms.currentParaData = data.AppendData;
                //DoAfterUpdateData();
                getTimeSpanList(id, tst);
                bootbox.alert('更新成功！');
            }
            else {
                bootbox.alert('更新失败！');
            }

            $.isLoading("hide");
        },
        function () {
            bootbox.alert('执行失败！');
            $.isLoading("hide");
        });
}

//更新历史数据
function btnUpdateHistoryFunc(btn) {
    var currentdNode = Qrms.currentdNode;

    if (!currentdNode) return;
    var code = currentdNode.deviceCode;
    var id = currentdNode.id;
    var cmbHistoryType = $("#cmbHistoryType").val();
    var nuRowNumber = $("#nuRowNumber").val();
    //$(btn).isLoading({
    //    text: "正在加载...",
    //    position: "overlay"
    //});
    var dataUrl = "/BaseDataApi/UpdateHistory?rand=" + Math.random();

    //var clientdata = { cmbHistoryType: cmbHistoryType, nuRowNumber: nuRowNumber };

    //var postdata = { ID: "1", NAME: "Jim", CREATETIME: "1988-09-11" };

    AjaxPost(dataUrl,
         //JSON.stringify({ NAME:"Lilei", Charging:postdata }),
        //{ scode: code, clientdata: "{ \"cmbHistoryType\": "+cmbHistoryType+", \"nuRowNumber\": "+nuRowNumber+" }" },
        //{ scode: code, clientdata: JSON.stringify({ cmbHistoryType: cmbHistoryType, nuRowNumber: nuRowNumber }) },
        //JSON.stringify({ scode: code, clientdata: { cmbHistoryType: cmbHistoryType, nuRowNumber: nuRowNumber } }),
        { scode: code, cmbHistoryType: cmbHistoryType, nuRowNumber: nuRowNumber } ,
        //{ cmbHistoryType: cmbHistoryType, nuRowNumber: nuRowNumber },
        function (data) {
            //LoadDataFromDeviceData(data.AppendData);
            console.log(data);
            if (data.AppendData && data.AppendData != null) {
                $('#cmbHistoryType').trigger("change");
                bootbox.alert('更新成功！');
            }
            else {
                bootbox.alert('更新失败！');
            }

            $.isLoading("hide");
        },
        function () {
            bootbox.alert('执行失败！');
            $.isLoading("hide");
        });
}