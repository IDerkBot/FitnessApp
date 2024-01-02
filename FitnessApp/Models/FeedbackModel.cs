namespace FitnessApp.Models
{
    public class FeedbackModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => FirstName + " " + LastName;

        public string Feedback { get; set; }
    }
}
