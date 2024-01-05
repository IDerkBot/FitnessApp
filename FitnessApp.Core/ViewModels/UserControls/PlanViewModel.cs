using FitnessApp.Core;
using FitnessApp.Models;

namespace FitnessApp.Wpf.ViewModels.UserControls
{
    class PlanViewModel
    {
       
        private List<Plan> planModels;

        public PlanViewModel(int accountID)
        {
            planModels = Global.Database.GetPlans(accountID);
        }

        public List<Plan> Plans
        {
            get => planModels;
            set { }
        }
    }
}

    

