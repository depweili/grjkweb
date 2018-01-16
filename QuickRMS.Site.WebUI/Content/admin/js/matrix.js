
$(document).ready(function () {

    (function testConn() {

        var url = "http://" + window.location.host + "/Admin/Test/TestConn";
        var homeUrl = "http://" + window.location.host + '/Admin/Login';
        AjaxGet(url, null, function (res) {
            if (res && res.state === 0) {
                window.location.href = homeUrl;
                setTimeout(function () { alert("超过20分钟没有操作，您已自动退出系统。"); }, 1);
            } else {

                setTimeout(function () { testConn(); }, 1210000);
            }

        }, function (error) {
            window.location.href = homeUrl;
        });
    })();


    function computeMoney(editName) {
        var bxje = $("#modal-form input[name='BXJE']").val();
        var fl = $("#modal-form input[name='FL']").val();
        var ret = "";
        if ($.isNumeric(bxje) && $.isNumeric(fl)) {
            ret = (Number(bxje) * Number(fl)/100).toFixed(2);
        }
        if (editName != "MJBXF")
        $("#modal-form input[name='MJBXF']").val(ret);


        var bxje = $("#modal-form input[name='MJBXF']").val();
        var fl = $("#modal-form input[name='HL']").val();
        var ret = "";
        if ($.isNumeric(bxje) && $.isNumeric(fl)) {
            ret = (Number(bxje) * Number(fl)/100).toFixed(2);
        }

        if (editName != "RMBBF")
        $("#modal-form input[name='RMBBF']").val(ret);

        var bxje = $("#modal-form input[name='RMBBF']").val();
        var fl = $("#modal-form input[name='JJBL']").val();
        var ret = "";
        if ($.isNumeric(bxje) && $.isNumeric(fl)) {
            ret = (Number(bxje) * Number(fl)/100).toFixed(2);
        }

        if (editName != "JJFJE")
        $("#modal-form input[name='JJFJE']").val(ret);
    }
    //===计算保险金额==//
    $("body").delegate("input[name='BXJE'],input[name='FL']", "keyup", function() {
        computeMoney();
    });

    $("body").delegate("input[name='HL'],input[name='JJBL']", "keyup", function () {
        computeMoney();
    });
    /*
    $("body").delegate("input[name='MJBXF']", "keyup", function () {
        computeMoney("MJBXF");
    });

    $("body").delegate("input[name='RMBBF']", "keyup", function () {
        computeMoney("RMBBF");
    });
    */

    // === datepicker === //
    $("body").delegate("input[data-type = 'datepicker']", "click focus", function () {
        if (!$(this).hasClass("hasDatepicker")) {
            $(this).datepicker({
                showButtonPanel: true,
                dateFormat: 'yy-mm-dd'
            });
        }
    });


    // === Sidebar navigation === //

    $('.submenu > a').click(function (e) {
        e.preventDefault();
        var submenu = $(this).siblings('ul');
        var li = $(this).parents('li');
        var submenus = $('#sidebar li.submenu ul');
        var submenus_parents = $('#sidebar li.submenu');
        if (li.hasClass('open')) {
            if (($(window).width() > 768) || ($(window).width() < 479)) {
                submenu.slideUp();
            } else {
                submenu.fadeOut(250);
            }
            li.removeClass('open');
        } else {
            if (($(window).width() > 768) || ($(window).width() < 479)) {
                submenus.slideUp();
                submenu.slideDown();
            } else {
                submenus.fadeOut(250);
                submenu.fadeIn(250);
            }
            submenus_parents.removeClass('open');
            li.addClass('open');
        }
    });

    var ul = $('#sidebar > ul');

    $('#sidebar > a').click(function (e) {
        e.preventDefault();
        var sidebar = $('#sidebar');
        if (sidebar.hasClass('open')) {
            sidebar.removeClass('open');
            ul.slideUp(250);
        } else {
            sidebar.addClass('open');
            ul.slideDown(250);
        }
    });

    // === Resize window related === //
    $(window).resize(function () {
        if ($(window).width() > 479) {
            ul.css({ 'display': 'block' });
            $('#content-header .btn-group').css({ width: 'auto' });
        }
        if ($(window).width() < 479) {
            ul.css({ 'display': 'none' });
            fix_position();
        }
        if ($(window).width() > 768) {
            $('#user-nav > ul').css({ width: 'auto', margin: '0' });
            $('#content-header .btn-group').css({ width: 'auto' });
        }
    });

    if ($(window).width() < 468) {
        ul.css({ 'display': 'none' });
        fix_position();
    }

    if ($(window).width() > 479) {
        $('#content-header .btn-group').css({ width: 'auto' });
        ul.css({ 'display': 'block' });
    }

    // === Tooltips === //
    //$('.tip').tooltip();	
    //$('.tip-left').tooltip({ placement: 'left' });	
    //$('.tip-right').tooltip({ placement: 'right' });	
    //$('.tip-top').tooltip({ placement: 'top' });	
    //$('.tip-bottom').tooltip({ placement: 'bottom' });	

    // === Search input typeahead === //
    $('#search input[type=text]').typeahead({
        source: ['Dashboard', 'Form elements', 'Common Elements', 'Validation', 'Wizard', 'Buttons', 'Icons', 'Interface elements', 'Support', 'Calendar', 'Gallery', 'Reports', 'Charts', 'Graphs', 'Widgets'],
        items: 4
    });

    // === Fixes the position of buttons group in content header and top user navigation === //
    function fix_position() {
        var uwidth = $('#user-nav > ul').width();
        $('#user-nav > ul').css({ width: uwidth, 'margin-left': '-' + uwidth / 2 + 'px' });

        var cwidth = $('#content-header .btn-group').width();
        $('#content-header .btn-group').css({ width: cwidth, 'margin-left': '-' + uwidth / 2 + 'px' });
    }

    // === Style switcher === //
    $('#style-switcher i').click(function () {
        if ($(this).hasClass('open')) {
            $(this).parent().animate({ marginRight: '-=190' });
            $(this).removeClass('open');
        } else {
            $(this).parent().animate({ marginRight: '+=190' });
            $(this).addClass('open');
        }
        $(this).toggleClass('icon-arrow-left');
        $(this).toggleClass('icon-arrow-right');
    });

    $('#style-switcher a').click(function () {
        var style = $(this).attr('href').replace('#', '');
        $('.skin-color').attr('href', 'css/maruti.' + style + '.css');
        $(this).siblings('a').css({ 'border-color': 'transparent' });
        $(this).css({ 'border-color': '#aaaaaa' });
    });

    $('.lightbox_trigger').click(function (e) {

        e.preventDefault();

        var image_href = $(this).attr("href");

        if ($('#lightbox').length > 0) {

            $('#imgbox').html('<img src="' + image_href + '" /><p><i class="icon-remove icon-white"></i></p>');

            $('#lightbox').slideDown(500);
        }

        else {
            var lightbox =
			'<div id="lightbox" style="display:none;">' +
				'<div id="imgbox"><img src="' + image_href + '" />' +
					'<p><i class="icon-remove icon-white"></i></p>' +
				'</div>' +
			'</div>';

            $('body').append(lightbox);
            $('#lightbox').slideDown(500);
        }

    });


    $('#lightbox').on('click', function () {
        $('#lightbox').hide(200);
    });

    // === 左侧导航选中 === //
    var indexActive = true;
    var locationUrl = location.pathname + "/";

    $(".childmenu a").each(function () {
        var rel = $(this).attr('href') + "/";
        if (locationUrl == rel) {
            $(this).parent().addClass("child-active");
            $(this).parent().parent().parent().addClass("open");

            $(".child-active span").addClass("menu-text");
            //$(".parent-active span").addClass("menu-text");

            indexActive = false;
            return true;
        }
    });
    if (indexActive) {
        $(".index").addClass("parent-active");
        $(".parent-active span").addClass("menu-text");
    }

});


