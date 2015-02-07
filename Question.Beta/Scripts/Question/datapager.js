/*
 *File Created: 九月 7, 2014 
 *PageSplit JavaScript Method
 */
 (function ($) {
     $.extend({
         changePage: function (index, size, type, url, id, extandparam) {
             var mmsg = document.getElementById("alertmsg123");
             if (mmsg == null) {
                 $(id).append("<div id=\"alertmsg123\" style=\"position:absolute;_position:absolute;z-index:5000;width:100%;height:100%;text-align:center;background:url('/Content/layout/css/icons/loading.png') no-repeat scroll center center #fff; opacity:0.5;padding:0 0;text-indent:120px;line-height:30px;top:35px;\">数据加载中...</div>");
             }
             //$(id).html('');
             //alert(url + "?page=" + index + "&size=" + size + "&type=" + type + extandparam);
            // $(id).load(url + "?page=" + index + "&size=" + size + "&type=" + type + extandparam);
             $(id).load(url + "?page=" + index + "&size=" + size + "&type=" + type + extandparam, null, function (response, status, xhr) {
                 //alert(status);
                 if (status == "success") {
                     //加载完成
                     $('#alertmsg123').html('数据加载完成...');

                     //1秒后关闭提示
                     setTimeout(function () {
                         $(id).find('#alertmsg123').remove();
                     }, 100);
                 }
                 if (status == "error") {
                     $('#alertmsg123').html("Error: " + xhr.status + ": " + xhr.statusText);
                 }
             });
         }
     });
 })(jQuery);