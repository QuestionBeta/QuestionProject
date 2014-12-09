using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Shop.Web.Models
{
    public class AlterPwdModel
    {
        [Display(Name = "旧密码")]
        [Required(ErrorMessage = "请输入{0}")]        
        [RegularExpression(@"^(([a-z]+)|([A-Z]+)).([0-9]+)", ErrorMessage = "密码格式有误")]
        [StringLength(18, MinimumLength = 6, ErrorMessage = "{0}长度过大或过小")]
        [Remote("ValidatePwd", "Home", HttpMethod = "post", ErrorMessage = "旧密码输入错误")]
        /// <summary>
        /// 旧密码
        /// </summary>
        public string oldpwd { get; set; }

        [Display(Name = "新密码")]
        [Required(ErrorMessage = "请输入{0}")]
        [RegularExpression(@"^(([a-z]+)|([A-Z]+)).([0-9]+)", ErrorMessage = "密码格式有误")]
        [StringLength(18, MinimumLength = 6, ErrorMessage = "{0}长度过大或过小")]
        /// <summary>
        /// 新密码
        /// </summary>
        public string newpwd { get; set; }

        [Display(Name = "重复密码")]
        [Required(ErrorMessage = "请输入{0}")]
        [Compare("newpwd", ErrorMessage = "两次密码不一致,请重新输入")]
        [RegularExpression(@"^(([a-z]+)|([A-Z]+)).([0-9]+)", ErrorMessage = "密码格式有误")]
        [StringLength(18, MinimumLength = 6, ErrorMessage = "{0}长度过大或过小")]
        /// <summary>
        /// 重复密码
        /// </summary>
        public string repeatpwd { get; set; }
    }
}