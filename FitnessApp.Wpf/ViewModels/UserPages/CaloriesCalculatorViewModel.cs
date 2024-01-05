using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessApp.Models;

namespace FitnessApp.Wpf.ViewModels.UserPages;

public class CaloriesCalculatorViewModel : ObservableObject
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

    #region Age : int - Возраст

    private int _age;

    /// <summary> Возраст </summary>
    public int Age
    {
        get => _age;
        set => SetProperty(ref _age, value);
    }

    #endregion Age
    
    #region ResultCalculating : double - Результат расчета

    private double _resultCalculating;

    /// <summary> Результат расчета </summary>
    public double ResultCalculating
    {
        get => _resultCalculating;
        set => SetProperty(ref _resultCalculating, value);
    }

    #endregion ResultCalculating
    
    #endregion

    #region Commands

    #region CalculateCommand : Расчет калорий

    /// <summary> Расчет калорий </summary>
    public ICommand CalculateCommand { get; set; }

    private void OnCalculateCommandExecuted()
    {
        if (CurrentPerson.Gender == "Female")
        {
            var weight = CurrentPerson.Weight;
            var height = CurrentPerson.Height;
            double age = Age;
            var calculate = 665 + 9.6 * weight + 1.8 * height - 4.7 * age;
            ResultCalculating = calculate; // " kCal."
        }
        else
        {
            var weight = CurrentPerson.Weight;
            var height = CurrentPerson.Height;
            double age = Age;
            var calculate = 66 + 13.7 * weight + 1.8 * height - 4.7 * age;
            ResultCalculating = calculate;
        }
    }

    private bool CanCalculateCommandExecute() => true;

    #endregion Calculate

    #endregion
    
    public CaloriesCalculatorViewModel(Person selectedPerson)
    {
        CalculateCommand = new RelayCommand(OnCalculateCommandExecuted, CanCalculateCommandExecute);
        
        CurrentPerson = selectedPerson;
        Age = CurrentPerson.Age;
    }
}