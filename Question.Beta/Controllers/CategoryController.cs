using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Question.Beta.Controllers
{
    public class CategoryController : BaseController
    {
        //
        // GET: /Category/

        public ActionResult LoadData()
        {
            var categoryDataList = categoryhandler.GetDataList();
            return View(categoryDataList);
        }
    }
}
