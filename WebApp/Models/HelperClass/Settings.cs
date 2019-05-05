namespace WebApp.Models.HelperClass
{
    public class Settings
    {
        // Max questions allowed in a Survey
        public const int MaxQuestionsInSurvey = 3;

        // Users can answer between MinimumAnswer and MaximumAnswer on SurveyQuestions
        public const int MinimumAnswer = 1;
        public const int MaximumAnswer = 10;
        public const int DefaultAnswer = 5;

        public const string BaseurlWebApi = "https://productionwebapi.azurewebsites.net";
    }
}