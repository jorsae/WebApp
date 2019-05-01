namespace WebApp.Models.HelperClass
{
    public class SurveyQuestionFrequency
    {
        public int Answer { get; set; }
        public int Frequency { get; set; }

        public override string ToString()
        {
            return $"Answer: {Answer}, Frequency: {Frequency}";
        }
    }
}