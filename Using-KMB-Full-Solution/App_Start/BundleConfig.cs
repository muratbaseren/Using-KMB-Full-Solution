using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Using_KMB_Full_Solution
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            // *************************************************************//
            // SCRIPTS                                                      //
            // *************************************************************//

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/scripts/jquery-2.2.4.min.js",
                "~/scripts/bootstrap.min.js",
                "~/scripts/jquery.unobtrusive-ajax.min.js",
                "~/scripts/jquery.validate.min.js",
                "~/scripts/jquery.validate.unobtrusive.min.js",
                "~/scripts/modernizr-2.6.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/ckeditor").Include(
                "~/scripts/ckeditor/ckeditor.js",
                "~/scripts/myckeditor_loader.js"));

            bundles.Add(new ScriptBundle("~/bundles/gridmvcjs").Include(
                "~/scripts/gridmvc.min.js",
                "~/scripts/bootstrap-datepicker.js",
                "~/scripts/gridmvc.lang.tr.js"));

            //bundles.Add(new ScriptBundle("~/bundles/template-scripts").Include(
            //    "~/scripts/my-script.min.js"));


            // *************************************************************//
            // STYLES                                                       //
            // *************************************************************//

            bundles.Add(new StyleBundle("~/bundles/styles").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/hint.min.css",
                "~/font-awesome/css/font-awesome.min.css",
                "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/bundles/gridmvccss").Include(
                "~/Content/Gridmvc.css",
                "~/Content/gridmvc.datepicker.min.css"));

            //bundles.Add(new StyleBundle("~/bundles/template-styles").Include(
            //    "~/Content/my-template.min.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}