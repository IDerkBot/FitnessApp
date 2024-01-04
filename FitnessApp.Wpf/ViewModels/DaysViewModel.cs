using FitnessApp.Models;
using System.Collections.Generic;
using FitnessApp.DataAccessLayer;
using FitnessApp.Wpf;

namespace FitnessApp.ViewModels
{
    class DaysViewModel
    {
       
        private List<DayInPlan> _days;

        public DaysViewModel(int planID)
        {
            //Initialize 30 Days of Plan of ID: planNumber + 1 in Days Array

            Days = App.Database.GetPlanDays(planID);
        }

        public List<DayInPlan> Days
        {
            get => _days;
            set => _days = value;
        }
    }

}
