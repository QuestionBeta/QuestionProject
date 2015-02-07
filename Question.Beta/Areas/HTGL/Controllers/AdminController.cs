using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Question.Beta.Controllers;
using Shop.Web.Models;
using Shop.DTHandler;
using DataBase.AppData;
using WebUIFunction;

namespace Question.Beta.Areas.HTGL.Controllers
{
    public class AdminController : BaseController
    {
        //
        // GET: /HTGL/Admin/
        public AdminController()
        {

        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PersonCenter()
        {
            return View("Index");
        }

        public ActionResult Center()
        {
            return View();
        }

        
        /// <summary>
        /// 根据用户角色加载左侧菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthAttribute(Code = "all")]
        public string LeftMenuNav()
        {
            //获取用户id
            int uid = 0;
            uid = GetCurrentUserId(HttpContext.User.Identity.Name);
            SysUser_RoleFun user_rolefun = new SysUser_RoleFun();
            List<SysUser_Role> roleList = user_rolefun.GetRoleListByUser(uid);
            //获取功能项列表            
            SysRoleMenuFun roleMenuFun = new SysRoleMenuFun();
            List<SysRoleMenu> sysrole = new List<SysRoleMenu>();
            //根据角色获取功能项列表
            var data = menuhandler.GetDataList();
            List<Menu> menu = new List<Menu>();
            
            roleList.ForEach(x => {  roleMenuFun.GetDataListByRole(x.role_id).
                ForEach(p => 
                { 
                    SysRoleMenu rolemenuModel = new SysRoleMenu();
                    rolemenuModel.id = p.id;
                    rolemenuModel.Menu = p.Menu;
                    rolemenuModel.SysRole = p.SysRole;
                    sysrole.Add(rolemenuModel);
                });
            });

            sysrole.ForEach(p => {
                Menu m = new Menu();
                m.id = p.menu_id;
                m.pid = p.Menu.pid;
                m.name = p.Menu.name;
                m.isexpand = p.Menu.isexpand;
                m.roleId = p.Menu.roleId;
                m.time = p.Menu.time;
                m.type = p.Menu.type;
                m.url = p.Menu.url;
                m.iconcls = p.Menu.iconcls;
                menu.Add(m);
            });
            object obj = null;
            //List<DTMenu> list = new List<DTMenu>();
            //list = (List<DTMenu>)data;
            string[] array = (string[])menu.Select(p => p.pid.ToString()).Distinct().ToArray();
            //获取父菜单
            foreach (var item in array)
            {
                Menu menuModel = new Menu();
                menuModel = menuhandler.GetDataById(int.Parse(item));
                //判断是否存在相同实例 存在则不执行操作
                int pid = int.Parse(item);
                var dd = menu.Where(p => p.id == pid).FirstOrDefault();
                if (dd == null )
                {
                    menu.Add(menuModel);
                }
            }
            
            if (menu != null)
            {
                if (GetCurrentUserRole(HttpContext.User.Identity.Name) != "10")
                {
                    obj = TreeMenu.GetNodeTreeData(menu);
                }
                else
                {
                    obj = TreeMenu.GetNodeTreeData();
                }
            }

            return obj.ToString();
        }
    }
}