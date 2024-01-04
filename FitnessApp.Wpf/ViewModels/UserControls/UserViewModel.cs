using FitnessApp.Models;

namespace FitnessApp.Wpf.ViewModels.UserControls
{
    class UserViewModel
    {

        private List<User> userModels;

        public UserViewModel(string userNameOrEmail)
        {
            userModels = App.Database.SearchForUser(userNameOrEmail).ToList();
        }


        public List<User> UserModels { get => userModels; set { } }

    }
}
