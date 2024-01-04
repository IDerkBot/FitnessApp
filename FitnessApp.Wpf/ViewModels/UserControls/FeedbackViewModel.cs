using FitnessApp.Models;

namespace FitnessApp.Wpf.ViewModels.UserControls
{
    class FeedbackViewModel
    {
       

        private List<Feedback> _feedbacks;
        public FeedbackViewModel()
        {
            Feedbacks = App.Database.GetFeedbacks();
        }
        public List<Feedback> Feedbacks {  get => Feedbacks; set { } }
    }
}
