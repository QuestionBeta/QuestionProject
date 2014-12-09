using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBase.AppData;

namespace DataBase
{
    public partial class XFX_BugDB
    {
        /// <summary>
        /// 根据关键字查询BUG列表 带分页
        /// </summary>
        /// <returns>返回查询结果集</returns>
        public List<XFX_Bug> GetBugListByKeywords(int page, int size, string keyword)
        {
            return this.datacontext.XFX_Bug.Where(p => p.title.Contains(keyword) || p.key_values.Contains(keyword)).Skip((page - 1) * size).Take(size).ToList();
        }

        /// <summary>
        /// 根据关键字查询BUG列表 带分页
        /// </summary>
        /// <returns>返回查询结果集</returns>
        public List<XFX_Bug> GetBugListByKeywords(int page, int size, string keyword, int? category)
        {
            List<XFX_Bug> data = new List<XFX_Bug>();
            if (category == null)
            {
                data = this.datacontext.XFX_Bug.Where(p => p.title.Contains(keyword) || p.key_values.Contains(keyword)).Skip((page - 1) * size).Take(size).ToList();
            }
            else
            {
                data = this.datacontext.XFX_Bug.Where(p => (p.title.Contains(keyword) || p.key_values.Contains(keyword)) && p.category == category).Skip((page - 1) * size).Take(size).ToList();
            }

            return data;
        }

        /// <summary>
        /// 根据关键字查询BUG列表 带分页
        /// </summary>
        /// <returns>返回查询结果集</returns>
        public List<XFX_Bug> GetBugListByKeywords(int page, int size, int? category)
        {
            List<XFX_Bug> data = new List<XFX_Bug>();
            data = this.datacontext.XFX_Bug.Where(p => p.category == category).Skip((page - 1) * size).Take(size).ToList();
            return data;
        }

        /// <summary>
        /// 根据关键字查询BUG列表 
        /// </summary>
        /// <returns>返回查询结果集</returns>
        public List<XFX_Bug> GetBugListByKeywords(string keyword)
        {
            return this.datacontext.XFX_Bug.Where(p => p.title.Contains(keyword) || p.key_values.Contains(keyword)).ToList();
        }

        /// <summary>
        /// 根据关键字查询BUG列表 
        /// </summary>
        /// <returns>返回查询结果集</returns>
        public List<XFX_Bug> GetBugListByKeywords(int? category)
        {
            return this.datacontext.XFX_Bug.Where(p => p.category == category).ToList();
        }

        /// <summary>
        /// 根据关键字查询BUG列表 
        /// </summary>
        /// <returns>返回查询结果集</returns>
        public List<XFX_Bug> GetBugListByKeywords(string keyword, int? category)
        {
            List<XFX_Bug> data = new List<XFX_Bug>();
            if (category == null)
            {
                data = this.datacontext.XFX_Bug.Where(p => p.title.Contains(keyword) || p.key_values.Contains(keyword)).ToList();
            }
            else
            {
                data = this.datacontext.XFX_Bug.Where(p => (p.title.Contains(keyword) || p.key_values.Contains(keyword)) && p.category == category).ToList();
            }

            return data;
        }
    }
}
