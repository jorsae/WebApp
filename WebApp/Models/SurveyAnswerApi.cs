using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace WebApp.Models
{
    public class SurveyAnswerApi
    {
        private HttpClient client = new HttpClient();
        private const string Baseurl = "https://bo19webapi.azurewebsites.net/api/surveyanswer";

        public async Task<SurveyAnswer> GetSurveyAnswer(int surveyAnswerId)
        {
            string url = $"{Baseurl}/{surveyAnswerId}";

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<SurveyAnswer>();
            }
            return null;
        }

        public async Task<List<SurveyAnswer>> GetSurveyAnswers(int surveyQuestionId)
        {
            string url = $"{Baseurl}/question/{surveyQuestionId}";

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<SurveyAnswer>>();
            }
            return null;
        }

        public async Task<SurveyAnswer> PutSurveyAnswer(int surveyQuestionId, int userAnswer)
        {
            SurveyAnswer answer = new SurveyAnswer(userAnswer, surveyQuestionId);

            HttpResponseMessage response = await client.PutAsJsonAsync<SurveyAnswer>(Baseurl, answer);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<SurveyAnswer>();
            else
                return null;
        }
    }
}