namespace WebApp.Models
{
    public class SurveyAnswer
    {
        public int SurveyAnswerId { get; set; }
        public int Answer { get; set; }
        public int SurveyQuestionId { get; set; }
        public SurveyQuestion SurveyQuestion { get; set; }

        public SurveyAnswer()
        {

        }

        public SurveyAnswer(int answer, int surveyQuestionId)
        {
            Answer = answer;
            SurveyQuestionId = surveyQuestionId;
        }

        public override string ToString()
        {
            return $"SurveyAnswerId:{SurveyAnswerId}, Answer:{Answer}, SurveyQuestionId:{SurveyQuestionId}";
        }
    }
}