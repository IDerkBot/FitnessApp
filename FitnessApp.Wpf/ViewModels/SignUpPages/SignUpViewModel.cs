using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessApp.Models;
using FitnessApp.Windows;

namespace FitnessApp.Wpf.ViewModels.SignUpPages;

public class SignUpViewModel : ObservableObject
{
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
            // string.IsNullOrWhiteSpace(PasswordTextBox.Password) ||
            string.IsNullOrWhiteSpace(Person.Gender) ||
            Person.BirthDate == null)
        {
            SigningWindow.SigningWindowObject.ErrorsSnackbar.MessageQueue.Enqueue("All fields are required!");
        }

        else if (!Person.Email.Contains("@"))
            SigningWindow.SigningWindowObject.ErrorsSnackbar.MessageQueue.Enqueue("Invalid E-mail");

        else if (App.Database.IsEmailTaken(Person.Email))
            SigningWindow.SigningWindowObject.ErrorsSnackbar.MessageQueue.Enqueue("Email is already taken!");

        else if (User.Password != ConfirmedPassword)
        {
            SigningWindow.SigningWindowObject.ErrorsSnackbar.MessageQueue.Enqueue(
                "Password and Confirmation doesn't match!");
            return;
        }
        
        else if (User.Password.Length < 7)
        {
            SigningWindow.SigningWindowObject.ErrorsSnackbar.MessageQueue.Enqueue(
                "Password must be 7 characters or more");
            return;
        }

        App.Database.AddUser(User);
        App.Database.AddPerson(Person);

        // NavigationService.Navigate(SigningWindow.SetUpProfilePageObject);

        //Change Back Card Header
        // SigningWindow.SigningWindowObject.BackArrowButton.Visibility = Visibility.Hidden;
        // SigningWindow.SigningWindowObject.BackCardHeaderTextBlock.Text = "Set up your Profile";
        // SigningWindow.SigningWindowObject.BackCardHeaderTextBlock.Margin = new Thickness(-15);
    }

    private bool CanSignUpCommandExecute() => true;

    #endregion SignUp

    #endregion

    public SignUpViewModel()
    {
        SignUpCommand = new RelayCommand(OnSignUpCommandExecuted, CanSignUpCommandExecute);

        User = new User();
        Person = new Person(User);
    }
}