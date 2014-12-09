using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBase;
using DataBase.AppData;

namespace WebUIFunction
{
    public partial class Fun
    {
        /// <summary>
        /// 数据访问变量
        /// </summary>
        public DB  baseDataModel = null;

        /// <summary>
        /// 自定义构造函数
        /// </summary>
        public Fun() 
		{ 
			baseDataModel = new DB();
		}

        /// <summary>
        /// 新增信息 0 成功 1失败
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int InsertDataToTable( model)
        {
            int result = 1;
            if (model != null)
            {
                try
                {
                    baseDataModel.InsertDataToTable(model);
                    result = 0;
                }
                catch
                {

                }
            }

            return result;
        }

        /// <summary>
        /// 编辑信息 0 成功 1失败
        /// </summary>
        /// <param name="model">模型实体信息</param>
        /// <returns>0/1</returns>
        public int UpdateDataToTable( model)
        {
            int result = 1;
            if (model != null)
            {
                try
                {
                    baseDataModel.UpdateDataToTable(model);
                    result = 0;
                }
                catch
                {

                }
            }

            return result;
        }

        /// <summary>
        /// 根据 id获取信息
        /// </summary>
        /// <param name="id">导航Id</param>
        /// <returns>数据信息</returns>
        public  GetDataById(int? id)
        {
            if (id != null)
            {
                return this.baseDataModel.GetDataById(id);
            }
            return null;
        }

        /// <summary>
        /// 根据id删除 信息 0成功 1失败
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
        /// 获取表列表信息 不带分页
        /// </summary>
        /// <returns>返回导航信息</returns>
        public List<> GetDataList()
        {
            return this.baseDataModel.GetDataList();
        }

        /// <summary>
        /// 获取表列表信息 带分页
        /// </summary>
        /// <param name="page">页数</param>
        /// <param name="size">当前页数据量大小</param>
        /// <returns>返回数据集</returns>
        public List<> GetDataList(int? page, int? size)
        {
            page = page == null ? 1 : page;
            size = size == null ? 10 : size;
            return this.baseDataModel.GetDataList(page, size);
        }
    }
}
