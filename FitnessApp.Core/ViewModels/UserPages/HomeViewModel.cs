﻿using System.Globalization;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using FitnessApp.Core.Interfaces;
using FitnessApp.Models;
using LiveCharts;
using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Core.ViewModels.UserPages;

public class HomeViewModel : ObservableObject
{
    #region Private Properties

    private readonly IOpenView _openViewService;
    private readonly IAlertService _alertService;

    #endregion Private Properties
    
    #region Properties

    #region Labels : List<string> - Description

    private List<string> _labels;

    /// <summary> Description </summary>
    public List<string> Labels
    {
        get => _labels;
        set => SetProperty(ref _labels, value);
    }

    #endregion Labels

    #region YFormatter : Func<double, string> - Description

    private Func<double, string> _yFormatter;

    /// <summary> Description </summary>
    public Func<double, string> YFormatter
    {
        get => _yFormatter;
        set => SetProperty(ref _yFormatter, value);
    }

    #endregion YFormatter
    
    #region WeightsSeriesCollection : SeriesCollection - Description

    private SeriesCollection _weightsSeriesCollection;

    /// <summary> Description </summary>
    public SeriesCollection WeightsSeriesCollection
    {
        get => _weightsSeriesCollection;
        set => SetProperty(ref _weightsSeriesCollection, value);
    }

    #endregion WeightsSeriesCollection
    
    #region CurrentPerson : Person - Корректный пользователь

    private Person _currentPerson;

    /// <summary> Корректный пользователь </summary>
    public Person CurrentPerson
    {
        get => _currentPerson;
        set => SetProperty(ref _currentPerson, value);
    }

    #endregion CurrentPerson

    #region TodayWeightValue : double - Вес сегодняшний

    private double _todayWeightValue;

    /// <summary> Вес сегодняшний </summary>
    [Range(20.0, 300.0)]
    public double TodayWeightValue
    {
        get => _todayWeightValue;
        set => SetProperty(ref _todayWeightValue, value);
    }

    #endregion TodayWeightValue

    #region TotalWeightLostPerWeek : double - Description

    private double _totalWeightLostPerWeek;

    /// <summary> Description </summary>
    public double TotalWeightLostPerWeek
    {
        get => _totalWeightLostPerWeek;
        set => SetProperty(ref _totalWeightLostPerWeek, value);
    }

    #endregion TotalWeightLostPerWeek

    #region TotalWeightLostPerMonth : double - Description

    private double _totalWeightLostPerMonth;

    /// <summary> Description </summary>
    public double TotalWeightLostPerMonth
    {
        get => _totalWeightLostPerMonth;
        set => SetProperty(ref _totalWeightLostPerMonth, value);
    }

    #endregion TotalWeightLostPerMonth

    #region TotalWeightLostPerYear : double - Description

    private double _totalWeightLostPerYear;

    /// <summary> Description </summary>
    public double TotalWeightLostPerYear
    {
        get => _totalWeightLostPerYear;
        set => SetProperty(ref _totalWeightLostPerYear, value);
    }

    #endregion TotalWeightLostPerYear

    #region AverageWeightLostPerWeek : double - Description

    private double _averageWeightLostPerWeek;

    /// <summary> Description </summary>
    public double AverageWeightLostPerWeek
    {
        get => _averageWeightLostPerWeek;
        set => SetProperty(ref _averageWeightLostPerWeek, value);
    }

    #endregion AverageWeightLostPerWeek

    #region AverageWeightLostPerMonth : double - Description

    private double _averageWeightLostPerMonth;

    /// <summary> Description </summary>
    public double AverageWeightLostPerMonth
    {
        get => _averageWeightLostPerMonth;
        set => SetProperty(ref _averageWeightLostPerMonth, value);
    }

    #endregion AverageWeightLostPerMonth

    #region AverageWeightLostPerYear : double - Description

    private double _averageWeightLostPerYear;

    /// <summary> Description </summary>
    public double AverageWeightLostPerYear
    {
        get => _averageWeightLostPerYear;
        set => SetProperty(ref _averageWeightLostPerYear, value);
    }

    #endregion AverageWeightLostPerYear

