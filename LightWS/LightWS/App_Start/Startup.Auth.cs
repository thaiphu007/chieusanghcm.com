using System;
using System.Collections.Generic;
using Microsoft.Owin;
using Owin;
using System.Web.Security;
using System.Linq;

using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;


namespace LightWS
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")

            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        }
    }

}