using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Question.Beta.Controllers
{
    public class PersonCenterController : Controller
    {
        //
        // GET: /PersonCenter/

        public ActionResult Center()
        {
            return View("Index","_Layout");
        }
    }
}