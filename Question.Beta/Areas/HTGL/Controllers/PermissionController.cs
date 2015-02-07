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
    public class PermissionController : BaseController
    {
        //
        // GET: /HTGL/Permession/
        protected SysPermissionFun permissionhandler = null;
        public PermissionController()
        {
            permissionhandler = new SysPermissionFun();
        }

        public ActionResult PermissionList()
        {
            return View();
        }

        //获取权限列表
        public ActionResult GetData(int? page, int? rows)
        {
            page = page == null ? 1 : (int)page;
            rows = rows == null ? 10 : (int)rows;
            List<SysPermission> dt = new List<SysPermission>();
            var data = permissionhandler.GetDataList(page, rows).Select(p => new { p.id, p.name,p.order_num,p.add_time }).ToList();
            int total = permissionhandler.GetDataList().Count;
            var j = new { total = total, rows = data };
            return Json(j, JsonRequestBehavior.AllowGet);
        }

        //更新权限
        public ActionResult UpdatePermission(int? id)
        {
            SysPermission PermissionData = permissionhandler.GetDataById(id);
            return View("UpdateControl", PermissionData);
        }
        public ActionResult SavePermission(SysPermission PermissionModel)
        {
            if (string.IsNullOrEmpty(PermissionModel.name))
            {
                return Content("请输入权限名称");
            }
            if (PermissionModel.name.Length > 30)
            {
                return Content("权限名称输入长度过大，请重新输入");
            }
            SysPermission Permission = null;
            int result = -1;
            //编辑
            if (PermissionModel != null && PermissionModel.id != null && PermissionModel.id > 0)
            {
                Permission = permissionhandler.GetDataById(PermissionModel.id);
                Permission.name = PermissionModel.name;
                Permission.order_num = PermissionModel.order_num;
                result = permissionhandler.UpdateDataToSysPermissionTable(Permission);
            }
            //新增
            else
            {
                Permission = new SysPermission();
                Permission.name = PermissionModel.name;
                Permission.order_num = PermissionModel.order_num;
                Permission.add_time = DateTime.Now;
                result = permissionhandler.InsertDataToSysPermissionTable(Permission);
            }

            if (result == 0)
            {
                return Content("0");
            }
            else
            {
                return Content("保存失败");
            }
        }

        //删除权限
        public ActionResult DeletePermission(int? id)
        {
            int result = permissionhandler.DeleteById(id);
            if (result == 1)
            {
                return Content("该权限已经被分配");
            }
            else if (result == 0)
            {
                return Content("0");
            }
            else
            {
                return Content("删除失败");
            }
        }

        public ActionResult PermissionListAll()
        {
            List<SysPermission> syspermission = permissionhandler.GetDataList();
            return View("PermissionListAllControl", syspermission);
        }
    }
}