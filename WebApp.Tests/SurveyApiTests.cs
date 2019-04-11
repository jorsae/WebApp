using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Tests
{
    [TestFixture]
    public class SurveyApiTests
    {
        private SurveyApi surveyApi;

        [SetUp]
        public void SetUp()
        {
            surveyApi = new SurveyApi();
        }


        [Test]
        public async Task Assert_get_survey_by_id()
        {
            Survey survey = await surveyApi.GetSurvey(1);
            Assert.NotNull(survey);
        }

        [Test]
        public async Task Assert_get_all_survey()
        {
            List<Survey> surveys = await surveyApi.GetSurveys();
            Assert.IsNotEmpty(surveys);
        }

    }
}