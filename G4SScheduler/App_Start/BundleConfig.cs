using System.Web;
using System.Web.Optimization;

namespace G4SScheduler
{
    public class BundleConfig
    {
         
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            var CommanScript = (new ScriptBundle("~/Content/CommanScript")
                              .Include(
                                     "~/Scripts/jquery-{version}.js",
                                     "~/Content/js/bootstrap.min.js",
                                     "~/Scripts/jquery/jquery-ui.min.js",
                                     "~/Scripts/jquery.unobtrusive*",
                                     "~/Scripts/jquery.validate*",
                                     "~/Content/js/Utils.js"
                                      
                                  )
                              );
            //      "~/Content/js/enscroll-0.6.2.min.js",
            CommanScript.Orderer = new NonOrderingBundleOrderer();
            bundles.Add(CommanScript);


            var bundle = (new StyleBundle("~/Content/logincss")
                      .Include(
                              "~/Content/css/bootstrap.css",
                              "~/Content/css/bootstrap.min.css",
                              "~/Content/css/font-awesome.min.css",
                              "~/Content/css/style-metro.css",
                              "~/Content/css/mainstyle.css",
                              "~/Content/css/uniform.default.css",
                              "~/Content/css/CustomStyle.css",
                              "~/Content/css/login.css",
                              "~/Content/css/style.css",
                              "~/Content/css/jquery-ui.css"
                          )
                      );
            bundle.Orderer = new NonOrderingBundleOrderer();
            bundles.Add(bundle);

            var LoginScript = (new ScriptBundle("~/Content/LoginScript")
                   .Include(
                       "~/Scripts/jquery/external/jquery/jquery.js",
                       "~/Scripts/jquery/jquery-ui.min.js",
                       "~/Scripts/jquery.unobtrusive*",
                       "~/Scripts/jquery.validate*",
                       "~/Content/js/Utils.js"
                   )
               );
            LoginScript.Orderer = new NonOrderingBundleOrderer();
            bundles.Add(LoginScript);
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
