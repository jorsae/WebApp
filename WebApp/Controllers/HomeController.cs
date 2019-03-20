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
            // <h4>All questions to survey id: 2</h4>
            List< SurveyQuestion> surveyQuestions = await surveyQuestionApi.GetSurveysQuestions(2);
            foreach(SurveyQuestion surveyQuestion in surveyQuestions)
            {
                ViewBag.surveyQuestion += surveyQuestion.ToString() + "<br />";
            }

            // < h4 > Answer with answerId: 6 </ h4 >
            ViewBag.surveyAnswer = await surveyAnswerApi.GetSurveyAnswer(6);

            // < h4 > Answer surveyId1, question:1 </ h4 >
            List<SurveyQuestion> surveyQuestion1 = await surveyQuestionApi.GetSurveysQuestions(1);
            ViewBag.question = surveyQuestion1[0].Question;

            //< h4 > All Answers to question 2 </ h4 >
            List<SurveyAnswer> surveyAnswers = await surveyAnswerApi.GetSurveyAnswers(2);
            foreach(SurveyAnswer surveyAnswer in surveyAnswers)
            {
                ViewBag.answers += surveyAnswer + "<br />";
            }

            // <h4>All surveys</h4>
            List<Survey> surveys = await surveyApi.GetSurveys();
            return View(surveys);
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