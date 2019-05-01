using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

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

        public async Task<List<SurveyAnswer>> PutSurveyAnswer(List<SurveyAnswer> surveyAnswers)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync<List<SurveyAnswer>>(Baseurl, surveyAnswers);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<List<SurveyAnswer>>();
            else
                return null;
        }
    }
}