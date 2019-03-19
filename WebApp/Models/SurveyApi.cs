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
        private const string Baseurl = "https://bo19webapi.azurewebsites.net";

        public async Task<Survey> GetSurvey(int id)
        {
            string url = $"{Baseurl}/api/survey/{id}";

            Survey survey = null;

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                survey = await response.Content.ReadAsAsync<Survey>();
            }
            return survey;
        }
    }
}