using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Shop.Web.Models
{
    /// <summary>
    /// 登录实体类
    /// </summary>
    public class LoginModel
    {
        [Display(Name = "用户名")]
        [Required(ErrorMessage = "请输入{0}")]
        [RegularExpression(@"^(([a-z]+)|([A-Z]+)).([0-9]+)", ErrorMessage = "用户名称格式错误")]
        [StringLength(18, MinimumLength = 6, ErrorMessage = "{0}长度过大或过小")]
        [Remote("ValidateUserLogin", "Login", HttpMethod = "post", ErrorMessage = "用户不存在")]
        //[AllowHtml()]
        /// <summary>
        /// 用户登录名称
        /// </summary>
        public string user_login_name { get; set; }

        [Display(Name = "密码")]
        [Required(ErrorMessage = "请输入{0}")]
        [RegularExpression(@"^(([a-z]+)|([A-Z]+)).([0-9]+)", ErrorMessage = "密码格式有误")]
        /// <summary>
        /// 用户密码
        /// </summary>
        public string user_pwd { get; set; }

        [Display(Name = "验证码")]
        [Required(ErrorMessage = "请输入{0}")]
        [Remote("ValidateCode", "Login", HttpMethod = "post", ErrorMessage = "验证码输入有误")]
     
        /// <summary>
        /// 验证码
        /// </summary>
        public string validate_code { get; set; }
    }
}