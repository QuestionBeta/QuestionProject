using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Script.Serialization;
using Shop.DTModel.Admin;
using WebUIFunction;
using DataBase.AppData;
using System.Web.Routing;
using System.Configuration;

namespace Shop.Web.Models
{
    /// <summary>
    /// 身份验证类
    /// </summary>
    public class AuthAttribute : ActionFilterAttribute,IAuthorizationFilter
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 实现OnAuthorization方法  验证用户是否已登陆
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
             //如果存在身份信息
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //filterContext.HttpContext.Response.Redirect("http://www.baidu.com", false);
            }
        }

        /// <summary>
        /// 验证权限（action执行前会先执行这里）
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //如果存在身份信息
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                ContentResult Content = new ContentResult();
                //Content.Content = string.Format("<script type='text/javascript'>alert('请先登录！');window.location.href='{0}';</script>", FormsAuthentication.LoginUrl);
                //filterContext.HttpContext.Response.Redirect("/Error/Show", true);
                //获取后台根目录
                string path = ConfigurationManager.AppSettings["BootPath"].ToString();
                //错误发生地址
                string Url = HttpContext.Current.Request.RawUrl;
                string redirecUrl = "/Error/Show/" + HttpContext.Current.Request.UrlReferrer;
                if (Url.Contains(path))
                {
                   redirecUrl = HttpContext.Current.Request.ApplicationPath + redirecUrl;
                   //HttpContext.Current.Response.Redirect(redirecUrl);
                }
                DTError error = new DTError { Message = "对不起，您没有权限访问该页面", Url = filterContext.HttpContext.Request.RawUrl };
                //filterContext.HttpContext.ExceptionHandled = true;
                filterContext.HttpContext.Session["error"] = error;
                filterContext.HttpContext.Session.Timeout = 20;
                filterContext.Result = new RedirectResult(redirecUrl);
            }
            else
            {
                //获取所有角色
                SysRoleFun role = new SysRoleFun();
                List<SysRole> roleList = role.GetDataList();
                string[] Role = null;
                Role = (string[])roleList.Select(p => p.jb.ToString().Trim()).ToArray();
                //if (HttpContext.User.IsInRole("1"))
                //{

                //}
                string codeA = GetUser().Trim();
                if (Code.ToLower() != "all")
                {
                    if (!Role.Contains(Code) || Code != codeA) //验证权限
                    {
                        //验证不通过
                        ContentResult Content = new ContentResult();
                        DTError error = new DTError { Message = "对不起，您没有权限访问该页面", Url = filterContext.HttpContext.Request.RawUrl };
                        //filterContext.HttpContext.ExceptionHandled = true;
                        filterContext.HttpContext.Session["error"] = error;
                        filterContext.HttpContext.Session.Timeout = 20;
                        filterContext.Result = new RedirectResult("/Error/Show");
                        if (filterContext.HttpContext.Request.RawUrl.Contains("/htgl"))
                        {
                            filterContext.Result = new RedirectResult("/HTGL/Error/Show");
                        }
                        else
                        {
                            filterContext.Result = new RedirectResult("/Error/Show");
                        }
                    }
                    else
                    {

                    }
                }
            }
        }

        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <returns></returns>
        private string GetUser()
        {
            if (HttpContext.Current.Request.IsAuthenticated)//是否通过身份验证
            {
                HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];//获取cookie
                FormsAuthenticationTicket Ticket = FormsAuthentication.Decrypt(authCookie.Value);//解密
                //JavaScriptSerializer serializer = new JavaScriptSerializer();
                return Ticket.UserData;//反序列化
            }

            return null;
        }
    }
}