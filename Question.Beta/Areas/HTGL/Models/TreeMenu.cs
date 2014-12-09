using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shop.DTModel.Admin;
using DataBase.AppData;
using WebUIFunction;

namespace Shop.DTHandler
{
    /// <summary>
    /// 格式化菜单树
    /// </summary>
    public static class TreeMenu
    {
        ///// <summary>
        ///// 初始化树菜单
        ///// </summary>
        ///// <param name="organList">节点元数据，从数据库获得</param>
        ///// <returns>返回节点数据集合</returns>
        //public static object GetNodeTreeData(List<DTMenu> organList)
        //{
        //    StringBuilder jsonString = new StringBuilder();
        //    //开始构造树格式
        //    jsonString.Append("[");

        //    DTMenuHandler menu = new DTMenuHandler();
        //    foreach (var item in menu.GetParentNodes())
        //    {
        //        CreateTreeMenu(item, jsonString, organList);
        //    }
        //    jsonString.Append("]");
        //    //....删除多余','号
        //    string[] test = jsonString.ToString().Split(']');
        //    jsonString.Clear();
        //    foreach (var item in test)
        //    {
        //        if (item != "")
        //        {
        //            jsonString.Append(item.Remove(item.Length - 1, 1) + "]");
        //        }
        //    }

        //    return jsonString;
        //}

        /// <summary>
        /// 获取节点数据集合
        /// </summary>
        /// <param name="organList">节点元数据，从数据库获得</param>
        /// <returns>返回节点数据集合</returns>
        public static object GetNodeTreeData()
        {
            MenuFun menu = new MenuFun();
            StringBuilder jsonString = new StringBuilder();
            if (menu.GetParentNodes() != null && menu.GetParentNodes().Count > 0)
            {
                //开始构造树格式
                jsonString.Append("[");

                //指定Id,PId创建树
                foreach (var item in menu.GetParentNodes())
                {
                    if (menu.GetParentNodes() != null && menu.GetParentNodes().Count > 0)
                    {
                        int m = menu.GetParentNodes().Count;

                        jsonString.Append("{text:\"" + item.name + "\",isexpand:\"" + item.isexpand.ToString().ToLower() + "\"");
                        if (menu.GetChildrenNodes(item.pid) != null)
                        {
                            int n = menu.GetChildrenNodes(item.pid).Count;
                            jsonString.Append(",children:[");
                            foreach (var child in menu.GetChildrenNodes(item.pid))
                            {
                                string lastChar = ",";
                                string children = "{id:\"" + child.id + "\",pid:\"" + item.id + "\",text:\"" + child.name + "\",url:\"" + child.url + "\"}";
                                n--;
                                if (n == 0)
                                {
                                    jsonString.Append(children);
                                }
                                else
                                {
                                    jsonString.Append(children + lastChar);
                                }
                            }
                            jsonString.Append("]");
                        }

                        m--;
                        if (m == 0)
                        {
                            jsonString.Append("}");
                        }
                        else
                        {
                            jsonString.Append("},");
                        }
                    }

                    //CreateTreeMenu(item, jsonString, menu.GetChildrenNodes(item.pid.id));           
                }
                jsonString.Append("]");

                ////删除字符串最后一个,
                //jsonString = jsonString.Remove(jsonString.Length - 2, 1);

                ////树格式构造完成
                //jsonString.Append("]");
            }

            return jsonString;
        }

        /// <summary>
        /// 递归创建树
        /// </summary>
        /// <param name="item"></param>
        /// <param name="jsonString"></param>
        /// <param name="organList"></param>
        static void CreateTreeMenu(Menu item, StringBuilder jsonString, List<Menu> organList)
        {
            //判断是否有下级节点,如果有子节点，输出子节点
            bool isLeaf = IsLeaf(item.id);
            //添加根节点
            jsonString.Append("{text:\"" + item.name + "\"");
            //var n = organList.Where(p => p.pid.id == item.id).Count();
            int temJ = 0;
            if (isLeaf)
            {
                jsonString.Append(",children:[");
                //...输出子节点                
                foreach (var child in organList)
                {
                    temJ++;
                    jsonString.Append("{id:\"" + item.id + "\",pid:\"" + item.pid + "\",text:\"" + item.name + "\",url:\"" + item.url + "\"},");
                    CreateTreeMenu(child, jsonString, organList);
                }
                jsonString.Append("]},");
            }
            else
            {
                jsonString.Append("},");
            }
        }

        /// <summary>
        /// 判断是否有子节点 是返回true,否返回false
        /// </summary>
        /// <param name="id">节点Id</param>
        /// <returns>是</returns>
        static Boolean IsLeaf(int? id)
        {
            MenuFun aa = new MenuFun();
            if (aa.GetDataById(id) == null)
            {
                return false;
            }

            return true;
        }
    }
}