    #region HavePlans : bool - Указывает, есть учеловека планы

    private bool _havePlans;

    /// <summary> Указывает, есть учеловека планы </summary>
    public bool HavePlans
    {
        get => _havePlans;
        set => SetProperty(ref _havePlans, value);
    }

    #endregion HavePlans

    #region HaveChallenges : bool - Description

    private bool _haveChallenges;

    /// <summary> Description </summary>
    public bool HaveChallenges
    {
        get => _haveChallenges;
        set => SetProperty(ref _haveChallenges, value);
    }

    #endregion HaveChallenges
    
    #endregion

    #region Commands

    #region LoadedCommand : Description

    /// <summary> Description </summary>
    public ICommand LoadedCommand { get; set; }

    private void OnLoadedCommandExecuted()
    {
        LoadWeightChart();
        LoadTotalWeightLostCard();
        LoadAverageWeightLostCard();
        LoadJoinedChallengesCards();
        LoadJoinedPlanCard();
        LoadMotivationalQuoteCard();
        LoadCaloriesCard();
    }

    private bool CanLoadedCommandExecute() => true;

    #endregion Loaded
    
    #region OpenAddFoodCommand : Description

    /// <summary> Description </summary>
    public ICommand OpenAddFoodCommand { get; set; }

    private void OnOpenAddFoodCommandExecuted()
    {
        _openViewService.OpenFoodAdd();
    }

    private bool CanOpenAddFoodCommandExecute() => true;

    #endregion OpenAddFood

    #region OpenAddWorkoutCommand : Description

    /// <summary> Description </summary>
    public ICommand OpenAddWorkoutCommand { get; set; }

    private void OnOpenAddWorkoutCommandExecuted()
    {
        _openViewService.OpenWorkoutAdd();
    }

    private bool CanOpenAddWorkoutCommandExecute() => true;

    #endregion OpenAddWorkout

    #region CloseAddFoodCommand : Description

    /// <summary> Description </summary>
    public ICommand CloseAddFoodCommand { get; set; }

    private void OnCloseAddFoodCommandExecuted()
    {
        _openViewService.CloseFoodAdd();
    }

    private bool CanCloseAddFoodCommandExecute() => true;

    #endregion CloseAddFood

    #region CloseAddWorkoutCommand : Description

    /// <summary> Description </summary>
    public ICommand CloseAddWorkoutCommand { get; set; }

    private void OnCloseAddWorkoutCommandExecuted()
    {
        _openViewService.CloseWorkoutAdd();
    }

    private bool CanCloseAddWorkoutCommandExecute() => true;

    #endregion CloseAddWorkout

    #region SaveNewWeightCommand : Description

    /// <summary> Description </summary>
    public ICommand SaveNewWeightCommand { get; set; }

    private void OnSaveNewWeightCommandExecuted()
    {
        if (TodayWeightValue is 0 or < 20 or > 300)
        {
            _alertService.Error("Не корректное значение!");
            return;
        }

        // Update Weight in Database
        Global.Database.AddNewWeight(TodayWeightValue, CurrentPerson.User.Id);
        TodayWeightValue = 0;
        
        LoadWeightChart();
        LoadTotalWeightLostCard();
        LoadAverageWeightLostCard();
        LoadCaloriesCard();
    }

    private bool CanSaveNewWeightCommandExecute() => true;

    #endregion SaveNewWeight

    #region JoinedChallengeCommand : Description

    /// <summary> Description </summary>
    public ICommand JoinedChallengeCommand { get; set; }

    private void OnJoinedChallengeCommandExecuted()
    {
// UserWindow.UserWindowObject.UserWindowPagesListBox.SelectedIndex = 1;
    }

    private bool CanJoinedChallengeCommandExecute() => true;

    #endregion JoinedChallenge

    #region LeaveChallengeCommand : Description

    /// <summary> Description </summary>
    public ICommand LeaveChallengeCommand { get; set; }

