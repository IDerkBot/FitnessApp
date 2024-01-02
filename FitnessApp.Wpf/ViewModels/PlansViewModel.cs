using FitnessApp.Models;
using System.Collections.Generic;
using FitnessApp.DataAccessLayer;
using FitnessApp.Wpf;

namespace FitnessApp.ViewModels
{
    class PlansViewModel
    {
       
        private List<Plan> planModels;

        public PlansViewModel(int accountID)
        {
            planModels = App.Database.GetPlans(accountID);
        }

        public List<Plan> Plans
        {
            get => planModels;
            set { }
        }
    }
}

    

