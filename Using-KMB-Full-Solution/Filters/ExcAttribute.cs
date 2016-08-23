using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MuhendisAsci.Web.Filters
{
    public class ExcAttribute : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Controller.TempData["LastError"] == null)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Controller.TempData.Add("LastError", filterContext.Exception);
            }

            filterContext.Result = new RedirectResult("/Home/Error");
        }
    }
}