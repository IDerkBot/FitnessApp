using FitnessApp.Core;
using FitnessApp.Models;

namespace FitnessApp.Wpf.ViewModels.UserControls
{
    class DayViewModel
    {
       
        private List<DayInPlan> _days;

        public DayViewModel(int planID)
        {
            //Initialize 30 Days of Plan of ID: planNumber + 1 in Days Array

            Days = Global.Database.GetPlanDays(planID);
        }

        public List<DayInPlan> Days
        {
            get => _days;
            set => _days = value;
        }
    }

}
