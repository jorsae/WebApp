using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class SurveyController : Controller
    {
        // GET: Survey
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AnswerSurvey()
        {
            return View();
        }

        public ActionResult FinishSurvey()
        {
            return View();
        }
    }
}