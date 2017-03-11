using Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Utilities.Helper
{
    public static class HtmlHelpers
    {
        public static IHtmlString reCaptcha(this HtmlHelper helper)
        {
            StringBuilder sb = new StringBuilder();
            string publickey = Common.GetValue("Recaptcha.PublicKey");
            sb.AppendLine("<script type=\"text/javascript\" src='https://www.google.com/recaptcha/api.js'></script>");
            sb.AppendLine("");
            sb.AppendLine("<div class=\"g-recaptcha\" data-sitekey=\"" + publickey + "\"></div>");
            return MvcHtmlString.Create(sb.ToString());

        }

        public static string IsActive(this HtmlHelper htmlHelper, string action, string controller)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var routeAction = routeData.Values["action"].ToString();
            var routeController = routeData.Values["controller"].ToString();
            var returnActive = false;

            var actions = action.Split(',').ToArray();
            var controllers = controller.Split(',').ToArray();

            if (controllers.Contains(routeController) && (actions.Contains(routeAction) || action.Length == 0))
            {
                returnActive = true;
            }
            //returnActive = (controller == routeController && (action == routeAction || action.Length == 0));

            return returnActive ? "active" : "";
        }
      
      
    }
}