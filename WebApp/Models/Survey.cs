using System;
using System.ComponentModel.DataAnnotations;

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
        public DateTime CreationDate { get; set; }
        public DateTime ClosingDate { get; set; }

        // Empty constructor for EntityFramework
        public Survey()
        {
        }

        public Survey(DateTime? closingDate = null)
        {
            ClosingDate = DateTime.Now.AddDays(7);
            CreationDate = DateTime.Now;
        }

        public Survey(string surveyTitle, DateTime? closingDate = null)
        {
            ClosingDate = (closingDate == null) ? DateTime.Now.AddDays(7) : (DateTime)closingDate;
            SurveyTitle = surveyTitle;
            CreationDate = DateTime.Now;

        }

        public bool IsActive()
        {
            if (ClosingDate >= DateTime.Now)
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            return $"SurveyId:{SurveyId}, SurveyTitle:{SurveyTitle}, CreationDate:{CreationDate}, ClosingDate:{ClosingDate}, IsActive:{IsActive()}";
        }
    }
}