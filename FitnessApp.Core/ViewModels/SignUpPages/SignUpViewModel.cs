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
        if (string.IsNullOrWhiteSpace(Person.FirstName) ||
            string.IsNullOrWhiteSpace(Person.LastName) ||
            string.IsNullOrWhiteSpace(Person.Gender) ||
            string.IsNullOrWhiteSpace(Person.Email))
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

        var time = DateTime.Now.Subtract(Person.BirthDate);
        var years = time.Days / 365;


        if (years is < 8 or > 100)
        {
            _alertService.Error("Не корректная дата!");
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

        _openViewService.OpenSetUpProfileView();
    }

    private bool CanNextViewCommandExecute() => true;

    #endregion NextView

    #region SignUpCommand : Регистрация нового пользователя

    /// <summary> Регистрация нового пользователя </summary>
    public ICommand SignUpCommand { get; set; }

    private void OnSignUpCommandExecuted()
    {
        // Constarins to make sure that all fields are filled
        if (Person.Height == 0 || Person.Weight == 0 ||
            Person.TargetWeight == 0 || Person.KilosToLosePerWeek == 0 ||
            Person.WorkoutsPerWeek == 0 || Person.WorkoutHoursPerDay == 0)
        {
            _alertService.Error("All fields are required!");
            return;
        }
        
        // else if(
        //         Person.Height is < 80 or > 300 ||
        //         Person.Weight is < 20 or > 300 ||
        //         Person.TargetWeight is < 20 or > 150 ||
        //         
        //         )

        Global.Database.AddUser(User);
        Global.Database.AddPerson(Person);
    }

    private bool CanSignUpCommandExecute() => true;

    #endregion SignUp

    #region SelectImageForProfile

    public ICommand SelectImageForProfileCommand { get; }

    private void OnSelectImageForProfileCommandExecute()
    {

    }

    private bool CanSelectImageForProfileCommandExecuted() => true;

    #endregion SelectImageForProfile

    #endregion

    #region Constructor

    public SignUpViewModel(IOpenView openViewView, IAlertService alertService)
    {
        _openViewService = openViewView;
        _alertService = alertService;

        NextViewCommand = new RelayCommand(OnNextViewCommandExecuted, CanNextViewCommandExecute);
        SignUpCommand = new RelayCommand(OnSignUpCommandExecuted, CanSignUpCommandExecute);
        SelectImageForProfileCommand = new RelayCommand(OnSelectImageForProfileCommandExecute, CanSelectImageForProfileCommandExecuted);

        User = new User();
        Person = new Person(User);

        Genders = new ObservableCollection<string> { "Male", "Female" };
    }

    #endregion Constructor

    #region Private Methods

    private void ChooseUserProfilePhotoButton_Click()
    {
        // OpenFileDialog browsePhotoDialog = new OpenFileDialog();
        // browsePhotoDialog.Title  = "Select your Profile Photo";
        // browsePhotoDialog.Filter = "All formats|*.jpg;*.jpeg;*.png|" +
        //                            "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
        //                            "PNG (*.png)|*.png";
        //
        //
        // if (browsePhotoDialog.ShowDialog() == true)
        // {
        //     // profilePhoto = new ImageModel(browsePhotoDialog.FileName);
        //     // UserProfilePhoto.ImageSource = profilePhoto.Source;
        // }
    }

    #endregion
}