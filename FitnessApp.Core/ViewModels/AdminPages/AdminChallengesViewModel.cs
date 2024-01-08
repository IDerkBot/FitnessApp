using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessApp.Core.Interfaces;
using FitnessApp.Models;

namespace FitnessApp.Core.ViewModels.AdminPages;

public class AdminChallengesViewModel : ObservableObject
{
    #region Private Properties

    private readonly IOpenView _openViewService;

    #endregion Private Properties

    #region Properties

    #region Challenges : ObservableCollection<Challenge> - Description

    private ObservableCollection<Challenge> _challenges;

    /// <summary> Description </summary>
    public ObservableCollection<Challenge> Challenges
    {
        get => _challenges;
        set => SetProperty(ref _challenges, value);
    }

    #endregion Challenges

    #region Workouts : ObservableCollection<Workout> - Description

    private ObservableCollection<Workout> _workouts;

    /// <summary> Description </summary>
    public ObservableCollection<Workout> Workouts
    {
        get => _workouts;
        set => SetProperty(ref _workouts, value);
    }

    #endregion Workouts

    #region NewChallenge : Challenge - Description

    private Challenge _newChallenge;

    /// <summary> Description </summary>
    public Challenge NewChallenge
    {
        get => _newChallenge;
        set => SetProperty(ref _newChallenge, value);
    }

    #endregion NewChallenge

    #endregion Properties

    #region Commands

    #region LoadedCommand : Description

    /// <summary> Description </summary>
    public ICommand LoadedCommand { get; set; }

    private void OnLoadedCommandExecuted()
    {
        LoadAllChallenges();
        LoadWorkouts();
    }

    private bool CanLoadedCommandExecute() => true;

    #endregion Loaded

    #region OpenAddChallengeCommand : Description

    /// <summary> Description </summary>
    public ICommand OpenAddChallengeCommand { get; set; }

    private void OnOpenAddChallengeCommandExecuted()
    {
        NewChallenge = new Challenge();
        _openViewService.OpenChallengeAdd();
    }

    private bool CanOpenAddChallengeCommandExecute() => true;

    #endregion OpenAddChallenge

    #region CloseAddChallengeCommand : Description

    /// <summary> Description </summary>
    public ICommand CloseAddChallengeCommand { get; set; }

    private void OnCloseAddChallengeCommandExecuted()
    {
        _openViewService.CloseChallengeAdd();
    }

    private bool CanCloseAddChallengeCommandExecute() => true;

    #endregion CloseAddChallenge

    #region AddChallengeCommand : Description

    /// <summary> Description </summary>
    public ICommand AddChallengeCommand { get; set; }

    private void OnAddChallengeCommandExecuted()
    {
        // Validation
        if (string.IsNullOrWhiteSpace(NewChallenge.Name) ||
            string.IsNullOrWhiteSpace(NewChallenge.Description) ||
            string.IsNullOrWhiteSpace(NewChallenge.Reward) ||
            NewChallenge.TargetMinutes == 0)
        {
            return;
        }

        Global.Database.AddNewChallenge(NewChallenge);

        // Refresh Challenges
        LoadAllChallenges();

        _openViewService.CloseChallengeAdd();
    }

    private bool CanAddChallengeCommandExecute() => true;

    #endregion AddChallenge

    #region DeleteChallengeCommand : Description

    /// <summary> Description </summary>
    public ICommand DeleteChallengeCommand { get; set; }

    private void OnDeleteChallengeCommandExecuted()
    {
        //     // Get Challenge Index
        //     Button deleteButton = sender as Button;
        //     int selectedChallengeIndex = AllChallengesListBox.Items.IndexOf(deleteButton.DataContext);
        //     Challenge chosenChallenge = (Challenge)AllChallengesListBox.Items[selectedChallengeIndex];
        //     
        //     // Delete Challenge From Database
        //     App.Database.DeleteChallenge(chosenChallenge.Id);
        //     
        //     // Refresh Challenges
        //     LoadAllChallenges();
        //     
        //     // Confirmation Message
        //     AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Challenge deleted successfully");
    }

    private bool CanDeleteChallengeCommandExecute() => true;

    #endregion DeleteChallenge

    #region EditChallengeCommand : Description

    /// <summary> Description </summary>
    public ICommand EditChallengeCommand { get; set; }

    private void OnEditChallengeCommandExecuted()
    {
    }

    private bool CanEditChallengeCommandExecute() => true;

    #endregion EditChallenge

    #region AddImageCommand : Description

    /// <summary> Description </summary>
    public ICommand AddImageCommand { get; set; }

    private void OnAddImageCommandExecuted()
    {
        //     OpenFileDialog browsePhotoDialog = new OpenFileDialog();
        //     browsePhotoDialog.Title = "Select Challenge Photo";
        //     browsePhotoDialog.Filter = "All formats|*.jpg;*.jpeg;*.png|" +
        //                                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
        //                                "PNG (*.png)|*.png";
        //
        //     if (browsePhotoDialog.ShowDialog() == true)
        //     {
        //         // challengePhoto = new ImageModel(browsePhotoDialog.FileName);
        //         //
        //         // // Confirmation Message
        //         // AdminWindow.AdminWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Photo added successfully");
        //     }
    }

    private bool CanAddImageCommandExecute() => true;

    #endregion AddImage

    #endregion Commands

    #region Constructor

    public AdminChallengesViewModel(IOpenView openViewService)
    {
        _openViewService = openViewService;

        LoadedCommand = new RelayCommand(OnLoadedCommandExecuted, CanLoadedCommandExecute);
        OpenAddChallengeCommand = new RelayCommand(OnOpenAddChallengeCommandExecuted, CanOpenAddChallengeCommandExecute);
        CloseAddChallengeCommand = new RelayCommand(OnCloseAddChallengeCommandExecuted, CanCloseAddChallengeCommandExecute);
        DeleteChallengeCommand = new RelayCommand(OnDeleteChallengeCommandExecuted, CanDeleteChallengeCommandExecute);
        EditChallengeCommand = new RelayCommand(OnEditChallengeCommandExecuted, CanEditChallengeCommandExecute);
        AddChallengeCommand = new RelayCommand(OnAddChallengeCommandExecuted, CanAddChallengeCommandExecute);
        AddImageCommand = new RelayCommand(OnAddImageCommandExecuted, CanAddImageCommandExecute);

        Challenges = new ObservableCollection<Challenge>();
        Workouts = new ObservableCollection<Workout>();
    }

    #endregion Constructor

    #region Private Methods

    private void LoadAllChallenges()
    {
        Challenges.Clear();
        var challenges = Global.Database.GetChallenges();
        foreach (var challenge in challenges)
        {
            Challenges.Add(challenge);
        }
    }

    private void LoadWorkouts()
    {
        Workouts.Clear();
        foreach (var workout in Global.Database.GetWorkouts())
        {
            Workouts.Add(workout);
        }
    }

    #endregion Private Methods
}