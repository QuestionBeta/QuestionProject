using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBase.AppData;

namespace DataBase
{
    public class LoginDB : Base
    {
        /// <summary>
        /// 自定义构造函数
        /// </summary>
        public LoginDB() { }

        /// <summary>
        /// 新增Login信息 0 成功 1失败
        /// </summary>
        /// <param name="model">导航信息</param>
        public void InsertDataToLoginTable(Login model)
        {
            this.datacontext.Login.InsertOnSubmit(model);
            this.datacontext.SubmitChanges();
        }

        /// <summary>
        /// 编辑Login信息 0 成功 1失败
        /// </summary>
        /// <param name="model">导航信息</param>
        public void UpdateDataToLoginTable(Login model)
        {
            this.datacontext.SubmitChanges();
        }

        /// <summary>
        /// 根据Login id获取Login信息
        /// </summary>
        /// <param name="id">导航Id</param>
        /// <returns></returns>
        public Login GetDataById(int? id)
        {
            return this.datacontext.Login.Where(p => p.id == id).FirstOrDefault();
        }

        /// <summary>
        /// 根据id删除 Login信息
        /// </summary>
        /// <param name="id"></param>
        public void DeleteById(int? id)
        {
			//查找数据
            Login navigation = this.datacontext.Login.Where(p => p.id == id).FirstOrDefault();
            this.datacontext.Login.DeleteOnSubmit(navigation);
            this.datacontext.SubmitChanges();
        }

        /// <summary>
        /// 获取Login表列表信息 不带分页
        /// </summary>
        /// <returns>返回导航信息</returns>
        public List<Login> GetDataList()
        {
            List<Login> list = new List<Login>();
            list = this.datacontext.Login.ToList();
            return list;
        }

        /// <summary>
        /// 获取Login表列表信息 带分页
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="size">当前页数据量大小</param>
        /// <returns>返回数据集</returns>
        public List<Login> GetDataList(int? page, int? size)
        {
            int index = (int)page;
            int rows = (int)size;
            List<Login> list = new List<Login>();
            list = this.datacontext.Login.Skip(rows * (index - 1)).Take(rows).ToList();
            return list;
        }
    }
}
