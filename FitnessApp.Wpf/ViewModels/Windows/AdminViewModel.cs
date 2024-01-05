using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessApp.Core;
using FitnessApp.Wpf.ViewModels.AdminPages;

namespace FitnessApp.Wpf.ViewModels.Windows;

public class AdminViewModel : ObservableObject
{
    #region Private Properties

    private readonly IOpenView _openView;

    #endregion Private Properties

    #region Properties

    #region CurrentContext : ObservableObject - Description

    private ObservableObject _currentContext;

    /// <summary> Description </summary>
    public ObservableObject CurrentContext
    {
        get => _currentContext;
        set => SetProperty(ref _currentContext, value);
    }

    #endregion CurrentContext

    #region AdminHomeVm : AdminHomeViewModel - Description

    private AdminHomeViewModel _adminHomeVm;

    /// <summary> Description </summary>
    public AdminHomeViewModel AdminHomeVm
    {
        get => _adminHomeVm;
        set => SetProperty(ref _adminHomeVm, value);
    }

    #endregion AdminHomeVm

    #region AdminSettingsVm : AdminSettingsViewModel - Description

    private AdminSettingsViewModel _adminSettingsVm;

    /// <summary> Description </summary>
    public AdminSettingsViewModel AdminSettingsVm
    {
        get => _adminSettingsVm;
        set => SetProperty(ref _adminSettingsVm, value);
    }

    #endregion AdminSettingsVm

    #region AdminChallengesSetupVm : ChallengesSetupViewModel - Description

    private ChallengesSetupViewModel _adminChallengesSetupVm;

    /// <summary> Description </summary>
    public ChallengesSetupViewModel AdminChallengesSetupVm
    {
        get => _adminChallengesSetupVm;
        set => SetProperty(ref _adminChallengesSetupVm, value);
    }

    #endregion AdminChallengesSetupVm

    #region IsNewAdmin : bool - Description

    private bool _isNewAdmin;

    /// <summary> Description </summary>
    public bool IsNewAdmin
    {
        get => _isNewAdmin;
        set => SetProperty(ref _isNewAdmin, value);
    }

    #endregion IsNewAdmin

    #endregion Properties

    #region Commands

    #region Loaded - Description

    ///<summary> Description </summary>
    public ICommand LoadedCommand { get; }

    private void OnLoadedCommandExecuted()
    {
        MoveOnHomeCommand.Execute(null);
    }

    private bool CanLoadedCommandExecute()
    {
        return true;
    }

    #endregion Loaded

    #region MoveOnHome - Description

    ///<summary> Description </summary>
    public ICommand MoveOnHomeCommand { get; }

    private void OnMoveOnHomeCommandExecuted()
    {
        _openView.OpenAdminHomeView();
        CurrentContext = AdminHomeVm;
    }

    private bool CanMoveOnHomeCommandExecute()
    {
        return true;
    }

    #endregion MoveOnHome

    #region MoveOnChallengesSetup - Description

    ///<summary> Description </summary>
    public ICommand MoveOnChallengesSetupCommand { get; }

    private void OnMoveOnChallengesSetupCommandExecuted()
    {
        _openView.OpenChallengesSetupView();
        CurrentContext = AdminChallengesSetupVm;
    }

    private bool CanMoveOnChallengesSetupCommandExecute()
    {
        return true;
    }

    #endregion MoveOnChallengesSetup

    #region MoveOnSettings - Description

    ///<summary> Description </summary>
    public ICommand MoveOnSettingsCommand { get; }

    private void OnMoveOnSettingsCommandExecuted()
    {
        _openView.OpenAdminSettingsView();
        CurrentContext = AdminSettingsVm;
    }

    private bool CanMoveOnSettingsCommandExecute()
    {
        return true;
    }

    #endregion MoveOnSettings

    #region Logout - Description

    ///<summary> Description </summary>
    public ICommand LogoutCommand { get; }

    private void OnLogoutCommandExecuted()
    {
        App.Database.Logout();
        _openView.Logout(true);
    }

    private bool CanLogoutCommandExecute()
    {
        return true;
    }

    #endregion Logout
    
    #endregion Commands

    #region Constructor

