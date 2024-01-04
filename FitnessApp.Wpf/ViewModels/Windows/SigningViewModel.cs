﻿using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessApp.Core;

namespace FitnessApp.Wpf.ViewModels.Windows;

public class SigningViewModel : ObservableObject
{
    #region Private Properties

    private IOpenView _openViewService;

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

    #endregion

    #region Commands

    #region CreateAccountCommand : Создание нового аккаунта

    /// <summary> Создание нового аккаунта </summary>
    public ICommand CreateAccountCommand { get; set; }

    private void OnCreateAccountCommandExecuted()
    {
        _openViewService.OpenSignUpView();
    }

    private bool CanCreateAccountCommandExecute() => true;

    #endregion CreateAccount

    #region LoadedCommand : Загрузка окна

    /// <summary> Загрузка окна </summary>
    public ICommand LoadedCommand { get; set; }

    private void OnLoadedCommandExecuted()
    {
        CreateAccountCommand.Execute(null);
    }

    private bool CanLoadedCommandExecute() => true;

    #endregion Loaded

    #region SignInCommand : Аунтефикация пользователя

    /// <summary> Аунтефикация пользователя </summary>
    public ICommand SignInCommand { get; set; }

    private void OnSignInCommandExecuted()
    {
        bool isAccountFound = App.Database.IsUserExists(Login, Password);

        if (isAccountFound)
        {
            _openViewService.OpenUserView();
            _openViewService.CloseSigningView();
            // if (App.Database.AccountType == "User")
            // {
            //     // Open User Main Window
            //     UserWindow userWindowTemp = new UserWindow(App.Database.AccountId);
            //     userWindowTemp.Show();
            // }
            // else
            // {
            //     // Open Admin Main Window
            //     AdminWindow adminWindowTemp = new AdminWindow(App.Database.AccountId);
            //     adminWindowTemp.Show();
            // }

            // Close Signing Window
            // Close();
        }
        else
        {
            // Display error when the user is not found
            // ErrorsSnackbar.MessageQueue?.Enqueue("Incorrect Email Or Password");
        }
    }

    private bool CanSignInCommandExecute() => true;

    #endregion SignIn
    
    #endregion

    #region Constructor

    public SigningViewModel(IOpenView openView)
    {
        LoadedCommand = new RelayCommand(OnLoadedCommandExecuted, CanLoadedCommandExecute);
        CreateAccountCommand = new RelayCommand(OnCreateAccountCommandExecuted, CanCreateAccountCommandExecute);
        SignInCommand = new RelayCommand(OnSignInCommandExecuted, CanSignInCommandExecute);
        _openViewService = openView;
    }

    #endregion
}