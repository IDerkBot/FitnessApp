﻿using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessApp.Core.Interfaces;
using FitnessApp.Core.ViewModels.UserPages;
using FitnessApp.Models;

namespace FitnessApp.Core.ViewModels.Windows;

public class UserViewModel : ObservableObject
{
    #region Private Properties

    private readonly IOpenView _openView;

    #endregion

    #region Properties

    #region CurrentPerson : Person - Профиль пользователя

    private Person _currentPerson;

    /// <summary> Профиль пользователя </summary>
    public Person CurrentPerson
    {
        get => _currentPerson;
        set => SetProperty(ref _currentPerson, value);
    }

    #endregion Person

    #region CaloriesCalculatorVm : CaloriesCalculatorViewModel - View Model для калькулятора калорий

    private CaloriesCalculatorViewModel _caloriesCalculatorVm;

    /// <summary> View Model для калькулятора калорий </summary>
    public CaloriesCalculatorViewModel CaloriesCalculatorVm
    {
        get => _caloriesCalculatorVm;
        set => SetProperty(ref _caloriesCalculatorVm, value);
    }

    #endregion CaloriesCalculatorVm

    #region ChallengesVm : ChallengesViewModel - ViewModel для челенжей

    private ChallengesViewModel _challengesVm;

    /// <summary> ViewModel для челенжей </summary>
    public ChallengesViewModel ChallengesVm
    {
        get => _challengesVm;
        set => SetProperty(ref _challengesVm, value);
    }

    #endregion ChallengesVm

    #region HomeVm : HomeViewModel - View Model для домашней части

    private HomeViewModel _homeVm;

    /// <summary> View Model для домашней части </summary>
    public HomeViewModel HomeVm
    {
        get => _homeVm;
        set => SetProperty(ref _homeVm, value);
    }

    #endregion HomeVm

    #region PlansVm : PlansViewModel - Description

    private PlansViewModel _plansVm;

    /// <summary> Description </summary>
    public PlansViewModel PlansVm
    {
        get => _plansVm;
        set => SetProperty(ref _plansVm, value);
    }

    #endregion PlansVm

    #region SettingsVm : SettingsViewModel - Description

    private SettingsViewModel _settingsVm;

    /// <summary> Description </summary>
    public SettingsViewModel SettingsVm
    {
        get => _settingsVm;
        set => SetProperty(ref _settingsVm, value);
    }

    #endregion SettingsVm

    #region CurrentContext : ObservableObject - Корректная ViewModel

    private ObservableObject _currentContext;

    /// <summary> Корректная ViewModel </summary>
    public ObservableObject CurrentContext
    {
        get => _currentContext;
        set => SetProperty(ref _currentContext, value);
    }

    #endregion CurrentContext

    #endregion

    #region Commands

    #region MoveOnHomeCommand : Перемещение на домашнюю страницу

    /// <summary> Перемещение на домашнюю страницу </summary>
    public ICommand MoveOnHomeCommand { get; set; }

    private void OnMoveOnHomeCommandExecuted()
    {
        _openView.OpenHomeView();
        CurrentContext = HomeVm;
    }

    private bool CanMoveOnHomeCommandExecute() => true;

    #endregion MoveOnHome

    #region MoveOnChallengesCommand : Перемещение на страницу "Вызовы"

    /// <summary> Перемещение на страницу "Вызовы" </summary>
    public ICommand MoveOnChallengesCommand { get; set; }

    private void OnMoveOnChallengesCommandExecuted()
    {
        _openView.OpenChallengesView();
        CurrentContext = ChallengesVm;
    }

    private bool CanMoveOnChallengesCommandExecute() => true;

    #endregion MoveOnChallenges

    #region MoveOnCaloriesCalculatorCommand : Перемещение на страницу "Калькулятор калорий"

    /// <summary> Перемещение на страницу "Калькулятор калорий" </summary>
    public ICommand MoveOnCaloriesCalculatorCommand { get; set; }

    private void OnMoveOnCaloriesCalculatorCommandExecuted()
    {
        _openView.OpenCaloriesCalculatorView();
        CurrentContext = CaloriesCalculatorVm;
    }

    private bool CanMoveOnCaloriesCalculatorCommandExecute() => true;

    #endregion MoveOnCaloriesCalculator

    #region MoveOnPlansCommand : Перемещение на страницу "Тренировочные планы"

    /// <summary> Перемещение на страницу "Тренировочные планы" </summary>
    public ICommand MoveOnPlansCommand { get; set; }

    private void OnMoveOnPlansCommandExecuted()
    {
        _openView.OpenPlansView();
        CurrentContext = PlansVm;
    }

    private bool CanMoveOnPlansCommandExecute() => true;

    #endregion MoveOnPlans

    #region MoveOnSettingsCommand : Перемещение на страницу "Настройки"

    /// <summary> Перемещение на страницу "Настройки" </summary>
    public ICommand MoveOnSettingsCommand { get; set; }

    private void OnMoveOnSettingsCommandExecuted()
    {
        _openView.OpenSettingsView();
        CurrentContext = SettingsVm;
    }

    private bool CanMoveOnSettingsCommandExecute() => true;

    #endregion MoveOnSettings

    #region LogoutCommand : Выход из аккаунта

    /// <summary> Выход из аккаунта </summary>
    public ICommand LogoutCommand { get; set; }

    private void OnLogoutCommandExecuted()
    {
        _openView.Logout(false);
    }

    private bool CanLogoutCommandExecute() => true;

    #endregion Logout

    #region LoadedCommand : Загрузка отображения

    /// <summary> Загрузка отображения </summary>
    public ICommand LoadedCommand { get; set; }

    private void OnLoadedCommandExecuted()
    {
        MoveOnHomeCommand.Execute(null);
    }

    private bool CanLoadedCommandExecute() => true;

    #endregion Loaded

    #endregion

    #region Constructor

    /// <summary>  </summary>
    /// <param name="openView"></param>
    /// <param name="alertService"></param>
    public UserViewModel(IOpenView openView, IAlertService alertService)
    {
        _openView = openView;

        LoadedCommand = new RelayCommand(OnLoadedCommandExecuted, CanLoadedCommandExecute);
        MoveOnHomeCommand = new RelayCommand(OnMoveOnHomeCommandExecuted, CanMoveOnHomeCommandExecute);
        MoveOnChallengesCommand =
            new RelayCommand(OnMoveOnChallengesCommandExecuted, CanMoveOnChallengesCommandExecute);
        MoveOnCaloriesCalculatorCommand = new RelayCommand(OnMoveOnCaloriesCalculatorCommandExecuted,
            CanMoveOnCaloriesCalculatorCommandExecute);
        MoveOnPlansCommand = new RelayCommand(OnMoveOnPlansCommandExecuted, CanMoveOnPlansCommandExecute);
        MoveOnSettingsCommand = new RelayCommand(OnMoveOnSettingsCommandExecuted, CanMoveOnSettingsCommandExecute);
        LogoutCommand = new RelayCommand(OnLogoutCommandExecuted, CanLogoutCommandExecute);

        // Initialize User Model
        CurrentPerson = Global.Database.GetPersonByUserId(Global.Database.AccountId)!;

        // Initialize View Models
        HomeVm = new HomeViewModel(_openView, alertService, CurrentPerson);
        ChallengesVm = new ChallengesViewModel(CurrentPerson);
        PlansVm = new PlansViewModel(CurrentPerson);
        CaloriesCalculatorVm = new CaloriesCalculatorViewModel(CurrentPerson);
        SettingsVm = new SettingsViewModel(CurrentPerson);
    }

    #endregion Constructor
}