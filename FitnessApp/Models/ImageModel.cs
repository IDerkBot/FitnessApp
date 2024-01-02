using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FitnessApp.Models
{
    public class ImageModel
    {
        private ImageSource _source;

        public ImageModel() { }

        public ImageModel(string imageFilePath)
        {
            ByteArray = ConvertImageFileToByteArray(imageFilePath);
            _source = new BitmapImage(new Uri(imageFilePath, UriKind.RelativeOrAbsolute));
        }

        public string Default { get; set; }

        public byte[] ByteArray { get; set; }

        public ImageSource Source
        {
            get
            {
                if (Default == null ) { return _source; }

                if (ByteArray != null) { return ConvertByteArrayToImageSource(); }

                return new BitmapImage(new Uri(Default, UriKind.RelativeOrAbsolute));
            }
            set => _source = value;
        }

       
        // Convert Image to Byte array
        private byte[] ConvertImageFileToByteArray(string imageFilePath)
        {
            FileStream fileSteam = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileSteam);

            return binaryReader.ReadBytes((int)fileSteam.Length);
        }


        // Convert Byte array to Image source
        private ImageSource ConvertByteArrayToImageSource()
        {
            BitmapImage bitmapImage = new BitmapImage();
            MemoryStream memoryStream = new MemoryStream(ByteArray);

            bitmapImage.BeginInit();
            bitmapImage.StreamSource = memoryStream;
            bitmapImage.EndInit();

            return bitmapImage;
        }

    }
}
