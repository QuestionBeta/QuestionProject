using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Shop.Web.Models;
using System.Configuration;

namespace Question.Beta
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ErrorAttribute());
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "MyQuestion", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                new string[] { "Question.Beta.Controllers" }  
            );
            routes.MapRoute(
                "Bug", // Route name
                "{controller}/{action}/{mode}/{id}", // URL with parameters
                new { controller = "Bug", action = "AddBug", mode = "", id = UrlParameter.Optional }, // Parameter defaults
                new string[] { "Question.Beta.Controllers" }   
            );

            routes.MapRoute(
                "BugDefault", // Route name
                "{controller}/{action}/{mode}/{id}", // URL with parameters
                new { controller = "Bug", action = "AddBug", mode = "", id = UrlParameter.Optional }, // Parameter defaults
                new string[] { "Question.Beta.Controllers" }  
            );

            //routes.MapRoute(
            //    "Admin_Default",
            //    "{controller}/{action}/{id}",
            //    new { controller = "Admin", action = "Index", id = UrlParameter.Optional },
            //    new string[] { "Question.Beta.Areas.HTGL.Controllers" }
            //    );
            string path = ConfigurationManager.AppSettings["BootPath"].ToString();
            routes.MapRoute(
                "AdminLogin_Default",
                path + "/{controller}/{action}/{id}",
                new { controller = "AdminLogin", action = "Index", id = UrlParameter.Optional },
                new string[] { "Question.Beta.Areas.HTGL.Controllers" }
                );  
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}