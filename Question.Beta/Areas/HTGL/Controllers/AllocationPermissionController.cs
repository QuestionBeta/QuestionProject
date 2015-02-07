using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Question.Beta.Controllers;
using WebUIFunction;
using DataBase.AppData;
using Question.Beta.Areas.HTGL.Models;

namespace Question.Beta.Areas.HTGL.Controllers
{
    /// <summary>
    /// 分配权限配置类
    /// </summary>
    public class AllocationPermissionController : BaseController
    {
        //
        // GET: /HTGL/AllocationPermission/

        protected SysRole_PermissionFun rolepermissionhandler = null;
        public AllocationPermissionController()
        {
            rolepermissionhandler = new SysRole_PermissionFun();
        }

        //获取指定角色下的权限列表
        public ActionResult PermissionList(int? id)
        {
            //var permissionList = ro
            //获取所有权限列表
            SysPermissionFun permission = new SysPermissionFun();
            List<SysPermission> permissionList = permission.GetDataList();
            //根据角色Id获取关联权限
            List<SysRole_Permission> rolepermissionList = rolepermissionhandler.GetPermissionListByRole(id);
            ViewBag.Role = id;
            return View("PermissionListControl", new RolePermissionModel { Role_PermissionList = rolepermissionList, PermissionList = permissionList });
        }

        [HttpPost]
        //保存权限
        public ActionResult SavePermission(string chkdata, int? id)
        {
            string[] array = chkdata.TrimEnd(',').Split(',');
            if (array != null && array.Length > 0)
            {
               rolepermissionhandler.DeletePermissionRoleByRole(id);
               foreach (var i in array)
               {
                   bool flag = this.rolepermissionhandler.IsExist(id, int.Parse(i));
                   if (!flag)
                   {
                       SysRole_Permission permission = new SysRole_Permission();
                       permission.role_id = (int)id;
                       permission.permission_id = int.Parse(i);
                       int result = this.rolepermissionhandler.InsertDataToSysRole_PermissionTable(permission);
                   }
               }
            }
            
            return Content("0");
        }

        //判断权限是否已分配，如果分配则跳过，如果没有则新增。
    }
}
