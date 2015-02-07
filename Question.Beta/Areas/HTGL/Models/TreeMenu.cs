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

        #region -- Juqery LigerUITree 初始化树处理方法 --
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
        /// 获取节点数据集合
        /// </summary>
        /// <param name="organList">节点元数据，从数据库获得</param>
        /// <returns>返回节点数据集合</returns>
        public static object GetNodeTreeData(List<Menu> list)
        {
            MenuFun menu = new MenuFun();
            StringBuilder jsonString = new StringBuilder();
            var parentMenu = list.Where(p => p.id == p.pid).OrderByDescending(p => p.pid).ToList();
            if (parentMenu != null && parentMenu.Count > 0)
            {
                //开始构造树格式
                jsonString.Append("[");

                //指定Id,PId创建树
                foreach (var item in parentMenu)
                {
                    if (parentMenu != null && parentMenu.Count > 0)
                    {
                        int m = parentMenu.Count;

                        jsonString.Append("{text:\"" + item.name + "\",isexpand:\"" + item.isexpand.ToString().ToLower() + "\"");
                        if (list.Where(p => p.pid != p.id && p.pid == item.pid) != null)
                        {
                            int n = menu.GetChildrenNodes(item.pid).Count;
                            jsonString.Append(",children:[");
                            foreach (var child in list.Where(p => p.pid != p.id && p.pid == item.pid))
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

        #endregion

        #region -- 自定义JqueryTree插件树 --
        /// <summary>
        /// 自定义插件树 获取节点数据集合
        /// </summary>
        /// <param name="organList">节点元数据，从数据库获得</param>
        /// <returns>返回节点数据集合</returns>
        public static object GetJqueryNodeTreeData()
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

                        jsonString.Append("{\"name\":\"" + item.name + "\",\"isexpend\":\"" + item.isexpand.ToString().ToLower() + "\",\"iconCls\":\"123\",\"id\":\"" + item.id.ToString() + "\",\"pid\":\"" + item.pid.ToString() + "\"");
                        if (menu.GetChildrenNodes(item.pid) != null)
                        {
                            int n = menu.GetChildrenNodes(item.pid).Count;
                            jsonString.Append(",\"children\":[");
                            foreach (var child in menu.GetChildrenNodes(item.pid))
                            {
                                string lastChar = ",";
                                string children = "{\"id\":\"" + child.id + "\",\"pid\":\"" + item.id + "\",\"name\":\"" + child.name + "\",\"url\":\"" + child.url + "\",\"iconCls\":\"123\"}";
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
                jsonString = jsonString.Remove(jsonString.Length - 2, 1);

                ////树格式构造完成
                //jsonString.Append("]");
            }

            return jsonString;
        }

        /// <summary>
        /// 自定义插件树 获取节点数据集合
        /// </summary>
        /// <param name="organList">节点元数据，从数据库获得</param>
        /// <returns>返回节点数据集合</returns>
        public static object GetJqueryNodeTreeData(List<Menu> list)
        {
            StringBuilder jsonString = new StringBuilder();
            var parentMenu = list.Where(p => p.id == p.pid).OrderByDescending(p => p.pid).ToList();
            if (parentMenu != null && parentMenu.Count > 0)
            {
                //开始构造树格式
                jsonString.Append("[");
                int m = parentMenu.Count;
                //指定Id,PId创建树
                foreach (var item in parentMenu)
                {
                    if (parentMenu != null && parentMenu.Count > 0)
                    {
                        

                        jsonString.Append("{\"name\":\"" + item.name + "\",\"isexpend\":\"" + item.isexpand.ToString().ToLower() + "\",\"iconCls\":\""+ item.iconcls +"\",\"id\":\"" + item.id.ToString() + "\",\"pid\":\"" + item.pid.ToString() + "\"");
                        if (list.Where(p => p.pid == item.pid && p.pid != p.id) != null)
                        {
                            int n = list.Where(p => p.pid == item.pid && p.pid != p.id).ToList().Count;
                            jsonString.Append(",\"children\":[");
                            foreach (var child in list.Where(p => p.pid == item.pid && p.pid != p.id))
                            {
                                string lastChar = ",";
                                string children = "{\"id\":\"" + child.id + "\",\"pid\":\"" + item.id + "\",\"name\":\"" + child.name + "\",\"url\":\"" + child.url + "\",\"iconCls\":\""+ child.iconcls +"\"}";
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
                ////jsonString = jsonString.Remove(jsonString.Length - 2, 1);

                ////树格式构造完成
                //jsonString.Append("]");
            }

            return jsonString;
        }

        /// <summary>
        /// 自定义插件树 递归创建树
        /// </summary>
        /// <param name="item"></param>
        /// <param name="jsonString"></param>
        /// <param name="organList"></param>
        static void CreateJqueryTreeMenu(Menu item, StringBuilder jsonString, List<Menu> organList)
        {
            //判断是否有下级节点,如果有子节点，输出子节点
            bool isLeaf = IsLeaf(item.id);
            //添加根节点
            jsonString.Append("{name:\"" + item.name + "\",iconCls=\"\"");
            //var n = organList.Where(p => p.pid.id == item.id).Count();
            int temJ = 0;
            if (isLeaf)
            {
                jsonString.Append(",children:[");
                //...输出子节点                
                foreach (var child in organList)
                {
                    temJ++;
                    jsonString.Append("{id:\"" + item.id + "\",pid:\"" + item.pid + "\",name:\"" + item.name + "\",url:\"" + item.url + "\",iconCls=\"\"},");
                    CreateJqueryTreeMenu(child, jsonString, organList);
                }
                jsonString.Append("]},");
            }
            else
            {
                jsonString.Append("},");
            }
        }
        #endregion

        #region -- 公共处理函数 --
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
        #endregion
    }
}
