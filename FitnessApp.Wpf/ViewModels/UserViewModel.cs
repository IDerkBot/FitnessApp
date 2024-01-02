using FitnessApp.Models;
using System.Collections.Generic;
using FitnessApp.DataAccessLayer;
using FitnessApp.Wpf;


namespace FitnessApp.ViewModels
{
    class UserViewModel
    {

        private List<User> userModels;

        public UserViewModel(string userNameOrEmail)
        {
            userModels = App.Database.SearchForUser(userNameOrEmail);
        }


        public List<User> UserModels { get => userModels; set { } }

    }
}
