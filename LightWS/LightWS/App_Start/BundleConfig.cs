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

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/js/bootstrap.js",
                      "~/js/respond.js"));
            var bundle = new StyleBundle("~/content/css");

            bundle.Include(string.Format("~/themes/{0}/content/css/style.css", ThemeName), new CssRewriteUrlTransformFixed());
            bundle.Include(string.Format("~/themes/{0}/content/css/reponsive.css", ThemeName), new CssRewriteUrlTransformFixed());
            
            bundles.Add(bundle);
            
            
            
            BundleTable.EnableOptimizations = false;

        }
       
       
    }
   
}