/*******清除表单数据*********/
function ClearForm(obj) {
    obj.find(':input').not(':button, :submit, :reset,[type="hidden"],:hidden').val('').removeAttr('checked').removeAttr('selected');
}


/*******注册验证脚本*********/
function RegisterForm() {
    $('#modal-content').removeData('validator');
    $('#modal-content').removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse('#modal-content');
}

/*******关闭弹出框*********/
function CloseModal() {
    $('#modal-form').modal('hide');
    ClearForm($("#modal-content"));

    $("#msgFileload").hide().html("");
}

/*******刷新表格*********/
function ReloadDataTable(obj) {
    obj.fnDraw();
}


/*******初始化*********/
function InitDatatables(dataTableObj, actionUrl, aoColumns, oTable, footterCallback) {
    var fnFootterCallback = footterCallback || new Function();
    oTable = dataTableObj.dataTable({
        "bJQueryUI": true,
        "sPaginationType": "full_numbers",
        'bLengthChange': true,
        "bFilter": false,
        "bInfo": true,
        'bPaginate': true,
        "bProcessing": true,
        "bAutoWidth": false,
        "bServerSide": true,
        "bStateSave": false,
        "iDisplayLength": 10,
        "aLengthMenu": [[10, 20, 50, 100], [10, 20, 50, 100]],
        "oLanguage": {
            "sLengthMenu": "每页显示 _MENU_ 条记录",
            "sZeroRecords": "对不起，查询不到任何相关数据",
            "sInfo": "当前显示 _START_ 到 _END_ 条，共 _TOTAL_ 条记录",
            "sInfoEmtpy": "找不到相关数据",
            "sInfoFiltered": "数据表中共为 _MAX_ 条记录",
            "sProcessing": "正在加载中...",
            "sSearch": "搜索",
            "sEmptyTable": "表中数据为空",
            "oPaginate": {
                "sFirst": "第一页",
                "sPrevious": " 上一页 ",
                "sNext": " 下一页 ",
                "sLast": " 最后一页 "
            }
        },
        "fnFooterCallback": fnFootterCallback,
        "sAjaxSource": actionUrl,
        "aoColumns": aoColumns

    });
    //初始化下拉框
    $('select').select2();

    return oTable;
}

