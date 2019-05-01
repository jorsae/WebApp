namespace WebApp.Models
{
    public class SurveyQuestionStats
    {
        public int? Count { get; set; }
        public int? Max { get; set; }
        public int? Min { get; set; }
        public float? Average { get; set; }

        public override string ToString()
        {
            return $"Count: {Count}, Max: {Max}, Min: {Min}, Average: {Average}";
        }
    }
}