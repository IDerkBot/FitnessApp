using FitnessApp.Models;
using System.Collections.Generic;
using FitnessApp.DataAccessLayer;
using FitnessApp.Wpf;

namespace FitnessApp.ViewModels
{
    class FeedbacksViewModel
    {
       

        private List<Feedback> _feedbacks;
        public FeedbacksViewModel()
        {
            Feedbacks = App.Database.GetFeedbacks();
        }
        public List<Feedback> Feedbacks {  get => Feedbacks; set { } }
    }
}
