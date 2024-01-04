using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessApp.Models;
using LiveCharts;

namespace FitnessApp.Wpf.ViewModels.UserPages;

public class HomeViewModel : ObservableObject
{
    #region Private Properties

    ////////// All Weight Cards Functions/Event Handlers //////////

    // Weight Chart Properties
    public SeriesCollection WeightsSeriesCollection { get; set; }
    public List<string> Labels { get; set; }
    public Func<double, string> YFormatter { get; set; }

    #endregion

    #region Properties

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

    #endregion

    #region Commands
    
    #region OpenAddFoodCommand : Description

    /// <summary> Description </summary>
    public ICommand OpenAddFoodCommand { get; set; }

    private void OnOpenAddFoodCommandExecuted()
    {
        // DialogBox.IsOpen = true;
        // AddFoodDialogBox.Visibility = Visibility.Visible;
    }

    private bool CanOpenAddFoodCommandExecute() => true;

    #endregion OpenAddFood

    #region OpenAddWorkoutCommand : Description

    /// <summary> Description </summary>
    public ICommand OpenAddWorkoutCommand { get; set; }

    private void OnOpenAddWorkoutCommandExecuted()
    {
        // DialogBox.IsOpen = true;
        // AddWorkoutDialogBox.Visibility = Visibility.Visible;
    }

    private bool CanOpenAddWorkoutCommandExecute() => true;

    #endregion OpenAddWorkout

    #region SaveNewWeightCommand : Description

    /// <summary> Description </summary>
    public ICommand SaveNewWeightCommand { get; set; }

    private void OnSaveNewWeightCommandExecuted()
    {
// if (TodayWeightValue == 0)
        //     UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Please enter your weight!");
        // else
        // {
        //     // Update User Model Weight Porperty with the latest weight
        //     // UserWindow.signedInUser.Weight = double.Parse(TodaysWeightTextBox.Text);
        //
        //     // Update Weight in Database
        //     App.Database.AddNewWeight(TodayWeightValue, CurrentPerson.User.Id);
        //
        //     // Update User Weight Line Series:
        //     // TOOD Add one value and remove another to keep the number of values 10
        //     // WeightChart.Series[1].Values.Add(double.Parse(TodaysWeightTextBox.Text));
        //     // if (WeightChart.Series[1].Values.Count > 10)
        //     //     WeightChart.Series[1].Values.RemoveAt(0);
        //
        //     // Confirmation Message
        //     UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Weight added successfully");
        //
        //     // Reset TextBox
        //     TodayWeightValue = 0;
        //
        //     // Refresh Weight-Related Cards
        //     LoadTotalWeightLostCard();
        //     LoadAverageWeightLostCard();
        //
        //     // Refresh Calories Card and Chart
        //     // TODO CaloriesChart.DataContext = null;
        //     LoadCaloriesCard();
        //
        //     // Refresh CaloriesCalculatorPage DataContext
        //     UserWindow.CaloriesCalculatorPageObject.DataContext = null;
        //     UserWindow.CaloriesCalculatorPageObject.DataContext = UserWindow.SignedInUser;
        //
        //     // Refresh SettingsPage DataContext
        //     UserWindow.SettingsPageObject.DataContext = null;
        //     UserWindow.SettingsPageObject.DataContext = UserWindow.SignedInUser;
        // }
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
        // App.Database.UnjoinChallenge(CurrentPerson.User.Id, currentChallenge.Id);
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
        // App.Database.UnjoinChallenge(CurrentPerson.User.Id, currentChallenge.Id);
        //
        // LoadJoinedChallengesCards();
    }

    private bool CanCompletedChallengeCommandExecute() => true;

    #endregion CompletedChallenge

    private void JoinPlanButton_Click(object sender, RoutedEventArgs e)
    {
        // UserWindow.UserWindowObject.UserWindowPagesListBox.SelectedIndex = 2;
    }

    private void DismissPlanButton_Click(object sender, RoutedEventArgs e)
    {
        App.Database.UnjoinPlan(CurrentPerson.User.Id);
        LoadJoinedPlanCard();

        // UserWindow.PlansPageObject.LoadAllPlansCards();
    }
    
    #endregion

