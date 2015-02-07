using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataBase.AppData;
using Question.Beta.Helper;
using Shop.Web.Models;
using Shop.Web.Helper;

namespace Question.Beta.Controllers
{
    public class MyQuestionController : BaseController
    {
        //
        // GET: /MyQuestion/
        /// <summary>
        /// 默认页数
        /// </summary>
        protected string DefaultPage = string.Empty;
        /// <summary>
        /// 默认条数
        /// </summary>
        protected string DefaultSize = string.Empty;

        public MyQuestionController()
        {
            DefaultPage = "1";
            DefaultSize = "12";
        }
     
        public ActionResult Index()
        {
            int? page = null;
            int? size = null;
            page = page == null ? int.Parse(DefaultPage) : page;
            size = size == null ? int.Parse(DefaultSize) : size;
            //获取BUG列表            
            var datanewList = bughandler.GetDataListWherePublish(int.Parse(DefaultPage), int.Parse(DefaultSize));
            ////获取BUG列表            
            //var datanewList = bughandler.GetDataListWherePublish(int.Parse(DefaultPage), int.Parse(DefaultSize));
            ViewBag.List = datanewList;
            //设置列表分页
            DataPager.InitPagerControl<XFX_Bug> pagerControl = new DataPager.InitPagerControl<XFX_Bug>(bughandler.GetDataTotalWherePublish(), datanewList, (int)page, (int)size, 0, "/MyQuestion/LoadData", "#data_list", "");
            //pagerControl.list
            string mvcPager = pagerControl.InitPager();
            ViewBag.Data = new MvcHtmlString(mvcPager);
            //获取分类信息
            var categoryDataList = categoryhandler.GetDataList();
            ViewBag.Category = categoryDataList;
            return View();
        }
        
        //首页加载显示BUG列表 图文
        public ActionResult LoadData(int? page,int? size)
        {
            page = page == null ? int.Parse(DefaultPage) : page;
            size = size == null ? int.Parse(DefaultSize) : size;
            //获取BUG列表
            var dataList = bughandler.GetDataListWherePublish(page, size);
            //设置列表分页
            DataPager.InitPagerControl<XFX_Bug> pagerControl = new DataPager.InitPagerControl<XFX_Bug>(bughandler.GetDataTotalWherePublish(), dataList, (int)page, (int)size, 0, "/MyQuestion/LoadData", "#data_list", "");
            //pagerControl.list
            string mvcPager = pagerControl.InitPager();
            ViewBag.Data = new MvcHtmlString(mvcPager);
            return View(dataList);
        }

        [AuthAttribute(Code = "1")]
        //首页加载显示BUG列表 图文
        public ActionResult GetBugList(string id)
        {
            int page  = 1;
            int size = 5;
            int user_id = 0;
            int total = 0;
            user_id = GetCurrentUserId(HttpContext.User.Identity.Name);
            List<XFX_Bug> dataList = new List<XFX_Bug>();
            if (string.IsNullOrEmpty(id))
            {
                dataList = bughandler.GetDataList(page, size, user_id).ToList();
                total = bughandler.GetDataList(user_id).Count();                
            }
            else
            {
                dataList = bughandler.GetDataList(page, size, user_id).Where(p => p.title.Contains(id)).ToList();
                total = bughandler.GetDataList(user_id).Where(p => p.title.Contains(id)).Count();
                //保存关键字
                ViewBag.Keywords = id;
            }
            
            ViewBag.NewData = dataList;
            //设置列表分页
            DataPager.InitPagerControl<XFX_Bug> pagerControl = new DataPager.InitPagerControl<XFX_Bug>(total, dataList, (int)page, (int)size, 0, "/MyQuestion/LoadDataByPage", "#data_list", "&keywords=" + ViewBag.Keywords);
            //pagerControl.list
            string mvcPager = pagerControl.InitPager();
            ViewBag.Data = new MvcHtmlString(mvcPager);
            return View();
        }

        //获取分页数据
        public ActionResult LoadDataByPage(int? page, int? size, string keywords)
        {
            page = page == null ? 1 : page;
            size = size == null ? 5 : size;
            int user_id = 0;
            int total = 0;
            user_id = GetCurrentUserId(HttpContext.User.Identity.Name);
            List<XFX_Bug> dataList = new List<XFX_Bug>();
            //获取BUG列表
            if (string.IsNullOrEmpty(keywords))
            {
                dataList = bughandler.GetDataList(page, size, user_id).ToList();
                total = bughandler.GetDataList(user_id).Count;
            }
            else
            {
                ViewBag.KeyWords = keywords;
                dataList = bughandler.GetDataList(page, size, user_id).Where(p =>  p.title.Contains(keywords)).ToList();
                total = bughandler.GetDataList(user_id).Where(p => p.title.Contains(keywords)).Count();
            }
            
            //设置列表分页
            DataPager.InitPagerControl<XFX_Bug> pagerControl = new DataPager.InitPagerControl<XFX_Bug>(total, dataList, (int)page, (int)size, 0, "/MyQuestion/LoadDataByPage", "#data_list", "");
            //pagerControl.list
            string mvcPager = pagerControl.InitPager();
            ViewBag.Data = new MvcHtmlString(mvcPager);
            return View(dataList);
        }

        [AuthAttribute(Code = "1")]
        //个人中心新增BUG
        public ActionResult AddBug(string id)
        {
            return RedirectToAction("AddBug", "Bug", new { id = id });
        }
        [HttpPost]
        [AuthAttribute(Code = "1")]
        //删除BUG
        public ActionResult DeleteBug(string id)
        {
            //判断参数为空
            if (string.IsNullOrEmpty(id))
            {
                return Content("请选择行");
            }
            //解密参数，根据参数删除记录
            id = GetDeCodeStr(id);
            int tmpId = 0;
            int tmp = 0;
            if (!int.TryParse(id, out tmp))
            {
                return Content("数据格式转换失败");
            }
            tmpId = int.Parse(id);
            int result = bughandler.DeleteById(tmpId);
            if (result == 0)
            {
                return Content(result.ToString());
            }

            return Content("删除失败");
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
