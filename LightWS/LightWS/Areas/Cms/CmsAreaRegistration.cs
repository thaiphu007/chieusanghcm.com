using System.Web.Mvc;

namespace LightWS.Cms
{
    public class CMSAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CMS";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
               "CMS",
               "Cms/{controller}/{action}/{id}",
               new { controller = "default", action = "Index", id = UrlParameter.Optional },
               new[] { "LightWS.Areas.Cms.Controllers" }
           );         
        }
    }
}