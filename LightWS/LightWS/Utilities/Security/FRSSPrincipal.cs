
using System;
using System.Linq;
using System.Collections.Generic;

using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Routing;

namespace Utilities.Security
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        // Custom property
        public AuthorizeUserAttribute(params object[] roles)
        {
            if (roles.Any(r => r.GetType().BaseType != typeof(Enum)))
                throw new ArgumentException("roles");
            RoleTypes = roles;
        }
       
        public RoleType Role { get; set; }
        public object[] RoleTypes { get; set; }   
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }
            
            var user = (httpContext.User as FRSSPrincipal);

           if(RoleTypes!=null&& RoleTypes.Count()>0)
           {
               foreach (var item in RoleTypes)
               {
                  if(user.usertype ==  (int)item)
                      return true;
               }
           }
            
           return user.usertype == (int)Role || user.usertype == (int)RoleType.Admin;
            
            

        }
        
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            
            filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "home",
                                action = "index",
                                returnUrl = filterContext.HttpContext.Request.Url.GetComponents(UriComponents.PathAndQuery, UriFormat.SafeUnescaped)
                            })
                        );
        }
    }


    interface ICustomPrincipal : IPrincipal
    {
        long Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        int usertype { get; set; }
        int status { get; set; }
        //Campaigner Info { get; }
       
        

    }
    public class FRSSPrincipal : ICustomPrincipal
    {

        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            return role == usertype.ToString();
        }

        public FRSSPrincipal(string email)
        {
            this.Identity = new GenericIdentity(email);
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int usertype { get; set; }
        public int status { get; set; }        
        //public Campaigner Info
        //{
        //    get
        //    {
        //        return Common.GetUser(this.Id);
        //    }
        //}
    }
}