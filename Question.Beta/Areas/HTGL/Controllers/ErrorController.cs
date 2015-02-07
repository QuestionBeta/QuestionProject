using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.DTModel.Admin;

namespace Question.Beta.Areas.HTGL.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /HTGL/Error/

        public ActionResult Show()
        {
            DTError error = new DTError();
            if (Session["error"] != null)
            {
                error = (DTError)Session["error"];
            }
            else
            {
                error.Message = "未找到网页";
            }

            return View(error);
        }

    }
}