    private void OnLeaveChallengeCommandExecuted()
    {
        // ToggleButton toggleButton = sender as ToggleButton;
        // int selectedChallengeIndex = UncompletedJoinedChallengesListBox.Items.IndexOf(toggleButton.DataContext);
        //
        // Challenge currentChallenge = (Challenge)UncompletedJoinedChallengesListBox.Items[selectedChallengeIndex];
        //
        // Global.Database.UnjoinChallenge(CurrentPerson.User.Id, currentChallenge.Id);
        //
        // // Reloading Data context for JoinedChallengesListBox
        // ChallengesViewModel joinedChallengesDataContext = new ChallengesViewModel();
        // joinedChallengesDataContext.JoinedChallengesViewModel(CurrentPerson.User.Id);
        // UncompletedJoinedChallengesListBox.DataContext = joinedChallengesDataContext;
        //
        // // Refresh Challenges Page
        // UserWindow.ChallengesPageObject.LoadAllChallengesCards();
    }

    private bool CanLeaveChallengeCommandExecute() => true;

    #endregion LeaveChallenge

    #region CompletedChallengeCommand : Description

    /// <summary> Description </summary>
    public ICommand CompletedChallengeCommand { get; set; }

    private void OnCompletedChallengeCommandExecuted()
    {
        // Button button = sender as Button;
        // int selectedChallengeIndex = CompletedJoinedChallengesListBox.Items.IndexOf(button.DataContext);
        //
        // Challenge currentChallenge = (Challenge)CompletedJoinedChallengesListBox.Items[selectedChallengeIndex];
        //
        // Global.Database.UnjoinChallenge(CurrentPerson.User.Id, currentChallenge.Id);
        //
        // LoadJoinedChallengesCards();
    }

    private bool CanCompletedChallengeCommandExecute() => true;

    #endregion CompletedChallenge

    #region JoinPlanCommand : Description

    /// <summary> Description </summary>
    public ICommand JoinPlanCommand { get; set; }

    private void OnJoinPlanCommandExecuted()
    {

    }

    private bool CanJoinPlanCommandExecute() => true;

    #endregion JoinPlan
    
    #endregion

    /// <summary>
    /// Домашняя страница для пользователей
    /// </summary>
    /// <param name="openViewService"></param>
    /// <param name="alertService"></param>
    /// <param name="selectedPerson"></param>
    public HomeViewModel(IOpenView openViewService, IAlertService alertService, Person selectedPerson)
    {
        _openViewService = openViewService;
        _alertService = alertService;
        
        OpenAddFoodCommand = new RelayCommand(OnOpenAddFoodCommandExecuted, CanOpenAddFoodCommandExecute);
        OpenAddWorkoutCommand = new RelayCommand(OnOpenAddWorkoutCommandExecuted, CanOpenAddWorkoutCommandExecute);
        SaveNewWeightCommand = new RelayCommand(OnSaveNewWeightCommandExecuted, CanSaveNewWeightCommandExecute);
        JoinedChallengeCommand = new RelayCommand(OnJoinedChallengeCommandExecuted, CanJoinedChallengeCommandExecute);
        LeaveChallengeCommand = new RelayCommand(OnLeaveChallengeCommandExecuted, CanLeaveChallengeCommandExecute);
        CompletedChallengeCommand = new RelayCommand(OnCompletedChallengeCommandExecuted, CanCompletedChallengeCommandExecute);
        LoadedCommand = new RelayCommand(OnLoadedCommandExecuted, CanLoadedCommandExecute);
        CloseAddFoodCommand = new RelayCommand(OnCloseAddFoodCommandExecuted, CanCloseAddFoodCommandExecute);
        CloseAddWorkoutCommand = new RelayCommand(OnCloseAddWorkoutCommandExecuted, CanCloseAddWorkoutCommandExecute);
        JoinPlanCommand = new RelayCommand(OnJoinPlanCommandExecuted, CanJoinPlanCommandExecute);
        
        CurrentPerson = selectedPerson;
    }

    public void LoadWeightChart()
    {
        var chartCreator = Ioc.Default.GetService<IChartCreator>();
        if(chartCreator == null) return;

        var idealWeight = CalculateIdealWeight();
        
        WeightsSeriesCollection = chartCreator.GetWeight(idealWeight, CurrentPerson);

        Labels = Global.Database.GetWeightDates(CurrentPerson.User.Id).ToList();
        YFormatter = value => value.ToString(CultureInfo.InvariantCulture) + " кг";
    }

