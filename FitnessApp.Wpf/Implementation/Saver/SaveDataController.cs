using Arion.Data.Implementation.Saver.Interface;
using FitnessApp.Wpf.Implementation.Saver;

namespace Arion.Data.Implementation.Saver;

public class SaveDataController
{
    private static readonly IDataSaver Manager = new DataSaver();

    public static void Save<T>(string filePath, T item, bool crypt = false) where T : class => Manager.Save(filePath, item, crypt);
    
    public static T? Load<T>(string filePath, bool crypt = false) where T : class => Manager.Load<T>(filePath, crypt);
}