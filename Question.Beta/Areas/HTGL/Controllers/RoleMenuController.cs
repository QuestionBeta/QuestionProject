using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Question.Beta.Controllers;
using DataBase.AppData;
using WebUIFunction;

namespace Question.Beta.Areas.HTGL.Controllers
{
    public class RoleMenuController : BaseController
    {
        //
        // GET: /HTGL/RoleMenu/
        protected SysRoleMenuFun sysRoleMenuFunHandler = null;

        /// <summary>
        /// 初始化操作函数
        /// </summary>
        public RoleMenuController()
        {
            sysRoleMenuFunHandler = new SysRoleMenuFun();
        }

        //获取Menu列表
        public ActionResult List(int? id)
        {
            if (id == null)
            {
                id = 0;
            }
            //获取角色功能项列表
            List<SysRoleMenu> menuList = this.sysRoleMenuFunHandler.GetDataListByRole(id);
            //获取功能项列表
            List<Menu> listMenu = new List<Menu>();
            listMenu = menuhandler.GetDataList();
            ViewBag.Role = id;
            return View("MenuList", new object[] { menuList, listMenu });
        }

        //保存配置信息
        public ActionResult SaveData(string chkdata,int? id)
        {
            //存储操作结果
            int count = 0;
            //接收角色信息
            string[] array = chkdata.TrimEnd(',').Split(',');
            //存储返回信息
            string result = string.Empty;
            if (array != null && array.Length > 0)
            {
                try
                {
                    sysRoleMenuFunHandler.DeleteRoleMenuByRole(id);
                }
                catch (Exception ex)
                {
                    result = ex.InnerException.Message;
                }

                List<SysRoleMenu> list = new List<SysRoleMenu>();
                foreach (var i in array)
                {
                    SysRoleMenu user_role = new SysRoleMenu();
                    user_role.role_id = (int)id;
                    user_role.menu_id = int.Parse(i);
                    list.Add(user_role);
                }                               

                if (list != null && list.Count > 0)
                {
                    try
                    {
                        this.sysRoleMenuFunHandler.InsertDataToSysRoleMenuTable(list);
                        count++;
                    }
                    catch (Exception ex)
                    {
                        result = ex.InnerException.Message;
                    }
                }

                if (count > 0)
                {
                    result = "0";
                }
            }
            return Content(result);
        }
    }
}