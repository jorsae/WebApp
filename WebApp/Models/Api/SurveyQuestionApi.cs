using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.Models.HelperClass;

namespace WebApp.Models
{
    public class SurveyQuestionApi
    {
        private HttpClient client = new HttpClient();
        private const string Baseurl = "https://bo19webapi.azurewebsites.net/api/surveyquestion";

        public async Task<List<SurveyQuestion>> GetSurveyQuestions(int surveyId)
        {
            string url = $"{Baseurl}/surveyId/{surveyId}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<SurveyQuestion>>();
            }
            return null;
        }

        public async Task<SurveyQuestion> GetSurveyQuestion(int surveyQuestionId)
        {
            string url = $"{Baseurl}/{surveyQuestionId}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<SurveyQuestion>();
            else
                return null;
        }

        public async Task<SurveyQuestionStats> GetSurveyQuestionStats(int surveyQuestionId)
        {
            string url = $"{Baseurl}/stats/{surveyQuestionId}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<SurveyQuestionStats>();
            }
            return null;
        }

        public async Task<List<SurveyQuestionFrequency>> GetSurveyQuestionFrequency(int surveyQuestionId)
        {
            string url = $"{Baseurl}/frequency/{surveyQuestionId}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<SurveyQuestionFrequency>>();
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

        public async Task<bool> DeleteSurveyQuestion(int surveyQuestionId)
        {
            string url = $"{Baseurl}/{surveyQuestionId}";
            HttpResponseMessage response = await client.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
    }
}