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
        private SurveyQuestionApi surveyQuestionapi = new SurveyQuestionApi();
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
            List<SurveyQuestion> surveyQuestions = await surveyQuestionapi.GetSurveysQuestions(surveyId);

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

        // GET: Surveys/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Survey survey = await surveyApi.GetSurvey(id);
            return View(survey);
        }

        // GET: Surveys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Surveys/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,SurveyTitle,DateCreated")] Survey survey)
        {
            bool createdSurvey = await surveyApi.PutSurvey(survey);
            // Survey was created successfully in database
            if (createdSurvey)
            {
                return View(survey);
            }
            // Failed to add survey to database
            else
            {
                return View(survey);
            }
        }

        // GET: Surveys/Edit/5
        public ActionResult Edit(int? id)
        {
            /*
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);*/
            return View();
        }

        // POST: Surveys/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SurveyTitle,DateCreated")] Survey survey)
        {
            /*
            if (ModelState.IsValid)
            {
                db.Entry(survey).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }*/
            return View(survey);
        }

        // GET: Surveys/Delete/5
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

        // POST: Surveys/Delete/5
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
    }
}