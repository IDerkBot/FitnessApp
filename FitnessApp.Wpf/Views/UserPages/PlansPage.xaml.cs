using FitnessApp.Models;
using FitnessApp.ViewModels;
using FitnessApp.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using FitnessApp.Wpf;

namespace FitnessApp.UserWindowPages
{
    /// <summary>
    /// Interaction logic for PlansPage.xaml
    /// </summary>
    public partial class PlansPage : Page
    {

        public PlansPage()
        {
            InitializeComponent();
            UserWindow.PlansPageObject = this;
            LoadAllPlansCards();
        }

        public void LoadAllPlansCards()
        {
            PlansListBox.DataContext = new PlansViewModel(UserWindow.signedInUser.Id);
        }


        private void ViewMoreButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button button = sender as Button;
            int selectedPlanIndex = PlansListBox.Items.IndexOf(button.DataContext);
            Plan currentPlan = (Plan)PlansListBox.Items[selectedPlanIndex];

            PlanDaysListBox.DataContext = new DaysViewModel(currentPlan.Id);
            DaysSideDrawer.IsRightDrawerOpen = true;
        }


        private void JoinPlanButton_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            int selectedPlanIndex = PlansListBox.Items.IndexOf(toggleButton.DataContext);
            Plan currentPlan = (Plan)PlansListBox.Items[selectedPlanIndex];

            if (App.Database.IsInPlan(UserWindow.signedInUser.Id))
                UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("You are currently in a plan. Please unjoin it first.");
            else
                App.Database.JoinPlan(UserWindow.signedInUser.Id, currentPlan.Id);

            LoadAllPlansCards();

            // Rrefresh Joined Plan Card in Home Page 
            UserWindow.HomePageObject.LoadJoinedPlanCard();
        }

        private void JoinPlanButton_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            int selectedPlanIndex = PlansListBox.Items.IndexOf(toggleButton.DataContext);
            Plan currentPlan = (Plan)PlansListBox.Items[selectedPlanIndex];

            App.Database.UnjoinPlan(UserWindow.signedInUser.Id);

            LoadAllPlansCards();

            // Rrefresh Joined Plan Card in Home Page 
            UserWindow.HomePageObject.LoadJoinedPlanCard();
        }
    }
}
