using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Question.Beta.Controllers;
using DataBase.AppData;

namespace Question.Beta.Areas.HTGL.Controllers
{
    public class RoleController : BaseController
    {
        //
        // GET: /HTGL/Role/

        public ActionResult RoleList()
        {
            ViewBag.Title = "角色管理";
            return View();
        }
        //获取角色列表
        public ActionResult GetData(int? page, int? rows)
        {
            page = page == null ? 1 : (int)page;
            rows = rows == null ? 10 : (int)rows;
            List<SysRole> dt = new List<SysRole>();
            var data = rolehandler.GetDataList(page, rows).Select(p=>new { p.add_time,p.name,p.id,p.order_num,p.jb }).ToList();
            int total = rolehandler.GetDataList().Count;
            var j = new { total = total, rows = data };
            return Json(j, JsonRequestBehavior.AllowGet);
        }

        //更新角色
        public ActionResult UpdateRole(int? id)
        {
            SysRole roleData = rolehandler.GetDataById(id);
            return View("UpdateControl", roleData);
        }
        public ActionResult SaveRole(SysRole roleModel)
        {
            if (string.IsNullOrEmpty(roleModel.name))
            {
                return Content("请输入角色名称");
            }
            if (roleModel.name.Length > 30)
            {
                return Content("角色名称输入长度过大，请重新输入");
            }
            SysRole role = null;
            int result = -1;
            //编辑
            if (roleModel != null && roleModel.id != null && roleModel.id > 0)
            {                
                role = rolehandler.GetDataById(roleModel.id);
                role.name = roleModel.name;
                role.jb = roleModel.jb;
                role.order_num = roleModel.order_num;
                result = rolehandler.UpdateDataToSysRoleTable(role);                
            }
            //新增
            else
            {
                role = new SysRole();
                role.name = roleModel.name;
                role.order_num = roleModel.order_num;
                role.jb = roleModel.jb;
                role.add_time = DateTime.Now;
                result = rolehandler.InsertDataToSysRoleTable(role);
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

        //删除角色
        public ActionResult DeleteRole(int? id)
        {
            //查找子菜单
            int result = rolehandler.DeleteById(id);
            if (result == 1)
            {
                return Content("该角色已经分配主体");
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
    }
}
