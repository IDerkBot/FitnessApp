using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using FitnessApp.Core;
using FitnessApp.Core.Interfaces;
using FitnessApp.Core.ViewModels.SignUpPages;
using FitnessApp.Core.ViewModels.Windows;
using FitnessApp.Wpf.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessApp.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        Global.Database = new Database();

        Ioc.Default.ConfigureServices(
                new ServiceCollection()
                    .AddSingleton<IOpenView, OpenWindowService>()
                    .AddSingleton<IChartCreator, ChartCreator>()
                    .AddSingleton<IAlertService, AlertService>()
                    .AddSingleton<IOpenLocalData, OpenLocalData>()
                    .AddSingleton<SigningViewModel>()
                    .AddSingleton<SignUpViewModel>()
                    .AddSingleton<UserViewModel>()
                    .AddSingleton<AdminViewModel>()
                
                    
                    .BuildServiceProvider());
        
        
        var openView = Ioc.Default.GetService<IOpenView>();
        openView?.OpenSigningView();
    }
}