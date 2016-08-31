using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Using_KMB_Full_Solution.Infrastructure;

namespace MuhendisAsci.Web.Filters
{
    public class MyExcAttribute : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Controller.TempData[MyTempDataKeys.lasterror] == null)
            {
                filterContext.ExceptionHandled = true;

                filterContext.Controller.TempData.Add(
                    MyTempDataKeys.lasterror, filterContext.Exception);
            }

            filterContext.Result = new RedirectResult("/Error/Show");
        }
    }
}