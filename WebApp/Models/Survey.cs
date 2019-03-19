using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Survey
    {
        public int SurveyId { get; set; }
        public int UserId { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual ICollection<SurveyQuestion> SurveyQuestions { get; set; } = new List<SurveyQuestion>();

        private static int NumberOfSurveys = 0;

        // Empty constructor for EntityFramework
        public Survey()
        {

        }

        public Survey(int userId)
        {
            SurveyId = NumberOfSurveys++;
            UserId = userId;
            CreationDate = DateTime.Now;
        }

        public override string ToString()
        {
            return $"SurveyId:{SurveyId}, UserId:{UserId}, CreationDate:{CreationDate}";
        }
    }
}