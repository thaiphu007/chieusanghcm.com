using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Utilities.Security

{
    public class BaseController : Controller
    {
        protected virtual new FRSSPrincipal User
        {
            get { return HttpContext.User as FRSSPrincipal; }
        }
         public string GetCurrentAction
         {
             get
             {
                 string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                 return string.Format("{0}", actionName);
             }
         }
         public string GetAllErrors()
         {
             string Result = string.Empty;
             foreach (ModelState modelState in ViewData.ModelState.Values)
             {
                 foreach (ModelError error in modelState.Errors)
                 {
                     if (string.IsNullOrEmpty(Result))
                         Result = string.Format("{0}.", error.ErrorMessage);
                     else Result += string.Format("<br/>{0}.", error.ErrorMessage);
                 }
             }
             return Result;
         }
         protected override void OnException(ExceptionContext filterContext)
         {
             //Exception ex = filterContext.Exception;
             //filterContext.ExceptionHandled = true;
             //filterContext.Result = RedirectToAction(string.Empty, "home");

         }
    }
    public abstract class BaseViewPage : WebViewPage
    {
      

        public virtual new FRSSPrincipal User
        {
            get { return base.User as FRSSPrincipal; }
        }

        public string T(string key)
        {
            return Common.ValueLang(key);
        }
       
    }
    public abstract class BaseViewPage<TModel> : WebViewPage<TModel>
    {   
        public virtual new FRSSPrincipal User
        {
            get { return base.User as FRSSPrincipal; }
        }
        public HtmlString T(string key)
        {
            string result = Common.ValueLang(key);
            return new HtmlString(string.IsNullOrEmpty(result) ? key : result);
        }
       
        public string GetUrl(string actionname)
        {
            return Common.GetUrl(actionname);
        }
      
    }
   

   
}