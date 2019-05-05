using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.Models.HelperClass;

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
        public async Task<ActionResult> AnswerSurvey(string guid)
        {
            if (String.IsNullOrEmpty(guid))
            {
                return View(new SurveySurveyQuestionSurveyAnswer(null, null, null));
            }

            Survey survey = await surveyApi.GetSurveyByGuid(guid);
            if (survey == null)
            {
                // TODO: AnswerSurvey, Make a better display (own page?) that a survey is no longer active?
                return View(new SurveySurveyQuestionSurveyAnswer(null, null, null));
            }
            List<SurveyQuestion> surveyQuestions = await surveyQuestionApi.GetSurveyQuestions(survey.SurveyId);
            List<SurveyAnswer> surveyAnswers = new List<SurveyAnswer>();
            for (int i = 0; i < surveyQuestions.Count; i++)
                surveyAnswers.Add(new SurveyAnswer());

            return View(new SurveySurveyQuestionSurveyAnswer(survey, surveyQuestions, surveyAnswers));
        }

        [HttpPost]
        public async Task<ActionResult> FinishSurvey([Bind(Include = "Answer,SurveyQuestionId")] List<SurveyAnswer> surveyAnswers)
        {
            List<SurveyAnswer> answers = await surveyAnswerApi.PutSurveyAnswer(surveyAnswers);
            if (answers == null)
            {
                ViewBag.result = "Failed to save answers";
            }
            else
            {
                foreach (SurveyAnswer answer in answers)
                    ViewBag.result += answer + "<br />";
            }
            return View(answers);
        }

        // GET: Survey/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Survey survey = await surveyApi.GetSurveyById(id);
            ViewBag.AnswerSurveyUrl = survey.GetSurveyAnswerUrl();
            
            List<SurveyQuestion> surveyQuestions = await surveyQuestionApi.GetSurveyQuestions(survey.SurveyId);
            if (surveyQuestions == null)
            {
                return View(survey);
            }

            foreach (SurveyQuestion sq in surveyQuestions)
            {
                SurveyQuestionStats stats = await surveyQuestionApi.GetSurveyQuestionStats(sq.SurveyQuestionId);
                List<SurveyQuestionFrequency> frequencyStats = await surveyQuestionApi.GetSurveyQuestionFrequency(sq.SurveyQuestionId);
                if (stats == null || frequencyStats == null)
                {
                    ViewBag.SurveyQuestionStats = "No replies to the survey yet";
                    break;
                }

                ViewBag.SurveyQuestionStats += $"<b>[{sq.QuestionNumber}] {sq.Question}:</b>";

                ViewBag.SurveyQuestionStats += $"<blockquote>Total answers: {stats.Count}<br />";
                ViewBag.SurveyQuestionStats += $"Average result: {stats.Average}<br />";
                foreach(SurveyQuestionFrequency freq in frequencyStats)
                {
                    ViewBag.SurveyQuestionStats += $"{freq}<br />";
                }
                ViewBag.SurveyQuestionStats += "</blockquote>";
            }

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
            // TODO: Display error message that survey was not added to database
            Survey createdSurvey = await surveyApi.PutSurvey(survey);
            // Survey was created successfully in database
            if (createdSurvey != null)
            {
                return RedirectToAction("Edit", "Survey", new { id = createdSurvey.SurveyId });
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

            Survey survey = await surveyApi.GetSurveyById((int)id);
            if (survey == null)
                return View(new SurveyAndSurveyQuestions(null, null));

            List<SurveyQuestion> surveyQuestions = await surveyQuestionApi.GetSurveyQuestions(survey.SurveyId);

            return View(new SurveyAndSurveyQuestions(survey, surveyQuestions));
        }

        // POST: Survey/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SurveyId,SurveyTitle,ClosingDate")] Survey survey)
        {
            if (survey == null)
                return View();
            Survey newSurvey = await surveyApi.PostSurveyChange(survey);
            // Changes was unusuccesfull
            if (newSurvey == null)
            {
                return View(new SurveyAndSurveyQuestions(survey, null));
            }
            List<SurveyQuestion> surveyQuestions = await surveyQuestionApi.GetSurveyQuestions(survey.SurveyId);
            return View(new SurveyAndSurveyQuestions(survey, surveyQuestions));
        }

        // POST: Survey/CreateSurveyQuestion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteSurveyQuestion(int surveyQuestionId)
        {
            SurveyQuestion sq = await surveyQuestionApi.GetSurveyQuestion(surveyQuestionId);
            bool deleted = await surveyQuestionApi.DeleteSurveyQuestion(surveyQuestionId);
            if(deleted)
            {
                return RedirectToActionPermanent("Edit", new { id = sq.SurveyId });
            }
            else
            {
                // TODO: Failed to delete SurveyQuestion
                return RedirectToActionPermanent("Index");
            }
        }

        // GET: Survey/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = await surveyApi.GetSurveyById((int)id);
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