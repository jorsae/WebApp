using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class SurveyAnswer
    {
        public int SurveyAnswerId { get; set; }
        [Required]
        [Range(1, 10)]
        public int Answer { get; set; }

        [ForeignKey("SurveyQuestion")]
        public int SurveyQuestionId { get; set; }

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