/*******弹出表单*********/
function ShowModal(actionUrl, param, title, hiddenBtn) {

    //表单初始化
    $(".modal-title").html(title);
    $("#modal-content").attr("action", actionUrl);

    $.ajax({
        type: "GET",
        url: actionUrl,
        data: param,
        beforeSend: function () {
            //
        },
        success: function (result) {
            $("#modal-content").html(result);
            if (hiddenBtn) {
                $("#modal-form .modal-footer").hide();
            } else {
                $("#modal-form .modal-footer").show();
            }
            $('select').select2();
            $('#modal-form').modal('show');
            RegisterForm();
        },
        error: function () {
            window.location.reload();
        },
        complete: function () {
            //
        }
    });
}

/*******弹出文件上传表单*********/
function ShowFileForm(actionUrl, param, title) {

    //表单初始化
    $(".modal-title").html(title);
    $("#modal-content").attr("action", actionUrl);

    $.ajax({
        type: "GET",
        url: actionUrl,
        data: param,
        beforeSend: function () {
            //
        },
        success: function (result) {
            $("#modal-content").html(result);
            $('select').select2();
            $('#modal-form').modal('show');
            RegisterForm();
        },
        error: function () {
            window.location.reload();
        },
        complete: function () {
            //
        }
    });
}

/*******保存表单*********/
function SaveModal(oTable) {
    var actionUrl = $("#modal-content").attr("action");
    var $form = $("#modal-content");

    if (!$form.valid()) {
        return;
    }

    $.ajax({
        type: "POST",
        url: actionUrl,
        data: $form.serialize(),
        success: function (data) {
            //判断返回值，若为Object类型，即操作成功
            var result = ((typeof data == 'object') && (data.constructor == Object));
            if (result) {
                bootbox.alert(data.Message);
                $('#modal-form').modal('hide');
                ReloadDataTable(oTable);
            }
            else {
                $("#modal-content").html(data);
            }
        }
    });
}

