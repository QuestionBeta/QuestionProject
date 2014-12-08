using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBase.AppData;

namespace DataBase
{
    public class XFX_NavigationDB : Base
    {
        /// <summary>
        /// 自定义构造函数
        /// </summary>
        public XFX_NavigationDB() { }

        /// <summary>
        /// 新增XFX_Navigation信息 0 成功 1失败
        /// </summary>
        /// <param name="model">导航信息</param>
        public void InsertDataToXFX_NavigationTable(XFX_Navigation model)
        {
            this.datacontext.XFX_Navigation.InsertOnSubmit(model);
            this.datacontext.SubmitChanges();
        }

        /// <summary>
        /// 编辑XFX_Navigation信息 0 成功 1失败
        /// </summary>
        /// <param name="model">导航信息</param>
        public void UpdateDataToXFX_NavigationTable(XFX_Navigation model)
        {
            this.datacontext.SubmitChanges();
        }

        /// <summary>
        /// 根据XFX_Navigation id获取XFX_Navigation信息
        /// </summary>
        /// <param name="id">导航Id</param>
        /// <returns></returns>
        public XFX_Navigation GetDataById(int? id)
        {
            return this.datacontext.XFX_Navigation.Where(p => p.id == id).FirstOrDefault();
        }

        /// <summary>
        /// 根据id删除 XFX_Navigation信息
        /// </summary>
        /// <param name="id"></param>
        public void DeleteById(int? id)
        {
			//查找数据
            XFX_Navigation navigation = this.datacontext.XFX_Navigation.Where(p => p.id == id).FirstOrDefault();
            this.datacontext.XFX_Navigation.DeleteOnSubmit(navigation);
            this.datacontext.SubmitChanges();
        }

        /// <summary>
        /// 获取XFX_Navigation表列表信息 不带分页
        /// </summary>
        /// <returns>返回导航信息</returns>
        public List<XFX_Navigation> GetDataList()
        {
            List<XFX_Navigation> list = new List<XFX_Navigation>();
            list = this.datacontext.XFX_Navigation.ToList();
            return list;
        }

        /// <summary>
        /// 获取XFX_Navigation表列表信息 带分页
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="size">当前页数据量大小</param>
        /// <returns>返回数据集</returns>
        public List<XFX_Navigation> GetDataList(int? page, int? size)
        {
            int index = (int)page;
            int rows = (int)size;
            List<XFX_Navigation> list = new List<XFX_Navigation>();
            list = this.datacontext.XFX_Navigation.Skip(rows * (index - 1)).Take(rows).ToList();
            return list;
        }
    }
}
