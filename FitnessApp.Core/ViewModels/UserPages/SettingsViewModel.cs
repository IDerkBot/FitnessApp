﻿using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessApp.Models;

namespace FitnessApp.Core.ViewModels.UserPages;

public class SettingsViewModel : ObservableObject
{
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

    #endregion

    #region Commands

    #region UpdateProfileCommand : Description

    /// <summary> Description </summary>
    public ICommand UpdateProfileCommand { get; set; }

    private void OnUpdateProfileCommandExecuted()
    {
        Global.Database.UpdatePersonProfile(CurrentPerson);
    }

    private bool CanUpdateProfileCommandExecute() => true;

    #endregion UpdateProfile

    #endregion Commands
    
    public SettingsViewModel(Person selectedPerson)
    {
        CurrentPerson = selectedPerson;
        
        UpdateProfileCommand = new RelayCommand(OnUpdateProfileCommandExecuted, CanUpdateProfileCommandExecute);
        
        // UserWindow.SettingsPageObject = this;

        // Initialize Profile Expander to be expanded
        // ProfileExpander.IsExpanded = true;

        // Initialize DataContext with signedInUser Model
        // DataContext = UserWindow.SignedInUser;
    }


    // private void Expander_Expanded(object sender, System.Windows.RoutedEventArgs e)
    // {
    //     // // Remove Expanded Event Handler from all Expanders
    //     // // to prevent calling Expander_Expanded Event Handler 
    //     // // recursively.
    //     // ProfileExpander.Expanded -= Expander_Expanded;
    //     // AccountExpander.Expanded -= Expander_Expanded;
    //     // SecurityExpander.Expanded -= Expander_Expanded;
    //     // AboutExpander.Expanded -= Expander_Expanded;
    //     // FeedbackExpander.Expanded -= Expander_Expanded;
    //     // HelpExpander.Expanded -= Expander_Expanded;
    //     //
    //     // // Close all Expanders.
    //     // ProfileExpander.IsExpanded = false;
    //     // AccountExpander.IsExpanded = false;
    //     // SecurityExpander.IsExpanded = false;
    //     // AboutExpander.IsExpanded = false;
    //     // FeedbackExpander.IsExpanded = false;
    //     // HelpExpander.IsExpanded = false;
    //     //
    //     // // Re-add Expanded Event Handler to all Expanders.
    //     // ProfileExpander.Expanded += Expander_Expanded;
    //     // AccountExpander.Expanded += Expander_Expanded;
    //     // SecurityExpander.Expanded += Expander_Expanded;
    //     // AboutExpander.Expanded += Expander_Expanded;
    //     // FeedbackExpander.Expanded += Expander_Expanded;
    //     // HelpExpander.Expanded += Expander_Expanded;
    //     //
    //     //
    //     // // Get Current Expander object from sender.
    //     // Expander currentExpander = sender as Expander;
    //     //
    //     // // Remove Expanded Event Handler from Current Expander.
    //     // currentExpander.Expanded -= Expander_Expanded;
    //     //
    //     // // Open current Expander only.
    //     // currentExpander.IsExpanded = true;
    //     //
    //     // // Re-add Expanded Event Handler to Current Expander.
    //     // currentExpander.Expanded += Expander_Expanded;
    // }
    //
    // // private Image currentProfilePhoto;
    //
    // private void UpdateUserProfilePhotoButton_Click(object sender, System.Windows.RoutedEventArgs e)
    // {
    //     // OpenFileDialog browsePhotoDialog = new OpenFileDialog();
    //     // browsePhotoDialog.Title = "Select your New Profile Photo";
    //     // browsePhotoDialog.Filter = "All formats|*.jpg;*.jpeg;*.png|" +
    //     //                            "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
    //     //                            "PNG (*.png)|*.png";
    //     //
    //     // if (browsePhotoDialog.ShowDialog() == true)
    //     // {
    //     //     // currentProfilePhoto = new Image(browsePhotoDialog.FileName);
    //     //     // UserProfilePhoto.ImageSource = currentProfilePhoto.Source;
    //     // }
    // }
    //
    // // private void DecimalNumbersOnlyFieldValidation(object sender, TextCompositionEventArgs e)
    // // {
    // //     // var s = sender as TextBox;
    // //     // var text = s.Text.Insert(s.SelectionStart, e.Text);
    // //     // e.Handled = !double.TryParse(text, out double d);
    // // }
    //
    // private void UpdateProfileButton_Click(object sender, System.Windows.RoutedEventArgs e)
    // {
    //     // // Empty Fields Validation
    //     // if (string.IsNullOrWhiteSpace(WeightTextBox.Text))
    //     //     UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Weight Is Empty!");
    //     //
    //     // else if (string.IsNullOrWhiteSpace(HeightTextBox.Text))
    //     //     UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Height Is Empty!");
    //     //
    //     // else if (string.IsNullOrWhiteSpace(TargetWeightTextBox.Text))
    //     //     UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Target Weight Is Empty!");
    //     //
    //     // else if (string.IsNullOrWhiteSpace(KilosToLosePerWeekTextBox.Text))
    //     //     UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Kilos To Lose Per Week Is Empty!");
    //     //
    //     // else if (string.IsNullOrWhiteSpace(WorkoutsPerWeekTextBox.Text))
    //     //     UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Workouts Per Week Is Empty!");
    //     //
    //     // else if (string.IsNullOrWhiteSpace(WorkoutHoursPerDayTextBox.Text))
    //     //     UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Workout Hours Per Day Is Empty!");
    //     //
    //     // else
    //     // {
    //     //     // Update signedInUser User Model
    //     //     // if (currentProfilePhoto != null) // Check if profile photo is updated
    //     //     //     UserWindow.signedInUser.ProfilePhoto   = currentProfilePhoto;
    //     //     // UserWindow.signedInUser.Weight             = double.Parse(WeightTextBox            .Text);
    //     //     // UserWindow.signedInUser.Height             = double.Parse(HeightTextBox            .Text);
    //     //     // UserWindow.signedInUser.TargetWeight       = double.Parse(TargetWeightTextBox      .Text);
    //     //     // UserWindow.signedInUser.KilosToLosePerWeek = double.Parse(KilosToLosePerWeekTextBox.Text);
    //     //     // UserWindow.signedInUser.WorkoutsPerWeek    = double.Parse(WorkoutsPerWeekTextBox   .Text);
    //     //     // UserWindow.signedInUser.WorkoutHoursPerDay = double.Parse(WorkoutHoursPerDayTextBox.Text);
    //     //
    //     //     // Update User's Profile in database
    //     //     App.Database.UpdatePersonProfile(UserWindow.SignedInUser);
    //     //
    //     //     // Refresh UserWindow DataContext
    //     //     UserWindow.UserWindowObject.DataContext = null;
    //     //     UserWindow.UserWindowObject.DataContext = UserWindow.SignedInUser;
    //     //
    //     //     // Refresh CaloriesCalculatorPage DataContext
    //     //     UserWindow.CaloriesCalculatorPageObject.DataContext = null;
    //     //     UserWindow.CaloriesCalculatorPageObject.DataContext = UserWindow.SignedInUser;
    //     //
    //     //     // TODO Refresh Weight and Calories Cards in Home Page
    //     //     // UserWindow.HomePageObject.WeightChart.DataContext = null;
    //     //     UserWindow.HomePageObject.LoadWeightChart();
    //     //     UserWindow.HomePageObject.LoadTotalWeightLostCard();
    //     //     UserWindow.HomePageObject.LoadAverageWeightLostCard();
    //     //     UserWindow.HomePageObject.LoadCaloriesCard();
    //     //
    //     //     // Confirmation Message
    //     //     UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Profile Updated!");
    //     // }
    // }
    //
    // private void UpdateAccountButton_Click(object sender, System.Windows.RoutedEventArgs e)
    // {
    //     // // Empty Fields Validation
    //     // if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text))
    //     //     UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("First Name is Empty!");
    //     //
    //     // else if (string.IsNullOrWhiteSpace(LastNameTextBox.Text))
    //     //     UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Last Name is Empty!");
    //     //
    //     // else if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
    //     //     UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Email is Empty!");
    //     //
    //     // // Check Email Validation
    //     // else if (!EmailTextBox.Text.Contains("@") || !EmailTextBox.Text.Contains(".com"))
    //     //     UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Invalid E-mail");
    //     //
    //     // // Check Email Availability
    //     // // else if (EmailTextBox.Text != UserWindow.signedInUser.Email)
    //     // // {
    //     // //     if (App.Database.IsEmailTaken(EmailTextBox.Text))
    //     // //         UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("E-mail is in use");
    //     // // }
    //     //
    //     // else
    //     // {
    //     //     // Update signedInUser User Model
    //     //     // UserWindow.signedInUser.FirstName = FirstNameTextBox.Text;
    //     //     // UserWindow.signedInUser.LastName = LastNameTextBox.Text;
    //     //     // UserWindow.signedInUser.Email = EmailTextBox.Text;
    //     //     //
    //     //     // // Update User's Account in database
    //     //     // App.Database.UpdateUserAccount(UserWindow.signedInUser);
    //     //     //
    //     //     // // Refresh UserWindow DataContext
    //     //     // UserWindow.UserWindowObject.DataContext = null;
    //     //     // UserWindow.UserWindowObject.DataContext = UserWindow.signedInUser;
    //     //     //
    //     //     // // Confirmation Message
    //     //     // UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Account Updated!");
    //     // }
    // }
    //
    // private void UpdatePasswordButton_Click(object sender, System.Windows.RoutedEventArgs e)
    // {
    //     // // Empty Fields Validation
    //     // if (string.IsNullOrWhiteSpace(OldPasswordTextBox.Password))
    //     //     UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Please enter your old password!");
    //     //
    //     // else if (string.IsNullOrWhiteSpace(NewPasswordTextBox.Password))
    //     //     UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Please enter your new password!");
    //     //
    //     // else if (NewPasswordTextBox.Password != ConfirmNewPasswordTextBox.Password)
    //     //     UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue(
    //     //         "New Password and Confirmation doesn't match!");
    //     //
    //     // // Password Validation
    //     // else if (NewPasswordTextBox.Password.Length < 7)
    //     //     UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Password must be 7 characters or more");
    //     //
    //     // else if (App.Database.EncryptPassword(OldPasswordTextBox.Password) != UserWindow.SignedInUser.Password)
    //     //     UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Old Password is Incorrect!");
    //     //
    //     // else
    //     // {
    //     //     // Update signedInUser User Model
    //     //     UserWindow.SignedInUser.Password = App.Database.EncryptPassword(NewPasswordTextBox.Password);
    //     //
    //     //     // Update User's Password in database
    //     //     App.Database.UpdateUserPassword(UserWindow.SignedInUser);
    //     //
    //     //     // Confirmation Message
    //     //     UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Password Updated!");
    //     // }
    // }
    //
    // private void SubmitFeedbackButton_Click(object sender, System.Windows.RoutedEventArgs e)
    // {
    //     // App.Database.SaveFeedback(UserWindow.signedInUser.Id, RatingBar.Value , FeedbackTextBox.Text);
    //     //
    //     // // Confirmation Message
    //     // UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Thank you for your feedback!");
    // }
}