using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Web.Models;
using System.Web.Security;
using System.Web.Script.Serialization;
using Question.Beta.Controllers;

using DataBase.AppData;
using Shop.Web.Helper;
using WebUIFunction;

namespace Shop.Web.Controllers
{
    public class LoginController : BaseController
    {
        //
        // GET: /Login/

        public ActionResult LoginRegister()
        {
            return View();
        }

        public PartialViewResult LoginResult()
        {
            return PartialView();
        }
        
        [HttpPost]
        public ActionResult LoginRegister(RegisterModel model)
        {
            //判断验证是否通过
            if (!ModelState.IsValid)
            {
                return View();
            }

            User user = new User();
            user.isopen = (bool)true;
            user.time = DateTime.Now;
            user.user_email = model.user_email;
            user.user_login_name = StringHelper.GetCleanStyle(model.user_login_name," ");
            user.user_name = model.user_show_name;
            user.user_pwd = MD5Helper.Encode(model.user_pwd);
            user.user_role = "member";
            int result = userhandler.InsertDataToUserTable(user);
            ViewBag.Data = result;
            return View();
            //return View("LoginResult", result);
        }

        public ActionResult ValidateUserShow(string user_show_name,string user_login_name)
        {
            bool iscontact = false;
            if (!user_show_name.Equals(user_login_name))
            {
                iscontact = true;
            }

            return Json(iscontact, JsonRequestBehavior.AllowGet);
        }

        //初始化验证码
        public ActionResult InitValidateCode()
        {
            Session["ValidateCode"] = null;
            ValidateCodeHelper vCode = new ValidateCodeHelper();
            string code = vCode.CreateValidateCode(5);
            Session["ValidateCode"] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }

        //协议
        public ActionResult Agreement()
        {
            return View();
        }

        //同意协议界面
        public ActionResult RegisterResult()
        {
            ViewBag.Agree = "0";

            return View("", ViewBag.Agree);
        }

        #region === 验证函数 ==
        [HttpPost]
        //判断验证码是否正确
        public ActionResult ValidateCode(string validate_code)
        {
            bool iscontact = false;
            if (Session["ValidateCode"] == null)
            {
                return Json(iscontact, JsonRequestBehavior.AllowGet);
            }

            var nowcode = Session["ValidateCode"].ToString();
            if (nowcode.ToLower() == validate_code.ToLower())
            {
                iscontact = true;
            }

            return Json(iscontact,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //判断用户名是否存在
        public ActionResult ValidateUser(string user_login_name)
        {
            bool iscontact = false;
            var isexist = userhandler.GetSingleByName(StringHelper.GetCleanStyle(user_login_name," "));
            //如果存在 返回false,反之 返回true
            if (isexist == null)
            {
                iscontact = true;
            }

            return Json(iscontact, JsonRequestBehavior.AllowGet);
        }

        //判断用户名是否存在
        public ActionResult ValidateUserLogin(string user_login_name)
        {
            bool iscontact = false;
            var isexist = userhandler.GetSingleByName(StringHelper.GetCleanStyle(user_login_name, " "));
            //如果存在 返回false,反之 返回true
            if (isexist != null && isexist.isopen)
            {
                iscontact = true;
            }

            return Json(iscontact, JsonRequestBehavior.AllowGet);
        }

        //判断邮箱是否被占用
        public ActionResult ValidateEmail(string user_email)
        {
            bool iscontact = false;
            var isexist = userhandler.GetSingleByEmail(StringHelper.GetCleanStyle(user_email, " "));
            if (isexist == null)
            {
                iscontact = true;
            }

            return Json(iscontact, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult LoginOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginOn(LoginModel model)
        {
            bool flag = false;
            //判断验证是否通过
            if (!ModelState.IsValid)
            {
                return View();
            }
            var Dmodel = userhandler.GetSingleByName(model.user_login_name);
            if (Dmodel == null)
            {
                ViewBag.Data = 1;
                return View(flag.ToString());
            }

            //判断用户是否禁用，禁用提示信息
            if (!Dmodel.isopen)
            {
                ModelState.AddModelError("user_login_name", "用户名不存在");
                return View();
            }

            var str = MD5Helper.Decode(Dmodel.user_pwd);
            if (!str.Equals(model.user_pwd))
            {
                ModelState.AddModelError("user_pwd", "密码不正确");
                return View();
            }

            model.user_pwd = MD5Helper.Encode(model.user_pwd);
            var j = new { record = Dmodel };
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            //response.Write(serializer.Serialize(Dmodel));
            //保存身份信息，参数说明可以看提示
            //string roles = "admin,member,developer";
            SysUser_RoleFun fun = new SysUser_RoleFun();
            List<SysUser_Role> role = fun.GetRoleListByUser(Dmodel.id);
            string user_role = string.Empty;
            if (role != null && role.Count > 0)
            {
                user_role = role.FirstOrDefault().SysRole.jb;
            }
            FormsAuthenticationTicket Ticket = new FormsAuthenticationTicket(1, model.user_login_name, DateTime.Now, DateTime.Now.AddHours(2), false, user_role);
            HttpCookie Cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(Ticket));//加密身份信息，保存至Cookie
            Cookie.HttpOnly = true; //客户端无法访问Cookie
            Response.Cookies.Add(Cookie);
            ViewBag.Data = 0;
            return View();
        }

        public ActionResult LoginOut()
        {
            int result = 1;
            try
            {
                FormsAuthentication.SignOut();
                result = 0;
            }
            catch
            {

            }
            return Content(result.ToString());
        }
    }
}
