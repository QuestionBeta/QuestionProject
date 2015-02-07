/*****************************************************
 * 用户自定义函数类SysRoleMenuFun 此代码是由工具自动生成
 *****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBase.AppData;

namespace WebUIFunction
{
    public partial class SysRoleMenuFun 
    {
        //此代码是由工具自动生成
        /// <summary>
        /// 根据角色id删除角色功能项关联信息
        /// </summary>
        /// <param name="roleid">角色id</param>
        public void DeleteRoleMenuByRole(int? roleid)
        {
            this.baseDataModel.DeleteRoleMenuByRole(roleid);
        }

        /// <summary>
        /// 批量新增SysRoleMenu信息 0 成功 1失败
        /// </summary>
        /// <param name="model">导航信息</param>
        public void InsertDataToSysRoleMenuTable(List<SysRoleMenu> model)
        {
            this.baseDataModel.InsertDataToSysRoleMenuTable(model);
        }

        /// <summary>
        /// 根据角色获取角色功能项关联列表
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <returns>返回功能项信息</returns>
        public List<SysRoleMenu> GetDataListByRole(int? roleid)
        {
            return this.baseDataModel.GetDataListByRole(roleid);
        }
    }
}
