using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class SurveyController : Controller
    {
        private SurveyQuestionApi surveyQuestionApi = new SurveyQuestionApi();
        private SurveyAnswerApi surveyAnswerApi = new SurveyAnswerApi();
        private SurveyApi surveyApi = new SurveyApi();

        // GET: Survey
        public async Task<ActionResult> Index()
        {
            List<Survey> surveys = await surveyApi.GetSurveys();

            return View(surveys);
        }

        [HttpGet]
        public async Task<ActionResult> AnswerSurvey(int surveyId)
        {
            Survey survey = await surveyApi.GetSurvey(surveyId);
            ViewBag.surveyActivity = (survey.IsActive()) ? "Survey is active" : "Survey is no longer active";
            List<SurveyQuestion> surveyQuestions = await surveyQuestionApi.GetSurveyQuestions(surveyId);

            ViewBag.surveyId = surveyQuestions.Count;
            ViewBag.surveyTitle = survey.SurveyTitle;

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
                        bool surveyAnswer = await surveyAnswerApi.PutSurveyAnswer(questionId, answer);
                        if(surveyAnswer)
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

        // GET: Survey/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Survey survey = await surveyApi.GetSurvey(id);
            List<SurveyQuestion> surveyQuestions = await surveyQuestionApi.GetSurveyQuestions(survey.SurveyId);
            if(surveyQuestions == null)
            {
                return View(survey);
            }

            foreach(SurveyQuestion sq in surveyQuestions)
            {
                SurveyQuestionStats stats = await surveyQuestionApi.GetSurveyQuestionStats(sq.SurveyQuestionId);
                ViewBag.surveyQuestionStats += $"Question: {sq.QuestionNumber}: ";
                ViewBag.surveyQuestionStats += stats + "<br />";
            }

            ViewBag.surveyQuestionStats += "<br />" + survey;
            return View(survey);
        }

        // GET: Survey/Create
        public ActionResult Create()
        {
            return View(new Survey(""));
        }

        // POST: Survey/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SurveyTitle, ClosingDate")] Survey survey)
        {
            Survey createdSurvey = await surveyApi.PutSurvey(survey);
            // Survey was created successfully in database
            if (createdSurvey != null)
            {
                return RedirectToAction("Edit", "Survey", new { id = createdSurvey.SurveyId} );
            }
            // Failed to add survey to database
            else
            {
                return View(survey);
            }
        }

        // GET: Survey/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return View(new SurveyAndSurveyQuestions(null, null));

            Survey survey = await surveyApi.GetSurvey((int)id);
            if(survey == null)
                return View(new SurveyAndSurveyQuestions(null, null));

            List<SurveyQuestion> surveyQuestions = await surveyQuestionApi.GetSurveyQuestions(survey.SurveyId);

            return View(new SurveyAndSurveyQuestions(survey, surveyQuestions));
        }

        // POST: Survey/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SurveyId,SurveyTitle,ClosingDate")] Survey survey)
        {
            if(survey == null)
                return View();
            Survey newSurvey = await surveyApi.PostSurveyChange(survey);
            // Changes was unusuccesfull
            if(newSurvey == null)
            {
                return View(new SurveyAndSurveyQuestions(survey, null));
            }
            List<SurveyQuestion> surveyQuestions = await surveyQuestionApi.GetSurveyQuestions(survey.SurveyId);
            return View(new SurveyAndSurveyQuestions(survey, surveyQuestions));
        }

        // GET: Survey/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            Debug.WriteLine(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = await surveyApi.GetSurvey((int)id);
            Debug.WriteLine(survey);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // POST: Survey/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            bool deletedSurvey = await surveyApi.DeleteSurvey(id);
            // Deleted survey successfully
            if (deletedSurvey)
            {
                return RedirectToAction("Index");
            }
            // Failed to delete survey
            else
            {
                Console.WriteLine("Did not delete survey");
                return RedirectToAction("Index");
            }
        }

        // GET: Survey/CreateSurveyQuestion
        public ActionResult CreateSurveyQuestion(Survey survey)
        {
            ViewBag.SurveyId = survey.SurveyId;
            return View();
        }

        // POST: Survey/CreateSurveyQuestion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateSurveyQuestion([Bind(Include = "Question,SurveyId")] SurveyQuestion surveyQuestion)
        {
            SurveyQuestion createdSurveyQuestion = await surveyQuestionApi.PutSurveyQuestion(surveyQuestion);
            // Surveyquestion was created successfully in database
            if (createdSurveyQuestion != null)
            {
                return RedirectToAction("Edit", "Survey", new { id = createdSurveyQuestion.SurveyId });
            }
            // Failed to add surveyquestion to database
            else
            {
                ViewBag.SurveyId = surveyQuestion.SurveyId;
                return View(surveyQuestion);
            }
        }
    }
}