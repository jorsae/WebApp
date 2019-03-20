using System.Collections.Generic;

namespace WebApp.Models
{
    public class SurveyQuestion
    {
        public int SurveyQuestionId { get; set; }
        public string Question { get; set; }
        public int QuestionNumber { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }

        public virtual List<SurveyAnswer> SurveyAnswers { get; set; } = new List<SurveyAnswer>();

        private static int NumberOfSurveyQuestions = 0;

        public SurveyQuestion()
        {

        }

        public SurveyQuestion(int questionNumber, string question)
        {
            SurveyQuestionId = NumberOfSurveyQuestions++;
            QuestionNumber = questionNumber;
            Question = question;
        }

        public override string ToString()
        {
            return $"SurveyQuestionId:{SurveyQuestionId}, Question:{Question}, QuestionNumber:{QuestionNumber}, SurveyId:{SurveyId}";
        }
    }
}