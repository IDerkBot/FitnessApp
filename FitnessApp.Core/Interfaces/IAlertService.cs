namespace FitnessApp.Core.Interfaces;

public interface IAlertService
{
    void Info(string message);
    void Warning(string message);
    void Error(string message);
}