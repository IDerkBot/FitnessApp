using FitnessApp.Models;
using FitnessApp.ViewModels;
using FitnessApp.Windows;
using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using FitnessApp.Wpf;
using FitnessApp.Wpf.ViewModels;

namespace FitnessApp.UserWindowPages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {


        public HomePage()
        {
            InitializeComponent();
            UserWindow.HomePageObject = this;

            LoadWeightChart();
            LoadTotalWeightLostCard();
            LoadAverageWeightLostCard();
            LoadJoinedChallengesCards();
            LoadJoinedPlanCard();
            LoadMotivationalQuoteCard();
            LoadCaloriesCard();

            FoodComboBox.ItemsSource = App.Database.GetAllFood();
            WorkoutsComboBox.ItemsSource = App.Database.GetAllWorkouts();

        }


        ////////// All Weight Cards Functions/Event Handlers //////////

        // Weight Chart Properties

        public SeriesCollection WeightsSeriesCollection { get; set; }
        public List<string> Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public void LoadWeightChart()
        {

            WeightsSeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Ideal Weight",
                    Values = Enumerable.Repeat(CalculateIdealWeight(), 10).AsChartValues(),
                    PointGeometry = null,
                    Fill = Brushes.Transparent,
                    Stroke = Brushes.ForestGreen,
                    StrokeDashArray = new DoubleCollection {3},
                },

                new LineSeries
                {
                    Title = "Weight",
                    Values = App.Database.GetWeightValues(UserWindow.signedInUser.Id).AsChartValues(),
                },

