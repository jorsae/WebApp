using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error404(string aspxerrorpath)
        {
            string redirectedUrl = $"{Request.Url.Scheme}://{Request.Url.Authority}{aspxerrorpath}";
            return View(model: redirectedUrl);
        }
    }
}