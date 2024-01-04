using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessApp.Models;
using FitnessApp.Wpf.Views.Windows;

namespace FitnessApp.Wpf.ViewModels.UserPages;

public class PlansViewModel : ObservableObject
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

    #region ViewMoreCommand : Description

    /// <summary> Description </summary>
    public ICommand ViewMoreCommand { get; set; }

    private void OnViewMoreCommandExecuted()
    {
        // Button button = sender as Button;
        // int selectedPlanIndex = PlansListBox.Items.IndexOf(button.DataContext);
        // Plan currentPlan = (Plan)PlansListBox.Items[selectedPlanIndex];
        //
        // PlanDaysListBox.DataContext = new DaysViewModel(currentPlan.Id);
        // DaysSideDrawer.IsRightDrawerOpen = true;
    }

    private bool CanViewMoreCommandExecute() => true;

    #endregion ViewMore

    #region JoinPlanCommand : Description

    /// <summary> Description </summary>
    public ICommand JoinPlanCommand { get; set; }

    private void OnJoinPlanCommandExecuted()
    {
        // ToggleButton toggleButton = sender as ToggleButton;
        // int selectedPlanIndex = PlansListBox.Items.IndexOf(toggleButton.DataContext);
        // Plan currentPlan = (Plan)PlansListBox.Items[selectedPlanIndex];
        //
        // if (App.Database.IsInPlan(UserWindow.SignedInUser.Id))
        //     UserWindow.UserWindowObject.MessagesSnackbar.MessageQueue.Enqueue(
        //         "You are currently in a plan. Please unjoin it first.");
        // else
        //     App.Database.JoinPlan(UserWindow.SignedInUser.Id, currentPlan.Id);
        //
        // LoadAllPlansCards();
        //
        // // Rrefresh Joined Plan Card in Home Page 
        // UserWindow.HomePageObject.LoadJoinedPlanCard();
    }

    private bool CanJoinPlanCommandExecute() => true;

    #endregion JoinPlan

    #region LeavePlanCommand : Description

    /// <summary> Description </summary>
    public ICommand LeavePlanCommand { get; set; }

    private void OnLeavePlanCommandExecuted()
    {
        // ToggleButton toggleButton = sender as ToggleButton;
        // int selectedPlanIndex = PlansListBox.Items.IndexOf(toggleButton.DataContext);
        // Plan currentPlan = (Plan)PlansListBox.Items[selectedPlanIndex];
        //
        // App.Database.UnjoinPlan(UserWindow.SignedInUser.Id);
        //
        // LoadAllPlansCards();
        //
        // // Rrefresh Joined Plan Card in Home Page 
        // UserWindow.HomePageObject.LoadJoinedPlanCard();
    }

    private bool CanLeavePlanCommandExecute() => true;


    #endregion LeavePlan
    
    #endregion
    
    public PlansViewModel(Person selectedPerson)
    {
        ViewMoreCommand = new RelayCommand(OnViewMoreCommandExecuted, CanViewMoreCommandExecute);
        JoinPlanCommand = new RelayCommand(OnJoinPlanCommandExecuted, CanJoinPlanCommandExecute);
        LeavePlanCommand = new RelayCommand(OnLeavePlanCommandExecuted, CanLeavePlanCommandExecute);
        
        CurrentPerson = selectedPerson;
        LoadAllPlansCards();
    }

    public void LoadAllPlansCards()
    {
        // PlansListBox.DataContext = new PlansViewModel(UserWindow.SignedInUser.Id);
    }
}