    private double CalculateIdealWeight()
    {
        if (CurrentPerson.Gender == "Male")
            return CurrentPerson.Height - 100 + (CurrentPerson.Height - 100) * 0.10;

        return CurrentPerson.Height - 100 + (CurrentPerson.Height - 100) * 0.15;
    }

    public void LoadTotalWeightLostCard()
    {
        TotalWeightLostPerWeek = Global.Database.GetTotalWeightLostPerDuration(CurrentPerson.User.Id, "WEEK");
        TotalWeightLostPerMonth = Global.Database.GetTotalWeightLostPerDuration(CurrentPerson.User.Id, "MONTH");
        TotalWeightLostPerYear = Global.Database.GetTotalWeightLostPerDuration(CurrentPerson.User.Id, "YEAR");
    }

    public void LoadAverageWeightLostCard()
    {
        AverageWeightLostPerWeek = Global.Database.GetAverageWeightLostPerDuration(CurrentPerson.User.Id, "WEEK");
        AverageWeightLostPerMonth = Global.Database.GetAverageWeightLostPerDuration(CurrentPerson.User.Id, "MONTH");
        AverageWeightLostPerYear = Global.Database.GetAverageWeightLostPerDuration(CurrentPerson.User.Id, "YEAR");
        // Value < 0 = Foreground Red
    }

    ////////// Joined Challenges Cards Functions/Event Handlers //////////

    /// <summary>
    /// Загрузка принятых вызовов
    /// </summary>
    public void LoadJoinedChallengesCards()
    {
        var challenges = Global.Database.GetJoinedChallenges(Global.Database.AccountId);
        HaveChallenges = challenges.Any();
    }

    private void ControlNoChallengesCard(ChallengesViewModel challengesViewModel)
    {
        // if (challengesViewModel.UncompletedJoinedChallengeModels.Count > 0)
        //     NoChallengesCard.Visibility = Visibility.Collapsed;
        // else
        //     NoChallengesCard.Visibility = Visibility.Visible;
    }

    ////////// Joined Plan Card Functions/Event Handlers //////////

    public void LoadJoinedPlanCard()
    {
        bool checkJoinedInPlan = Global.Database.HavePlans(CurrentPerson.User.Id);

        HavePlans = checkJoinedInPlan;

        // NoPlanCard.Visibility = Visibility.Visible;
        // JoinedPlanCard.Visibility = Visibility.Visible;
        // PlanCompletedCard.Visibility = Visibility.Visible;

        // if (checkJoinedInPlan)
        // {
        //     // NoPlanCard.Visibility = Visibility.Collapsed;
        //
        //     int planDayNum = Global.Database.GetJoinedPlanDayNumber(CurrentPerson.User.Id);
        //
        //     if (planDayNum > 30)
        //         JoinedPlanCard.Visibility = Visibility.Collapsed;
        //     else
        //     {
        //         PlanCompletedCard.Visibility = Visibility.Collapsed;
        //
        //         // Load Header
        //         string planName = Global.Database.GetJoinedPlanName(CurrentPerson.User.Id).ToString();
        //         PlanHeaderTextBlock.Text = planName + " | Day #" + planDayNum;
        //         Global.Database.UpdatePlanDayNumber(CurrentPerson.User.Id, planDayNum);
        //
        //         // Load CheckBoxes
        //         BreakfastCheckBox.IsChecked = Global.Database.GetDayBreakfastStatus(CurrentPerson.User.Id);
        //         LunchCheckBox.IsChecked = Global.Database.GetDayLunchStatus(CurrentPerson.User.Id);
        //         DinnerCheckBox.IsChecked = Global.Database.GetDayDinnerStatus(CurrentPerson.User.Id);
        //         WorkoutsCheckBox.IsChecked = Global.Database.GetDayWorkoutStatus(CurrentPerson.User.Id);
        //
        //         // Load Descriptions
        //         BreakfastDescriptionTextBlock.Text =
        //             Global.Database.GetDayBreakfastDescription(CurrentPerson.User.Id);
        //         LunchDescriptionTextBlock.Text = Global.Database.GetDayLunchDescription(CurrentPerson.User.Id);
        //         DinnerDescriptionTextBlock.Text = Global.Database.GetDayDinnerDescription(CurrentPerson.User.Id);
        //         WorkoutsDescriptionTextBlock.Text = Global.Database.GetDayWorkoutDescription(CurrentPerson.User.Id);
        //
        //         // Load Progress Bar
        //         PlanProgressBar.Value = planDayNum;
        //     }
        // }
        // else
        // {
        //     JoinedPlanCard.Visibility = Visibility.Collapsed;
        //     PlanCompletedCard.Visibility = Visibility.Collapsed;
        // }
    }

