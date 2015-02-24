using System.Web;
using System.Web.Optimization;

namespace vls
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterStyleBundles(bundles);
            RegisterJavascriptBundles(bundles);
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/css")
                .Include("~/Content/css/semantic.min.css")
                .Include("~/Content/css/main.css")
                .Include("~/Content/css/c3.min.css"));
        }

        private static void RegisterJavascriptBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js")
                .Include("~/Content/js/moment.min.js")
                .Include("~/Content/js/jquery.min.js")
                .Include("~/Content/js/semantic.min.js")
                .Include("~/Content/js/knockout.js")

                .Include("~/Content/js/accounting.min.js")
                .Include("~/Content/js/d3.min.js")
                .Include("~/Content/js/c3.min.js")
                .Include("~/Content/js/general.js"));
            /*bundles.Add(new ScriptBundle("~/js")
                .Include("~/Content/js/require.js"));*/
        }
    }
}
