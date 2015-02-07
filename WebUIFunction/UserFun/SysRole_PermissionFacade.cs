/*****************************************************
 * 用户自定义函数类SysRole_PermissionFun 此代码是由工具自动生成
 *****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBase.AppData;

namespace WebUIFunction
{
    public partial class SysRole_PermissionFun 
    {
       //此代码是由工具自动生成

        /// <summary>
        /// 根据角色Id获取权限
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns>返回角色下权限集合</returns>
        public List<SysRole_Permission> GetPermissionListByRole(int? roleId)
        {
            if (roleId == null) { roleId = 0; }
            return this.baseDataModel.GetPermissionListByRole(roleId);
        }

        /// <summary>
        /// 判断权限是否已经分配 true已分配，false 未分配
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="permissionid">权限id</param>
        /// <returns>true or false</returns>
        public bool IsExist(int? roleid, int? permissionid)
        {
            return this.baseDataModel.IsExist(roleid, permissionid);
        }

        /// <summary>
        /// 根据角色Id删除角色权限关联信息 0成功 1失败
        /// </summary>
        /// <param name="roleId">角色id</param>
        public void DeletePermissionRoleByRole(int? roleId)
        {
            this.baseDataModel.DeletePermissionRoleByRole(roleId);
        }
    }
}