using FitnessApp.Models;
using System.Collections.Generic;
using FitnessApp.DataAccessLayer;

namespace FitnessApp.ViewModels
{
    class PlansViewModel
    {
       
        private List<Plan> planModels;

        public PlansViewModel(int accountID)
        {
            planModels = Database.GetPlans(accountID);
        }

        public List<Plan> Plans
        {
            get => planModels;
            set { }
        }
    }
}

    

