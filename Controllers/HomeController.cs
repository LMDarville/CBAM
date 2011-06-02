using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CBAM.Controllers
{
    [HandleError]
    public partial class HomeController : Controller
    {
        public virtual ActionResult Index()
        {
            return RedirectToAction ("Index","Project",null);
            //ViewData["Message"] = "Welcome to ASP.NET MVC!";
            //return View();
        }

        public virtual ActionResult About()
        {
            return View();
        }
    }
}
