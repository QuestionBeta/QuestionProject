using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.DTModel.Admin;

namespace Shop.Web.Models
{
    /// <summary>
    /// 异常处理
    /// </summary>
    public class ErrorAttribute:ActionFilterAttribute, IExceptionFilter 
    {
        /// <summary>
        /// 异常
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnException(ExceptionContext filterContext)
        {
            //获取异常信息，入库保存
            Exception Error = filterContext.Exception;
            string Message = Error.Message;//错误信息
            string Url = HttpContext.Current.Request.RawUrl;//错误发生地址
            DTError error = new DTError { Message = Message, Url = Url };         
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Session["error"] = error;
            filterContext.HttpContext.Session.Timeout = 20;
            filterContext.Result = new RedirectResult("/Error/Show/");//跳转至错误提示页面
        } 
    }
}