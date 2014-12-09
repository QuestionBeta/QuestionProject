using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Shop.Web.Models
{
    public class RegisterModel
    {
        [Display(Name = "用户名称")]
        [Required(ErrorMessage = "请输入{0}")]
        [RegularExpression(@"^(([a-z]+|[A-Z]+)|([A-z]+|[a-z]+)).([0-9]+)", ErrorMessage = "用户名称格式错误")]
        [StringLength(18, MinimumLength = 6, ErrorMessage = "用户名称必须在6-18个字符以内")]
        [Remote("ValidateUser", "Login", HttpMethod = "POST", ErrorMessage = "该用户名已被占用")]
        [Editable(true)]
        /// <summary>
        /// 用户登录名称
        /// </summary>
        public string user_login_name { get; set; }

        [Display(Name = "显示名称")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(8, MinimumLength = 3, ErrorMessage = "用户名称长度过大或过小")]
        [RegularExpression(@"^(([\u4e00-\u9fa5]+)|(([a-z]+|[A-Z]+).([0-9]+)))", ErrorMessage = "必须为中文或者英文数字组合")]
        [Remote("ValidateUserShow", "Login", HttpMethod = "post", ErrorMessage = "用户显示名称输入有误")]
        /// <summary>
        /// 用户显示名称
        /// </summary>
        public string user_show_name { get; set; }

        [Display(Name = "邮箱")]
        [Required(ErrorMessage = "请输入{0}")]
        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "邮箱格式不正确")]
        [Remote("ValidateEmail", "Login", HttpMethod = "post", ErrorMessage = "该邮箱已被占用")]
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string user_email { get; set; }

        [Display(Name = "密码")]
        [Required(ErrorMessage = "请输入{0}")]        
        [RegularExpression(@"^(([a-z]+)|([A-Z]+)).([0-9]+)", ErrorMessage = "密码格式有误")]
        [DataType(DataType.Password)]
        /// <summary>
        /// 用户密码
        /// </summary>
        public string user_pwd { get; set; }

        [Display(Name = "确认密码")]
        [Required(ErrorMessage = "请再次输入密码")]
        [Compare("user_pwd", ErrorMessage = "两次密码不一致,请重新输入")]
        [DataType(DataType.Password)]
        /// <summary>
        /// 确认密码
        /// </summary>
        public string user_repeat_pwd { get; set; }

        [Display(Name = "验证码")]
        [Required(ErrorMessage = "请输入{0}")]
        [Remote("ValidateCode", "Login", HttpMethod = "post", ErrorMessage = "验证码输入有误")]
        [Editable(true)]
        /// <summary>
        /// 验证码
        /// </summary>
        public string validate_code { get; set; }
    }
} 