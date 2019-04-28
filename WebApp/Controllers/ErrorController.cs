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
            // TODO: Make 404 page look better
            string redirectedUrl = $"{Request.Url.Scheme}://{Request.Url.Authority}{aspxerrorpath}";
            return View(model: redirectedUrl);
        }

        
        // TODO: Add Error redirect for 5xx?
        public ActionResult Error500(string a)
        {
            return View();
        }
    }
}