/*******保存表单*********/
function PostForm(form, url, fnSuccess, fnError) {

    var $form = typeof form == "string" ? $("#" + form) : form;
    var actionUrl = url || $form.attr("action");

    if (!$form.valid()) {
        return;
    }

    $.ajax({
        type: "POST",
        url: actionUrl,
        data: $form.serialize(),
        success: function (data) {
            if (typeof fnSuccess == "function") {
                fnSuccess(data);
            }
        },
        error: function () {
            if (typeof fnError == "function") {
                fnError();
            }
        }
    });
}

/*******查询*********/
function SearchRecord(actionUrl, oTable) {
    oTable.fnReloadAjax(actionUrl);
}

/*******删除操作*********/
function DeleteRecord(actionUrl, param, oTable) {
    bootbox.dialog({
        "message": "你确认要删除这条记录？",
        "title": "删除",
        // "className": "btn-danger",
        "callback": function () {
            //
        },
        "buttons": {
            confirm: {
                "label": "确定",
                "className": "btn-danger",
                "callback": function () {
                    $.ajax({
                        type: "POST",
                        url: actionUrl,
                        data: param,
                        beforeSend: function () {
                            //
                        },
                        success: function (result) {
                            bootbox.alert(result.Message);
                            if (result.ResultType == 0) {
                                ReloadDataTable(oTable);
                            }
                        },
                        error: function () {
                            //
                        },
                        complete: function () {
                            //
                        }
                    });
                }
            },
            cancel: {
                "label": "取消",
                "className": "btn-default"
            }

        }
    });
}

/*******删除全部操作*********/
function DeleteAllRecord(actionUrl, oTable) {
    bootbox.dialog({
        "message": "你确认要删除这条记录？",
        "title": "删除",
        //"class": "btn-danger",
        "callback": function () {
            //
        },
        "buttons": {
            confirm: {
                "label": "确定",
                "className": "btn-danger",
                "callback": function () {
                    $.ajax({
                        type: "POST",
                        url: actionUrl,
                        beforeSend: function () {
                            //
                        },
                        success: function (result) {
                            bootbox.alert(result.Message);
                            if (result.ResultType == 0) {
                                ReloadDataTable(oTable);
                            }
                        },
                        error: function () {
                            //
                        },
                        complete: function () {
                            //
                        }
                    });
                }
            },
            cancel: {
                "label": "取消",
                "className": "btn-default"
            }
        }
    });
}

/* 通过Ajax获取页面片段 */
function AjaxHtml(url, paras, success, error) {
    $.ajax({
        type: "Get",
        url: url,
        cache: false,
        data: paras,
        dataType: "html",
        success: success,
        error: error
    });
}

function AjaxPost(url, paras, success, error) {
    $.ajax({
        type: "Post",
        url: url,
        async: true,
        data: paras,
        cache: false,
        //contentType:"application/json",
        dataType: "json",
        success: success,
        error: function () {
            if (typeof error == "function") {
                error();
            }
        }
    });
}


function AjaxGet(url, paras, success, error) {
    $.ajax({
        type: "Get",
        url: url,
        async: true,
        data: paras,
        cache: false,
        dataType: "json",
        success: success,
        error: function (e,h) {
            if (typeof error == "function") {
                error(e);
            }
        }
    });
}

/*******保单提交*********/
function SubmitInsurance(actionUrl, param, oTable) {
    bootbox.dialog({
        "message": "你确认要提交这条记录？",
        "title": "提交",
        // "className": "btn-danger",
        "callback": function () {
            //
        },
        "buttons": {
            confirm: {
                "label": "确定",
                "className": "btn-danger",
                "callback": function () {
                    $.ajax({
                        type: "POST",
                        url: actionUrl,
                        data: param,
                        beforeSend: function () {
                            //
                        },
                        success: function (result) {
                            bootbox.alert(result.Message);
                            if (result.ResultType == 0) {
                                ReloadDataTable(oTable);
                            }
                        },
                        error: function () {
                            //
                        },
                        complete: function () {
                            //
                        }
                    });
                }
            },
            cancel: {
                "label": "取消",
                "className": "btn-default"
            }

        }
    });
}

