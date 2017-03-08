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
            new HomeController().Index();   //这个方法会调用view("index")渲染index
            return View("MyView");
        }
    }
}