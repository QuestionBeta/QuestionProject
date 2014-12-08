using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBase.AppData;

namespace DataBase
{
    public partial class MenuDB : Base
    {
        /// <summary>
        /// 自定义构造函数
        /// </summary>
        public MenuDB() { }

        /// <summary>
        /// 新增Menu信息 0 成功 1失败
        /// </summary>
        /// <param name="model">导航信息</param>
        public void InsertDataToMenuTable(Menu model)
        {
            this.datacontext.Menu.InsertOnSubmit(model);
            this.datacontext.SubmitChanges();
        }

        /// <summary>
        /// 编辑Menu信息 0 成功 1失败
        /// </summary>
        /// <param name="model">导航信息</param>
        public void UpdateDataToMenuTable(Menu model)
        {
            this.datacontext.SubmitChanges();
        }

        /// <summary>
        /// 根据Menu id获取Menu信息
        /// </summary>
        /// <param name="id">导航Id</param>
        /// <returns></returns>
        public Menu GetDataById(int? id)
        {
            return this.datacontext.Menu.Where(p => p.id == id).FirstOrDefault();
        }

        /// <summary>
        /// 根据id删除 Menu信息
        /// </summary>
        /// <param name="id"></param>
        public void DeleteById(int? id)
        {
			//查找数据
            Menu navigation = this.datacontext.Menu.Where(p => p.id == id).FirstOrDefault();
            this.datacontext.Menu.DeleteOnSubmit(navigation);
            this.datacontext.SubmitChanges();
        }

        /// <summary>
        /// 获取Menu表列表信息 不带分页
        /// </summary>
        /// <returns>返回导航信息</returns>
        public List<Menu> GetDataList()
        {
            List<Menu> list = new List<Menu>();
            list = this.datacontext.Menu.ToList();
            return list;
        }

        /// <summary>
        /// 获取Menu表列表信息 带分页
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="size">当前页数据量大小</param>
        /// <returns>返回数据集</returns>
        public List<Menu> GetDataList(int? page, int? size)
        {
            int index = (int)page;
            int rows = (int)size;
            List<Menu> list = new List<Menu>();
            list = this.datacontext.Menu.Skip(rows * (index - 1)).Take(rows).ToList();
            return list;
        }
    }
}
