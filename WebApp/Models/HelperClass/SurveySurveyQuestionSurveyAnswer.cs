using System.Collections.Generic;

namespace WebApp.Models
{
    public class SurveySurveyQuestionSurveyAnswer
    {
        public Survey Survey { get; set; }
        public List<SurveyQuestion> SurveyQuestions { get; set; }
        public List<SurveyAnswer> SurveyAnswers { get; set; }

        public SurveySurveyQuestionSurveyAnswer(Survey survey, List<SurveyQuestion> surveyQuestions, List<SurveyAnswer> surveyAnswers)
        {
            Survey = survey;
            SurveyQuestions = surveyQuestions;
            SurveyAnswers = surveyAnswers;
        }
    }
}