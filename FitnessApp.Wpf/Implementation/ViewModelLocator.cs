using CommunityToolkit.Mvvm.DependencyInjection;
using FitnessApp.Wpf.ViewModels.SignUpPages;
using FitnessApp.Wpf.ViewModels.Windows;
using FitnessApp.Wpf.Views.SignUpPages;

namespace FitnessApp.Wpf.Implementation;

public class ViewModelLocator
{
    public SigningViewModel? SigningVm => Ioc.Default.GetService<SigningViewModel>();
    public SignUpViewModel? SignUpVm => Ioc.Default.GetService<SignUpViewModel>();
    // public SigningViewModel? SigningVm => Ioc.Default.GetService<SigningViewModel>();
    // public SigningViewModel? SigningVm => Ioc.Default.GetService<SigningViewModel>();
    // public SigningViewModel? SigningVm => Ioc.Default.GetService<SigningViewModel>();
    // public SigningViewModel? SigningVm => Ioc.Default.GetService<SigningViewModel>();
    // public SigningViewModel? SigningVm => Ioc.Default.GetService<SigningViewModel>();
    // public SigningViewModel? SigningVm => Ioc.Default.GetService<SigningViewModel>();
}