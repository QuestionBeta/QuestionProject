using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBase.AppData;

namespace DataBase
{
    public class XFX_DictionaryValueDB : Base
    {
        /// <summary>
        /// 自定义构造函数
        /// </summary>
        public XFX_DictionaryValueDB() { }

        /// <summary>
        /// 新增XFX_DictionaryValue信息 0 成功 1失败
        /// </summary>
        /// <param name="model">导航信息</param>
        public void InsertDataToXFX_DictionaryValueTable(XFX_DictionaryValue model)
        {
            this.datacontext.XFX_DictionaryValue.InsertOnSubmit(model);
            this.datacontext.SubmitChanges();
        }

        /// <summary>
        /// 编辑XFX_DictionaryValue信息 0 成功 1失败
        /// </summary>
        /// <param name="model">导航信息</param>
        public void UpdateDataToXFX_DictionaryValueTable(XFX_DictionaryValue model)
        {
            this.datacontext.SubmitChanges();
        }

        /// <summary>
        /// 根据XFX_DictionaryValue id获取XFX_DictionaryValue信息
        /// </summary>
        /// <param name="id">导航Id</param>
        /// <returns></returns>
        public XFX_DictionaryValue GetDataById(int? id)
        {
            return this.datacontext.XFX_DictionaryValue.Where(p => p.id == id).FirstOrDefault();
        }

        /// <summary>
        /// 根据id删除 XFX_DictionaryValue信息
        /// </summary>
        /// <param name="id"></param>
        public void DeleteById(int? id)
        {
			//查找数据
            XFX_DictionaryValue navigation = this.datacontext.XFX_DictionaryValue.Where(p => p.id == id).FirstOrDefault();
            this.datacontext.XFX_DictionaryValue.DeleteOnSubmit(navigation);
            this.datacontext.SubmitChanges();
        }

        /// <summary>
        /// 获取XFX_DictionaryValue表列表信息 不带分页
        /// </summary>
        /// <returns>返回导航信息</returns>
        public List<XFX_DictionaryValue> GetDataList()
        {
            List<XFX_DictionaryValue> list = new List<XFX_DictionaryValue>();
            list = this.datacontext.XFX_DictionaryValue.ToList();
            return list;
        }

        /// <summary>
        /// 获取XFX_DictionaryValue表列表信息 带分页
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="size">当前页数据量大小</param>
        /// <returns>返回数据集</returns>
        public List<XFX_DictionaryValue> GetDataList(int? page, int? size)
        {
            int index = (int)page;
            int rows = (int)size;
            List<XFX_DictionaryValue> list = new List<XFX_DictionaryValue>();
            list = this.datacontext.XFX_DictionaryValue.Skip(rows * (index - 1)).Take(rows).ToList();
            return list;
        }
    }
}
