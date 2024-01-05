using CommunityToolkit.Mvvm.DependencyInjection;
using FitnessApp.Core.ViewModels.SignUpPages;
using FitnessApp.Core.ViewModels.Windows;
using FitnessApp.Wpf.Views.SignUpPages;

namespace FitnessApp.Wpf.Implementation;

public class ViewModelLocator
{
    #region Windows

    public SigningViewModel? SigningVm => Ioc.Default.GetService<SigningViewModel>();
    public UserViewModel? UserVm => Ioc.Default.GetService<UserViewModel>();
    public AdminViewModel? AdminVm => Ioc.Default.GetService<AdminViewModel>();

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