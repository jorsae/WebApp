using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace WebApp.Models
{
    public class SurveyApi
    {
        private HttpClient client = new HttpClient();
        private const string Baseurl = "https://bo19webapi.azurewebsites.net/api/survey";

        public async Task<Survey> GetSurvey(int surveyId)
        {
            string url = $"{Baseurl}/{surveyId}";

            Survey survey = null;

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                survey = await response.Content.ReadAsAsync<Survey>();
            }
            return survey;
        }

        public async Task<List<Survey>> GetSurveys()
        {
            HttpResponseMessage response = await client.GetAsync(Baseurl);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<Survey>>();
            }
            return null;
        }

        public async Task<bool> PutSurvey(Survey survey)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync<Survey>(Baseurl, survey);
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> DeleteSurvey(int id)
        {
            string url = $"{Baseurl}/{id}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
    }
}