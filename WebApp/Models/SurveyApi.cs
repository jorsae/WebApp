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
    }
}