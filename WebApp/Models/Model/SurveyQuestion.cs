using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WebApp.Models
{
    public class SurveyQuestion
    {
        public int SurveyQuestionId { get; set; }
        [Required]
        [MaxLength(255)]
        public string Question { get; set; }
        [Required]
        [Range(1, 3)]
        public int QuestionNumber { get; set; }
        [ForeignKey("Survey")]
        public int SurveyId { get; set; }
        [IgnoreDataMember]
        public Survey Survey { get; set; }

        public SurveyQuestion()
        {
            
        }

        public SurveyQuestion(int surveyId, int questionNumber, string question)
        {
            SurveyId = surveyId;
            QuestionNumber = questionNumber;
            Question = question;
        }

        public override string ToString()
        {
            return $"SurveyQuestionId:{SurveyQuestionId}, Question:{Question}, QuestionNumber:{QuestionNumber}, SurveyId:{SurveyId}";
        }
    }
}