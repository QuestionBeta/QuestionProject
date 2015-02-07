/*****************************************************
 * 用户自定义函数类SysUser_RoleFun 此代码是由工具自动生成
 *****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBase.AppData;

namespace WebUIFunction
{
    public partial class SysUser_RoleFun 
    {
        //此代码是由工具自动生成

        /// <summary>
        /// 通过用户id获取角色
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns>返回角色信息</returns>
        public List<SysUser_Role> GetRoleListByUser(int? userid)
        {
            if (userid == null)
            {
                userid = 0;
            }
            return this.baseDataModel.GetRoleListByUser(userid);
        }

        /// <summary>
        /// 根据用户id删除用户信息
        /// </summary>
        /// <param name="userid">用户id</param>
        public void DeleteUserRoleByUser(int? userid)
        {
            this.baseDataModel.DeleteUserRoleByUser(userid);
        }
        /// <summary>
        /// 批量新增角色
        /// </summary>
        /// <param name="list">角色列表</param>
        public void InsertDataToSysUser_RoleTable(List<SysUser_Role> list)
        {
            this.baseDataModel.InsertDataToSysUser_RoleTable(list);
        }
    }
}
