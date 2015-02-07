(function ($) {
    $.dialog = {
        url: 'http://www.baidu.com',
        height: 'auto',
        width: 'auto',
        top: '100px',
        left: '500px',
        iconCls: 'add',
        mainFrame: '',
        subForm: '',
        alert: function (title, message,  ishowbtn, callback) {
            if (title == '' || title == null) {
                title = "标题"
            }
            $.dialog._show(title, message, null, 'alert', ishowbtn,
			function (result) {
			    if (callback) callback(result)
			});
        }, confirm: function (title, message, ishowbtn, callback) {
            if (title == '' || title == null) {
                title = "标题"
            }
            $.dialog._show(title, message, null, 'confirm',ishowbtn,
			function (result) {
			    if (callback) callback(result)
			})
        },
        window: function (title, message, value, ishowbtn, callback) {
            if (title == '' || title == null) {
                title = "提示窗口"
            }

            $.dialog._show(title, message, value, 'window',ishowbtn,
			function (result) {
			    if (callback) callback(result)
			});
        },
        _show: function (title, msg, value, type, ishowbtn, callback) {
            
            if (ishowbtn == null) {
                ishowbtn = false;
            }
            var id = null;
            //$("body").append('<div id="popup_container" style="width: ' + $.dialog.width + ';height:' + $.dialog.height + ';top:' + $.dialog.top + ';left:' + $.dialog.left + '; margin: auto; overflow: hidden; position: absolute; left: 300px; z-index: 100000; border: 1px solid rgb(234, 234, 234); background: none repeat scroll 0% 0% rgb(255, 255, 255);">' + '<h1 id="popup_title" class="popup_title"><div style="float:left;color:#eaeaea;width:16px;height:16px; margin: 10px 5px;" class="' + $.dialog.iconCls + '"></div><div class="popup_title_c" style="margin: auto; width: 90%;cursor:move;">' + title + '</div><div style="float:right;position: absolute;right: 0px;top:0px;padding:0px 8px;cursor:pointer"><span onclick="$.dialog._hide();">X</span></div></h1>' + '<div id="popup_content" style="height:75%;">' + msg + '</div>' + '<div id="popup_message"></div>' + '</div>');
            var max = 0;
            switch (type) {
                case 'alert':
                    id = "popup_close_alert";
                    $.dialog._overlay('show', "overflow_alert");
                    max = $.dialog._getCurrentZIndex();
                    $("body").append('<div id="popup_container_alert" class="modal-content animation" style="width: ' + $.dialog.width + ';height:' + $.dialog.height + ';top:' + $.dialog.top + ';left:' + $.dialog.left + '; margin: auto; overflow: hidden; position: absolute;_position:absolute; z-index: ' + max + '; border: 1px solid rgb(234, 234, 234); background: none repeat scroll 0% 0% rgb(255, 255, 255);min-width:300px;">' + '<div  class="popup_title modal-header" style="padding: 0 0 10px; "><div style="float:left;color:#eaeaea;width:16px;height:16px; margin: 10px 5px;" class="' + $.dialog.iconCls + '"></div><div class="popup_title_c" style="margin: auto; width: 90%;cursor:move;">' + title + '<span onclick="$.dialog._hide(\'popup_container_alert\', \'overflow_alert\')" style="float:right">X</span></div></div>' + '<div id="popup_content_alert" class="modal-body" style="height:75%;" style="padding: 5px 0 15px 5px;">' + msg + '</div>' + '<div id="popup_message_alert"></div>' + '</div>');
                    if (ishowbtn) {
                        $("#popup_message_alert").after('<div id="popup_panel_alert" class="modal-footer"><input type="button" value="确定" id="popup_close_alert" class="btn btn-danger"/></div>');
                    }
                    break;
                case 'confirm':
                    id = "popup_ok_confirm";
                    $.dialog._overlay('show', "overflow_confirm");
                    max = $.dialog._getCurrentZIndex();
                    $("body").append('<div id="popup_container_confirm" class="modal-content animation" style="width: ' + $.dialog.width + ';height:' + $.dialog.height + ';top:' + $.dialog.top + ';left:' + $.dialog.left + '; margin: auto; overflow: hidden; position: absolute; _position:absolute; z-index: ' + max + '; border: 1px solid rgb(234, 234, 234); background: none repeat scroll 0% 0% rgb(255, 255, 255);min-width:300px;">' + '<div class="popup_title modal-header" style="padding: 0 0 10px; "><div style="float:left;color:#eaeaea;width:16px;height:16px; margin: 10px 5px;" class="' + $.dialog.iconCls + '"></div><div class="popup_title_c" style="margin: auto; width: 90%;cursor:move;">' + title + '<span onclick="$.dialog._hide(\'popup_container_confirm\', \'overflow_confirm\')" style="float:right">X</span></div></div>' + '<div id="popup_content_confirm" class="modal-body" style="height:75%;">' + msg + '</div>' + '<div id="popup_message_confirm"></div>' + '</div>');
                    if (ishowbtn) {
                        $("#popup_message_confirm").after('<div id="popup_panel_confirm" class="modal-footer"><input type="button" value="确定" class="btn btn-default" id="popup_ok_confirm" /><input type="button" value="取消" class="btn btn-danger" id="popup_cancel_confirm" /></div>');
                    }
                    break;
                case 'window':
                    id = "popup_ok_window";
                    $.dialog._overlay('show', "overflow_window");
                    max = $.dialog._getCurrentZIndex();
                    $("body").append('<div id="popup_container_window" class="modal-content animation" style="width: ' + $.dialog.width + ';height:' + $.dialog.height + ';top:' + $.dialog.top + ';left:' + $.dialog.left + '; margin: auto;min-width:300px; overflow: hidden; position: absolute;_position:absolute;  z-index: ' + max + '; border: 1px solid rgb(234, 234, 234); background: none repeat scroll 0% 0% rgb(255, 255, 255);">' + '<div  class="popup_title modal-header" style="padding: 0 0 10px;"><div style="float:left;color:#eaeaea;width:16px;height:16px; margin: 10px 5px;" class="' + $.dialog.iconCls + '"></div><div class="popup_title_c" style="margin: auto; width: 90%;cursor:move;">' + title + '<span onclick="$.dialog._hide(\'popup_container_window\', \'overflow_window\')" style="float:right">X</span></div></div>' + '<div id="popup_content_window" class="modal-body" style="height:75%;">' + msg + '</div>' + '<div id="popup_message_window"></div>' + '</div>');
                    $('#popup_content_window').html('');

                    var inhtml = '<div class="a_msg" id="msg_20134010" style="margin: auto;font-size:16px; top:300;left:350;right:300;z-index:1001; color:#000; overflow: hidden;  left: 300px;">数据加载中...</div>';
                    var domV = document.getElementById("msg_20134010");
                    if (domV == null) {
                        $('#popup_content_window').append(inhtml);
                    }
                    switch (value) {
                        case '0':
                            $('#popup_content_window').load(msg, null, function (response, status, xhr) {
                                if (xhr.readyState == 4) {
                                    //加载完成
                                    $('#msg_20134010').html('数据加载完成...');
                                    //1秒后关闭提示
                                    setTimeout(function () {
                                        $('#msg_20134010').html('');
                                        $('body').find('div').remove("#msg_20134010");
                                    }, 1000);
                                }
                            });
                            break;
                        case '1':
                            $('body').find('div').remove("#msg_20134010");
                            $('#popup_content_window').append('<div style="width:auto"><iframe scrolling="no" style="border:0px;width:100%;height:100%;overflow:hidden" id="main_frame_01" src="' + msg + '"></iframe></div>');
                            //自动控制Iframe高度和宽度
                            $("#main_frame_01").load(function () {
                                $(this).height(0); //用于每次刷新时控制IFRAME高度初始化
                                var height = $(this).contents().height() + 10;
                                var width = $(this).contents().width() + 50;

                                $(this).height(height < 100 ? 100 : height);
                                $(this).width(width < 300 ? 300 : width);
                            });
                            break;
                        default:
                            $('body').find('div').remove("#msg_20134010");
                            $('#popup_content_window').append(msg);
                            break
                    }
                    if (ishowbtn) {
                        $("#popup_message_window").after('<div id="popup_panel_window" class="modal-footer"><input type="button" value="保存" id="popup_ok_window" class="btn btn-primary"/><input type="button" value="关闭" id="popup_cancel_window" class="btn btn-danger"/></div>');
                    }
                    break;
            }
            //关闭对话框
            $("#popup_close_alert").click(function () {
                $.dialog._hide("popup_container_alert", "overflow_alert");
            });
            //关闭确认框
            $("#popup_cancel_confirm").click(function () {
                $.dialog._hide("popup_container_confirm", "overflow_confirm");
            });
            //关闭窗口
            $("#popup_cancel_window").click(function () {
                $.dialog._hide("popup_container_window", "overflow_window");
            });

            $("#" + id).click(function () {
                callback(true);
            });
        },
        _hide: function (id, overflwId) {
            $("#" + id).remove();
            $.dialog._overlay('hide', overflwId);
        },
        _overlay: function (status, id) {
            var zindex = 90000;
            var a_alert_Zindex = 0;
            var a_confirm_Zindex = 0;
            var a_window_Zindex = 0;
            //弹框
            var alert_w = document.getElementById("popup_container_alert");
            if (alert_w != null) {
                a_alert_Zindex = $("#popup_container_alert").css("z-index");
            }
            //确认框
            var confirm_w = document.getElementById("popup_container_confirm");
            if (confirm_w != null) {
                a_confirm_Zindex = $("#popup_container_confirm").css("z-index");
            }
            //窗口
            var window_w = document.getElementById("popup_container_window");
            if (window_w != null) {
                a_window_Zindex = $("#popup_container_window").css("z-index");
            }
            //获取当前最大的遮罩层
            var max = $.dialog._compareTo(a_alert_Zindex + "," + a_confirm_Zindex + "," + a_window_Zindex);

            if (max != 0) {
                zindex = max + 1;
            }

            //当前对象
            var c_obj_w = document.getElementById(id);
            if (alert_w != null) {
                zindex += 10000;
            }

            switch (status) {
                case 'show':
                    $.dialog._overlay('hide');
                    //不包含此元素
                    if (c_obj_w == null) {
                        $("BODY").append('<div id="' + id + '" ></div>');
                    }
                    $("#" + id).css({
                        position: 'fixed',
                        _position: 'fixed',
                        zIndex: zindex,
                        top: '0px',
                        left: '0px',
                        bottom: '0px',
                        right: '0px',
                        background: 'none repeat scroll 0% 0% rgba(0, 0, 0,0.6)',
                        opacity: '0.5'
                    });
                    break;
                case 'hide':
                    $("#" + id).remove();
                    break
            }
        },
        _compareTo: function (arr) {
            var arrr = new Array();
            arrr = arr.split(',');
            var tem = 0;
            //获取数组中最大的数字
            return Math.max.apply(null, arrr);
        },
        _getCurrentZIndex: function () {
            var a_alert_Zindex = 0;
            var a_confirm_Zindex = 0;
            var a_window_Zindex = 0;
            //弹框
            var alert_w = document.getElementById("overflow_alert");
            if (alert_w != null) {
                a_alert_Zindex = $("#overflow_alert").css("z-index");
            }
            //确认框
            var confirm_w = document.getElementById("overflow_confirm");
            if (confirm_w != null) {
                a_confirm_Zindex = $("#overflow_confirm").css("z-index");
            }
            //窗口
            var window_w = document.getElementById("overflow_window");
            if (window_w != null) {
                a_window_Zindex = $("#overflow_window").css("z-index");
            }
            //获取当前最大的遮罩层
            return $.dialog._compareTo(a_alert_Zindex + "," + a_confirm_Zindex + "," + a_window_Zindex);
        }
    };
    var Dragable = function (validateHandler) {
        var draggingObj = null;
        var diffX = 0;
        var diffY = 0;
        var clientWidth = document.body.clientWidth;
        var b = document.body.clientHeight;
        var topV,
		leftV,
		oDiv;
        var h = 0;
        var w = 0;
        function setHtmlStyle() { }
        function mouseHandler(e) {
            switch (e.type) {
                case 'mousedown':
                    draggingObj = validateHandler(e);
                    if (draggingObj != null) {
                        topV = draggingObj.style.top.replace('px', '');
                        leftV = draggingObj.style.left.replace('px', '');
                        w = draggingObj.style.width.replace('px', '');
                        h = draggingObj.style.height.replace('px', '');
                        setHtmlStyle();
                        diffX = e.clientX - leftV;
                        diffY = e.clientY - topV
                    }
                    break;
                case 'mousemove':
                    if (draggingObj) {
                        var l = e.clientX - diffX;
                        var t = e.clientY - diffY;
                        if (l < 0) {
                            l = 0
                        }
                        if (t < 0) {
                            t = 0
                        }
                        if (clientWidth - w - draggingObj.style.left.replace('px', '') < 0) {
                            l = clientWidth - w
                        }
                        if (b + h - draggingObj.style.top.replace('px', '') < -1) {
                            t = b + h
                        }
                        draggingObj.style.left = l + 'px';
                        draggingObj.style.top = t + 'px'
                    }
                    break;
                case 'mouseup':
                    draggingObj = null;
                    break
            }
        };
        return {
            enable: function () {
                document.addEventListener('mousedown', mouseHandler);
                document.addEventListener('mousemove', mouseHandler);
                document.addEventListener('mouseup', mouseHandler)
            },
            disable: function () {
                document.removeEventListener('mousedown', mouseHandler);
                document.removeEventListener('mousemove', mouseHandler);
                document.removeEventListener('mouseup', mouseHandler);
            }
        }
    };
    function getDraggingDialog(e) {
        var target = e.target;
        while (target && target.className.indexOf('popup_title_c') == -1) {
            target = target.offsetParent
        }
        if (target != null) {
            return target.offsetParent;
        } else {
            return null
        }
    }
    Dragable(getDraggingDialog).enable()
})(jQuery)