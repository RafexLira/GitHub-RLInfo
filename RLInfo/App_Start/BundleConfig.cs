using System.Web;
using System.Web.Optimization;

namespace RLInfo
{
    public class BundleConfig
    {
        // Para obter mais informações sobre o agrupamento, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use a versão em desenvolvimento do Modernizr para desenvolver e aprender. Em seguida, quando estiver
            // pronto para a produção, utilize a ferramenta de build em https://modernizr.com para escolher somente os testes que precisa.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/css/font-awesome").Include(
                      "~/css/font-awesome.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/inputmask").Include(
            "~/Scripts/inputmask/jquery.inputmask.js"));

             bundles.Add(new ScriptBundle("~/bundles/mask").Include(
            "~/Scripts/mask/jquery.mask.js"));

            bundles.Add(new ScriptBundle("~/bundles/script").Include(
          "~/Scripts/Script.js"));
          
            bundles.Add(new ScriptBundle("~/bundles/sweetalert").Include(
         "~/Scripts/sweetalert.js"));
        }
    }
}
