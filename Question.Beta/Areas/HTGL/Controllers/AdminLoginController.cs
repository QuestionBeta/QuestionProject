using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Question.Beta.Controllers;

namespace Question.Beta.Areas.HTGL.Controllers
{
    public class AdminLoginController : BaseController
    {
        //
        // GET: /HTGL/AdminLogin/

        public ActionResult Index()
        {
            return View();
        }

    }
}