function Export2Excel(path, did) {
    var currentdNode = Qrms.currentdNode;
    if (!currentdNode) {
        bootbox.alert('请先选择设备！');
        return;

    };
    var id = currentdNode.id;

    filterdata = "&deviceId=" + id;
    var url = path + "?" + filterdata;
    window.open(url);

} 

function Export2ExcelTem(path, did) {
    
    var id = 0;

    filterdata = "&deviceId=" + id;
    var url = path + "?" + filterdata;
    window.open(url);

}

function ExpandCondition(t) {
    var $this = $(t);
    var openTxt = "展开全部", closeTxt = "隐藏部分";
    var txt = $this.children(".title").text();
    if (txt === openTxt) {
        $(".exCondition").show(100);
        $this.children(".title").text(closeTxt);
        $this.children(".glyphicon").removeClass("glyphicon-menu-down");
        $this.children(".glyphicon").addClass("glyphicon-menu-up");
        
    }
    if (txt === closeTxt) {
        $(".exCondition").hide(100);
        $this.children(".title").text(openTxt);
        $this.children(".glyphicon").removeClass("glyphicon-menu-up");
        $this.children(".glyphicon").addClass("glyphicon-menu-down");

    }

}

function showLocale(objD) {
    var str, colorhead, colorfoot;
    var yy = objD.getYear();
    if (yy < 1900) yy = yy + 1900;
    var MM = objD.getMonth() + 1;
    if (MM < 10) MM = '0' + MM;
    var dd = objD.getDate();
    if (dd < 10) dd = '0' + dd;
    var hh = objD.getHours();
    if (hh < 10) hh = '0' + hh;
    var mm = objD.getMinutes();
    if (mm < 10) mm = '0' + mm;
    var ss = objD.getSeconds();
    if (ss < 10) ss = '0' + ss;
    var ww = objD.getDay();
    if (ww == 0) colorhead = "<font color=\"#FF0000\">";
    if (ww > 0 && ww < 6) colorhead = "<font color=\"#373737\">";
    if (ww == 6) colorhead = "<font color=\"#008000\">";
    if (ww == 0) ww = "星期日";
    if (ww == 1) ww = "星期一";
    if (ww == 2) ww = "星期二";
    if (ww == 3) ww = "星期三";
    if (ww == 4) ww = "星期四";
    if (ww == 5) ww = "星期五";
    if (ww == 6) ww = "星期六";
    colorfoot = "</font>";
    str = colorhead + yy + "年" + MM + "月" + dd + "日" + hh + ":" + mm + ":" + ss + " " + ww + colorfoot;
    return (str);
}


function showWeather(city) {
    var url = "http://api.map.baidu.com/telematics/v3/weather?location="+city+"&output=json&ak=Xs55VjEMSH6PotH80dFtk6moGbdq9AHG&callback=?";
    $.getJSON(url, function (data) {
        var winfo = "<label>";
        if (data.status === "success") {
            var wd = data.results[0].weather_data[0];
            
            winfo += wd.date;
            winfo += "  ";
            winfo += wd.weather;
            winfo += "  ";
            winfo += wd.wind;
            winfo += "  ";
            winfo += wd.temperature;
            winfo += "</label>";
            winfo += "<img style='margin-left: 1em;' src='";
            winfo += wd.dayPictureUrl;
            winfo += "'>";
        }
        $("#currentWeather").html(winfo);
    });
}

function enableButton(btn) {
    if(typeof btn=="string")
    {
        btn = $("#" + btn);
    }
    btn.removeClass("btn-default");
    btn.addClass("btn-primary");
    btn.removeAttr("disabled");
}

function disableButton(btn) {
    if (typeof btn == "string") {
        btn = $("#" + btn);
    }
    btn.removeClass("btn-primary");
    btn.addClass("btn-default");
    btn.attr("disabled","disabled");
}

function enableLabel(lbl) {
    if (typeof lbl == "string") {
        lbl = $("#" + lbl);
    }
    lbl.removeClass("label-default");
    lbl.addClass("label-success");
}

