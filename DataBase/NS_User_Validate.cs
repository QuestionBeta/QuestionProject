using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Shop.DTDAL
{
    [MetadataType(typeof(User_Validate))]
    public partial class User
    {
        
    }

    public class User_Validate
    {
        [Display(Name = "用户名称")]
        [StringLength(18, MinimumLength = 6, ErrorMessage = "{0}长度过大或过小")]
        public string user_name { get; set; }

        [Display(Name = "用户昵称")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(18, MinimumLength = 6, ErrorMessage = "{0}长度过大或过小")]
        public string user_nick_name { get; set; }

        [Display(Name = "用户登录名称")]
        public string user_login_name { get; set; }

        [Display(Name = "电话号码")]
        [Required(ErrorMessage = "请输入{0}")]
        [RegularExpression(@"^(13|14|15|16|18|19)\d{9}$", ErrorMessage = "电话号码格式错误")]
        public string user_tel { get; set; }
        
        [Display(Name = "电子邮箱")]
        [Required(ErrorMessage = "请输入{0}")]
        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "邮箱格式不正确")]
        public string user_email { get; set; }

        [Display(Name = "注册时间")]        
        public string time { get; set; }
    }
}
