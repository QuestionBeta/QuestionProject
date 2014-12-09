using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Web.Models;
using Shop.DTDAL;

namespace Shop.Web.Controllers
{
    public class CommentController : Controller
    {
        public DTUserHandler userhandler = new DTUserHandler();
        public DTCommentHandler commenthandler = new DTCommentHandler();
        //
        #region == 保存用户评论 ==
        // GET: /Comment/
        [AuthAttribute(Code = "member")]
        public ActionResult Comment(int? goodsId,int? n_type)
        {
            ViewBag.NTYPE = n_type;
            if (goodsId != null)
            {
                //shoppinghandler.GetSingleByGoodsId
                CommentModel model = new CommentModel();
                var comment = commenthandler.GetSingleByCart(goodsId);
                if (comment == null)
                {
                    return View();
                }
                model.Comment = comment.comment_content;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        [AuthAttribute(Code = "member")]
        public ActionResult Comment(int? goodsId /*购物车id*/, string comment, int? n_type /*类别：0是新增 1是编辑*/)
        {
            ViewData["goodsid"] = goodsId;
            ViewData["n_type"] = n_type;
            return RedirectToAction("SaveComment", new { goodsId = goodsId, n_type = n_type,comment = comment });
        }

        [AuthAttribute(Code = "member")]
        public ActionResult SaveComment(int? goodsId /*购物车id*/, string comment, int? n_type /*类别：0是新增 1是编辑*/)
        {
            if (goodsId != null)
            {
                //判断评论内容是否为空
                if (string.IsNullOrEmpty(comment))
                {
                    ViewBag.MSG = "评论内容不能为空";
                    return View();
                }
                //获取用户id
                long uid = 0;
                if (n_type == 0)
                {
                    var user = userhandler.GetSingleByName(HttpContext.User.Identity.Name);
                    if (user != null)
                    {
                        uid = user.id;
                    }

                    if (!shoppinghandler.IsExist(goodsId))
                    {
                        ViewBag.MSG = "商品不存在或者已经评价";
                        return View();
                    }
                }
                //添加评论
                NS_Goods_Appraisal nga = new NS_Goods_Appraisal();

                //获取商品id
                var goo = shoppinghandler.GetSingleById(goodsId);
                nga.userid = n_type == 0 ? uid : goo.userid;
                nga.goodsid = goo != null ? goo.goodsid : 0;

                switch (n_type)
                {
                    case 1: //编辑评论
                        nga = commenthandler.GetSingleByCart(goodsId);
                        nga.comment_content = comment;
                        int result1 = this.commenthandler.UpdateComment(nga);
                        ViewBag.MSG = result1 == 0 ? "评论修改成功" : "评论修改失败";
                        return View();
                    case 0:
                        nga.comment_content = comment;
                        nga.comment_time = DateTime.Now;
                        nga.comment_type = 1;
                        nga.cartid = goodsId;
                        int result = this.commenthandler.AddComment(nga);
                        ViewBag.MSG = result == 0 ? "评论发表成功" : "评论发表失败";
                        Redirect("comment/getcommentList?type=2");
                        return View();
                }
            }
            ViewBag.MSG = "商品不存在";
            Redirect("/Home/Center");
            return View();
        }

        [HttpPost]
        [AuthAttribute(Code = "member")]
        public ActionResult SaveComment()
        {
            //if (category_id == null) { category_id = int.Parse(); }
            return View();
        }
        #endregion

        //[AuthAttribute(Code = "member")]
        public ActionResult LoadComment(int goods_id, int? page, int? size)
        {
            page = page == null ? 1 : page;
            size = size == null ? 10 : size;
            int total = 0; //定义变量接收评论总数
            List<NS_Goods_Appraisal> commentList = null; //定义变量接收评论集合
            List<NS_Goods_Appraisal> currentCommentList = null; //定义变量接收当前页的评论内容
            //获取商品评论信息
            commentList = commenthandler.LoadCommentListByGoods(goods_id);
            if (commentList != null)
            {
                total = commentList.Count;
            }
            currentCommentList = commenthandler.LoadCommentListByGoods(page, size, goods_id);
            DataPager<NS_Goods_Appraisal> pager = new DataPager<NS_Goods_Appraisal>(total, currentCommentList, (int)page, (int)size, (int)0, "/Comment/LoadComment", "#comment_list", "&goods_id=" + goods_id);
            string data = pager.InitPager();
            ViewBag.Data = new MvcHtmlString(data);
            //获取
            return View(currentCommentList);
        }

        #region == 根据用户Id和是否评价获取评价和待评价列表 ==
        public ActionResult GetCommentList(int? type)
        {
            //获取用户id
            long uid = 0;
            var user = userhandler.GetSingleByName(HttpContext.User.Identity.Name);
            if (user != null)
            {
                uid = user.id;
            }
            bool iscomment = false;
            //初始化评论类型
            switch (type)
            {
                case 0:
                    iscomment = true;
                    break;
            }
            //获取订单集合
            var orderlist = orderhandler.GetMyOrder(HttpContext.User.Identity.Name, 2);
            //获取订单购物车Id
            string orderArray = "";
            foreach (var order in orderlist)
            {
                orderArray += order.cart + ",";
            }
            orderArray = orderArray.TrimEnd(',');
            string[] array = orderArray.Split(',');
            //获取指定Id的商品信息
            List<GoodsOrderModel> listModel = new List<GoodsOrderModel>();
            if (array != null && array.Count() > 0)
            {
                foreach (var i in array)
                {
                    if (string.IsNullOrEmpty(i))
                    {
                        break;
                    }
                    GoodsOrderModel goods = new GoodsOrderModel();
                    //获取购物车Id
                    var cart = this.shoppinghandler.GetSingleById(int.Parse(i), iscomment);
                    if (cart != null)
                    {
                        goods.id = int.Parse(i);
                        goods.Goods = cart.NS_Goods;
                        goods.iscomment = (bool)cart.iscomment;
                        listModel.Add(goods);
                    }
                }
            }
            ViewBag.Type = 0;
            return View(listModel);
        }
        #endregion
    }
}