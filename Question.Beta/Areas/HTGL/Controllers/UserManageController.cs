using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Question.Beta.Controllers;
using DataBase.AppData;
using Shop.Web.Helper;
using Shop.Web.Models;

namespace Question.Beta.Areas.HTGL.Controllers
{
    public class UserManageController : BaseController
    {
        //
        // GET: /HTGL/UserManage/

        public ActionResult Index()
        {
            ViewBag.UrlPath = PathUrl;
            return View();
        }

        public ActionResult RoleList()
        {
            return View();
        }

        [HttpPost]
        [AuthAttribute(Code = "all")]
        public ActionResult GetData(int? page, int? rows)
        {
            //根据用户id获取用户当前角色
            int currentUserId = GetCurrentUserId(HttpContext.User.Identity.Name);
            string currentRole = GetCurrentUserRole(HttpContext.User.Identity.Name).Trim();

            page = page == null ? 1 : (int)page;
            rows = rows == null ? 10 : (int)rows;
            int count = 0;

            if (currentRole != "10")
            {
                var userList = userhandler.GetDataList(page, rows).Where(p => p.id == currentUserId).Select(p => new
                 {
                     p.id,
                     p.user_login_name,
                     p.user_name,
                     p.user_email,
                     p.user_pwd,
                     p.user_role,
                     p.user_tel,
                     p.user_nick_name,
                     p.time,
                     p.isopen
                 }).ToList();
                count = userList.Count;

                var j = new { total = count, rows = userList };
                return Json(j, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var userList = userhandler.GetDataList(page, rows).Select(p => new
                {
                    p.id,
                    p.user_login_name,
                    p.user_name,
                    p.user_email,
                    p.user_pwd,
                    p.user_role,
                    p.user_tel,
                    p.user_nick_name,
                    p.time,
                    p.isopen
                }).ToList();
                count = userList.Count;
                var j = new { total = count, rows = userList };
                return Json(j, JsonRequestBehavior.AllowGet);
            }            
        }

        //禁用用户
        public ActionResult DisabledUser(int? id, bool isopen)
        {
            if (id == null) { id = 0; }
            int result = 1;
            User userModel = userhandler.GetDataById(id);
            if (userModel != null)
            {
                isopen = !isopen;
                userModel.isopen = isopen;
                result = userhandler.UpdateDataToUserTable(userModel);
            }

            return Content(result.ToString());
        }

        //删除用户
        public ActionResult DelUser(int? id)
        {
            int result = 1;
            result = userhandler.DeleteById(id);
            return Content(result.ToString());
        }

        //更新用户窗口
        public ActionResult UpdateUser(int? id)
        {
            if (id != null)
            {
                User user = userhandler.GetDataById(id);
                if (user != null)
                {
                    return View(user);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult UpdateUser(User user, bool flag)
        {
            int result = 1;
            string msg = "";
            //新增
            if (flag)
            {
                if (user != null && !string.IsNullOrEmpty(user.user_login_name)
                    && !string.IsNullOrEmpty(user.user_pwd)
                    && !string.IsNullOrEmpty(user.user_email))
                {
                    if (ValidateUserLogin(user.user_login_name))
                    {
                        return Content("用户登录名已被占用，请更换");
                    }
                    user.user_pwd = MD5Helper.Encode(user.user_pwd);
                    user.time = DateTime.Now;
                }
                result = userhandler.InsertDataToUserTable(user);
                switch (result)
                {
                    case 0:
                        msg = "用户信息新增成功";
                        break;
                    case 1:
                        msg = "用户信息新增失败";
                        break;
                }
            }
            //编辑
            else
            {
                User userEntity = userhandler.GetDataById((int)user.id);
                if (userEntity != null)
                {
                    if (userEntity.user_pwd != user.user_pwd)
                    {
                        user.user_pwd = MD5Helper.Encode(user.user_pwd);
                    }
                    if (user != null && !string.IsNullOrEmpty(user.user_login_name)
                   && !string.IsNullOrEmpty(user.user_pwd)
                   && !string.IsNullOrEmpty(user.user_email))
                    {
                        user.time = userEntity.time;
                        user.id = userEntity.id;
                        userEntity.user_email = user.user_email;
                        userEntity.user_login_name = user.user_login_name;
                        userEntity.user_name = user.user_name;
                        userEntity.user_nick_name = user.user_nick_name;
                        userEntity.user_pwd = user.user_pwd;
                        //userEntity.user_role = user.user_role;
                        userEntity.user_tel = user.user_tel;
                        result = userhandler.UpdateDataToUserTable(userEntity);
                    }
                }
                switch (result)
                {
                    case 0:
                        msg = "用户信息编辑成功";
                        break;
                    case 1:
                        msg = "用户信息编辑失败";
                        break;
                }
            }
            return Content(msg);
        }

        //判断用户名是否存在
        protected bool ValidateUserLogin(string user_login_name)
        {
            bool iscontact = false;
            var isexist = userhandler.GetSingleByName(StringHelper.GetCleanStyle(user_login_name, " "));
            //如果存在 返回true,反之 返回false
            if (isexist != null)
            {
                iscontact = true;
            }

            return iscontact;
        }
    }
}
