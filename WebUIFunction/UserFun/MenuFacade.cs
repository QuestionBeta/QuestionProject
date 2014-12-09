using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBase.AppData;

namespace WebUIFunction
{
    public partial class MenuFun
    {
        //自定义函数

        //获取菜单列表
        public List<Menu> GetMenuList(int? id)
        {
            if (id == null)
            {
                return null;
            }

            return this.baseDataModel.GetDataListByRoleId(id);
        }

        /// <summary>
        /// 获取所有父节点
        /// </summary>
        /// <returns></returns>
        public List<Menu> GetParentNodes()
        {
            return this.baseDataModel.GetParentNodes();
        }

        /// <summary>
        /// 获取所有子节点
        /// </summary>
        /// <returns></returns>
        public List<Menu> GetChildrenNodes(int? pid)
        {
            if (pid == null)
            {
                return null;
            }

            return this.baseDataModel.GetChildrenNodes(pid);
        }
    }
}
