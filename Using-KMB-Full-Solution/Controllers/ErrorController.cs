using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Using_KMB_Full_Solution.Infrastructure;

namespace Using_KMB_Full_Solution.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Show()
        {
            ViewBag.Exception = TempData[MyTempDataKeys.lasterror] as Exception;

            return View();
        }
    }
}