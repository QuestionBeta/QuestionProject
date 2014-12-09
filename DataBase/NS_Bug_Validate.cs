using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DataBase.AppData
{
    [MetadataType(typeof(NS_Bug_Validate))]
    public partial class XFX_Bug
    {

    }

    public class NS_Bug_Validate
    {
        [Display(Name = "BUG标题")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0}长度限制在2-50个字符")]
        public string title { get; set; }

        [Display(Name = "BUG截图")]
        [DataType(DataType.ImageUrl)]
        public string url { get; set; }
        [AllowHtml]
        [Display(Name = "BUG描述")]      
        public string description { get; set; }

        [Display(Name = "关键字")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "{0}长度限制在2-30个字符")]
        public string key_values { get; set; }

        [Display(Name = "是否解决")]
        public bool iscomplete { get; set; }

        [Display(Name = "解决方案")]
        [AllowHtml]
        public string anwser { get; set; }

        [Display(Name = "所属类别")]
        public int category { get; set; }
    }
}