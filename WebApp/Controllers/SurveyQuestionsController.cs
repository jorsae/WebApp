using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class SurveyQuestionsController : Controller
    {
        private WebAppContext db = new WebAppContext();

        // GET: SurveyQuestions
        public async Task<ActionResult> Index()
        {
            var surveyQuestions = db.SurveyQuestions.Include(s => s.Survey);
            return View(await surveyQuestions.ToListAsync());
        }

        // GET: SurveyQuestions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyQuestion surveyQuestion = await db.SurveyQuestions.FindAsync(id);
            if (surveyQuestion == null)
            {
                return HttpNotFound();
            }
            return View(surveyQuestion);
        }

        // GET: SurveyQuestions/Create
        public ActionResult Create()
        {
            ViewBag.SurveyId = new SelectList(db.Surveys, "SurveyId", "SurveyTitle");
            return View();
        }

        // POST: SurveyQuestions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SurveyQuestionId,Question,QuestionNumber,SurveyId")] SurveyQuestion surveyQuestion)
        {
            if (ModelState.IsValid)
            {
                db.SurveyQuestions.Add(surveyQuestion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SurveyId = new SelectList(db.Surveys, "SurveyId", "SurveyTitle", surveyQuestion.SurveyId);
            return View(surveyQuestion);
        }

        // GET: SurveyQuestions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyQuestion surveyQuestion = await db.SurveyQuestions.FindAsync(id);
            if (surveyQuestion == null)
            {
                return HttpNotFound();
            }
            ViewBag.SurveyId = new SelectList(db.Surveys, "SurveyId", "SurveyTitle", surveyQuestion.SurveyId);
            return View(surveyQuestion);
        }

        // POST: SurveyQuestions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SurveyQuestionId,Question,QuestionNumber,SurveyId")] SurveyQuestion surveyQuestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(surveyQuestion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SurveyId = new SelectList(db.Surveys, "SurveyId", "SurveyTitle", surveyQuestion.SurveyId);
            return View(surveyQuestion);
        }

        // GET: SurveyQuestions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SurveyQuestion surveyQuestion = await db.SurveyQuestions.FindAsync(id);
            if (surveyQuestion == null)
            {
                return HttpNotFound();
            }
            return View(surveyQuestion);
        }

        // POST: SurveyQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SurveyQuestion surveyQuestion = await db.SurveyQuestions.FindAsync(id);
            db.SurveyQuestions.Remove(surveyQuestion);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
