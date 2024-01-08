﻿namespace FitnessApp.Core.Interfaces;

public interface IOpenView
{
    void OpenAdminView();
    void CloseAdminView();
    void OpenSigningView();
    void CloseSigningView();
    void OpenUserView();
    void CloseUserView();
    void OpenAdminHomeView();
    void CloseAdminHomeView();
    void OpenAdminSettingsView();
    void CloseAdminSettingsView();
    void OpenChallengesSetupView();
    void CloseChallengesSetupView();
    void OpenSetUpProfileView();
    void CloseSetUpProfileView();
    void OpenSignUpView();
    void CloseSignUpView();
    void OpenCaloriesCalculatorView();
    void CloseCaloriesCalculatorView();
    void OpenChallengesView();
    void CloseChallengesView();
    void OpenHomeView();
    void CloseHomeView();
    void OpenPlansView();
    void ClosePlansView();
    void OpenSettingsView();
    void CloseSettingsView();

    void OpenWorkout();
    void CloseWorkout();
    void OpenFood();
    void CloseFood();
    
    void Logout(bool isAdmin);
    void Shutdown();
}