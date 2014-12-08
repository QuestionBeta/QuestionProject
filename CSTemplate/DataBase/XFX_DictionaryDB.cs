using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBase.AppData;

namespace DataBase
{
    public class XFX_DictionaryDB : Base
    {
        /// <summary>
        /// 自定义构造函数
        /// </summary>
        public XFX_DictionaryDB() { }

        /// <summary>
        /// 新增XFX_Dictionary信息 0 成功 1失败
        /// </summary>
        /// <param name="model">导航信息</param>
        public void InsertDataToXFX_DictionaryTable(XFX_Dictionary model)
        {
            this.datacontext.XFX_Dictionary.InsertOnSubmit(model);
            this.datacontext.SubmitChanges();
        }

        /// <summary>
        /// 编辑XFX_Dictionary信息 0 成功 1失败
        /// </summary>
        /// <param name="model">导航信息</param>
        public void UpdateDataToXFX_DictionaryTable(XFX_Dictionary model)
        {
            this.datacontext.SubmitChanges();
        }

        /// <summary>
        /// 根据XFX_Dictionary id获取XFX_Dictionary信息
        /// </summary>
        /// <param name="id">导航Id</param>
        /// <returns></returns>
        public XFX_Dictionary GetDataById(int? id)
        {
            return this.datacontext.XFX_Dictionary.Where(p => p.id == id).FirstOrDefault();
        }

        /// <summary>
        /// 根据id删除 XFX_Dictionary信息
        /// </summary>
        /// <param name="id"></param>
        public void DeleteById(int? id)
        {
			//查找数据
            XFX_Dictionary navigation = this.datacontext.XFX_Dictionary.Where(p => p.id == id).FirstOrDefault();
            this.datacontext.XFX_Dictionary.DeleteOnSubmit(navigation);
            this.datacontext.SubmitChanges();
        }

        /// <summary>
        /// 获取XFX_Dictionary表列表信息 不带分页
        /// </summary>
        /// <returns>返回导航信息</returns>
        public List<XFX_Dictionary> GetDataList()
        {
            List<XFX_Dictionary> list = new List<XFX_Dictionary>();
            list = this.datacontext.XFX_Dictionary.ToList();
            return list;
        }

        /// <summary>
        /// 获取XFX_Dictionary表列表信息 带分页
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="size">当前页数据量大小</param>
        /// <returns>返回数据集</returns>
        public List<XFX_Dictionary> GetDataList(int? page, int? size)
        {
            int index = (int)page;
            int rows = (int)size;
            List<XFX_Dictionary> list = new List<XFX_Dictionary>();
            list = this.datacontext.XFX_Dictionary.Skip(rows * (index - 1)).Take(rows).ToList();
            return list;
        }
    }
}
