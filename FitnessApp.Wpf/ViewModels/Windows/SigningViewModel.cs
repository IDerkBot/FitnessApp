using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FitnessApp.Core;

namespace FitnessApp.Wpf.ViewModels.Windows;

public class SigningViewModel : ObservableObject
{
    #region Private Properties

    private IOpenView _openViewService;

    #endregion
    
    #region Properties

    

    #endregion

    #region Commands

    #region CreateAccountCommand : Создание нового аккаунта

    /// <summary> Создание нового аккаунта </summary>
    public ICommand CreateAccountCommand { get; set; }

    private void OnCreateAccountCommandExecuted()
    {
        _openViewService.OpenSignUpView();
    }

    private bool CanCreateAccountCommandExecute() => true;

    #endregion CreateAccount

    #endregion

    #region Constructor

    public SigningViewModel(IOpenView openView)
    {
        _openViewService = openView;
        
        CreateAccountCommand = new RelayCommand(OnCreateAccountCommandExecuted, CanCreateAccountCommandExecute);
        
        CreateAccountCommand.Execute(null);
    }

    #endregion
}