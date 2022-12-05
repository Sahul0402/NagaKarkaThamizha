using System.Web;
using System.Web.Optimization;

namespace KarkaThamizha
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/framework").Include(
                "~/framework/js/jquery-2.0.3.min.js",
                "~/framework/js/jquery-migrate-1.2.1.min.js",
                "~/framework/js/jquery.easing.1.3.js",
                "~/Scripts/jquery-ui.js",
                "~/Scripts/jquery.star-rating.js",
                "~/framework/bootstrap/js/bootstrap.min.js",
                "~/framework/addons/camera/js/camera.min.js",
                "~/framework/addons/owl/owl.carousel.min.js",
                "~/framework/addons/breaking-news-ticker/jquery.ticker.js",
                "~/framework/addons/mobile-menu/pushy.js",
                "~/framework/addons/show-on-scroll/jquery.appear.js",
                "~/framework/addons/lightbox/nivo-lightbox.js",
                "~/framework/holder.js",
                "~/framework/js/serpentsoft-scripts.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.unobtrusive*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*",
                         "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/framework/bootstrap/css/bootstrap.css",
                        "~/framework/bootstrap/css/bootstrap-theme.css",
                        "~/framework/addons/font-vectors/icon-moon/style.css",
                        "~/framework/addons/font-vectors/zocial/fontellozocial.css",
                        "~/framework/addons/breaking-news-ticker/ticker-style.css",
                        "~/framework/addons/mobile-menu/pushy.css",
                        "~/framework/addons/owl/owl.carousel.css",
                        "~/framework/addons/owl/owl.transitions.css",
                        "~/framework/addons/animate.css",
                      "~/framework/addons/camera/css/camera.css",
                      "~/Content/social-icons.css",
                      "~/Content/style.css",
                      "~/Content/theme-color.css",
                      "~/Content/responsive.css",
                      "~/Content/firefox.css",
                      "~/framework/addons/lightbox/nivo-lightbox.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
