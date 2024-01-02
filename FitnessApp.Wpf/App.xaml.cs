using System.Windows;
using FitnessApp.Core;
using FitnessApp.Wpf.Implementation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FitnessApp.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    internal static Database Database = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        Database = new Database();
        
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<IOpenView, OpenWindowService>();
            })
            .Build();
        
        
        var openView = host.Services.GetService<IOpenView>();
        openView?.OpenSigningView();
    }
}