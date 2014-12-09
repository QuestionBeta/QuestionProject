/*
 *File Created: 九月 7, 2014 
 *PageSplit JavaScript Method
 */
$.extend({
    changePage: function (index, size, type, url, id, extandparam) {
        $(id).html('');
        $(id).load(url + "?page=" + index + "&size=" + size + "&type=" + type + extandparam, null, function (response, status, xhr) {
            var ovl = document.getElementById("ovf0010");
            var mmsg = document.getElementById("msg_20134010");
            if (ovl == null) {
                $("BODY").append('<div id="ovf0010" style="z-index:1001" ></div>');
            } else {
                $('#ovf0010').css({ display: "" });
            }
            $("#ovf0010").css({
                position: 'fixed',
                zIndex: 1000,
                top: '0px',
                left: '0px',
                bottom: '0px',
                right: '0px',
                background: 'none repeat scroll 0% 0% rgba(0, 0, 0,0.6)',
                opacity: '0.5'
            });
            if (mmsg == null) {
                $('BODY').append('<div class="a_msg" id="msg_20134010" style="margin: auto;font-size:20px; top:0;left:0;right:0;z-index:1001; color:#fff; overflow: hidden; position: absolute; left: 300px;">数据加载中...</div>');
            } else {
                $('#msg_20134010').html('数据加载中...');
            }

            if (xhr.readyState == 4) {
                //加载完成
                $('#msg_20134010').html('数据加载完成...');
                //1秒后关闭提示
//                setTimeout(function () {
//                    $('#msg_20134010').html('');
//                    $('#ovf0010').css({ display: "none" });
                //                }, 500);
                $('#msg_20134010').html('');
                $('#ovf0010').css({ display: "none" });
            }
            //$('body').find('.a_msg').remove();
        });
    }
});