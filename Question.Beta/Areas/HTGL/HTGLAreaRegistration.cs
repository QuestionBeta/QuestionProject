using System.Web.Mvc;

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
            context.MapRoute(
                "HTGL_default",
                "HTGL/{controller}/{action}/{id}",
                new { controller = "Admin", action = "PersonCenter", id = UrlParameter.Optional },
                new string[] { "Question.Beta.Areas.HTGL.Controllers" }  //新加的命名空间，用于Area
            );
        }
    }
}
