using FitnessApp.Models;
using System.Collections.Generic;
using FitnessApp.DataAccessLayer;

namespace FitnessApp.ViewModels
{
    class FeedbacksViewModel
    {
       

        private List<Feedback> _feedbacks;
        public FeedbacksViewModel()
        {
            Feedbacks = Database.GetFeedbacks();
        }
        public List<Feedback> Feedbacks {  get => Feedbacks; set { } }
    }
}
