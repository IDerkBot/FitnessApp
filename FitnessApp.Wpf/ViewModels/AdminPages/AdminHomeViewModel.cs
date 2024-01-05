using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using FitnessApp.Core;
using FitnessApp.Models;
using FitnessApp.Wpf.Implementation;
using LiveCharts;

namespace FitnessApp.Wpf.ViewModels.AdminPages;

public class AdminHomeViewModel : ObservableObject
{
    #region Private Properties

    #endregion Private Properties

    #region Properties

    #region SeriesCollection : SeriesCollection - Description

    private SeriesCollection _seriesCollection;

    /// <summary> Description </summary>
    public SeriesCollection SeriesCollection
    {
        get => _seriesCollection;
        set => SetProperty(ref _seriesCollection, value);
    }

    #endregion SeriesCollection

    #region Labels : List<string> - Description

    private List<string> _labels;

    /// <summary> Description </summary>
    public List<string> Labels
    {
        get => _labels;
        set => SetProperty(ref _labels, value);
    }

    #endregion Labels

    #region Formatter : Func<double, string> - Description

    private Func<double, string> _formatter;

    /// <summary> Description </summary>
    public Func<double, string> Formatter
    {
        get => _formatter;
        set => SetProperty(ref _formatter, value);
    }

    #endregion Formatter

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

    #region DeleteFeedback - Description

    ///<summary> Description </summary>
    public ICommand DeleteFeedbackCommand { get; }

    private void OnDeleteFeedbackCommandExecuted()
    {
        // Button deleteFeedbackButton = sender as Button;
        // int selectedFeedbackIndex = FeedbacksListBox.Items.IndexOf(deleteFeedbackButton.DataContext);
        //
        // Feedback chosenFeedback = (Feedback)FeedbacksListBox.Items[selectedFeedbackIndex];
        // Global.Database.DeleteFeedback(chosenFeedback.Message);
        //
        // FeedbacksListBox.DataContext = null;
        // FeedbacksListBox.DataContext = new FeedbackViewModel();
        // LoadAppRatingChart();
    }

    private bool CanDeleteFeedbackCommandExecute()
    {
        return true;
    }

    #endregion DeleteFeedback

    #region AddNewAdmin - Description

    ///<summary> Description </summary>
    public ICommand AddNewAdminCommand { get; }

    private void OnAddNewAdminCommandExecuted()
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
        // else if (Global.Database.IsEmailTaken(NewAdminEmailTextBox.Text))
        //     AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("E-mail is in use");
        // else
        // {
        //     Global.Database.AddNewAdmin(FirstNameTextBox.Text, LastNameTextBox.Text, NewAdminEmailTextBox.Text);
        //     AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Admin Added Succesfully");
        // }
    }

    private bool CanAddNewAdminCommandExecute()
    {
        return true;
    }

    #endregion AddNewAdmin
    
    #region SearchUser - Description

    ///<summary> Description </summary>
    public ICommand SearchUserCommand { get; }

    private void OnSearchUserCommandExecuted()
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

    private bool CanSearchUserCommandExecute()
    {
        return true;
    }

    #endregion SearchUser

    #region DeleteUser - Description

    ///<summary> Description </summary>
    public ICommand DeleteUserCommand { get; }

    private void OnDeleteUserCommandExecuted()
    {
        // // Get User ID
        // Button deleteButton = sender as Button;
        // int selectedUserIndex = DeleteUserListBox.Items.IndexOf(deleteButton.DataContext);
        // User chosenUser = (User)DeleteUserListBox.Items[selectedUserIndex];
        //
        // // Delete Challenge From Database
        // Global.Database.DeleteUser(chosenUser.Id);
        //
        // // Refresh Listbox and Number of users
        // DeleteUserListBox.DataContext = null;
        // UserViewModel deletedUserDataContext = new UserViewModel(UserSearchTextBox.Text);
        // DeleteUserListBox.DataContext = deletedUserDataContext;
        // AppUsersNumberTextBlock.Text = Global.Database.GetAppUsersNumber().ToString();
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

    private bool CanDeleteUserCommandExecute()
    {
        return true;
    }
    
    #endregion DeleteUser

    #endregion Commands

    #region Constructor

    public AdminHomeViewModel()
    {
        DeleteFeedbackCommand = new RelayCommand(OnDeleteFeedbackCommandExecuted, CanDeleteFeedbackCommandExecute);
        AddNewAdminCommand = new RelayCommand(OnAddNewAdminCommandExecuted, CanAddNewAdminCommandExecute);
        SearchUserCommand = new RelayCommand(OnSearchUserCommandExecuted, CanSearchUserCommandExecute);
        DeleteUserCommand = new RelayCommand(OnDeleteUserCommandExecuted, CanDeleteUserCommandExecute);
        
        LoadAppRatingChart();
        LoadAppUsersNumber();
        LoadFeedbacks();
    }

    #endregion Constructor

    #region Private Methods

    private void LoadFeedbacks()
    {
        Feedbacks = new ObservableCollection<Feedback>(Global.Database.GetFeedbacks().Take(5));
    }

    private void LoadAppRatingChart()
    {
        var chartCreator = Ioc.Default.GetService<IChartCreator>();
        
        if(chartCreator == null) return;
        
        SeriesCollection = chartCreator.GetRating();
        Labels = new List<string> { "1", "2", "3", "4", "5" };
        Formatter = value => value.ToString("N");
    }

    private void LoadAppUsersNumber()
    {
        TotalUsers = Global.Database.GetAppUsersNumber();
    }

    #endregion Private Methods
}