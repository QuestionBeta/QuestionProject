using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBase.AppData;

namespace DataBase
{
    public class UserDB : Base
    {
        /// <summary>
        /// 自定义构造函数
        /// </summary>
        public UserDB() { }

        /// <summary>
        /// 新增User信息 0 成功 1失败
        /// </summary>
        /// <param name="model">导航信息</param>
        public void InsertDataToUserTable(User model)
        {
            this.datacontext.User.InsertOnSubmit(model);
            this.datacontext.SubmitChanges();
        }

        /// <summary>
        /// 编辑User信息 0 成功 1失败
        /// </summary>
        /// <param name="model">导航信息</param>
        public void UpdateDataToUserTable(User model)
        {
            this.datacontext.SubmitChanges();
        }

        /// <summary>
        /// 根据User id获取User信息
        /// </summary>
        /// <param name="id">导航Id</param>
        /// <returns></returns>
        public User GetDataById(int? id)
        {
            return this.datacontext.User.Where(p => p.id == id).FirstOrDefault();
        }

        /// <summary>
        /// 根据id删除 User信息
        /// </summary>
        /// <param name="id"></param>
        public void DeleteById(int? id)
        {
			//查找数据
            User navigation = this.datacontext.User.Where(p => p.id == id).FirstOrDefault();
            this.datacontext.User.DeleteOnSubmit(navigation);
            this.datacontext.SubmitChanges();
        }

        /// <summary>
        /// 获取User表列表信息 不带分页
        /// </summary>
        /// <returns>返回导航信息</returns>
        public List<User> GetDataList()
        {
            List<User> list = new List<User>();
            list = this.datacontext.User.ToList();
            return list;
        }

        /// <summary>
        /// 获取User表列表信息 带分页
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="size">当前页数据量大小</param>
        /// <returns>返回数据集</returns>
        public List<User> GetDataList(int? page, int? size)
        {
            int index = (int)page;
            int rows = (int)size;
            List<User> list = new List<User>();
            list = this.datacontext.User.Skip(rows * (index - 1)).Take(rows).ToList();
            return list;
        }
    }
}
