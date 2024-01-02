using FitnessApp.Models;
using System.Collections.Generic;
using FitnessApp.DataAccessLayer;

namespace FitnessApp.ViewModels
{
    class DaysViewModel
    {
       
        private List<Day> _days;

        public DaysViewModel(int planID)
        {
            //Initialize 30 Days of Plan of ID: planNumber + 1 in Days Array

            Days = Database.GetPlanDays(planID);
        }

        public List<Day> Days
        {
            get => _days;
            set => _days = value;
        }
    }

}
