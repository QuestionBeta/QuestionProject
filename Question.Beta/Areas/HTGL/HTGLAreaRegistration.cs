using System.Web.Mvc;
using System.Configuration;

namespace Question.Beta.Areas.HTGL
{
    public class HTGLAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "HTGL";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //获取webconfig
            string path = ConfigurationManager.AppSettings["BootPath"].ToString();
            context.MapRoute(
                "HTGL_default",
                path + "/{controller}/{action}/{id}",
                new { controller = "Admin", action = "Index", id = UrlParameter.Optional },
                new string[] { "Question.Beta.Areas.HTGL.Controllers" }  //新加的命名空间，用于Area
            );

            context.MapRoute(
                "HTGL1_default",
                path + "/AdminLogin/{action}/{id}",
                new { controller = "AdminLogin", action = "Index", id = UrlParameter.Optional },
                new string[] { "Question.Beta.Areas.HTGL.Controllers" }  //新加的命名空间，用于Area
            );
        }
    }
}
