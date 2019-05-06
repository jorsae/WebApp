namespace WebApp.Models.HelperClass
{
    public class Settings
    {
        // Max questions allowed in a Survey
        public const int MaxQuestionsInSurvey = 3;

        // Minimum answer the user can give to a question
        public const int MinimumAnswer = 1;
        // Maximum answer the user can give to a question
        public const int MaximumAnswer = 10;
        // Default answer the user can give to a question
        public const int DefaultAnswer = 5;

        // Baseurl to the Web API the Web App should use
        public const string BaseurlWebApi = "https://bo19webapi.azurewebsites.net";
    }
}