                // new LineSeries
                // {
                //     Title = "Target Weight",
                //     Values = Enumerable.Repeat(UserWindow.signedInUser.TargetWeight, 10).AsChartValues(),
                //     PointGeometry = null,
                //     Fill = Brushes.Transparent,
                //     Stroke = Brushes.Red,
                //     StrokeDashArray = new DoubleCollection {3},
                // }
                
            };

            Labels = App.Database.GetWeightDateValues(UserWindow.signedInUser.Id);
            YFormatter = value => value.ToString() + " kg";

            // Setting Data context for Weight Chart
            WeightChart.DataContext = this;
        }

        private double CalculateIdealWeight()
        {
            // if (UserWindow.signedInUser.Gender == "Male")
            //     return (UserWindow.signedInUser.Height - 100) + ((UserWindow.signedInUser.Height - 100) * 0.10);
            //
            // else
            //     return (UserWindow.signedInUser.Height - 100) + ((UserWindow.signedInUser.Height - 100) * 0.15);
            return 0;
        }

        private void DecimalNumbersOnlyFieldValidation(object sender, TextCompositionEventArgs e)
        {
            var s = sender as TextBox;
            var text = s.Text.Insert(s.SelectionStart, e.Text);
            e.Handled = !double.TryParse(text, out double d);
        }

        private void SaveWeightButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TodaysWeightTextBox.Text))
                UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Please enter your weight!");
            else
            {
                // Update User Model Weight Porperty with the latest weight
                // UserWindow.signedInUser.Weight = double.Parse(TodaysWeightTextBox.Text);

                // Update Weight in Database
                App.Database.AddNewWeight(double.Parse(TodaysWeightTextBox.Text), UserWindow.signedInUser.Id);

                // Update User Weight Line Series:
                // Add one value and remove another to keep the number of values 10
                WeightChart.Series[1].Values.Add(double.Parse(TodaysWeightTextBox.Text));
                if (WeightChart.Series[1].Values.Count > 10)
                    WeightChart.Series[1].Values.RemoveAt(0);

                // Confirmation Message
                UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Weight added successfully");

                // Reset TextBox
                TodaysWeightTextBox.Text = "";

                // Refresh Weight-Related Cards
                LoadTotalWeightLostCard();
                LoadAverageWeightLostCard();

                // Refresh Calories Card and Chart
                CaloriesChart.DataContext = null;
                LoadCaloriesCard();

                // Refresh CaloriesCalculatorPage DataContext
                UserWindow.CaloriesCalculatorPageObject.DataContext = null;
                UserWindow.CaloriesCalculatorPageObject.DataContext = UserWindow.signedInUser;

                // Refresh SettingsPage DataContext
                UserWindow.SettingsPageObject.DataContext = null;
                UserWindow.SettingsPageObject.DataContext = UserWindow.signedInUser;
            }
        }

        public void LoadTotalWeightLostCard()
        {

            double totalWeightLostPerWeek  = App.Database.GetTotalWeightLostPerDuration(UserWindow.signedInUser.Id, "WEEK");
            double totalWeightLostPerMonth = App.Database.GetTotalWeightLostPerDuration(UserWindow.signedInUser.Id, "MONTH");
            double totalWeightLostPerYear  = App.Database.GetTotalWeightLostPerDuration(UserWindow.signedInUser.Id, "YEAR");


            // Set Colours
            if (totalWeightLostPerWeek < 0)
                WeightLostPerWeekTextBlock.Foreground = new SolidColorBrush(Colors.Red);
            else
                WeightLostPerWeekTextBlock.Foreground = (Brush)Application.Current.Resources["PrimaryHueMidBrush"];

            if (totalWeightLostPerMonth < 0)
                WeightLostPerMonthTextBlock.Foreground = new SolidColorBrush(Colors.Red);
            else
                WeightLostPerMonthTextBlock.Foreground = (Brush)Application.Current.Resources["PrimaryHueMidBrush"];

            if (totalWeightLostPerYear < 0)
                WeightLostPerYearTextBlock.Foreground = new SolidColorBrush(Colors.Red);
            else
                WeightLostPerYearTextBlock.Foreground = (Brush)Application.Current.Resources["PrimaryHueMidBrush"];


            // Assign Values to TextBoxes
            WeightLostPerWeekTextBlock .Text = totalWeightLostPerWeek.ToString();
            WeightLostPerMonthTextBlock.Text = totalWeightLostPerMonth.ToString();
            WeightLostPerYearTextBlock .Text = totalWeightLostPerYear.ToString();

        }

        public void LoadAverageWeightLostCard()
        {

            double averageWeightLostPerWeek  = App.Database.GetAverageWeightLostPerDuration(UserWindow.signedInUser.Id, "WEEK");
            double averageWeightLostPerMonth = App.Database.GetAverageWeightLostPerDuration(UserWindow.signedInUser.Id, "MONTH");
            double averageWeightLostPerYear  = App.Database.GetAverageWeightLostPerDuration(UserWindow.signedInUser.Id, "YEAR");


            // Set Colours
            if (averageWeightLostPerWeek < 0)
                AverageWeightLostPerWeekTextBlock.Foreground = new SolidColorBrush(Colors.Red);
            else
                AverageWeightLostPerWeekTextBlock.Foreground = (Brush)Application.Current.Resources["PrimaryHueMidBrush"];

            if (averageWeightLostPerMonth < 0)
                AverageWeightLostPerMonthTextBlock.Foreground = new SolidColorBrush(Colors.Red);
            else
                AverageWeightLostPerMonthTextBlock.Foreground = (Brush)Application.Current.Resources["PrimaryHueMidBrush"];

            if (averageWeightLostPerYear < 0)
                AverageWeightLostPerYearTextBlock.Foreground = new SolidColorBrush(Colors.Red);
            else
                AverageWeightLostPerYearTextBlock.Foreground = (Brush)Application.Current.Resources["PrimaryHueMidBrush"];


            // Assign Values to TextBoxes
            AverageWeightLostPerWeekTextBlock .Text = averageWeightLostPerWeek.ToString();
            AverageWeightLostPerMonthTextBlock.Text = averageWeightLostPerMonth.ToString();
            AverageWeightLostPerYearTextBlock .Text = averageWeightLostPerYear.ToString();
        }




        ////////// Joined Challenges Cards Functions/Event Handlers //////////


        // Setting Data context for JoinedChallengesListBox
        public void LoadJoinedChallengesCards()
        {
            ChallengesViewModel joinedChallengesDataContext = new ChallengesViewModel();
            joinedChallengesDataContext.JoinedChallengesViewModel(UserWindow.signedInUser.Id);
            CompletedJoinedChallengesListBox.DataContext = joinedChallengesDataContext;

            ChallengesViewModel uncompletedJoinedChallengesDataContext = new ChallengesViewModel();
            uncompletedJoinedChallengesDataContext.JoinedChallengesViewModel(UserWindow.signedInUser.Id);
            UncompletedJoinedChallengesListBox.DataContext = uncompletedJoinedChallengesDataContext;
            ControlNoChallengesCard(joinedChallengesDataContext);
        }

        private void JoinChallengeButton_Click(object sender, RoutedEventArgs e)
        {
            UserWindow.UserWindowObject.UserWindowPagesListBox.SelectedIndex = 1;
        }

        private void UnjoinChallengeButton_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            int selectedChallengeIndex = UncompletedJoinedChallengesListBox.Items.IndexOf(toggleButton.DataContext);

            Challenge currentChallenge = (Challenge)UncompletedJoinedChallengesListBox.Items[selectedChallengeIndex];

            App.Database.UnjoinChallenge(UserWindow.signedInUser.Id, currentChallenge.Id);

            // Reloading Data context for JoinedChallengesListBox
            ChallengesViewModel joinedChallengesDataContext = new ChallengesViewModel();
            joinedChallengesDataContext.JoinedChallengesViewModel(UserWindow.signedInUser.Id);
            UncompletedJoinedChallengesListBox.DataContext = joinedChallengesDataContext;

            // Refresh Challenges Page
            UserWindow.ChallengesPageObject.LoadAllChallengesCards();
        }

        private void ControlNoChallengesCard(ChallengesViewModel challengesViewModel)
        {
            // if (challengesViewModel.UncompletedJoinedChallengeModels.Count > 0)
            //     NoChallengesCard.Visibility = Visibility.Collapsed;
            // else
            //     NoChallengesCard.Visibility = Visibility.Visible;
        }

        private void CompletedChallengeButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int selectedChallengeIndex = CompletedJoinedChallengesListBox.Items.IndexOf(button.DataContext);

            Challenge currentChallenge = (Challenge)CompletedJoinedChallengesListBox.Items[selectedChallengeIndex];

            App.Database.UnjoinChallenge(UserWindow.signedInUser.Id, currentChallenge.Id);

            LoadJoinedChallengesCards();
        }



        ////////// Joined Plan Card Functions/Event Handlers //////////


        public void LoadJoinedPlanCard()
        {
            bool checkJoinedInPlan = App.Database.IsInPlan(UserWindow.signedInUser.Id);

            NoPlanCard.Visibility = Visibility.Visible;
            JoinedPlanCard.Visibility = Visibility.Visible;
            PlanCompletedCard.Visibility = Visibility.Visible;

            if (checkJoinedInPlan)
            {
                NoPlanCard.Visibility = Visibility.Collapsed;

                int planDayNum = App.Database.GetJoinedPlanDayNumber(UserWindow.signedInUser.Id);

                if (planDayNum > 30)
                    JoinedPlanCard.Visibility = Visibility.Collapsed;
                else
                {
                    PlanCompletedCard.Visibility = Visibility.Collapsed;

                    // Load Header
                    string planName = App.Database.GetJoinedPlanName(UserWindow.signedInUser.Id).ToString();
                    PlanHeaderTextBlock.Text = planName + " | Day #" + planDayNum;
                    App.Database.UpdatePlanDayNumber(UserWindow.signedInUser.Id, planDayNum);

                    // Load CheckBoxes
                    BreakfastCheckBox.IsChecked = App.Database.GetDayBreakfastStatus(UserWindow.signedInUser.Id);
                    LunchCheckBox    .IsChecked = App.Database.GetDayLunchStatus(UserWindow.signedInUser.Id);
                    DinnerCheckBox   .IsChecked = App.Database.GetDayDinnerStatus(UserWindow.signedInUser.Id);
                    WorkoutsCheckBox .IsChecked = App.Database.GetDayWorkoutStatus(UserWindow.signedInUser.Id);

                    // Load Descriptions
                    BreakfastDescriptionTextBlock.Text = App.Database.GetDayBreakfastDescription(UserWindow.signedInUser.Id);
                    LunchDescriptionTextBlock    .Text = App.Database.GetDayLunchDescription(UserWindow.signedInUser.Id);
                    DinnerDescriptionTextBlock   .Text = App.Database.GetDayDinnerDescription(UserWindow.signedInUser.Id);
                    WorkoutsDescriptionTextBlock .Text = App.Database.GetDayWorkoutDescription(UserWindow.signedInUser.Id);

                    // Load Progress Bar
                    PlanProgressBar.Value = planDayNum;
                }
            }
            else
            {
                JoinedPlanCard   .Visibility = Visibility.Collapsed;
                PlanCompletedCard.Visibility = Visibility.Collapsed;
            }
        }

        private void DayItemCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox currentCheckBox = sender as CheckBox;

            switch (currentCheckBox.Name)
            {
                case "BreakfastCheckBox":
                    App.Database.UpdateDayBreakfastStatus(true, UserWindow.signedInUser.Id);
                    break;

                case "LunchCheckBox":
                    App.Database.UpdateDayLunchStatus(true, UserWindow.signedInUser.Id);
                    break;

                case "DinnerCheckBox":
                    App.Database.UpdateDayDinnerStatus(true, UserWindow.signedInUser.Id);
                    break;

                case "WorkoutsCheckBox":
                    App.Database.UpdateDayWorkoutStatus(true, UserWindow.signedInUser.Id);
                    break;
            }
        }

        private void DayItemCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox currentCheckBox = sender as CheckBox;

            switch (currentCheckBox.Name)
            {
                case "BreakfastCheckBox":
                    App.Database.UpdateDayBreakfastStatus(false, UserWindow.signedInUser.Id);
                    break;

                case "LunchCheckBox":
                    App.Database.UpdateDayLunchStatus(false, UserWindow.signedInUser.Id);
                    break;

                case "DinnerCheckBox":
                    App.Database.UpdateDayDinnerStatus(false, UserWindow.signedInUser.Id);
                    break;

                case "WorkoutsCheckBox":
                    App.Database.UpdateDayWorkoutStatus(false, UserWindow.signedInUser.Id);
                    break;
            }
        }

        private void JoinPlanButton_Click(object sender, RoutedEventArgs e)
        {
            UserWindow.UserWindowObject.UserWindowPagesListBox.SelectedIndex = 2;
        }

        private void DismissPlanButton_Click(object sender, RoutedEventArgs e)
        {
            App.Database.UnjoinPlan(UserWindow.signedInUser.Id);
            LoadJoinedPlanCard();

            UserWindow.PlansPageObject.LoadAllPlansCards();
        }


        //////////////////////////////////////////////////////////////




        ////////// Motivational Quotes Card Functions/Event Handlers //////////

        private void LoadMotivationalQuoteCard()
        {
            MotiationalQuoteTextBlock.Text = App.Database.GetMotivationalQuote();
        }

        //////////////////////////////////////////////////////////////////////




        ////////// Calories Card Functions/Event Handlers //////////

        public SeriesCollection CaloriesSeriesCollection { get; set; }
        public string[] CaloriesLabels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public void LoadCaloriesCard()
        {

            double caloiresGained = App.Database.GetCaloriesGainedToday(UserWindow.signedInUser.Id);
            double caloriesNeeded = CalculateCaloriedNeeded();
            double caloriesLost   = App.Database.GetCaloriesLostToday(UserWindow.signedInUser.Id);

            CaloriesGainedTextBlock.Text = caloiresGained.ToString();
            CaloriesNeededTextBlock.Text = caloriesNeeded.ToString();
            CaloriesLostTextBlock  .Text = caloriesLost.ToString();


            CaloriesSeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title  = "Gaind",
                    Values = new ChartValues<double>() { caloiresGained },
                    Fill   = Brushes.Red,
                },

                new ColumnSeries
                {
                    Title  = "Needed",
                    Values = new ChartValues<double>() { caloriesNeeded },
                    Fill   = (Brush)Application.Current.Resources["PrimaryHueMidBrush"],
                },

                new ColumnSeries
                {
                     Title  = "Lost",
                     Values = new ChartValues<double>() { caloriesLost },
                     Fill   = Brushes.ForestGreen,
                }
            };


            CaloriesLabels = new[] { "Calories" };
            Formatter = value => value.ToString() + "  kCal.";
            CaloriesChart.DataContext = this;
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

        ///////////////////////////////////////////////////////////




        ////////// PopUpBox Functions/Event Handlers //////////

        private void AddFoodButton_Click(object sender, RoutedEventArgs e)
        {
            DialogBox.IsOpen = true;
            AddFoodDialogBox.Visibility = Visibility.Visible;
        }

        private void AddWorkoutButton_Click(object sender, RoutedEventArgs e)
        {
            DialogBox.IsOpen = true;
            AddWorkoutDialogBox.Visibility = Visibility.Visible;
        }

        //////////////////////////////////////////////////////




        ////////// DialogBoxes Functions/Event Handlers //////////

        private void DialogBoxAddFoodButton_Click(object sender, RoutedEventArgs e)
        {

            if (FoodComboBox.SelectedIndex == -1)
                UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Please choose Food!");

            else if (string.IsNullOrWhiteSpace(FoodQuantityTextBox.Text))
                UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Please enter Food Quantity!");

            else
            {
                App.Database.AddFood(FoodComboBox.Text, double.Parse(FoodQuantityTextBox.Text), UserWindow.signedInUser.Id);
                AddFoodDialogBox.Visibility = Visibility.Collapsed;
                DialogBox.IsOpen = false;

                // Refresh Calories Card
                CaloriesChart.DataContext = null;
                LoadCaloriesCard();
            }

            // Reset Dialog Box Fields
            FoodComboBox.SelectedIndex = -1;
            FoodQuantityTextBox.Text = "";
        }

        private void DialogBoxAddWorkoutButton_Click(object sender, RoutedEventArgs e)
        {

            if (WorkoutsComboBox.SelectedIndex == -1)
                UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Please choose Workout!");

            else if (string.IsNullOrWhiteSpace(WorkoutsDurationTextBox.Text))
                UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Please enter Workout Duration!");

            else
            {
                App.Database.AddWorkout(WorkoutsComboBox.Text, double.Parse(WorkoutsDurationTextBox.Text), UserWindow.signedInUser);
                AddWorkoutDialogBox.Visibility = Visibility.Collapsed;
                DialogBox.IsOpen = false;

                // Update Progress of the Challenges having the same type as the entered workout
                App.Database.UpdateChallengesProgress(UserWindow.signedInUser.Id, WorkoutsComboBox.Text, double.Parse(WorkoutsDurationTextBox.Text));

                // Refresh Challenges card
                LoadJoinedChallengesCards();

                // Refresh Calories Card
                CaloriesChart.DataContext = null;
                LoadCaloriesCard();
            }

            // Reset Dialog Box Fields
            WorkoutsComboBox.SelectedIndex = -1;
            WorkoutsDurationTextBox.Text = "";
        }

        private void DialogBoxCancelButton_Click(object sender, RoutedEventArgs e)
        {
            AddFoodDialogBox.Visibility = Visibility.Collapsed;
            AddWorkoutDialogBox.Visibility = Visibility.Collapsed;
            DialogBox.IsOpen = false;

            WorkoutsComboBox.SelectedIndex = -1;
            WorkoutsDurationTextBox.Text = "";

            FoodComboBox.SelectedIndex = -1;
            FoodQuantityTextBox.Text = "";
        }

        ///////////////////////////////////////////////////////////


    }
}
