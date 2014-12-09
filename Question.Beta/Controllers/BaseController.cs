using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUIFunction;
using Shop.Web.Models;

namespace Question.Beta.Controllers
{
    [ErrorAttribute]
    public class BaseController : Controller
    {
        protected XFX_NavigationFun navigation = null;
        protected UserFun userhandler = null;
        protected XFX_BugFun bughandler = null;
        protected CategoryFun categoryhandler = null;
        protected MenuFun menuhandler = null;
        public BaseController() 
        {
            navigation = new XFX_NavigationFun();
            userhandler = new UserFun();
            bughandler = new XFX_BugFun();
            categoryhandler = new CategoryFun();
            menuhandler = new MenuFun();
        }
    }
}
