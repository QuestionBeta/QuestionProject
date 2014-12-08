using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBase.AppData;

namespace WebUIFunction
{
    public partial class XFX_BugFun
    {
        /// <summary>
        /// 根据关键字查询BUG列表 带分页
        /// </summary>
        /// <returns>返回查询结果集</returns>
        public List<XFX_Bug> GetBugListByKeywords(int? page, int? size, string keyword)
        {
            page = page == null ? 0 : page;
            size = size == null ? 15 : size;
            return this.baseDataModel.GetBugListByKeywords((int)page, (int)size, keyword);
        }

        /// <summary>
        /// 根据关键字查询BUG列表 带分页
        /// </summary>
        /// <returns>返回查询结果集</returns>
        public List<XFX_Bug> GetBugListByKeywords(int? page, int? size, string keyword,int? category)
        {
            page = page == null ? 0 : page;
            size = size == null ? 15 : size;

            return this.baseDataModel.GetBugListByKeywords((int)page, (int)size, keyword, category);
        }
        /// <summary>
        /// 根据关键字查询BUG列表 带分页
        /// </summary>
        /// <returns>返回查询结果集</returns>
        public List<XFX_Bug> GetBugListByKeywords(int page, int size, int? category)
        {
            List<XFX_Bug> data = new List<XFX_Bug>();
            data = this.baseDataModel.GetBugListByKeywords(page, size, category);
            return data;
        }

        /// <summary>
        /// 根据关键字查询BUG列表 
        /// </summary>
        /// <returns>返回查询结果集</returns>
        public List<XFX_Bug> GetBugListByKeywords(string keyword)
        {
            return this.baseDataModel.GetBugListByKeywords(keyword);
        }

        /// <summary>
        /// 根据关键字查询BUG列表 
        /// </summary>
        /// <returns>返回查询结果集</returns>
        public List<XFX_Bug> GetBugListByKeywords(int? category)
        {
            return this.baseDataModel.GetBugListByKeywords(category);
        }

        /// <summary>
        /// 根据关键字查询BUG列表 
        /// </summary>
        /// <returns>返回查询结果集</returns>
        public List<XFX_Bug> GetBugListByKeywords(string keyword,int? category)
        {
            return this.baseDataModel.GetBugListByKeywords(keyword, category);
        }
    }
}
