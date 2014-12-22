using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataBase.AppData;
using Question.Beta.Helper;

namespace Question.Beta.Controllers
{
    public class MyQuestionController : BaseController
    {
        //
        // GET: /MyQuestion/

        public ActionResult Index()
        {
            int? page = null;
            int? size = null;
            page = page == null ? 1 : page;
            size = size == null ? 12 : size;
            //获取BUG列表
            var dataList = bughandler.GetDataList(page, size);
            ViewBag.List = dataList;
            var datanewList = bughandler.GetDataList(1, 10).OrderByDescending(p => p.time).ToList();
            ViewBag.NewsList = datanewList;
            //设置列表分页
            DataPager.InitPagerControl<XFX_Bug> pagerControl = new DataPager.InitPagerControl<XFX_Bug>(bughandler.GetDataTotal(), dataList, (int)page, (int)size, 0, "/MyQuestion/LoadData", "#data_list", "");
            //pagerControl.list
            string mvcPager = pagerControl.InitPager();
            ViewBag.Data = new MvcHtmlString(mvcPager);
            //获取分类信息
            var categoryDataList = categoryhandler.GetDataList();
            ViewBag.Category = categoryDataList;
            EmailHelper email = new EmailHelper("smtp.exmail.qq.com", "jacklision@foxmail.com ", "liweili0721", "1967270996@qq.com");
            email.SendEmail("这是测试内容", "欢迎使用BUG知识库管理系统");
            return View();
        }

        //首页加载显示BUG列表 图文
        public ActionResult LoadData(int? page,int? size)
        {
            page = page == null ? 1 : page;
            size = size == null ? 12 : size;
            //获取BUG列表
            var dataList = bughandler.GetDataList(page, size);
            //设置列表分页
            DataPager.InitPagerControl<XFX_Bug> pagerControl = new DataPager.InitPagerControl<XFX_Bug>(bughandler.GetDataTotal(), dataList, (int)page, (int)size, 0, "/MyQuestion/LoadData", "#data_list", "");
            //pagerControl.list
            string mvcPager = pagerControl.InitPager();
            ViewBag.Data = new MvcHtmlString(mvcPager);
            return View(dataList);
        }

        //首页加载分类信息
        public ActionResult LoadCategory()
        {
            return View();
        }

        //首页 加载最新动态信息
        public ActionResult LoadLastedActicle()
        {
            return View();
        }
    }
}
