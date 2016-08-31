using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Using_KMB_Full_Solution.Infrastructure;

namespace MuhendisAsci.Web.Filters
{
    public class MyAuthAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session[MySessionNames.login] == null)
            {
                //string returnUrl = HttpUtility.HtmlEncode(filterContext.HttpContext.Request.Url.ToString());
                //filterContext.Result = new RedirectResult("/Home/Login?returnUri=" + returnUrl);
                filterContext.Result = new RedirectResult("/Home/Login");
            }
        }
    }
}