using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace MuhendisAsci.Web.Infrastructure
{
    public static class MyHtmlHelper
    {
        public static MvcHtmlString SetHtml(this HtmlHelper htmlHelper,
    string templateId, Func<object, HelperResult> template)
        {
            return MvcHtmlString.Create("<div id=\"" + templateId +
                "\">" + template.Invoke(null) + "</script>");
        }

        public static string RenderViewToString(ControllerContext context, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = context.RouteData.GetRequiredString("action");

            var viewData = new ViewDataDictionary(model);

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
                var viewContext = new ViewContext(context, viewResult.View, viewData, new TempDataDictionary(), sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}