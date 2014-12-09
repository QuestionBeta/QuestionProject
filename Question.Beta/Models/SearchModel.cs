using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataBase.AppData;
using System.Web.Mvc;

namespace Question.Beta.Models
{
    public class SearchModel
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string keywords { get; set; }

        /// <summary>
        /// BUG列表
        /// </summary>
        public List<XFX_Bug> ListBug { get; set; }

        /// <summary>
        /// 分页代码
        /// </summary>
        public string PagerHtml { get; set; }

        /// <summary>
        /// 获取分类列表
        /// </summary>
        public SelectList CategoryList { get; set; }
    }
}