    public AdminViewModel(IOpenView openView)
    {
        _openView = openView;

        LoadedCommand = new RelayCommand(OnLoadedCommandExecuted, CanLoadedCommandExecute);
        MoveOnHomeCommand = new RelayCommand(OnMoveOnHomeCommandExecuted, CanMoveOnHomeCommandExecute);
        MoveOnChallengesSetupCommand = new RelayCommand(OnMoveOnChallengesSetupCommandExecuted, CanMoveOnChallengesSetupCommandExecute);
        MoveOnSettingsCommand = new RelayCommand(OnMoveOnSettingsCommandExecuted, CanMoveOnSettingsCommandExecute);
        LogoutCommand = new RelayCommand(OnLogoutCommandExecuted, CanLogoutCommandExecute);


        ControlUpdateNewAdminPasswordGrid(App.Database.SignedUser.Id);

        // Initialize AdminWindowPages Objects

        AdminHomeVm = new AdminHomeViewModel();
        AdminChallengesSetupVm = new ChallengesSetupViewModel();
        AdminSettingsVm = new AdminSettingsViewModel();

        // Intialize MessagesQueue and Assign it to MessagesSnackbar's MessageQueue
        // var MessagesQueue = new SnackbarMessageQueue(System.TimeSpan.FromMilliseconds(2000));
        // MessagesSnackbar.MessageQueue = MessagesQueue;
    }

    #endregion Constructor

    private void AdminWindowPagesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Close Side Menu Drawer
        // SideMenuDrawer.IsLeftDrawerOpen = false;

        // Navigate to the selected Page and Highlight the chosen Item
        // switch (AdminWindowPagesListBox.SelectedIndex)
        // {
        //     case 0:
        //         AdminWindowPagesFrame.NavigationService.Navigate(AdminHomePageObject);
        //         HighlightItem(HomeTextBlock, HomeIcon);
        //         break;
        //
        //     case 1:
        //         AdminWindowPagesFrame.NavigationService.Navigate(ChallengesSetupPageObject);
        //         HighlightItem(SetupChallengesTextBlock, ChallengesIcon);
        //         break;
        //
        //     case 2:
        //         AdminWindowPagesFrame.NavigationService.Navigate(AdminSettingsPageObject);
        //         HighlightItem(SettingsTextBlock, SettingsIcon);
        //         break;
        // }
    }

    private void ControlUpdateNewAdminPasswordGrid(int accountId)
    {
        // if (App.Database.IsNewAdmin(accountId))
        // {
        //     UpdateNewAdminPasswordGrid.Visibility = Visibility.Visible;
        // }
        // else
        //     UpdateNewAdminPasswordGrid.Visibility = Visibility.Collapsed;
    }

    private void UpdatePasswordButton_Click(object sender, RoutedEventArgs e)
    {
        // Empty Fields Validation
        // if (string.IsNullOrWhiteSpace(OldPasswordTextBox.Password))
        //     AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Please enter your old password!");
        //
        // else if (string.IsNullOrWhiteSpace(NewPasswordTextBox.Password))
        //     AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Please enter your new password!");
        //
        // else if (NewPasswordTextBox.Password != ConfirmNewPasswordTextBox.Password)
        //     AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("New Password and Confirmation doesn't match!");
        //
        // // Password Validation
        // else if (NewPasswordTextBox.Password.Length < 7)
        //     AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Password must be 7 characters or more");
        //
        // else if (App.Database.EncryptPassword(OldPasswordTextBox.Password) != signedInAdmin.Password)
        //     AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Old Password is Incorrect!");
        //
        // else
        // {
        //     // Update signedInAdmin User Model
        //     signedInAdmin.Password = App.Database.EncryptPassword(NewPasswordTextBox.Password);
        //
        //     // Update Admin's Password in database
        //     App.Database.UpdateAdminPassword(signedInAdmin);
        //
        //     // Confirmation Message
        //     AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Password Updated!");
        //
        //     // Hide UpdateNewAdminPasswordGrid
        //     UpdateNewAdminPasswordGrid.Visibility = Visibility.Collapsed;
        // }
    }

    private void LogoutListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // SigningWindow SigningWindowTemp = new SigningWindow();
        // Close();
        // SigningWindowTemp.Show();
    }
}