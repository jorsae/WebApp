using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private SurveyApi surveyApi = new SurveyApi();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async Task<ActionResult> Test()
        {
            ViewBag.survey = await surveyApi.GetSurvey(1);

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Result()
        {
            var keys = Request.Form.AllKeys;
            var idKey = Request.Form.Get(keys[0]);

            int id;
            int.TryParse(idKey.ToString(), out id);
            if(id >= 0)
                ViewBag.result = await surveyApi.GetSurvey(id);

            return View();
        }
    }
}