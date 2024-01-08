using FitnessApp.Models;

namespace FitnessApp.Core.Interfaces;

public interface IOpenLocalData
{
    LocalData? OpenData();
    void SaveData(LocalData localData);
}