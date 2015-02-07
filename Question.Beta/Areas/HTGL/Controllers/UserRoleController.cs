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
    public class UserRoleController : BaseController
    {
        //
        // GET: /HTGL/UserRole/
        protected SysUser_RoleFun sysUser_RoleFunHandler = null;
        /// <summary>
        /// 自定义构造函数，初始化处理程序
        /// </summary>
        public UserRoleController()
        {
            sysUser_RoleFunHandler = new SysUser_RoleFun();
        }

        //获取用户角色,一个用户可能包含多个角色
        public ActionResult UserRoleList(int? id)
        {
            if (id == null)
            {
                id = 0;
            }
            ViewBag.User = id;
            //根据用户id获取角色
            List<SysUser_Role> userRoleList = sysUser_RoleFunHandler.GetRoleListByUser(id);
            //获取所有角色
            SysRoleFun roleFun = new SysRoleFun();
            List<SysRole> roleList = roleFun.GetDataList();
            var AllocationModel = new object[] { userRoleList, roleList };

            return View("RoleListControl", AllocationModel);
        }

        //保存角色分配信息
        [HttpPost]
        public ActionResult SaveData(int? id, string chkdata)
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
                    sysUser_RoleFunHandler.DeleteUserRoleByUser(id);
                }
                catch (Exception ex)
                {
                    result = ex.InnerException.Message;
                }

                List<SysUser_Role> list = new List<SysUser_Role>();
                foreach (var i in array)
                {
                    SysUser_Role user_role = new SysUser_Role();
                    user_role.user_id = (int)id;
                    user_role.role_id = int.Parse(i);
                    list.Add(user_role);
                }

                if (list != null && list.Count > 0)
                {
                    try
                    {
                        this.sysUser_RoleFunHandler.InsertDataToSysUser_RoleTable(list);
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
                else
                {
                    result = "未请至少选中一个角色";
                }
            }
            return Content(result);
        }
    }
}