function disableLabel(lbl) {
    if (typeof lbl == "string") {
        lbl = $("#" + lbl);
    }
    lbl.removeClass("label-success");
    lbl.addClass("label-default");
}

function stringToBytes(str) {

    var ch, st, re = []; 
    for (var i = 0; i < str.length; i++ ) { 
        ch = str.charCodeAt(i);  // get char  
        st = [];                 // set up "stack"  

        do {  
            st.push( ch & 0xFF );  // push byte to stack  
            ch = ch >> 8;          // shift value down by 1 byte  
        }    

        while ( ch );  
        // add stack contents to result  
        // done because chars have "wrong" endianness  
        re = re.concat( st.reverse() ); 
    }  
    // return an array of bytes  
    return re;  
}

function stringToBytes(str) {

    var ch, st, re = [];
    for (var i = 0; i < str.length; i++) {
        ch = str.charCodeAt(i);  // get char  
        st = [];                 // set up "stack"  

        do {
            st.push(ch & 0xFF);  // push byte to stack  
            ch = ch >> 8;          // shift value down by 1 byte  
        }

        while (ch);
        // add stack contents to result  
        // done because chars have "wrong" endianness  
        re = re.concat(st.reverse());
    }
    // return an array of bytes  
    return re;
}

var _base64code = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/".split("");

function base64ToBytes(str) {
    var bitString = "";
    var tail = 0;
    for (var i = 0; i < str.length; i++) {
        if (str[i] != "=") {
            var decode = _base64code.indexOf(str[i]).toString(2);
            bitString += (new Array(7 - decode.length)).join("0") + decode;
        } else {
            tail++;
        }
    }
    return bitString.substr(0, bitString.length - tail * 2);
}

var Utility = {
    HiByte: function (nValue) {
        return (nValue >> 8);
    },

    Low4Bit: function (nValue) {
        return (nValue & 0x0F);
    },

    Hi4Bit: function (nValue) {
        return (nValue >> 4);
    },

    GetIntegerSomeBit: function (resource, mask) {
        return resource >> mask & 1;
    }
};

(function(win) {

        var Qrms = win.Qrms = win.Qrms || {};
        if (win.QR == undefined) win.QR = Qrms;

        Qrms.DeviceObjs = [];
        Qrms.DeviceMapObjs = [];
        Qrms.Map = {};

        Qrms.currentParaData = null;

        Qrms.Notice=function(title,msg,type){
            $.notify({
                icon: 'glyphicon glyphicon-bell',
                title: title,
                message: msg
            },{
                // settings
                element: 'body',
                position: null,
                type: type,
                allow_dismiss: true,
                newest_on_top: false,
                showProgressbar: false,
                placement: {
                    from: "bottom",
                    align: "right"
                },
                offset: 20,
                spacing: 10,
                z_index: 1031,
                delay: 10000,
                timer: 1000,
                url_target: '_blank',
                mouse_over: null,
                animate: {
                    enter: 'animated fadeInDown',
                    exit: 'animated fadeOutUp'
                },
                icon_type: 'class'
	
            });
        }

        Qrms.cPageFunction = "";
        Qrms.cPageFunctions = {
            systemstate: 1,
            timeperiodsetting: 2,
            modesetting: 3,
            valvesetting: 4,
            temperaturesetting: 5,
            terminalsetting: 6
        };

        Qrms.ValveControlChannel = {
            "未知":0,
            "A通路": 1,
            "B通路": 2,
            "A和B通路": 3
        };
       
        Qrms.CheckValveState=
        {
            "无校阀": 0,
            "一号校阀": 1,
            "二号校阀": 2
        }

        Qrms.WorkStatus=
        {
            "假日模式": 0, "工作模式": 1
        }


        Qrms.GetEnumPropName = function(e, v) {
            var ee = e || {};
            for (var i in ee) {
                if (ee.hasOwnProperty(i)) {
                    var v1 = ee[i];
                    if (v1 === v) return i;
                }
            }
            return "";
        };



        Qrms.updatePageData = null;
    }
)(window);