    public HomeViewModel(Person selectedPerson)
    {
        OpenAddFoodCommand = new RelayCommand(OnOpenAddFoodCommandExecuted, CanOpenAddFoodCommandExecute);
        OpenAddWorkoutCommand = new RelayCommand(OnOpenAddWorkoutCommandExecuted, CanOpenAddWorkoutCommandExecute);
        SaveNewWeightCommand = new RelayCommand(OnSaveNewWeightCommandExecuted, CanSaveNewWeightCommandExecute);
        JoinedChallengeCommand = new RelayCommand(OnJoinedChallengeCommandExecuted, CanJoinedChallengeCommandExecute);
        LeaveChallengeCommand = new RelayCommand(OnLeaveChallengeCommandExecuted, CanLeaveChallengeCommandExecute);
        CompletedChallengeCommand = new RelayCommand(OnCompletedChallengeCommandExecuted, CanCompletedChallengeCommandExecute);

        CurrentPerson = selectedPerson;

        LoadWeightChart();
        LoadTotalWeightLostCard();
        LoadAverageWeightLostCard();
        LoadJoinedChallengesCards();
        LoadJoinedPlanCard();
        LoadMotivationalQuoteCard();
        LoadCaloriesCard();

        // FoodComboBox.ItemsSource = App.Database.GetAllFood().ToList();
        // WorkoutsComboBox.ItemsSource = App.Database.GetWorkouts().ToList();
    }

    public void LoadWeightChart()
    {
        // WeightsSeriesCollection = new SeriesCollection
        // {
        //     new LineSeries
        //     {
        //         Title = "Ideal Weight",
        //         Values = Enumerable.Repeat(CalculateIdealWeight(), 10).AsChartValues(),
        //         PointGeometry = null,
        //         Fill = Brushes.Transparent,
        //         Stroke = Brushes.ForestGreen,
        //         StrokeDashArray = new DoubleCollection { 3 },
        //     },
        //
        //     new LineSeries
        //     {
        //         Title = "Weight",
        //         Values = App.Database.GetWeightValues(CurrentPerson.User.Id).AsChartValues(),
        //     },
        //
        //     new LineSeries
        //     {
        //         Title = "Target Weight",
        //         Values = Enumerable.Repeat(CurrentPerson?.TargetWeight, 10).AsChartValues(),
        //         PointGeometry = null,
        //         Fill = Brushes.Transparent,
        //         Stroke = Brushes.Red,
        //         StrokeDashArray = new DoubleCollection { 3 },
        //     }
        // };

        Labels = App.Database.GetWeightDateValues(CurrentPerson.User.Id);
        YFormatter = value => value.ToString(CultureInfo.InvariantCulture) + " kg";

        // TODO Setting Data context for Weight Chart
        // WeightChart.DataContext = this;
    }

    private double CalculateIdealWeight()
    {
        if (CurrentPerson == null) return 0;

        if (CurrentPerson.Gender == "Male")
            return CurrentPerson.Height - 100 + (CurrentPerson.Height - 100) * 0.10;

        return CurrentPerson.Height - 100 + (CurrentPerson.Height - 100) * 0.15;
    }

    private void DecimalNumbersOnlyFieldValidation(object sender, TextCompositionEventArgs e)
    {
        var s = sender as TextBox;
        var text = s.Text.Insert(s.SelectionStart, e.Text);
        e.Handled = !double.TryParse(text, out double d);
    }

    public void LoadTotalWeightLostCard()
    {
        TotalWeightLostPerWeek = App.Database.GetTotalWeightLostPerDuration(CurrentPerson.User.Id, "WEEK");
        TotalWeightLostPerMonth = App.Database.GetTotalWeightLostPerDuration(CurrentPerson.User.Id, "MONTH");
        TotalWeightLostPerYear = App.Database.GetTotalWeightLostPerDuration(CurrentPerson.User.Id, "YEAR");
    }

    public void LoadAverageWeightLostCard()
    {
        AverageWeightLostPerWeek = App.Database.GetAverageWeightLostPerDuration(CurrentPerson.User.Id, "WEEK");
        AverageWeightLostPerMonth = App.Database.GetAverageWeightLostPerDuration(CurrentPerson.User.Id, "MONTH");
        AverageWeightLostPerYear = App.Database.GetAverageWeightLostPerDuration(CurrentPerson.User.Id, "YEAR");
        // Value < 0 = Foreground Red
    }

