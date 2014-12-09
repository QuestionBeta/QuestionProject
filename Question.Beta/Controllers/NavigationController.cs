using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataBase.AppData;

namespace Question.Beta.Controllers
{
    public class NavigationController : BaseController
    {
        //
        // GET: /Navigation/

        public ActionResult GetNavigation()
        {
            List<XFX_Navigation> data = navigation.GetDataList();
            return View(data);
        }
    }
}
