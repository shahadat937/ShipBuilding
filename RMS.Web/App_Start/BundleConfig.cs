using System.Web.Optimization;

namespace RMS.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                     "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
               "~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                         "~/Scripts/jquery-ui-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/AppScripts").Include(
             "~/Scripts/AppScripts/ajax.js",
            "~/Scripts/AppScripts/jquery.alert-confirm.js",
            "~/Scripts/AppScripts/common.js",
              "~/Scripts/showLoading/jquery.showLoading.js",
              "~/Scripts/App/datepicker.js",
               "~/Scripts/APP/custom.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/themes/base/minified/jquery-ui.min.css",
                  "~/Content/bootstrap.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/Site.css",
                       "~/Content/AppStyles/FormControlStyle.css",
                      "~/Content/AppStyles/webGridScript.css",
                        "~/Content/custom-responsive.css"
                     ));
        }
    }
}
