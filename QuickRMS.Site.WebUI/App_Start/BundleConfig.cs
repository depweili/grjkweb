using System.Web;
using System.Web.Optimization;

namespace QuickRMS.Site.WebUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*",
                "~/Scripts/jquery.unobtrusive*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/jquery-ui-zh.js"
                ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                     "~/Scripts/bootstrap3js/bootstrap.js"));
            //---Admin Site---

            //-----CSS-----
            bundles.Add(new StyleBundle("~/bundles/css/bootstrap2").Include(
                      "~/Content/admin/css/bootstrap.min.css",
                      "~/Content/admin/css/bootstrap-responsive.min.css"));

            bundles.Add(new StyleBundle("~/bundles/css/bootstrap3").Include(
                     "~/Content/admin/css/bootstrap3/css/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/bundles/css/admin")
                      .Include("~/Content/admin/css/matrix-style.css", new CssRewriteUrlTransform())
                      .Include("~/Content/admin/font-awesome/css/font-awesome.css", new CssRewriteUrlTransform())
                      //.Include("~/Content/admin/css/uniform.css", new CssRewriteUrlTransform())
                      .Include("~/Content/admin/css/select2.css", new CssRewriteUrlTransform())
                      .Include("~/Content/admin/css/select2-bootstrap.css", new CssRewriteUrlTransform())
                      .Include("~/Content/admin/css/matrix-media.css", new CssRewriteUrlTransform())
                      .Include("~/Content/admin/css/font.css", new CssRewriteUrlTransform())
                      );

            bundles.Add(new StyleBundle("~/bundles/css/jqueryui")
                .Include("~/Content/themes/base/jquery.ui.core.css", new CssRewriteUrlTransform())
                 .Include("~/Content/themes/base/jquery.ui.autocomplete.css", new CssRewriteUrlTransform())
                  .Include("~/Content/themes/base/jquery.ui.datepicker.css", new CssRewriteUrlTransform())
                .Include(
                      "~/Content/themes/base/jquery.ui.theme.css", new CssRewriteUrlTransform()
                      ));

            //-----JS-----
            bundles.Add(new ScriptBundle("~/bundles/js/admin-jq").Include(
                     "~/Content/admin/js/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/bootstrap2").Include(
                     "~/Content/admin/js/bootstrap.min.js",
                     "~/Content/admin/js/bootbox.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/jqdataTables").Include(
                      "~/Content/admin/js/jquery.dataTables.min.js",
                      "~/Content/admin/js/jquery.dataTables.AjaxSource.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/admin-plugin").Include(
                      "~/Content/admin/js/select2.min.js", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/bundles/js/admin-echarts").Include(
                     "~/Content/admin/js/echarts.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/bootstrap3").Include(
                    "~/Content/admin/js/bootstrap3js/bootstrap.min.js",
                    "~/Content/admin/js/bootstrap3js/bootbox.min.js",
                    "~/Content/admin/js/bootstrap3js/typeahead.bundle.min.js"));


        }
    }
}
