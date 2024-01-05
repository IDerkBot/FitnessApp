using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessApp.Core.Interfaces;
using FitnessApp.Models;

namespace FitnessApp.Core.ViewModels.SignUpPages;

public class SignUpViewModel : ObservableObject
{
    #region Private Properties

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
    
    #endregion

    #region Commands

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

        // NavigationService.Navigate(SigningWindow.SetUpProfilePageObject);

        //Change Back Card Header
        // SigningWindow.SigningWindowObject.BackArrowButton.Visibility = Visibility.Hidden;
        // SigningWindow.SigningWindowObject.BackCardHeaderTextBlock.Text = "Set up your Profile";
        // SigningWindow.SigningWindowObject.BackCardHeaderTextBlock.Margin = new Thickness(-15);
    }

    private bool CanSignUpCommandExecute() => true;

    #endregion SignUp

    #endregion

    #region Constructor

    public SignUpViewModel(IAlertService alertService)
    {
        _alertService = alertService;
        
        SignUpCommand = new RelayCommand(OnSignUpCommandExecuted, CanSignUpCommandExecute);

        User = new User();
        Person = new Person(User);
    }

    #endregion Constructor
}