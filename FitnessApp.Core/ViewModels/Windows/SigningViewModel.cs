using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessApp.Core.Interfaces;
using FitnessApp.Core.ViewModels.SignUpPages;

namespace FitnessApp.Core.ViewModels.Windows;

public class SigningViewModel : ObservableObject
{
    #region Private Properties

    private IOpenView _openViewService;
    private IAlertService _alertService;

    #endregion

    #region Properties

    #region Login : string - Логин пользователя

    private string _login;

    /// <summary> Логин пользователя </summary>
    public string Login
    {
        get => _login;
        set => SetProperty(ref _login, value);
    }

    #endregion Login

    #region Password : string - Пароль пользователя

    private string _password;

    /// <summary> Пароль пользователя </summary>
    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    #endregion Password

    #region SignUpVm : SignUpViewModel - Description

    private SignUpViewModel _signUpVm;

    /// <summary> Description </summary>
    public SignUpViewModel SignUpVm
    {
        get => _signUpVm;
        set => SetProperty(ref _signUpVm, value);
    }

    #endregion SignUpVm

    #region IsCreateNewAccount : bool - Description

    private bool _isCreateNewAccount;

    /// <summary> Description </summary>
    public bool IsCreateNewAccount
    {
        get => _isCreateNewAccount;
        set => SetProperty(ref _isCreateNewAccount, value);
    }

    #endregion IsCreateNewAccount
    
    #endregion

    #region Commands

    #region CreateAccountCommand : Создание нового аккаунта

    /// <summary> Создание нового аккаунта </summary>
    public ICommand CreateAccountCommand { get; set; }

    private void OnCreateAccountCommandExecuted()
    {
        IsCreateNewAccount = true;
        _openViewService.OpenSignUpView();
    }

    private bool CanCreateAccountCommandExecute() => true;

    #endregion CreateAccount

    #region CancelCreateAccount - Description

    ///<summary> Description </summary>
    public ICommand CancelCreateAccountCommand { get; }

    private void OnCancelCreateAccountCommandExecuted()
    {
        IsCreateNewAccount = false;
    }

    private bool CanCancelCreateAccountCommandExecute() => true;

    #endregion CancelCreateAccount

    #region LoadedCommand : Загрузка окна

    /// <summary> Загрузка окна </summary>
    public ICommand LoadedCommand { get; set; }

    private void OnLoadedCommandExecuted()
    {
        if (!Global.Database.IsConnected)
        {
            _alertService.Error("Нет подключения к базе данных! Попробуйте войти позднее");
            _openViewService.Shutdown();
        }
    }

    private bool CanLoadedCommandExecute() => true;

    #endregion Loaded

    #region SignInCommand : Аунтефикация пользователя

    /// <summary> Аунтефикация пользователя </summary>
    public ICommand SignInCommand { get; set; }

    private void OnSignInCommandExecuted()
    {
        bool isAccountFound = Global.Database.UserExists(Login, Password);

        if (isAccountFound)
        {
            var user = Global.Database.GetUserByLogin(Login)!;
            Global.Database.Authorization(user);

            if (Global.Database.AccountType == AccountAccess.User)
            {
                // Open User Main Window
                _openViewService.OpenUserView();
            }
            else if (Global.Database.AccountType == AccountAccess.Admin)
            {
                // Open Admin Main Window
                _openViewService.OpenAdminView();
            }

            _openViewService.CloseSigningView();
        }
        else
        {
            _alertService.Error("Пользователь не найден");
            // Display error when the user is not found
            // ErrorsSnackbar.MessageQueue?.Enqueue("Incorrect Email Or Password");
        }
    }

    private bool CanSignInCommandExecute() => true;

    #endregion SignIn

    #endregion

    #region Constructor

    public SigningViewModel(IOpenView openView, IAlertService alertService)
    {
        _openViewService = openView;
        _alertService = alertService;
        
        LoadedCommand = new RelayCommand(OnLoadedCommandExecuted, CanLoadedCommandExecute);
        CreateAccountCommand = new RelayCommand(OnCreateAccountCommandExecuted, CanCreateAccountCommandExecute);
        SignInCommand = new RelayCommand(OnSignInCommandExecuted, CanSignInCommandExecute);
        CancelCreateAccountCommand = new RelayCommand(OnCancelCreateAccountCommandExecuted, CanCancelCreateAccountCommandExecute);

        SignUpVm = new SignUpViewModel(_openViewService, _alertService);
    }

    #endregion
}