    // private void DayItemCheckBox_Checked(object sender, RoutedEventArgs e)
    // {
    //     CheckBox currentCheckBox = sender as CheckBox;
    //
    //     switch (currentCheckBox.Name)
    //     {
    //         case "BreakfastCheckBox":
    //             Global.Database.UpdateDayBreakfastStatus(true, CurrentPerson.User.Id);
    //             break;
    //
    //         case "LunchCheckBox":
    //             Global.Database.UpdateDayLunchStatus(true, CurrentPerson.User.Id);
    //             break;
    //
    //         case "DinnerCheckBox":
    //             Global.Database.UpdateDayDinnerStatus(true, CurrentPerson.User.Id);
    //             break;
    //
    //         case "WorkoutsCheckBox":
    //             Global.Database.UpdateDayWorkoutStatus(true, CurrentPerson.User.Id);
    //             break;
    //     }
    // }

    // private void DayItemCheckBox_Unchecked(object sender, RoutedEventArgs e)
    // {
    //     CheckBox currentCheckBox = sender as CheckBox;
    //
    //     switch (currentCheckBox.Name)
    //     {
    //         case "BreakfastCheckBox":
    //             Global.Database.UpdateDayBreakfastStatus(false, CurrentPerson.User.Id);
    //             break;
    //
    //         case "LunchCheckBox":
    //             Global.Database.UpdateDayLunchStatus(false, CurrentPerson.User.Id);
    //             break;
    //
    //         case "DinnerCheckBox":
    //             Global.Database.UpdateDayDinnerStatus(false, CurrentPerson.User.Id);
    //             break;
    //
    //         case "WorkoutsCheckBox":
    //             Global.Database.UpdateDayWorkoutStatus(false, CurrentPerson.User.Id);
    //             break;
    //     }
    // }

    //////////////////////////////////////////////////////////////

    ////////// Motivational Quotes Card Functions/Event Handlers //////////

    private void LoadMotivationalQuoteCard()
    {
        // MotiationalQuoteTextBlock.Text = Global.Database.GetMotivationalQuote();
    }

    //////////////////////////////////////////////////////////////////////

    ////////// Calories Card Functions/Event Handlers //////////

    public SeriesCollection CaloriesSeriesCollection { get; set; }
    public string[] CaloriesLabels { get; set; }
    public Func<double, string> Formatter { get; set; }

    public void LoadCaloriesCard()
    {
        double caloriesGained = Global.Database.GetCaloriesGainedToday(CurrentPerson.User.Id);
        double caloriesNeeded = CalculateCaloriedNeeded();
        double caloriesLost = Global.Database.GetCaloriesLostToday(CurrentPerson.User.Id);

        // CaloriesGainedTextBlock.Text = caloiresGained.ToString();
        // CaloriesNeededTextBlock.Text = caloriesNeeded.ToString();
        // CaloriesLostTextBlock.Text = caloriesLost.ToString();


        // CaloriesSeriesCollection = new SeriesCollection
        // {
        //     new ColumnSeries
        //     {
        //         Title = "Gaind",
        //         Values = new ChartValues<double>() { caloiresGained },
        //         Fill = Brushes.Red,
        //     },
        //
        //     new ColumnSeries
        //     {
        //         Title = "Needed",
        //         Values = new ChartValues<double>() { caloriesNeeded },
        //         Fill = (Brush)Application.Current.Resources["PrimaryHueMidBrush"],
        //     },
        //
        //     new ColumnSeries
        //     {
        //         Title = "Lost",
        //         Values = new ChartValues<double>() { caloriesLost },
        //         Fill = Brushes.ForestGreen,
        //     }
        // };


        CaloriesLabels = new[] { "Calories" };
        Formatter = value => value.ToString() + "  kCal.";
        // CaloriesChart.DataContext = this;
    }

