using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataBase.AppData;
using System.IO;
using Shop.Web.Models;

namespace Question.Beta.Controllers
{
    public class BugController : BaseController
    {
        //
        // GET: /Bug/

        #region -- 获取BUG列表 --
        public ActionResult BugList()
        {
            return View();
        }
        #endregion

        #region -- 添加BUG --
        [AuthAttribute(Code = "member")]
        public ActionResult AddBUG(int? id)
        {
            ViewBag.Msg = "";
            XFX_Bug bugModel = null;
            //编辑操作
            if (id != null && id != 0)
            {
                bugModel = bughandler.GetDataById(id);
                if (bugModel == null)
                {
                    ModelState.AddModelError("title", "获取标题失败！");
                    return View();
                }
                else
                {
                    //初始化分类信息
                    InitCategory(bugModel.category);
                }
            }
            else
            {
                //初始化分类信息
                InitCategory(null);
            }
            return View(bugModel);
        }

        [HttpPost]
        [AuthAttribute(Code = "member")]
        public ActionResult AddBUG(XFX_Bug bug, int? id)
        {
            //初始化分类信息
            InitCategory(bug.category);
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            string uploadFlag = string.Empty;
            //判断图片是否上传成功
            uploadFlag = ImgUpload(Request);
            
            XFX_Bug bugA = bughandler.GetDataById(id);
            if (bugA == null && id != null && id != 0)
            {
                ViewBag.Msg = "页面错误";
                return View();
            }
            string flags = "1,2";
            if (flags.Contains(uploadFlag))
            {
                string msg = string.Empty;
                switch (uploadFlag)
                {
                    case "1":
                        msg = "图片格式有误(.gif,.jpg,.png)";
                        break;
                    case "2":
                        msg = "图片大小不能超过3M";
                        break;
                }
                ModelState.AddModelError("url", msg);
                return View();
            }
            else
            {
                if (bugA != null && uploadFlag == "0")
                {
                    uploadFlag = bugA.url;
                }
            }
            
            //获取用户信息
            int uid = 0;
            var user = userhandler.GetSingleByName(HttpContext.User.Identity.Name);
            if (user != null)
            {
                uid = user.id;
            }
            //保存实体信息
            XFX_Bug bugModel = null;
            //编辑操作
            if (id != null)
            {
                bugModel = bughandler.GetDataById(id);
                if (bugModel == null)
                {
                    ModelState.AddModelError("url", "获取标题失败！");
                    return View();
                }
                else
                {

                }
            }
            else
            {
                bugModel = new XFX_Bug();
                bugModel.time = DateTime.Now;
            }

            bugModel.title = bug.title;
            bugModel.key_values = bug.key_values;
            bugModel.description = bug.description;
            bugModel.anwser = bug.anwser;
            bugModel.iscomplete = bug.iscomplete;
            bugModel.user_id = uid;
            bugModel.url = uploadFlag == "0" ? "" : uploadFlag;
            bugModel.category = bug.category;
            int result = 0;
            string returnMsg = string.Empty;
            if (id == null)
            {
                result = this.bughandler.InsertDataToXFX_BugTable(bugModel);
                ViewBag.Msg = result == 0 ? "发布成功" : "发布失败";
            }
            else
            {
                result = this.bughandler.UpdateDataToXFX_BugTable(bugModel);
                ViewBag.Msg = result == 0 ? "保存成功" : "保存失败";
            }
            
            return View();
        }
        #endregion

        #region -- BUG详情页面 --
        public ActionResult Detail(int? id)
        {
            bool flag = true;
            if (id == null)
            {
                return View();
            }
            try
            {
                int tmpId = (int)id;

            }
            catch
            {
                flag = false;
            }

            var bugData = bughandler.GetDataById(id);

            return View(bugData);
        }
        #endregion

        #region -- 上传图片处理函数 --
        //商品照片
        public ActionResult ImgUpload()
        {
            ViewBag.Result = "";
            return View();
        }

        protected string ImgUpload(HttpRequestBase Request)
        {
            bool flag = false;
            string pathBase = @"/Content/Uploads/";
            ViewBag.CurrentID = Request.Form["url"];
            int n = Request.Files.Count;
            HttpPostedFileBase postfile = Request.Files[0];
            if (postfile.ContentLength <= 0)
            {
                ViewBag.Result = "请选择上传图片";

                return "0";
            }

            string filename = string.Empty;
            string path = string.Empty;
            if (postfile != null)
            {
                //filename = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString()+postfile.ContentType;
                filename = Path.GetFileName(postfile.FileName);

                string extension = Path.GetExtension(postfile.FileName);
                string extensions = ".gif,.jpg,.png";
                if (!extensions.Contains(extension))
                {
                    return "1";
                }
                if (postfile.ContentLength > 3 * 1024 * 1024)
                {
                    return "2";
                }
                string newname = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + extension;
                try
                {
                    //保存路径
                    path = Server.MapPath(pathBase);

                    ////设置缓存32KB  
                    //int bufferSize = 1024 << 4;
                    //byte[] buffer = new byte[bufferSize];
                    //一次读取到字节数  
                    ViewBag.Result = "上传成功！";
                    flag = true;
                    ViewBag.Name = pathBase + newname;
                    filename = pathBase + newname;
                    postfile.SaveAs(path + newname);
                }
                catch
                {

                }
            }
            return filename;
        }
        #endregion

        #region -- 初始化分类 --
        void InitCategory(int? categoryId)
        {
            //初始化分类，根据分类id自动绑定分类
            var category = categoryhandler.GetDataList();
            if (categoryId == null)
            {
                var tmpData = category.Select(p => new {  name = p.name, value = p.id }).ToList();
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