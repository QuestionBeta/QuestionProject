using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataBase.AppData;
using Question.Beta.Models;

namespace Question.Beta.Controllers
{
    public class SearchController : BaseController
    {
        //
        // GET: /Search/

        public ActionResult SearchResult(string id = "0"/* 关键字*/, string mode = "0"/*分类Id*/)
        {
            string keywords = string.Empty;
            int cid = 0;
            //初始化关键字和分类
            if (mode == "0" && id != "0")
            {
                keywords = id;
                InitCategory(null);
            }
            else if (mode != "0" && id == "0")
            {
                InitCategory(int.Parse(mode));
            }
            else
            {
                if (id != "0")
                {
                    keywords = id;
                }

                if (mode != "0")
                {
                    InitCategory(int.Parse(mode));
                }
                else
                {
                    InitCategory(null);
                }
            }

            List<XFX_Bug> bugList = new List<XFX_Bug>();
            if (!string.IsNullOrEmpty(keywords))
            {
                //若关键字长度大于50 则保留50字
                if (keywords.Length > 30)
                {
                    keywords = keywords.Remove(30, keywords.Length - 30);
                }
                //若关键字包括空格，则删除空格
                if (keywords.Contains(" "))
                {
                    keywords = keywords.Replace(" ", "");
                }
                //获取数据
                if (mode != "0")
                {
                    bugList = bughandler.GetBugListByKeywords(1, 15, keywords, int.Parse(mode));
                    ViewBag.Total = bughandler.GetBugListByKeywords(keywords, int.Parse(mode)).Count;
                }
                else
                {
                    bugList = bughandler.GetBugListByKeywords(1, 15, keywords);
                    ViewBag.Total = bughandler.GetBugListByKeywords(keywords).Count;
                }
            }
            else
            {
                //当关键字为空时根据分类获取数据
                if (mode != "0")
                {
                    bugList = bughandler.GetBugListByKeywords(1, 15, int.Parse(mode));
                    ViewBag.Total = bughandler.GetBugListByKeywords(int.Parse(mode)).Count;
                }
                else
                {
                    bugList = bughandler.GetDataListWherePublish(1, 15);
                    ViewBag.Total = bughandler.GetDataList().Count;
                }
            }
            //设置列表分页
            DataPager.InitPagerControl<XFX_Bug> pagerControl = new DataPager.InitPagerControl<XFX_Bug>(ViewBag.Total, bugList, 1, 15, 0, "/Search/LoadData", "#data_list", "&keyword=" + keywords + "&category=" + mode);
            //pagerControl.list
            string mvcPager = pagerControl.InitPager();
            //ViewBag.Data = new MvcHtmlString(mvcPager);
            //获取分类列表
            var category = categoryhandler.GetDataList();

            SearchModel searchModel = new SearchModel() { keywords = keywords, PagerHtml = mvcPager, ListBug = bugList, CategoryList = (SelectList)ViewData["value"] };

            return View("BugList", null, searchModel);
        }

        //加载搜索结果
        public PartialViewResult LoadSearchResult()
        {
            return PartialView();
        }

        //加载显示BUG列表 图文
        public ActionResult LoadData(int? page, int? size, string keyword, string category)
        {
            page = page == null ? 1 : page;
            size = size == null ? 12 : size;
            //获取BUG列表
            List<XFX_Bug> bugList = new List<XFX_Bug>();
            //if (!string.IsNullOrEmpty(keyword))
            //{
            //    dataList = bughandler.GetBugListByKeywords(page, size, keyword);
            //    ViewBag.Total = bughandler.GetBugListByKeywords(keyword).Count;
            //}
            //else
            //{
            //    dataList = bughandler.GetDataList(page, size);
            //    ViewBag.Total = bughandler.GetDataList().Count;
            //}

            if (!string.IsNullOrEmpty(keyword))
            {
                //获取数据
                if (category != "0")
                {
                    bugList = bughandler.GetBugListByKeywords(page, size, keyword, int.Parse(category));
                    ViewBag.Total = bughandler.GetBugListByKeywords(keyword, int.Parse(category)).Count;
                }
                else
                {
                    bugList = bughandler.GetBugListByKeywords(page, size, keyword);
                    ViewBag.Total = bughandler.GetBugListByKeywords(keyword).Count;
                }
            }
            else
            {
                //当关键字为空时根据分类获取数据
                if (category != "0")
                {
                    bugList = bughandler.GetBugListByKeywords((int)page, (int)size, int.Parse(category));
                    ViewBag.Total = bughandler.GetBugListByKeywords(int.Parse(category)).Count;
                }
                else
                {
                    bugList = bughandler.GetDataList(page, (int)size);
                    ViewBag.Total = bughandler.GetDataList().Count;
                }
            }

            //设置列表分页
            DataPager.InitPagerControl<XFX_Bug> pagerControl = new DataPager.InitPagerControl<XFX_Bug>(ViewBag.Total, bugList, (int)page, (int)size, 0, "/Search/LoadData", "#data_list", "&keyword=" + keyword + "&category=" + category);
            //pagerControl.list
            string mvcPager = pagerControl.InitPager();
            ViewBag.Data = new MvcHtmlString(mvcPager);
            return View(bugList);
        }

        #region -- 初始化分类 --
        void InitCategory(int? categoryId)
        {
            var category = categoryhandler.GetDataList();
            if (categoryId == null)
            {
                var tmpData = category.Select(p => new { name = p.name, value = p.id }).ToList();
                ViewData["value"] = new SelectList(tmpData, "value", "name");
            }
            else
            {
                var tmpData = category.Select(p => new { name = p.name, value = p.id }).ToList();
                ViewData["value"] = new SelectList(tmpData, "value", "name", categoryId);
            }
        }
        #endregion
    }
}