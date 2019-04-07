using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class SurveyQuestionApi
    {
        private HttpClient client = new HttpClient();
        private const string Baseurl = "https://bo19webapi.azurewebsites.net/api/surveyquestion";

        public async Task<List<SurveyQuestion>> GetSurveyQuestions(int surveyId)
        {
            List<SurveyQuestion> surveyQuestions = new List<SurveyQuestion>();

            string url = $"{Baseurl}/surveyId/{surveyId}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                surveyQuestions = await response.Content.ReadAsAsync<List<SurveyQuestion>>();
            }
            return surveyQuestions;
        }

        public async Task<SurveyQuestionStats> GetSurveyQuestionStats(int surveyQuestionId)
        {
            string url = $"{Baseurl}/stats/{surveyQuestionId}";
            HttpResponseMessage response = await client.GetAsync(url);
            Debug.WriteLine($"GetSurveyQuestionStats: {surveyQuestionId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<SurveyQuestionStats>();
            }
            return null;
        }

        public async Task<SurveyQuestion> PutSurveyQuestion(SurveyQuestion surveyQuestion)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync<SurveyQuestion>(Baseurl, surveyQuestion);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<SurveyQuestion>();
            }
            return null;
        }
    }
}