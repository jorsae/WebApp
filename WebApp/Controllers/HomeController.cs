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
        private SurveyQuestionApi surveyQuestionApi = new SurveyQuestionApi();
        private SurveyAnswerApi surveyAnswerApi = new SurveyAnswerApi();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "A small description of this application.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "If you have any questions, please contact us.";

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

        [HttpPost]
        public async Task<ActionResult> AnswerTest()
        {
            var keys = Request.Form.AllKeys;
            var answerKey = Request.Form.Get(keys[0]);

            int answer;
            int.TryParse(answerKey.ToString(), out answer);
            if (answer >= 0)
            {
                ViewBag.result = await surveyAnswerApi.PutSurveyAnswer(2, answer);
            }
            else
            {
                ViewBag.result = "You did not answer the question";
            }

            return View();
        }
    }
}