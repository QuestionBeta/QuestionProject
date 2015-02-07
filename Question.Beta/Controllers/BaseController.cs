using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUIFunction;
using Shop.Web.Models;
using DataBase.AppData;
using System.Configuration;
using Shop.Web.Helper;

namespace Question.Beta.Controllers
{
    [ErrorAttribute]
    public class BaseController : Controller
    {
        protected XFX_NavigationFun navigation = null;
        protected UserFun userhandler = null;
        protected XFX_BugFun bughandler = null;
        protected CategoryFun categoryhandler = null;
        protected MenuFun menuhandler = null;
        protected SysRoleFun rolehandler = null;
        protected string CurrentUserRole = string.Empty;
        protected string PathUrl = string.Empty;
        public BaseController()
        {
            navigation = new XFX_NavigationFun();
            userhandler = new UserFun();
            bughandler = new XFX_BugFun();
            categoryhandler = new CategoryFun();
            menuhandler = new MenuFun();
            rolehandler = new SysRoleFun();
            PathUrl = ReturnPathUrlName();
            ViewBag.Url = PathUrl;
        }

        /// <summary>
        /// 获取当前用户角色
        /// </summary>
        protected string GetCurrentUserRole(string user_name)
        {
            int uid = GetCurrentUserId(user_name);
            SysUser_RoleFun user_roleFun = new SysUser_RoleFun();
            List<SysUser_Role> user_role = user_roleFun.GetRoleListByUser(uid);
            if(user_role != null && user_role.Count > 0){
                CurrentUserRole = user_role.FirstOrDefault().SysRole.jb;
            }

            return CurrentUserRole;
        }

        /// <summary>
        /// 获取用户id
        /// </summary>
        /// <param name="user_name"></param>
        /// <returns></returns>
        protected int GetCurrentUserId(string user_name)
        {
            //根据用户id获取用户角色级别
            int uid = 0;
            var user = userhandler.GetSingleByName(user_name);
            if (user != null)
            {
                uid = user.id;
            }

            return uid;
        }

        public string ReturnPathUrlName()
        {
            //获取webconfig
            string path = ConfigurationManager.AppSettings["BootPath"].ToString();
            return path;
        }

        /// <summary>
        /// 获取默认页数
        /// </summary>
        /// <returns></returns>
        public string GetDefaultPage()
        {
            return ConfigurationManager.AppSettings["DefaultPage"].ToString();
        }

        /// <summary>
        /// 获取默认显示条数
        /// </summary>
        /// <returns></returns>
        public string GetDefaultSize()
        {
            return ConfigurationManager.AppSettings["DefaultSize"].ToString();
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="str">目标字符串</param>
        /// <returns>源字符串</returns>
        public string GetDeCodeStr(string id)
        {
            //判断参数为空
            if (string.IsNullOrEmpty(id))
            {
                return "请选择行";
            }
            //解密参数，根据参数删除记录
            id = MD5Helper.Decrypt(id);
            return id;
        }
    }
}
