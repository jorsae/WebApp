using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    [RoutePrefix("Survey")]
    public class TestController : Controller
    {
        // GET: Test
        [Route("Survey/Test")]
        public ActionResult Index()
        {
            Debug.WriteLine("Test/Index");
            return View();
        }
    }
}