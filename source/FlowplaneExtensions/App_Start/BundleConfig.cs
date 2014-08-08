using System.Web;
using System.Web.Optimization;

namespace FlowplaneExtensions
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css/style").Include(
                "~/Content/css/asana.css",
                "~/Content/css/facebook.css",
                "~/Content/css/paymo.css",
                "~/Content/css/podio.css"
            ));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
