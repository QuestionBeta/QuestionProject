using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBase.AppData;

namespace DataBase
{
    public partial class XFX_BugDB : Base
    {
        /// <summary>
        /// 自定义构造函数
        /// </summary>
        public XFX_BugDB() { }

        /// <summary>
        /// 新增XFX_Bug信息 0 成功 1失败
        /// </summary>
        /// <param name="model">导航信息</param>
        public void InsertDataToXFX_BugTable(XFX_Bug model)
        {
            this.datacontext.XFX_Bug.InsertOnSubmit(model);
            this.datacontext.SubmitChanges();
        }

        /// <summary>
        /// 编辑XFX_Bug信息 0 成功 1失败
        /// </summary>
        /// <param name="model">导航信息</param>
        public void UpdateDataToXFX_BugTable(XFX_Bug model)
        {
            this.datacontext.SubmitChanges();
        }

        /// <summary>
        /// 根据XFX_Bug id获取XFX_Bug信息
        /// </summary>
        /// <param name="id">导航Id</param>
        /// <returns></returns>
        public XFX_Bug GetDataById(int? id)
        {
            return this.datacontext.XFX_Bug.Where(p => p.id == id).FirstOrDefault();
        }

        /// <summary>
        /// 根据id删除 XFX_Bug信息
        /// </summary>
        /// <param name="id"></param>
        public void DeleteById(int? id)
        {
			//查找数据
            XFX_Bug navigation = this.datacontext.XFX_Bug.Where(p => p.id == id).FirstOrDefault();
            this.datacontext.XFX_Bug.DeleteOnSubmit(navigation);
            this.datacontext.SubmitChanges();
        }

        /// <summary>
        /// 获取XFX_Bug表列表信息 不带分页
        /// </summary>
        /// <returns>返回导航信息</returns>
        public List<XFX_Bug> GetDataList()
        {
            List<XFX_Bug> list = new List<XFX_Bug>();
            list = this.datacontext.XFX_Bug.ToList();
            return list;
        }

        /// <summary>
        /// 获取XFX_Bug表列表信息 带分页
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="size">当前页数据量大小</param>
        /// <returns>返回数据集</returns>
        public List<XFX_Bug> GetDataList(int? page, int? size)
        {
            int index = (int)page;
            int rows = (int)size;
            List<XFX_Bug> list = new List<XFX_Bug>();
            list = this.datacontext.XFX_Bug.Skip(rows * (index - 1)).Take(rows).ToList();
            return list;
        }
    }
}
