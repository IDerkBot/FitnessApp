using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using FitnessApp.Models;
using FitnessApp.Wpf.ViewModels.UserControls;
using FitnessApp.Wpf.Views.Windows;
using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;

namespace FitnessApp.Wpf.ViewModels.AdminPages;

public class AdminHomeViewModel : ObservableObject
{
    #region Private Properties

    #endregion Private Properties

    #region Properties

    public SeriesCollection SeriesCollection { get; set; }
    public List<string> Labels { get; set; }
    public Func<double, string> Formatter { get; set; }

    #region Feedbacks : ObservableCollection<Feedback> - Description

    private ObservableCollection<Feedback> _feedbacks;

    /// <summary> Description </summary>
    public ObservableCollection<Feedback> Feedbacks
    {
        get => _feedbacks;
        set => SetProperty(ref _feedbacks, value);
    }

    #endregion Feedbacks

    #region TotalUsers : int - Количество пользователей

    private int _totalUsers;

    /// <summary> Количество пользователей </summary>
    public int TotalUsers
    {
        get => _totalUsers;
        set => SetProperty(ref _totalUsers, value);
    }

    #endregion TotalUsers
    
    #endregion Properties

    #region Commands

    #endregion Commands

    #region Constructor

    public AdminHomeViewModel()
    {
        // LoadAppRatingChart();
        // LoadAppUsersNumber();
        LoadFeedbacks();
    }

    #endregion Constructor

    #region Private Methods

    private void LoadFeedbacks()
    {
        Feedbacks = new ObservableCollection<Feedback>(App.Database.GetFeedbacks().Take(5));
    }

    private void LoadAppRatingChart()
    {
        // AppRatingChart.DataContext = null;
        //
        // SeriesCollection = new SeriesCollection
        // {
        //     new ColumnSeries
        //     {
        //         Title = "Rating",
        //         Values = App.Database.GetAppRatingValues().AsChartValues()
        //     }
        // };
        // Labels = new List<string> { "1", "2", "3", "4", "5" };
        // Formatter = value => value.ToString("N");
        //
        // AppRatingChart.DataContext = this;
    }

    private void LoadAppUsersNumber()
    {
        TotalUsers = App.Database.GetAppUsersNumber();
    }

    private void DeleteFeedbackButton_Click(object sender, RoutedEventArgs e)
    {
        // Button deleteFeedbackButton = sender as Button;
        // int selectedFeedbackIndex = FeedbacksListBox.Items.IndexOf(deleteFeedbackButton.DataContext);
        //
        // Feedback chosenFeedback = (Feedback)FeedbacksListBox.Items[selectedFeedbackIndex];
        // App.Database.DeleteFeedback(chosenFeedback.Message);
        //
        // FeedbacksListBox.DataContext = null;
        // FeedbacksListBox.DataContext = new FeedbackViewModel();
        // LoadAppRatingChart();
    }

    private void AddNewAdminButton_Click(object sender, RoutedEventArgs e)
    {
        // // Empty Fields Validation
        // if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text))
        //     AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("First Name Is Empty!");
        //
        // else if (string.IsNullOrWhiteSpace(LastNameTextBox.Text))
        //     AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Last Name Is Empty!");
        //
        // else if (string.IsNullOrWhiteSpace(NewAdminEmailTextBox.Text))
        //     AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Email Is Empty!");
        //
        // // Check Email Validation
        // else if (!NewAdminEmailTextBox.Text.Contains("@") || !NewAdminEmailTextBox.Text.Contains(".com"))
        //     AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Invalid E-mail");
        //
        // // Check Email not used before
        // else if (App.Database.IsEmailTaken(NewAdminEmailTextBox.Text))
        //     AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("E-mail is in use");
        // else
        // {
        //     App.Database.AddNewAdmin(FirstNameTextBox.Text, LastNameTextBox.Text, NewAdminEmailTextBox.Text);
        //     AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Admin Added Succesfully");
        // }
    }

    private void UserSearchButton_Click(object sender, RoutedEventArgs e)
    {
        // if (!string.IsNullOrWhiteSpace(UserSearchTextBox.Text))
        // {
        //     UserViewModel deletedUserDataContext = new UserViewModel(UserSearchTextBox.Text);
        //     DeleteUserListBox.DataContext = deletedUserDataContext;
        //
        //     // Show card or Error message; depending on number of users
        //     if (deletedUserDataContext.UserModels.Count > 0)
        //         DeleteUsersCard.Visibility = Visibility.Visible;
        //     else
        //         AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("No users found");
        // }
    }

    private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
    {
        // // Get User ID
        // Button deleteButton = sender as Button;
        // int selectedUserIndex = DeleteUserListBox.Items.IndexOf(deleteButton.DataContext);
        // User chosenUser = (User)DeleteUserListBox.Items[selectedUserIndex];
        //
        // // Delete Challenge From Database
        // App.Database.DeleteUser(chosenUser.Id);
        //
        // // Refresh Listbox and Number of users
        // DeleteUserListBox.DataContext = null;
        // UserViewModel deletedUserDataContext = new UserViewModel(UserSearchTextBox.Text);
        // DeleteUserListBox.DataContext = deletedUserDataContext;
        // AppUsersNumberTextBlock.Text = App.Database.GetAppUsersNumber().ToString();
        //
        // // Refresh Feedbacks and Rating Chart
        // FeedbacksListBox.DataContext = null;
        // FeedbacksListBox.DataContext = new FeedbackViewModel();
        // LoadAppRatingChart();
        //
        // // Hide DeleteUsersCard if no remaining users exist 
        // if (deletedUserDataContext.UserModels.Count == 0)
        //     DeleteUsersCard.Visibility = Visibility.Collapsed;
        //
        // // Confirmation Message
        // AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("User deleted successfully");
    }

    #endregion Private Methods
}