using System.Web;
using System.Web.Optimization;

namespace KarkaThamizha.Admin
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jquerymain").Include(
                      "~/bootstrap/js/bootstrap.js",
                      "~/plugins/jQuery/raphael-min.js",
                      "~/plugins/morris/morris.js",
                      "~/plugins/sparkline/jquery.sparkline.js",
                      "~/plugins/jvectormap/jquery-jvectormap-2.0.5.min.js",
                      "~/plugins/jvectormap/jquery-jvectormap-world-mill-en.js",
                      "~/plugins/knob/jquery.knob.js",
                      "~/plugins/jQuery/moment.min.js",
                      "~/plugins/daterangepicker/daterangepicker.js",
                      "~/plugins/datepicker/bootstrap-datepicker.js",
                      "~/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js",
                      "~/plugins/slimScroll/jquery.slimscroll.min.js",
                      "~/plugins/fastclick/fastclick.js",
                      "~/dist/js/app.min.js",
                      "~/dist/js/pages/dashboard.js",
                      "~/dist/js/demo.js"
                      ));

            // Boorstrap dropdown select
            bundles.Add(new ScriptBundle("~/bundles/bootstrapselect").Include(
                                  "~/Scripts/bootstrap-select.js"));


            bundles.Add(new StyleBundle("~/Content/cssmain").Include(
                      "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/csspage").Include(
                                "~/dist/css/AdminLTE.css",
                                 "~/dist/css/skins/_all-skins.css",
                                 "~/plugins/iCheck/flat/blue.css",
                                 "~/plugins/morris/morris.css",
                                 "~/plugins/jvectormap/jquery-jvectormap-1.2.2.css",
                                 "~/plugins/datepicker/datepicker3.css",
                                 "~/plugins/daterangepicker/daterangepicker.css",
                                 "~/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.css",
                                 "~/Content/Site.css"
                                 ));

            // Bootstrap dropdown select
            bundles.Add(new StyleBundle("~/Content/cssselect").Include(
                                 "~/bootstrap/css/bootstrap-select.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
