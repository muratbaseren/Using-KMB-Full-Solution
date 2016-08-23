using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MuhendisAsci.Web.Filters
{
    public class AuthAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["sys_login"] == null)
            {
                //string returnUrl = HttpUtility.HtmlEncode(filterContext.HttpContext.Request.Url.ToString());
                //filterContext.Result = new RedirectResult("/Home/Login?returnUri=" + returnUrl);
                filterContext.Result = new RedirectResult("/Home/Login");
            }
        }
    }
}