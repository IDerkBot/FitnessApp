using System.IO;
using System.Xml.Serialization;
using Arion.Data.Implementation.Saver.Interface;
using Newtonsoft.Json;

namespace FitnessApp.Wpf.Implementation.Saver;

public class DataSaver : IDataSaver
{
    public T? Load<T>(string filePath, bool crypt = false) where T : class
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentNullException(nameof(filePath), "Путь до файла не может быть пустым!");
        }

        if (crypt)
        {
            var file = new FileInfo(filePath);
            if (!Directory.Exists(file.DirectoryName))
                Directory.CreateDirectory(file.DirectoryName);
            using var jFs = new FileStream(filePath, FileMode.OpenOrCreate);
            using var jSr = new StreamReader(jFs);
            var text = jSr.ReadToEnd();
            return JsonConvert.DeserializeObject<T>(text);
        }

        var formatter = new XmlSerializer(typeof(T));

        using var fs = new FileStream(filePath, FileMode.OpenOrCreate);
        
        if (fs.Length > 0 && formatter.Deserialize(fs) is T item)
            return item;

        return null;
    }

    public void Save<T>(string filePath, T item, bool crypt = false) where T : class
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentNullException(nameof(filePath), "Путь до файла не может быть пустым!");
        }

        if (item == null)
        {
            throw new ArgumentNullException(nameof(item), "Данные для сохранения не могут быть NULL!");
        }

        if (crypt)
        {
            var text = JsonConvert.SerializeObject(item);
            File.WriteAllText(filePath, text);
        }
        else
        {
            var formatter = new XmlSerializer(typeof(T));

            using var fs = new FileStream(filePath, FileMode.OpenOrCreate);
        
            formatter.Serialize(fs, item);
        }
    }
}