    ////////// Joined Challenges Cards Functions/Event Handlers //////////

    // Setting Data context for JoinedChallengesListBox
    public void LoadJoinedChallengesCards()
    {
        // ChallengesViewModel joinedChallengesDataContext = new ChallengesViewModel();
        // joinedChallengesDataContext.JoinedChallengesViewModel(CurrentPerson.User.Id);
        // CompletedJoinedChallengesListBox.DataContext = joinedChallengesDataContext;
        //
        // ChallengesViewModel uncompletedJoinedChallengesDataContext = new ChallengesViewModel();
        // uncompletedJoinedChallengesDataContext.JoinedChallengesViewModel(CurrentPerson.User.Id);
        // UncompletedJoinedChallengesListBox.DataContext = uncompletedJoinedChallengesDataContext;
        // ControlNoChallengesCard(joinedChallengesDataContext);
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
        bool checkJoinedInPlan = App.Database.IsInPlan(CurrentPerson.User.Id);

        // NoPlanCard.Visibility = Visibility.Visible;
        // JoinedPlanCard.Visibility = Visibility.Visible;
        // PlanCompletedCard.Visibility = Visibility.Visible;

        // if (checkJoinedInPlan)
        // {
        //     // NoPlanCard.Visibility = Visibility.Collapsed;
        //
        //     int planDayNum = App.Database.GetJoinedPlanDayNumber(CurrentPerson.User.Id);
        //
        //     if (planDayNum > 30)
        //         JoinedPlanCard.Visibility = Visibility.Collapsed;
        //     else
        //     {
        //         PlanCompletedCard.Visibility = Visibility.Collapsed;
        //
        //         // Load Header
        //         string planName = App.Database.GetJoinedPlanName(CurrentPerson.User.Id).ToString();
        //         PlanHeaderTextBlock.Text = planName + " | Day #" + planDayNum;
        //         App.Database.UpdatePlanDayNumber(CurrentPerson.User.Id, planDayNum);
        //
        //         // Load CheckBoxes
        //         BreakfastCheckBox.IsChecked = App.Database.GetDayBreakfastStatus(CurrentPerson.User.Id);
        //         LunchCheckBox.IsChecked = App.Database.GetDayLunchStatus(CurrentPerson.User.Id);
        //         DinnerCheckBox.IsChecked = App.Database.GetDayDinnerStatus(CurrentPerson.User.Id);
        //         WorkoutsCheckBox.IsChecked = App.Database.GetDayWorkoutStatus(CurrentPerson.User.Id);
        //
        //         // Load Descriptions
        //         BreakfastDescriptionTextBlock.Text =
        //             App.Database.GetDayBreakfastDescription(CurrentPerson.User.Id);
        //         LunchDescriptionTextBlock.Text = App.Database.GetDayLunchDescription(CurrentPerson.User.Id);
        //         DinnerDescriptionTextBlock.Text = App.Database.GetDayDinnerDescription(CurrentPerson.User.Id);
        //         WorkoutsDescriptionTextBlock.Text = App.Database.GetDayWorkoutDescription(CurrentPerson.User.Id);
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

    private void DayItemCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        CheckBox currentCheckBox = sender as CheckBox;

        switch (currentCheckBox.Name)
        {
            case "BreakfastCheckBox":
                App.Database.UpdateDayBreakfastStatus(true, CurrentPerson.User.Id);
                break;

            case "LunchCheckBox":
                App.Database.UpdateDayLunchStatus(true, CurrentPerson.User.Id);
                break;

            case "DinnerCheckBox":
                App.Database.UpdateDayDinnerStatus(true, CurrentPerson.User.Id);
                break;

            case "WorkoutsCheckBox":
                App.Database.UpdateDayWorkoutStatus(true, CurrentPerson.User.Id);
                break;
        }
    }

