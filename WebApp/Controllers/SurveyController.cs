using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class SurveyController : Controller
    {
        private SurveyQuestionApi surveyQuestionapi = new SurveyQuestionApi();
        // GET: Survey
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AnswerSurvey(int surveyId)
        {
            List<SurveyQuestion> surveyQuestions = await surveyQuestionapi.GetSurveysQuestions(surveyId);

            ViewBag.test = surveyQuestions.Count;

            return View(surveyQuestions);
        }

        public ActionResult FinishSurvey()
        {
            return View();
        }
    }
}