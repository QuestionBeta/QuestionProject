using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Question.Beta.Controllers;

namespace Shop.Web.Controllers
{
    public class UserControlController : BaseController
    {
        //
        // GET: /UserControl/

        [HttpPost]
        public PartialViewResult LoadCategory()
        {
            //var obj = categoryHandler.LoadPCategoryList();
            //return PartialView(obj);
            return PartialView();
        }

        public PartialViewResult LoadCategory(int? n)
        {
            //var obj = categoryHandler.LoadPCategoryList();
            //return PartialView(obj);
            return PartialView();
        }
    }
}
