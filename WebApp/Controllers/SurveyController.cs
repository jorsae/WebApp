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
        private SurveyAnswerApi surveyAnswerApi = new SurveyAnswerApi();

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

        [HttpPost]
        public async Task<ActionResult> FinishSurvey(FormCollection collection)
        {
            string prefix = "surveyQuestionId-";
            string result = "";
            foreach(string property in collection)
            {
                if (property.Contains(prefix))
                {
                    int questionId, answer;
                    bool questionParsed = int.TryParse(property.Replace(prefix, ""), out questionId);
                    bool answerParsed = int.TryParse(Request.Form[property], out answer);
                    if (questionParsed && answerParsed)
                    {
                        bool insertedAnswer = await surveyAnswerApi.PutSurveyAnswer(questionId, answer);
                        if(insertedAnswer)
                            result += $"{property} answer saved <br />";
                        else
                            result += $"{property} failed to save answer <br />";
                    }
                    else
                    {
                        result += $"{property} Something went wrong, could not get questionId and/or answer";
                    }
                }
            }

            ViewBag.result = result;
            return View();
        }
    }
}