    private void DayItemCheckBox_Unchecked(object sender, RoutedEventArgs e)
    {
        CheckBox currentCheckBox = sender as CheckBox;

        switch (currentCheckBox.Name)
        {
            case "BreakfastCheckBox":
                App.Database.UpdateDayBreakfastStatus(false, CurrentPerson.User.Id);
                break;

            case "LunchCheckBox":
                App.Database.UpdateDayLunchStatus(false, CurrentPerson.User.Id);
                break;

            case "DinnerCheckBox":
                App.Database.UpdateDayDinnerStatus(false, CurrentPerson.User.Id);
                break;

            case "WorkoutsCheckBox":
                App.Database.UpdateDayWorkoutStatus(false, CurrentPerson.User.Id);
                break;
        }
    }

    //////////////////////////////////////////////////////////////

    ////////// Motivational Quotes Card Functions/Event Handlers //////////

    private void LoadMotivationalQuoteCard()
    {
        // MotiationalQuoteTextBlock.Text = App.Database.GetMotivationalQuote();
    }

    //////////////////////////////////////////////////////////////////////

    ////////// Calories Card Functions/Event Handlers //////////

    public SeriesCollection CaloriesSeriesCollection { get; set; }
    public string[] CaloriesLabels { get; set; }
    public Func<double, string> Formatter { get; set; }

    public void LoadCaloriesCard()
    {
        double caloriesGained = App.Database.GetCaloriesGainedToday(CurrentPerson.User.Id);
        double caloriesNeeded = CalculateCaloriedNeeded();
        double caloriesLost = App.Database.GetCaloriesLostToday(CurrentPerson.User.Id);

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

    private void DialogBoxAddFoodButton_Click(object sender, RoutedEventArgs e)
    {
        // if (FoodComboBox.SelectedIndex == -1)
        //     UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Please choose Food!");
        //
        // else if (string.IsNullOrWhiteSpace(FoodQuantityTextBox.Text))
        //     UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Please enter Food Quantity!");
        //
        // else
        // {
        //     App.Database.AddFood(FoodComboBox.Text, double.Parse(FoodQuantityTextBox.Text), CurrentPerson.User.Id);
        //     AddFoodDialogBox.Visibility = Visibility.Collapsed;
        //     DialogBox.IsOpen = false;
        //
        //     // Refresh Calories Card
        //     CaloriesChart.DataContext = null;
        //     LoadCaloriesCard();
        // }
        //
        // // Reset Dialog Box Fields
        // FoodComboBox.SelectedIndex = -1;
        // FoodQuantityTextBox.Text = "";
    }

    private void DialogBoxAddWorkoutButton_Click(object sender, RoutedEventArgs e)
    {
        // if (WorkoutsComboBox.SelectedIndex == -1)
        //     UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Please choose Workout!");
        //
        // else if (string.IsNullOrWhiteSpace(WorkoutsDurationTextBox.Text))
        //     UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Please enter Workout Duration!");
        //
        // else
        // {
        //     App.Database.AddWorkout(WorkoutsComboBox.Text, double.Parse(WorkoutsDurationTextBox.Text),
        //         UserWindow.SignedInUser);
        //     AddWorkoutDialogBox.Visibility = Visibility.Collapsed;
        //     DialogBox.IsOpen = false;
        //
        //     // Update Progress of the Challenges having the same type as the entered workout
        //     App.Database.UpdateChallengesProgress(CurrentPerson.User.Id, WorkoutsComboBox.Text,
        //         double.Parse(WorkoutsDurationTextBox.Text));
        //
        //     // Refresh Challenges card
        //     LoadJoinedChallengesCards();
        //
        //     // Refresh Calories Card
        //     CaloriesChart.DataContext = null;
        //     LoadCaloriesCard();
        // }
        //
        // // Reset Dialog Box Fields
        // WorkoutsComboBox.SelectedIndex = -1;
        // WorkoutsDurationTextBox.Text = "";
    }

    private void DialogBoxCancelButton_Click(object sender, RoutedEventArgs e)
    {
        // AddFoodDialogBox.Visibility = Visibility.Collapsed;
        // AddWorkoutDialogBox.Visibility = Visibility.Collapsed;
        // DialogBox.IsOpen = false;
        //
        // WorkoutsComboBox.SelectedIndex = -1;
        // WorkoutsDurationTextBox.Text = "";
        //
        // FoodComboBox.SelectedIndex = -1;
        // FoodQuantityTextBox.Text = "";
    }

    ///////////////////////////////////////////////////////////
}