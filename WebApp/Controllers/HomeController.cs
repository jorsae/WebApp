using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        // TODO: Make a default 404 page

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}