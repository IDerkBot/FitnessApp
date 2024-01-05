using System.Windows;
using FitnessApp.Core.Interfaces;

namespace FitnessApp.Wpf.Implementation;

public class AlertService : IAlertService
{
    public void Info(string message)
    {
        MessageBox.Show(message);
    }

    public void Warning(string message)
    {
        MessageBox.Show(message);
    }

    public void Error(string message)
    {
        MessageBox.Show(message);
    }
}