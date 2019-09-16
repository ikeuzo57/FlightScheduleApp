using System.Web;
using System.Web.Optimization;

namespace WebApplication2
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").IncludeDirectory("~/Scripts/", "*.js", true));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/fontawesome/fontawesome.js",
                        "~/Scripts/moment.js",
                        "~/Scripts/bs-datepicker.js",
                        "~/Scripts/ajaxCalls.js",                        
                        "~/Scripts/owl-carousel.js",
                        "~/Scripts/blur-area.js",
                         "~/Scripts/icheck.js",
                         "~/Scripts/gmap.js",
                         "~/Scripts/magnific-popup.js",
                        "~/Scripts/ion-range-slider.js",
                         "~/Scripts/sticky-kit.js",
                         "~/Scripts/smooth-scroll.js",
                         "~/Scripts/fotorama.js",
                         "~/Scripts/bs-datepicker.js",
                         "~/Scripts/typeahead.js",
                         "~/Scripts/quantity-selector.js",
                         "~/Scripts/countdown.js",
                         "~/Scripts/window-scroll-action.js",
                         "~/Scripts/fitvid.js",
                        "~/Scripts/youtube-bg.js",
                         "~/Scripts/custom.js"

                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                       "~/Scripts/font-awesome.min.css",

                      "~/Content/styles.css",
                      "~/Content/weather.css",
                      "~/Content/mystyles.css",
                      "~/Content/lineicons.css",
                      "~/Content/font-awesome.css",
                      "~/Content/site.css"));
        }
    }
}
