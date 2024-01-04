using CommunityToolkit.Mvvm.DependencyInjection;
using FitnessApp.Wpf.ViewModels.SignUpPages;
using FitnessApp.Wpf.ViewModels.UserPages;
using FitnessApp.Wpf.ViewModels.Windows;
using FitnessApp.Wpf.Views.SignUpPages;

namespace FitnessApp.Wpf.Implementation;

public class ViewModelLocator
{
    #region Windows

    public SigningViewModel? SigningVm => Ioc.Default.GetService<SigningViewModel>();
    public UserViewModel? UserVm => Ioc.Default.GetService<UserViewModel>();

    #endregion

    #region UserPages

    public CaloriesCalculatorViewModel? CaloriesCalculatorVm => Ioc.Default.GetService<CaloriesCalculatorViewModel>();
    public ChallengesViewModel? ChallengesVm => Ioc.Default.GetService<ChallengesViewModel>();
    public HomeViewModel? HomeVm => Ioc.Default.GetService<HomeViewModel>();
    public PlansViewModel? PlansVm => Ioc.Default.GetService<PlansViewModel>();
    public SettingsViewModel? SettingsVm => Ioc.Default.GetService<SettingsViewModel>();

    #endregion

    #region SignUpPages

    public SignUpViewModel? SignUpVm => Ioc.Default.GetService<SignUpViewModel>();

    #endregion

    #region AdminPages

    

    #endregion
    
    // public SigningViewModel? SigningVm => Ioc.Default.GetService<SigningViewModel>();
    // public SigningViewModel? SigningVm => Ioc.Default.GetService<SigningViewModel>();
    // public SigningViewModel? SigningVm => Ioc.Default.GetService<SigningViewModel>();
    // public SigningViewModel? SigningVm => Ioc.Default.GetService<SigningViewModel>();
    // public SigningViewModel? SigningVm => Ioc.Default.GetService<SigningViewModel>();
}