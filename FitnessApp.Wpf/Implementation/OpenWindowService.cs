using System.Windows;
using FitnessApp.AdminWindowPages;
using FitnessApp.Core;
using FitnessApp.SignUpPages;
using FitnessApp.UserWindowPages;
using FitnessApp.Windows;
using FitnessApp.Wpf.Views.SignUpPages;

namespace FitnessApp.Wpf.Implementation;

public class OpenWindowService : IOpenView
{
    internal AdminWindow? AdminWindow { get; set; }
    internal SigningWindow? SigningWindow { get; set; }
    internal UserWindow? UserWindow { get; set; }
    internal SignUpPage? SignUpPage { get; set; }
    internal SetUpProfilePage? SetUpProfilePage { get; set; }
    internal CaloriesCalculatorPage? CaloriesCalculatorPage { get; set; }
    internal ChallengesPage? ChallengesPage { get; set; }
    internal HomePage? HomePage { get; set; }
    internal PlansPage? PlansPage { get; set; }
    internal SettingsPage? SettingsPage { get; set; }
    internal AdminHomePage? AdminHomePage { get; set; }
    internal AdminSettingsPage? AdminSettingsPage { get; set; }
    internal ChallengesSetupPage? ChallengesSetupPage { get; set; }

    public void OpenAdminView()
    {
        
    }

    public void OpenSigningView()
    {
        if (SigningWindow != null) return;
        SigningWindow = new SigningWindow();
        SigningWindow.Closed += (_, _) => SigningWindow = null;
        SigningWindow.Show();
    }

    public void OpenUserView()
    {
        
    }

    public void OpenAdminHomeView()
    {
        throw new NotImplementedException();
    }

    public void OpenAdminSettingsView()
    {
        throw new NotImplementedException();
    }

    public void OpenChallengesSetupView()
    {
        throw new NotImplementedException();
    }

    public void OpenSetUpProfileView()
    {
        throw new NotImplementedException();
    }

    public void OpenSignUpView()
    {
        SignUpPage ??= new SignUpPage();
        SigningWindow?.SignUpPagesFrame.NavigationService.Navigate(SignUpPage);
    }

    public void OpenCaloriesCalculatorView()
    {
        throw new NotImplementedException();
    }

    public void OpenChallengesView()
    {
        throw new NotImplementedException();
    }

    public void OpenHomeView()
    {
        throw new NotImplementedException();
    }

    public void OpenPlansView()
    {
        throw new NotImplementedException();
    }

    public void OpenSettingsView()
    {
        throw new NotImplementedException();
    }
}