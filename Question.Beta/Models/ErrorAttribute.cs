using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.DTModel.Admin;
using System.Configuration;
using System.Security.Policy;

namespace Shop.Web.Models
{    
    /// <summary>
    /// 异常处理
    /// </summary>
    public class ErrorAttribute : FilterAttribute,IExceptionFilter
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
            
            //获取后台根目录
            string path = ConfigurationManager.AppSettings["BootPath"].ToString();
            

            HttpException httpException = filterContext.Exception as HttpException;
            if (httpException != null)
            {
                //404错误
                if (httpException.GetHttpCode() == 404)
                {
                    //跳转到相应页面
                    Message = "页面找不到了";
                }
                //500错误
                else if (httpException.GetHttpCode() == 500)
                {
                    //跳转到相应页面
                    Message = "服务器内部错误";
                }                
            }

            string redirecUrl = "/Error/Show/";
            if (Url.Contains(path))
            {
                redirecUrl = path + redirecUrl;
            }
            DTError error = new DTError { Message = Message, Url = Url };
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Session["error"] = error;
            filterContext.HttpContext.Session.Timeout = 20;
            //跳转至错误提示页面
            filterContext.Result = new RedirectResult(redirecUrl);
        } 
    }
}