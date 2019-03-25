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

            ViewBag.surveyId = surveyQuestions.Count;

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
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
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
        public ActionResult Create([Bind(Include = "Id,SurveyTitle,DateCreated")] Survey survey)
        {
            if (ModelState.IsValid)
            {
                db.Surveys.Add(survey);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(survey);
        }

        // GET: Surveys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // POST: Surveys/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SurveyTitle,DateCreated")] Survey survey)
        {
            if (ModelState.IsValid)
            {
                db.Entry(survey).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(survey);
        }

        // GET: Surveys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // POST: Surveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Survey survey = db.Surveys.Find(id);
            db.Surveys.Remove(survey);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}