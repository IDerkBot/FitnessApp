using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessApp.Models;

namespace FitnessApp.Core.ViewModels.AdminPages;

public class AdminWorkoutsViewModel : ObservableObject
{
    #region Private Properties

    

    #endregion Private Properties

    #region Properties

    #region Workouts : ObservableCollection<Workout> - Description

    private ObservableCollection<Workout> _workouts;

    /// <summary> Description </summary>
    public ObservableCollection<Workout> Workouts
    {
        get => _workouts;
        set => SetProperty(ref _workouts, value);
    }

    #endregion Workouts

    #endregion Properties

    #region Commands

    #region LoadedCommand : Description

    /// <summary> Description </summary>
    public ICommand LoadedCommand { get; set; }

    private void OnLoadedCommandExecuted()
    {
        
    }

    private bool CanLoadedCommandExecute() => true;

    #endregion Loaded

    #endregion Commands
    
    #region Constructor

    public AdminWorkoutsViewModel()
    {

        LoadedCommand = new RelayCommand(OnLoadedCommandExecuted, CanLoadedCommandExecute);

        Workouts = new ObservableCollection<Workout>();
    }

    #endregion Constructor

    #region Private methods

    private void LoadWorkouts()
    {
        Workouts.Clear();
        foreach (var workout in Global.Database.GetWorkouts())
        {
            Workouts.Add(workout);
        }
    }

    #endregion Private methods
}