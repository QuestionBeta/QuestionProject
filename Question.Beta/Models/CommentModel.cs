using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Shop.Web.Models
{
    public class CommentModel
    {
        [Display(Name = "评论内容")]
        [Required(ErrorMessage = "请输入 {0}")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "{0} 不得大于200个字，")]
        public string Comment { get; set; }
    }
}