using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessApp.Core.Interfaces;
using FitnessApp.Models;

namespace FitnessApp.Core.ViewModels.SignUpPages;

public class SignUpViewModel : ObservableObject
{
    #region Private Properties

    private readonly IOpenView _openViewService;
    private readonly IAlertService _alertService;

    #endregion Private Properties
    
    #region Properties

    #region User : User - Пользователь

    private User _user;

    /// <summary> Пользователь </summary>
    public User User
    {
        get => _user;
        set => SetProperty(ref _user, value);
    }

    #endregion User

    #region Person : Person - Профиль пользователя

    private Person _person;

    /// <summary> Профиль пользователя </summary>
    public Person Person
    {
        get => _person;
        set => SetProperty(ref _person, value);
    }

    #endregion Person
    
    #region ConfirmedPassword : string - Подтверждение пароля

    private string _confirmedPassword;

    /// <summary> Подтверждение пароля </summary>
    public string ConfirmedPassword
    {
        get => _confirmedPassword;
        set => SetProperty(ref _confirmedPassword, value);
    }

    #endregion ConfirmedPassword

    #region Genders : ObservableCollection<string> - Description

    private ObservableCollection<string> _genders;

    /// <summary> Description </summary>
    public ObservableCollection<string> Genders
    {
        get => _genders;
        set => SetProperty(ref _genders, value);
    }

    #endregion Genders
    
    #endregion

    #region Commands

    #region NextViewCommand : Description

    /// <summary> Description </summary>
    public ICommand NextViewCommand { get; set; }

    private void OnNextViewCommandExecuted()
    {
        _openViewService.OpenSetUpProfileView();
    }

    private bool CanNextViewCommandExecute() => true;

    #endregion NextView
    
    #region SignUpCommand : Регистрация нового пользователя

    /// <summary> Регистрация нового пользователя </summary>
    public ICommand SignUpCommand { get; set; }

    private void OnSignUpCommandExecuted()
    {
        // Empty Fields Validation
        if (string.IsNullOrWhiteSpace(Person.FirstName) ||
            string.IsNullOrWhiteSpace(Person.LastName) ||
            string.IsNullOrWhiteSpace(Person.Gender))
        {
            _alertService.Error("All fields are required!");
            return;
        }

        if (!Person.Email.Contains("@"))
        {
            _alertService.Error("Invalid E-mail");
            return;
        }

        if (Global.Database.IsEmailTaken(Person.Email))
        {
            _alertService.Error("Email is already taken!");
            return;
        }

        if (User.Password != ConfirmedPassword)
        {
            _alertService.Error("Password and Confirmation doesn't match!");
            return;
        }

        if (User.Password.Length < 7)
        {
            _alertService.Error("Password must be 7 characters or more");
            return;
        }

        Global.Database.AddUser(User);
        Global.Database.AddPerson(Person);
        
        // // Constarins to make sure that all fields are filled
        //     if (string.IsNullOrWhiteSpace(HeightTextBox            .Text) ||
        //         string.IsNullOrWhiteSpace(WeightTextBox            .Text) ||
        //         string.IsNullOrWhiteSpace(TargetWeightTextBox      .Text) ||
        //         string.IsNullOrWhiteSpace(KilosToLosePerWeekTextBox.Text) ||
        //         string.IsNullOrWhiteSpace(WorkoutsPerWeekTextBox   .Text) ||
        //         string.IsNullOrWhiteSpace(WorkoutHoursPerDayTextBox.Text))
        //     {
        //         SigningWindow.SigningWindowObject.ErrorsSnackbar.MessageQueue.Enqueue("All fields are required!");
        //     }
        //
        //     else
        //     {
        //         var date = DateTime.Now;
        //         if (SigningWindow.SignUpPageObject.BirthDatePicker.SelectedDate != null)
        //             date = (DateTime)SigningWindow.SignUpPageObject.BirthDatePicker.SelectedDate;
        //         // Signing up
        //         // App.Database.AddUser(profilePhoto.ByteArray,
        //         //                  SigningWindow.SignUpPageObject.FirstNameTextBox.Text,
        //         //                  SigningWindow.SignUpPageObject.LastNameTextBox.Text,
        //         //                  SigningWindow.SignUpPageObject.EmailTextBox.Text,
        //         //                  SigningWindow.SignUpPageObject.Password,
        //         //                  SigningWindow.SignUpPageObject.GenderComboBox.Text,
        //         //                  date,
        //         //                  double.Parse(WeightTextBox.Text),
        //         //                  double.Parse(HeightTextBox.Text),
        //         //                  double.Parse(TargetWeightTextBox.Text),
        //         //                  double.Parse(KilosToLosePerWeekTextBox.Text),
        //         //                  double.Parse(WorkoutsPerWeekTextBox.Text),
        //         //                  double.Parse(WorkoutHoursPerDayTextBox.Text));
        //
        //         // UserWindow UserWindowTemp = new UserWindow(App.Database.AccountId);
        //         // SigningWindow.SigningWindowObject.Close();
        //         // UserWindowTemp.Show();
        //     }
    }

    private bool CanSignUpCommandExecute() => true;

    #endregion SignUp

    #endregion

    #region Constructor

    public SignUpViewModel(IOpenView openViewView, IAlertService alertService)
    {
        _openViewService = openViewView;
        _alertService = alertService;
        
        NextViewCommand = new RelayCommand(OnNextViewCommandExecuted, CanNextViewCommandExecute);
        SignUpCommand = new RelayCommand(OnSignUpCommandExecuted, CanSignUpCommandExecute);

        User = new User();
        Person = new Person(User);

        Genders = new ObservableCollection<string> { "Male", "Female" };
    }

    #endregion Constructor

    #region Private Methods

    // private void ChooseUserProfilePhotoButton_Click(object sender, RoutedEventArgs e)
    // {
    //     OpenFileDialog browsePhotoDialog = new OpenFileDialog();
    //     browsePhotoDialog.Title  = "Select your Profile Photo";
    //     browsePhotoDialog.Filter = "All formats|*.jpg;*.jpeg;*.png|" +
    //                                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
    //                                "PNG (*.png)|*.png";
    //
    //
    //     if (browsePhotoDialog.ShowDialog() == true)
    //     {
    //         // profilePhoto = new ImageModel(browsePhotoDialog.FileName);
    //         // UserProfilePhoto.ImageSource = profilePhoto.Source;
    //     }
    // }

    #endregion
}