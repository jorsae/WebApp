using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace WebApp.Models
{
    public class Survey
    {
        [Key]
        public int SurveyId { get; set; }
        [Required]
        [MaxLength(64)]
        public string SurveyTitle { get; set; }
        [Required]
        public string SurveyGuid { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime ClosingDate { get; set; }
        
        // Empty constructor for EntityFramework
        public Survey()
        {
            CreationDate = DateTime.Now;
            SurveyGuid = Guid.NewGuid().ToString();
        }

        public Survey(DateTime? closingDate = null)
        {
            ClosingDate = DateTime.Now.AddDays(7);
            CreationDate = DateTime.Now;
            SurveyGuid = Guid.NewGuid().ToString();
        }

        public Survey(string surveyTitle, DateTime? closingDate = null)
        {
            ClosingDate = (closingDate == null) ? DateTime.Now.AddDays(7) : (DateTime)closingDate;
            SurveyTitle = surveyTitle;
            CreationDate = DateTime.Now;
            SurveyGuid = Guid.NewGuid().ToString();
        }

        public bool IsActive()
        {
            if (ClosingDate >= DateTime.Now)
                return true;
            else
                return false;
        }

        public string GetSurveyAnswerUrl() {
            var request = HttpContext.Current.Request;
            return $"{request.Url.Scheme}://{request.Url.Authority}/Survey/AnswerSurvey?guid={SurveyGuid}";
        }

        public override string ToString()
        {
            return $"SurveyId:{SurveyId}, SurveyTitle:{SurveyTitle}, CreationDate:{CreationDate}, ClosingDate:{ClosingDate}, IsActive:{IsActive()}";
        }
    }
}