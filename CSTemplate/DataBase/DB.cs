using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBase.AppData;

namespace DataBase
{
    public partial class DB : Base
    {
        /// <summary>
        /// 自定义构造函数
        /// </summary>
        public DB() { }

        /// <summary>
        /// 新增信息 0 成功 1失败
        /// </summary>
        /// <param name="model">导航信息</param>
        public void InsertDataToTable( model)
        {
            this.datacontext..InsertOnSubmit(model);
            this.datacontext.SubmitChanges();
        }

        /// <summary>
        /// 编辑信息 0 成功 1失败
        /// </summary>
        /// <param name="model">导航信息</param>
        public void UpdateDataToTable( model)
        {
            this.datacontext.SubmitChanges();
        }

        /// <summary>
        /// 根据 id获取信息
        /// </summary>
        /// <param name="id">导航Id</param>
        /// <returns></returns>
        public  GetDataById(int? id)
        {
            return this.datacontext..Where(p => p.id == id).FirstOrDefault();
        }

        /// <summary>
        /// 根据id删除 信息
        /// </summary>
        /// <param name="id"></param>
        public void DeleteById(int? id)
        {
			//查找数据
             navigation = this.datacontext..Where(p => p.id == id).FirstOrDefault();
            this.datacontext..DeleteOnSubmit(navigation);
            this.datacontext.SubmitChanges();
        }

        /// <summary>
        /// 获取表列表信息 不带分页
        /// </summary>
        /// <returns>返回导航信息</returns>
        public List<> GetDataList()
        {
            List<> list = new List<>();
            list = this.datacontext..ToList();
            return list;
        }

        /// <summary>
        /// 获取表列表信息 带分页
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="size">当前页数据量大小</param>
        /// <returns>返回数据集</returns>
        public List<> GetDataList(int? page, int? size)
        {
            int index = (int)page;
            int rows = (int)size;
            List<> list = new List<>();
            list = this.datacontext..Skip(rows * (index - 1)).Take(rows).ToList();
            return list;
        }
    }
}
