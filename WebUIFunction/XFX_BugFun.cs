using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBase;
using DataBase.AppData;

namespace WebUIFunction
{
    public partial class XFX_BugFun
    {
        /// <summary>
        /// 数据访问变量
        /// </summary>
        public XFX_BugDB  baseDataModel = null;

        /// <summary>
        /// 自定义构造函数
        /// </summary>
        public XFX_BugFun() 
		{ 
			baseDataModel = new XFX_BugDB();
		}

        /// <summary>
        /// 新增XFX_Bug信息 0 成功 1失败
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertDataToXFX_BugTable(XFX_Bug model)
        {
            int result = 1;
            if (model != null)
            {
                try
                {
                    baseDataModel.InsertDataToXFX_BugTable(model);
                    result = 0;
                }
                catch
                {

                }
            }

            return result;
        }

        /// <summary>
        /// 编辑XFX_Bug信息 0 成功 1失败
        /// </summary>
        /// <param name="model">模型实体信息</param>
        /// <returns>0/1</returns>
        public int UpdateDataToXFX_BugTable(XFX_Bug model)
        {
            int result = 1;
            if (model != null)
            {
                try
                {
                    baseDataModel.UpdateDataToXFX_BugTable(model);
                    result = 0;
                }
                catch
                {

                }
            }

            return result;
        }

        /// <summary>
        /// 根据XFX_Bug id获取XFX_Bug信息
        /// </summary>
        /// <param name="id">导航Id</param>
        /// <returns>数据信息</returns>
        public XFX_Bug GetDataById(int? id)
        {
            if (id != null)
            {
                return this.baseDataModel.GetDataById(id);
            }
            return null;
        }

        /// <summary>
        /// 根据id删除 XFX_Bug信息 0成功 1失败
        /// </summary>
        /// <param name="id"></param>
        public int DeleteById(int? id)
        {
            int result = 1;
            if (id != null)
            {
                try
                {
                    this.baseDataModel.DeleteById(id);
                    result = 0;
                }
                catch
                {

                }
            }

            return result;
        }

        /// <summary>
        /// 获取XFX_Bug表列表信息 不带分页
        /// </summary>
        /// <returns>返回导航信息</returns>
        public List<XFX_Bug> GetDataList()
        {
            return this.baseDataModel.GetDataList();
        }

        /// <summary>
        /// 获取XFX_Bug表列表信息 带分页
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="size">当前页数据量大小</param>
        /// <returns>返回数据集</returns>
        public List<XFX_Bug> GetDataList(int? page, int? size)
        {
            page = page == null ? 1 : page;
            size = size == null ? 10 : size;
            return this.baseDataModel.GetDataList(page, size);
        }

        /// <summary>
        /// 获取数据总量
        /// </summary>
        /// <returns></returns>
        public int GetDataTotal()
        {
            return this.baseDataModel.GetDataTotal();
        }
    }
}
