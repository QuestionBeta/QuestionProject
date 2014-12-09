using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBase.AppData;

namespace DataBase
{
    public class CategoryDB : Base
    {
        /// <summary>
        /// 自定义构造函数
        /// </summary>
        public CategoryDB() { }

        /// <summary>
        /// 新增Category信息 0 成功 1失败
        /// </summary>
        /// <param name="model">导航信息</param>
        public void InsertDataToCategoryTable(Category model)
        {
            this.datacontext.Category.InsertOnSubmit(model);
            this.datacontext.SubmitChanges();
        }

        /// <summary>
        /// 编辑Category信息 0 成功 1失败
        /// </summary>
        /// <param name="model">导航信息</param>
        public void UpdateDataToCategoryTable(Category model)
        {
            this.datacontext.SubmitChanges();
        }

        /// <summary>
        /// 根据Category id获取Category信息
        /// </summary>
        /// <param name="id">导航Id</param>
        /// <returns></returns>
        public Category GetDataById(int? id)
        {
            return this.datacontext.Category.Where(p => p.id == id).FirstOrDefault();
        }

        /// <summary>
        /// 根据id删除 Category信息
        /// </summary>
        /// <param name="id"></param>
        public void DeleteById(int? id)
        {
			//查找数据
            Category navigation = this.datacontext.Category.Where(p => p.id == id).FirstOrDefault();
            this.datacontext.Category.DeleteOnSubmit(navigation);
            this.datacontext.SubmitChanges();
        }

        /// <summary>
        /// 获取Category表列表信息 不带分页
        /// </summary>
        /// <returns>返回导航信息</returns>
        public List<Category> GetDataList()
        {
            List<Category> list = new List<Category>();
            list = this.datacontext.Category.ToList();
            return list;
        }

        /// <summary>
        /// 获取Category表列表信息 带分页
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="size">当前页数据量大小</param>
        /// <returns>返回数据集</returns>
        public List<Category> GetDataList(int? page, int? size)
        {
            int index = (int)page;
            int rows = (int)size;
            List<Category> list = new List<Category>();
            list = this.datacontext.Category.Skip(rows * (index - 1)).Take(rows).ToList();
            return list;
        }
    }
}
