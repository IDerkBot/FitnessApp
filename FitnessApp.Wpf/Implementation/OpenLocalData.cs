using Arion.Data.Implementation.Saver;
using FitnessApp.Core.Interfaces;
using FitnessApp.Models;

namespace FitnessApp.Wpf.Implementation;

public class OpenLocalData : IOpenLocalData
{
    public LocalData? OpenData()
    {
        return SaveDataController.Load<LocalData>($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\FitnessApp\data.dat", true);
    }

    public void SaveData(LocalData localData)
    {
        SaveDataController.Save($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\FitnessApp\data.dat", localData, true);
    }
}