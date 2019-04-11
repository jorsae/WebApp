using System.Collections.Generic;

namespace WebApp.Models
{
    public class SurveyAndSurveyQuestions
    {
        public Survey Survey { get; set; }
        public List<SurveyQuestion> SurveyQuestions { get; set; }

        public SurveyAndSurveyQuestions(Survey survey, List<SurveyQuestion> surveyQuestions)
        {
            Survey = survey;
            SurveyQuestions = surveyQuestions;
        }
    }
}