    private double CalculateCaloriedNeeded()
    {
        // if (UserWindow.signedInUser.Gender == "Male")
        //     return  66 + (13.7 * UserWindow.signedInUser.Weight)
        //                + (1.8  * UserWindow.signedInUser.Height)
        //                - (4.7  * UserWindow.signedInUser.Age);
        // else
        //     return 665 + (9.6  * UserWindow.signedInUser.Weight)
        //                + (1.8  * UserWindow.signedInUser.Height)
        //                - (4.7  * UserWindow.signedInUser.Age);
        return 0;
    }

    ////////// DialogBoxes Functions/Event Handlers //////////

    // private void DialogBoxAddFoodButton_Click(object sender, RoutedEventArgs e)
    // {
    //     // if (FoodComboBox.SelectedIndex == -1)
    //     //     UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Please choose Food!");
    //     //
    //     // else if (string.IsNullOrWhiteSpace(FoodQuantityTextBox.Text))
    //     //     UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Please enter Food Quantity!");
    //     //
    //     // else
    //     // {
    //     //     Global.Database.AddFood(FoodComboBox.Text, double.Parse(FoodQuantityTextBox.Text), CurrentPerson.User.Id);
    //     //     AddFoodDialogBox.Visibility = Visibility.Collapsed;
    //     //     DialogBox.IsOpen = false;
    //     //
    //     //     // Refresh Calories Card
    //     //     CaloriesChart.DataContext = null;
    //     //     LoadCaloriesCard();
    //     // }
    //     //
    //     // // Reset Dialog Box Fields
    //     // FoodComboBox.SelectedIndex = -1;
    //     // FoodQuantityTextBox.Text = "";
    // }

    // private void DialogBoxAddWorkoutButton_Click(object sender, RoutedEventArgs e)
    // {
    //     // if (WorkoutsComboBox.SelectedIndex == -1)
    //     //     UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Please choose Workout!");
    //     //
    //     // else if (string.IsNullOrWhiteSpace(WorkoutsDurationTextBox.Text))
    //     //     UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Please enter Workout Duration!");
    //     //
    //     // else
    //     // {
    //     //     Global.Database.AddWorkout(WorkoutsComboBox.Text, double.Parse(WorkoutsDurationTextBox.Text),
    //     //         UserWindow.SignedInUser);
    //     //     AddWorkoutDialogBox.Visibility = Visibility.Collapsed;
    //     //     DialogBox.IsOpen = false;
    //     //
    //     //     // Update Progress of the Challenges having the same type as the entered workout
    //     //     Global.Database.UpdateChallengesProgress(CurrentPerson.User.Id, WorkoutsComboBox.Text,
    //     //         double.Parse(WorkoutsDurationTextBox.Text));
    //     //
    //     //     // Refresh Challenges card
    //     //     LoadJoinedChallengesCards();
    //     //
    //     //     // Refresh Calories Card
    //     //     CaloriesChart.DataContext = null;
    //     //     LoadCaloriesCard();
    //     // }
    //     //
    //     // // Reset Dialog Box Fields
    //     // WorkoutsComboBox.SelectedIndex = -1;
    //     // WorkoutsDurationTextBox.Text = "";
    // }
    
    // private void DialogBoxCancelButton_Click(object sender, RoutedEventArgs e)
    // {
    //     // AddFoodDialogBox.Visibility = Visibility.Collapsed;
    //     // AddWorkoutDialogBox.Visibility = Visibility.Collapsed;
    //     // DialogBox.IsOpen = false;
    //     //
    //     // WorkoutsComboBox.SelectedIndex = -1;
    //     // WorkoutsDurationTextBox.Text = "";
    //     //
    //     // FoodComboBox.SelectedIndex = -1;
    //     // FoodQuantityTextBox.Text = "";
    // }

    ///////////////////////////////////////////////////////////
}