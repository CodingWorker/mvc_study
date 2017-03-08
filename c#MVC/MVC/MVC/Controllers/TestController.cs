using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        [NonAction]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            View("Index");
            return View("MyView");
        }
    }
}