using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Survey
    {
        public int SurveyId { get; set; }
        public string SurveyTitle { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ClosingDate { get; set; }

        public virtual ICollection<SurveyQuestion> SurveyQuestions { get; set; } = new List<SurveyQuestion>();

        // Empty constructor for EntityFramework
        public Survey()
        {

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