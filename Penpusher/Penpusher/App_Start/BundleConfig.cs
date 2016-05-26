using System.Web.Optimization;

namespace Penpusher
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
           "~/Scripts/knockout-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/subscriptions").Include(
                       "~/Scripts/Main/subscriptions.js"));

            bundles.Add(new ScriptBundle("~/bundles/favoritearticles").Include(
           "~/Scripts/Main/favoritearticles.js"));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/template").Include(
                     "~/Content/template/font-awesome.min.css",
                     "~/Content/template/metisMenu.min.css",
                     "~/Content/template/sb-admin-2.css"));
            bundles.Add(new StyleBundle("~/Scripts/template").Include(
                     "~/Scripts/template/metisMenu.min.js",
                     "~/Scripts/template/sb-admin-2.js"));
        }
    }
}