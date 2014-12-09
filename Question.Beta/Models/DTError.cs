using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.DTModel.Admin
{
    public class DTError
    {
        /// <summary>
        /// 出错页地址
        /// </summary>
        public string Url { get; set; }
        
        /// <summary>
        /// 出错页详情
        /// </summary>
        public string Message { get; set; }
    }
}
