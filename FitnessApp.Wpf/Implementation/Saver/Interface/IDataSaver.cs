namespace Arion.Data.Implementation.Saver.Interface;

public interface IDataSaver
{
    T? Load<T>(string filePath, bool crypt) where T : class;
    void Save<T>(string filePath, T item, bool crypt) where T : class;
}