using System.IO;

namespace Bezier.FileManager
{
    public class FileReader
    {
        private readonly string _path;
        public FileReader(string filePath)
        {
            _path = filePath;
        }

        public string GetContentFromFile()
        {
            using (StreamReader reader = new StreamReader(_path))
            {
                string fileContent = reader.ReadToEnd().Trim();

                return fileContent;
            }
        }
    }
}