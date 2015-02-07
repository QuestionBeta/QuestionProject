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

        /// <summary>
        /// 获取已发布数据总量
        /// </summary>
        /// <returns></returns>
        public int GetDataTotalWherePublish()
        {
            return this.baseDataModel.GetDataTotalWherePublish();
        }

        /// <summary>
        /// 获取已发布的XFX_Bug表列表信息 带分页
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="size">当前页数据量大小</param>
        /// <returns>返回数据集</returns>
        public List<XFX_Bug> GetDataListWherePublish(int? page, int? size)
        {
            return this.baseDataModel.GetDataListWherePublish(page, size);
        }

        /// <summary>
        /// 获取已发布的XFX_Bug表列表信息
        /// </summary>
        /// <returns>返回数据集</returns>
        public List<XFX_Bug> GetDataListWherePublish()
        {
            return this.baseDataModel.GetDataListWherePublish();
        }

        /// <summary>
        /// 根据用户Id获取XFX_Bug表列表信息 带分页
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="size">当前页数据量大小</param>
        /// <returns>返回数据集</returns>
        public List<XFX_Bug> GetDataList(int? page, int? size, int user_id)
        {
            int index = (int)page;
            int rows = (int)size;
            return this.baseDataModel.GetDataList(page, size, user_id);
        }

        /// <summary>
        /// 根据用户Id获取XFX_Bug表列表信息 
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="size">当前页数据量大小</param>
        /// <returns>返回数据集</returns>
        public List<XFX_Bug> GetDataList(int user_id)
        {
            return this.baseDataModel.GetDataList(user_id);
        }
    }
}
