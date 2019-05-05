using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.Models.HelperClass;

namespace WebApp.Models
{
    public class SurveyApi
    {
        private HttpClient client = new HttpClient();
        private const string Baseurl = "https://bo19webapi.azurewebsites.net/api/survey";

        public SurveyApi()
        {
            Baseurl = $"{Settings.BaseurlWebApi}/api/survey";
        }

        public async Task<Survey> GetSurveyById(int surveyId)
        {
            string url = $"{Baseurl}/{surveyId}";

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<Survey>();
            }
            return null;
        }

        public async Task<Survey> GetSurveyByGuid(string guid)
        {
            string url = $"{Baseurl}/{guid}";

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<Survey>();
            }
            return null;
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

        public async Task<Survey> PutSurvey(Survey survey)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync<Survey>(Baseurl, survey);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<Survey>();
            else
                return null;
        }

        public async Task<Survey> PostSurveyChange(Survey survey)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync<Survey>(Baseurl, survey);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<Survey>();
            else
                return null;
        }

        public async Task<bool> PostSurveyInactive(int id)
        {
            string url = $"{Baseurl}/inactive/{id}";
            HttpResponseMessage response = await client.PostAsync(url, null);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteSurvey(int id)
        {
            string url = $"{Baseurl}/{id}";
            HttpResponseMessage response = await client.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
    }
}