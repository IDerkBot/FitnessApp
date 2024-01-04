using System.Windows;
using FitnessApp.AdminWindowPages;
using FitnessApp.Core;
using FitnessApp.SignUpPages;
using FitnessApp.Windows;
using FitnessApp.Wpf.Views.SignUpPages;
using FitnessApp.Wpf.Views.UserPages;
using FitnessApp.Wpf.Views.Windows;

namespace FitnessApp.Wpf.Implementation;

public class OpenWindowService : IOpenView
{
    internal AdminWindow? AdminWindow { get; set; }
    private SigningWindow? SigningWindow { get; set; }
    private UserWindow? UserWindow { get; set; }
    private SignUpPage? SignUpPage { get; set; }
    internal SetUpProfilePage? SetUpProfilePage { get; set; }
    internal CaloriesCalculatorPage? CaloriesCalculatorPage { get; set; }
    internal ChallengesPage? ChallengesPage { get; set; }
    internal HomePage? HomePage { get; set; }
    internal PlansPage? PlansPage { get; set; }
    internal SettingsPage? SettingsPage { get; set; }
    internal AdminHomePage? AdminHomePage { get; set; }
    internal AdminSettingsPage? AdminSettingsPage { get; set; }
    internal ChallengesSetupPage? ChallengesSetupPage { get; set; }

    #region AdminView

    public void OpenAdminView()
    {
        
    }

    public void CloseAdminView()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region SigningView

    public void OpenSigningView()
    {
        if (SigningWindow != null) return;
        SigningWindow = new SigningWindow();
        SigningWindow.Closed += (_, _) => SigningWindow = null;
        SigningWindow.Show();
    }

    public void CloseSigningView() => SigningWindow?.Close();

    #endregion

    #region UserView

    public void OpenUserView()
    {
        if (UserWindow != null) return;
        UserWindow = new UserWindow();
        UserWindow.Closed += (_, _) => UserWindow = null;
        UserWindow.Show();
    }

    public void CloseUserView() => UserWindow?.Close();

    #endregion

    #region AdminHomeView

    public void OpenAdminHomeView()
    {
        throw new NotImplementedException();
    }

    public void CloseAdminHomeView()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region AdminSettingsView

    public void OpenAdminSettingsView()
    {
        throw new NotImplementedException();
    }

    public void CloseAdminSettingsView()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region ChallengesSetupView

    public void OpenChallengesSetupView()
    {
        throw new NotImplementedException();
    }

    public void CloseChallengesSetupView()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region SetUpProfileView

    public void OpenSetUpProfileView()
    {
        throw new NotImplementedException();
    }

    public void CloseSetUpProfileView()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region SignUpView

    public void OpenSignUpView()
    {
        SignUpPage ??= new SignUpPage();
        SigningWindow?.SignUpPagesFrame.NavigationService.Navigate(SignUpPage);
    }

    public void CloseSignUpView()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region CaloriesCalculatorView

    public void OpenCaloriesCalculatorView()
    {
        CaloriesCalculatorPage ??= new CaloriesCalculatorPage();
        UserWindow?.UserWindowPagesFrame.NavigationService.Navigate(CaloriesCalculatorPage);
    }

    public void CloseCaloriesCalculatorView() { }

    #endregion

    #region ChallengesView

    public void OpenChallengesView()
    {
        ChallengesPage ??= new ChallengesPage();
        UserWindow?.UserWindowPagesFrame.NavigationService.Navigate(ChallengesPage);
    }

    public void CloseChallengesView() { }

    #endregion

    #region HomeView

    public void OpenHomeView()
    {
        HomePage ??= new HomePage();
        UserWindow?.UserWindowPagesFrame.NavigationService.Navigate(HomePage);
    }

    public void CloseHomeView() { }

    #endregion

    #region PlansView

    public void OpenPlansView()
    {
        PlansPage ??= new PlansPage();
        UserWindow?.UserWindowPagesFrame.NavigationService.Navigate(PlansPage);
    }

    public void ClosePlansView() { }

    #endregion

    #region SettingsView

    public void OpenSettingsView()
    {
        SettingsPage ??= new SettingsPage();
        UserWindow?.UserWindowPagesFrame.NavigationService.Navigate(SettingsPage);
    }

    public void CloseSettingsView() { }

    #endregion
}