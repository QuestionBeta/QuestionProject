using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Web.Models;
using Shop.DTHandler;
using WebUIFunction;
using DataBase.AppData;

namespace Question.Beta.Controllers
{
    public class PersonCenterController : BaseController
    {
        //
        // GET: /PersonCenter/
        [AuthAttribute(Code = "1")]
        //个人中心        
        public ActionResult Center()
        {
            //获取用户姓名
            int uid = 0;
            uid = GetCurrentUserId(HttpContext.User.Identity.Name);
            var userData = base.userhandler.GetDataById(uid);
            ViewBag.UserName = userData.user_name;
            return View();
        }

        [AuthAttribute(Code = "1")]
        public ActionResult Index()
        {
            return View();
        }
                
        public ActionResult InitTreeMenu()
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

            roleList.ForEach(x =>
            {
                roleMenuFun.GetDataListByRole(x.role_id).
                    ForEach(p =>
                    {
                        SysRoleMenu rolemenuModel = new SysRoleMenu();
                        rolemenuModel.id = p.id;
                        rolemenuModel.Menu = p.Menu;
                        rolemenuModel.SysRole = p.SysRole;
                        sysrole.Add(rolemenuModel);
                    });
            });

            sysrole.ForEach(p =>
            {
                Menu m = new Menu();
                m.id = p.menu_id;
                m.pid = p.Menu.pid;
                m.name = p.Menu.name;
                m.isexpand = p.Menu.isexpand;
                m.roleId = p.Menu.roleId;
                m.time = p.Menu.time;
                m.type = p.Menu.type;
                m.url = p.Menu.url;
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
                if (dd == null)
                {
                    menu.Add(menuModel);
                }
            }

            obj = TreeMenu.GetJqueryNodeTreeData(menu);

            //ViewBag.InitMenu = obj.ToString();
            return Content(obj.ToString());
        }

        public ActionResult InitPageData()
        {
            return View();
        }
    }
}