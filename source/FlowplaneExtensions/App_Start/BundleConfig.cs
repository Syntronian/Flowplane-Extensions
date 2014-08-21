using System.Web;
using System.Web.Optimization;

namespace FlowplaneExtensions
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            var optimise = true;
#if DEBUG
            optimise = false;
#endif
            BundleTable.EnableOptimizations = optimise;


            bundles.Add(new StyleBundle("~/Content/css/style").Include(
                "~/Content/css/asana.css",
                "~/Content/css/facebook.css",
                "~/Content/css/paymo.css",
                "~/Content/css/podio.css",
                "~/Content/css/twitter.css",
                "~/Content/css/linkedin.css"
            ));

            Bundle js = new ScriptBundle("~/Content/js/all");
#if DEBUG
            js = new Bundle("~/Content/js/all");
#endif
            bundles.Add(js.Include(
                "~/Content/js/shearnie/tools.js",
                "~/Content/js/fpxt.js",
                "~/Content/js/forms/asana.js",
                "~/Content/js/forms/facebook.js",
                "~/Content/js/forms/paymo.js",
                "~/Content/js/forms/podio.js",
                "~/Content/js/forms/twitter.js",
                "~/Content/js/forms/linkedin.js"
            ));
        }
    }
}
