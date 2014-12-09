//*DTcms后台管理页JS函数，Jquery扩展
//*作者：一些事情
//*时间：2012年02月20日

//=============================切换验证码======================================
function ToggleCode(obj, codeurl) {
    $(obj).attr("src", codeurl + "?time=" + Math.random());
}

//表格隔行变色
$(function () {
    $(".msgtable tr:nth-child(odd)").addClass("tr_odd_bg"); //隔行变色
    $(".msgtable tr").hover(
			    function () {
			        $(this).addClass("tr_hover_col");
			    },
			    function () {
			        $(this).removeClass("tr_hover_col");
			    }
		    );
});
//==========================页面加载时JS函数结束===============================

//===========================系统管理JS函数开始================================

//Tab控制函数
function tabs(tabId, tabNum) {
    //设置点击后的切换样式
    $(tabId + " .tab_nav li").removeClass("selected");
    $(tabId + " .tab_nav li").eq(tabNum).addClass("selected");
    //根据参数决定显示内容
    $(tabId + " .tab_con").hide();
    $(tabId + " .tab_con").eq(tabNum).show();
}

//可以自动关闭的提示
function jsprint(msgtitle, url, msgcss, callback) {
    $("#msgprint").remove();
    var cssname = "";
    switch (msgcss) {
        case "Success":
            cssname = "pcent success";
            break;
        case "Error":
            cssname = "pcent error";
            break;
        default:
            cssname = "pcent warning";
            break;
    }
    var str = "<div id=\"msgprint\" class=\"" + cssname + "\">" + msgtitle + "</div>";
    $("body").append(str);
    $("#msgprint").show();
	var itemiframe = "#framecenter .l-tab-content .l-tab-content-item";
    var curriframe = "";
    $(itemiframe).each(function () {
        if ($(this).css("display") != "none") {
			curriframe = $(itemiframe).index($(this));
            return false;
        }
    });
    if (url == "back" && curriframe != "") {
        frames[curriframe].history.back(-1);
    } else if (url != "" && curriframe != "") {
        frames[curriframe].location.href = url;
    }
    //3秒后清除提示
    setTimeout(function () {
        $("#msgprint").fadeOut(500);
        //如果动画结束则删除节点
        if (!$("#msgprint").is(":animated")) {
            $("#msgprint").remove();
        }
    }, 3000);
    //执行回调函数
    if (typeof (callback) == "function") callback();
}

//全选取消按钮函数，调用样式如：
function checkAll(chkobj){
	if($(chkobj).find("span b").text()=="全选")
	{
	    $(chkobj).find("span b").text("取消");
		$(".checkall input").attr("checked", true);
	}else{
    $(chkobj).find("span b").text("全选");
		$(".checkall input").attr("checked", false);
	}
}

//执行回传函数
function ExePostBack(objId, objmsg) {
    if ($(".checkall input:checked").size() < 1) {
        $.ligerDialog.warn("对不起，请选中您要操作的记录！");
        return false;
    }
    var msg = "删除记录后不可恢复，您确定吗？";
    if (arguments.length == 2) {
        msg = objmsg;
    }
    $.ligerDialog.confirm(msg, "提示信息", function (result) {
        if (result) {
            __doPostBack(objId, '');
        }
    });
    return false;
}

//关闭提示窗口
function CloseTip(objId) {
    $("#" + objId).hide();
}

//===========================系统管理JS函数结束================================

//================上传文件JS函数开始，需和jquery.form.js一起使用===============
//文件上传
function Upload(action, repath, uppath, iswater, isthumbnail, filepath) {
    var sendUrl = "../../tools/upload_ajax.ashx?action=" + action + "&ReFilePath=" + repath + "&UpFilePath=" + uppath;
    //判断是否打水印
    if(arguments.length == 4){
        sendUrl = "../../tools/upload_ajax.ashx?action=" + action + "&ReFilePath=" + repath + "&UpFilePath=" + uppath + "&IsWater=" + iswater;
    }
    //判断是否生成宿略图
    if (arguments.length == 5) {
        sendUrl = "../../tools/upload_ajax.ashx?action=" + action + "&ReFilePath=" + repath + "&UpFilePath=" + uppath + "&IsWater=" + iswater + "&IsThumbnail=" + isthumbnail;
    }
    //自定义上传路径
    if (arguments.length == 6) {
        sendUrl = filepath + "tools/upload_ajax.ashx?action=" + action + "&ReFilePath=" + repath + "&UpFilePath=" + uppath + "&IsWater=" + iswater + "&IsThumbnail=" + isthumbnail;
    }
    //开始提交
    $("#form1").ajaxSubmit({
        beforeSubmit: function(formData, jqForm, options){
            //隐藏上传按钮
            $("#"+repath).nextAll(".files").eq(0).hide();
            //显示LOADING图片
            $("#"+repath).nextAll(".uploading").eq(0).show();
        },
        success: function(data, textStatus) {
            if (data.msg == 1) {
                $("#"+repath).val(data.msbox);
            } else {
                alert(data.msbox);
            }
            $("#"+repath).nextAll(".files").eq(0).show();
            $("#"+repath).nextAll(".uploading").eq(0).hide();
        },
        error: function(data, status, e) {
            alert("上传失败，错误信息：" + e);
            $("#"+repath).nextAll(".files").eq(0).show();
            $("#"+repath).nextAll(".uploading").eq(0).hide();
        },
        url: sendUrl,
        type: "post",
        dataType: "json",
        timeout: 600000
    });
};
//附件上传
function AttachUpload(repath, uppath) {
    var submitUrl = "../../tools/upload_ajax.ashx?action=AttachFile&UpFilePath=" + uppath;
    //开始提交
    $("#form1").ajaxSubmit({
        beforeSubmit: function (formData, jqForm, options) {
            //隐藏上传按钮
            $("#" + uppath).parent().hide();
            //显示LOADING图片
            $("#" + uppath).parent().nextAll(".uploading").eq(0).show();
        },
        success: function (data, textStatus) {
            if (data.msg == 1) {
                var listBox = $("#" + repath + " ul");
                var newLi = '<li>'
                + '<input name="hidFileName" type="hidden" value="0|' + data.mstitle + "|" + data.msbox + '" />'
                + '<b title="删除" onclick="DelAttachLi(this);"></b>附件：' + data.mstitle
                + '</li>';
                listBox.append(newLi);
                //alert(data.mstitle);
            } else {
                alert(data.msbox);
            }
            $("#" + uppath).parent().show();
            $("#" + uppath).parent().nextAll(".uploading").eq(0).hide();
        },
        error: function (data, status, e) {
            alert("上传失败，错误信息：" + e);
            $("#" + uppath).parent().show();
            $("#" + uppath).parent().nextAll(".uploading").eq(0).hide();
        },
        url: submitUrl,
        type: "post",
        dataType: "json",
        timeout: 600000
    });
};
//===========================上传文件JS函数结束================================