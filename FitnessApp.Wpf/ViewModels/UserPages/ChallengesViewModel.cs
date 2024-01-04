using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessApp.Models;

namespace FitnessApp.Wpf.ViewModels.UserPages;

public class ChallengesViewModel : ObservableObject
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

    #region Commads

    #region JoinChallengeCommand : Description

    /// <summary> Description </summary>
    public ICommand JoinChallengeCommand { get; set; }

    private void OnJoinChallengeCommandExecuted()
    {
        // ToggleButton toggleButton = sender as ToggleButton;
        // int selectedChallengeIndex = ChallengesListBox.Items.IndexOf(toggleButton.DataContext);
        //
        // Challenge currentChallenge = (Challenge) ChallengesListBox.Items[selectedChallengeIndex];
        //
        // App.Database.JoinChallenge(UserWindow.SignedInUser.Id, currentChallenge.Id);
        //
        // // Rrefresh Joined Challenges Cards in Home Page 
        // UserWindow.HomePageObject.LoadJoinedChallengesCards();
    }

    private bool CanJoinChallengeCommandExecute() => true;

    #endregion JoinChallenge

    #region LeaveChallengeCommand : Description

    /// <summary> Description </summary>
    public ICommand LeaveChallengeCommand { get; set; }

    private void OnLeaveChallengeCommandExecuted()
    {
        // ToggleButton toggleButton = sender as ToggleButton;
        // int selectedChallengeIndex = ChallengesListBox.Items.IndexOf(toggleButton.DataContext);
        //
        // Challenge currentChallenge = (Challenge) ChallengesListBox.Items[selectedChallengeIndex];
        //
        // App.Database.UnjoinChallenge(UserWindow.SignedInUser.Id, currentChallenge.Id);
        //
        // // Rrefresh Joined Challenges Cards in Home Page 
        // UserWindow.HomePageObject.LoadJoinedChallengesCards();
    }

    private bool CanLeaveChallengeCommandExecute() => true;

    #endregion LeaveChallenge
    
    #endregion
    
    public ChallengesViewModel(Person selectedPerson)
    {
        JoinChallengeCommand = new RelayCommand(OnJoinChallengeCommandExecuted, CanJoinChallengeCommandExecute);
        LeaveChallengeCommand = new RelayCommand(OnLeaveChallengeCommandExecuted, CanLeaveChallengeCommandExecute);
        
        CurrentPerson = selectedPerson;
        LoadAllChallengesCards();
    }

    private void LoadAllChallengesCards()
    {
        // // Setting Data context for ChallengesListBox
        // ChallengeViewModel challengeDataContext = new ChallengeViewModel();
        // challengeDataContext.AllChallengesViewModel(UserWindow.SignedInUser.Id);
        // DataContext = challengeDataContext;
    }
}