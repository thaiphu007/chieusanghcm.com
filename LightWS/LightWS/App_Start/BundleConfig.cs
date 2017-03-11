using System.Web;
using System.Web.Optimization;

namespace LightWS
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            string ThemeName = "Default";
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/js/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/js/jquery/jquery.validate*"));
         

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/js/bootstrap.js",
                      "~/js/respond.js"));
            var bundle = new StyleBundle("~/content/css");

            bundle.Include(string.Format("~/themes/{0}/screen.css", ThemeName), new CssRewriteUrlTransformFixed());
            bundle.Include(string.Format("~/themes/{0}/responsive-tabs.css", ThemeName), new CssRewriteUrlTransformFixed());
            bundle.Include(string.Format("~/themes/{0}/format-space-color.css", ThemeName), new CssRewriteUrlTransformFixed());    
            bundle.Include(string.Format("~/themes/{0}/header.css", ThemeName), new CssRewriteUrlTransformFixed());
            bundle.Include(string.Format("~/themes/{0}/footer.css", ThemeName), new CssRewriteUrlTransformFixed());
            

            bundles.Add(bundle);
            
            
            
            BundleTable.EnableOptimizations = false;

        }
       
       
    }
   
}
