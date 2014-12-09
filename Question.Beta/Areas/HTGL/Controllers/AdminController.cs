using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Question.Beta.Controllers;
using Shop.Web.Models;
using Shop.DTHandler;

namespace Question.Beta.Areas.HTGL.Controllers
{
    public class AdminController : BaseController
    {
        //
        // GET: /HTGL/Admin/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PersonCenter()
        {
            return View("Index");
        }

        public ActionResult Center()
        {
            return View();
        }

        
        /// <summary>
        /// 加载左侧菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string LeftMenuNav()
        {
            var data = menuhandler.GetDataList();
            object obj = null;
            //List<DTMenu> list = new List<DTMenu>();
            //list = (List<DTMenu>)data;

            if (data != null)
            {
                obj = TreeMenu.GetNodeTreeData();
            }

            ////设置版权
            //string copyright = System.Configuration.ConfigurationSettings.AppSettings["copyright"].ToString();
            //ViewBag.CopyRight = copyright;

            ////IEnumerable<DTMenu> dtmenu = TreeMenu.GetNodeTreeData(dtmenu);
            return obj.ToString();
        }
    }
}
