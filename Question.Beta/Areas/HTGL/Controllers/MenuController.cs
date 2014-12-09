using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Shop.DTModel.Admin;
using System.Collections;
using System.Data.SqlClient;
using Shop.DTDAL;
using System.IO;
using Question.Beta.Controllers;
using DataBase.AppData;

namespace Question.Beta.Areas.HTGL.Controllers
{
    public class MenuController : BaseController
    {
        //
        // GET: /Menu/

        public ActionResult List()
        {
            ListPMenu("");
            return View();
        }

        /// <summary>
        /// 获取父菜单
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        private List<GetModel> ListPMenu(string pid)
        {
            List<GetModel> list = new List<GetModel>();
            List<Menu> listMenu = new List<Menu>();
            listMenu = menuhandler.GetParentNodes();
            if (listMenu != null && listMenu.Count > 0)
            {
                foreach (var item in listMenu)
                {
                    GetModel table = new GetModel();
                    table.name = item.name;
                    table.pid = item.pid.ToString();
                    //list.Add(select);
                    list.Add(table);
                }


                if (string.IsNullOrEmpty(pid))
                {
                    ViewData["Value"] = new SelectList(list, "pid", "name");
                }
                else
                {
                    //List<GetModel> list = ListPMenu();
                    ViewData["Value"] = new SelectList(list, "pid", "name", pid);
                }
            }
            else
            {
                GetModel table = new GetModel();
                table.name = "";
                table.pid = "";
                //list.Add(select);
                list.Add(table);
            }
            
            return list;
        }

        //private void ListFilePath(string url)
        //{
        //    DirectoryInfo d = new DirectoryInfo(Server.MapPath("/Areas/Admin/Views"));
        //    ArrayList list = FileManager.GetFileNameByAssembly(d);
        //    List<GetFileModel> l = new List<GetFileModel>();
        //    if (list != null && list.Count > 0)
        //    {
        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            GetFileModel file = new GetFileModel();
        //            file.url = list[i].ToString();
        //            file.value = list[i].ToString();
        //            l.Add(file);
        //        }
        //    }

        //    if (l != null && l.Count > 0)
        //    {
               
        //    }
        //    else
        //    {
        //        GetFileModel table = new GetFileModel();
        //        table.url = "";
        //        table.value = "";
        //        //list.Add(select);
        //        l.Add(table);
        //    }

        //    if (string.IsNullOrEmpty(url))
        //    {
        //        ViewData["Url"] = new SelectList(l, "url", "value");
        //    }
        //    else
        //    {
        //        //List<GetModel> list = ListPMenu();
        //        ViewData["Url"] = new SelectList(l, "url", "value", url);
        //    }
        //}

        public ActionResult GetData(int? page, int? rows)
        {
            page = page == null ? 1 : (int)page;
            rows = rows == null ? 10 : (int)rows;
            List<Menu> dt = new List<Menu>();
            dt = menuhandler.GetDataList(page, rows);
            int total = menuhandler.GetDataList().Count;
            var j = new { total = total, rows = dt };
            return Json(j, JsonRequestBehavior.AllowGet);
        }

        #region 菜单操作方法
        public int AddMenu(string name, bool type, string url, bool? isexpand, int? pid)
        {
            Menu menu = new Menu();
            menu.name = name;
            menu.isexpand = isexpand;
            menu.defaultvalue = false;
            menu.type = type;
            if (pid != null)
            {
                menu.pid = pid;
            }
            menu.time = DateTime.Now;
            menu.url = url;
            int result = -1;
            result = menuhandler.InsertDataToMenuTable(menu);
            return result;
        }

        //删除菜单
        public ActionResult DeleteMenu(int? id)
        {
            //查找子菜单
            int result = menuhandler.DeleteById(id);
            if (result == 1)
            {
                return Content("该菜单下有子菜单，请删除后再进行删除本菜单");
            }
            else if (result == 0)
            {
                return Content("0");
            }
            else
            {
                return Content("删除失败");
            }
        }

        //编辑菜单
        public PartialViewResult UpdateMenu(int? id, int? type)
        {
            id = id == null ? 0 : id;
            Menu menu = menuhandler.GetDataById(id);
            //判断是否为编辑 0为编辑 1为新增
            if (type != null)
            {               
                if (menu != null)
                {
                        //编辑操作 是否有子菜单
                        var data = this.menuhandler.GetDataById(menu.pid);
                        if (data != null)
                        {
                            ListPMenu(data.pid.ToString());
                            //ListFilePath(data.url);
                        }
                        else
                        {
                            ListPMenu("");
                            //ListFilePath("");
                        }
                }
                else
                {
                    ListPMenu("");
                    //ListFilePath("");
                    return PartialView();
                }

                switch (type)
                {
                    case 0:
                        return PartialView(menu);
                    case 1:
                        return PartialView();
                }
            }

            return PartialView();
        }
        
        [HttpPost]
        public ViewResult LeftMenuNav(int? j)
        {
            return View();
        }

        [HttpPost]
        //编辑处理程序
        public PartialViewResult UpdateMenu()
        {
            return PartialView();
        }

        public PartialViewResult AddMenuInfo()
        {
            ListPMenu("");
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult AddMenuInfo(int? j)
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult SaveMenuInfo(Menu menuModel,int? pid,bool htype)
        {
            var menus = menuhandler.GetDataById(menuModel.id);
            menus.type = (bool)htype;
            menus.id = menuModel.id;
            //子菜单
            if (!htype)
            {
                menus.pid = pid;
                menus.url = menuModel.url;
            }
            else
            {
                menus.isexpand = menuModel.isexpand;                
            }

            menus.name = menuModel.name;
            int result = menuhandler.UpdateDataToMenuTable(menus);
            if (result == 0)
            {
                return Content("0");
            }

            if (result == 1)
            {
                return Content("编辑失败！");
            }

            if (result == 2)
            {
                return Content("数据格式错误");
            }
            return View();
        }
        #endregion

        //DataRow GetPId(string pid)
        //{
        //    string sql = "select id,name from [NS.Admin.Menu] where id=" + pid;
        //    DataTable dt = sqlBase.GetListNoPage(sql, "");
        //    if (dt != null)
        //    {
        //        return dt.Rows[0];
        //    }

        //    return null;
        //}
        class GetModel
        {
            public string pid { get; set; }
            public string name { get; set; }
        }
        class GetFileModel
        {
            public string url { get; set; }
            public string value { get; set; }
        }
    }
}
