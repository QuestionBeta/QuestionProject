using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBase.AppData;

namespace DataBase
{
    public partial class MenuDB
    {
        //自定义函数方法

        public List<Menu> GetDataListByRoleId(int? roleid)
        {
            return this.datacontext.Menu.Where(p => p.roleId == roleid).ToList();
        }

        /// <summary>
        /// 获取所有父节点
        /// </summary>
        /// <returns></returns>
        public List<Menu> GetParentNodes()
        {
            List<Menu> list = new List<Menu>();
            list = datacontext.Menu.Where(p => p.pid == p.id).ToList();
            return list;
        }

        /// <summary>
        /// 获取所有子节点
        /// </summary>
        /// <returns></returns>
        public List<Menu> GetChildrenNodes(int? pid)
        {
            List<Menu> list = new List<Menu>();
            list = datacontext.Menu.Where(p => p.pid == pid && p.pid != p.id).ToList();
            return list;
        }
    }
}
