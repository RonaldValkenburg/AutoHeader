using System;
using System.IO;
using System.Linq;

namespace AutoHeader.Options
{
    public class AddHeaderOption : Option
    {
        public override void Execute()
        {
            Console.WriteLine("Execute \'AddHeaderOption\': {0}", Arg);
            
            if (!Directory.Exists(Arg))
            {
                throw new ExecutionException(string.Format("Directory does not exist: {0}", Arg));
            }
            
            AddHeaderToFilesInDirectory(Arg);
        }

        private void AddHeaderToFilesInDirectory(string directory)
        {
            foreach (var filePath in Directory.GetFiles(directory).Where(f => f.EndsWith(".cs")))
            {
                AddHeaderToFile(filePath);
            }
            foreach (var subDir in Directory.GetDirectories(directory))
            {
                AddHeaderToFilesInDirectory(subDir);
            }
        }

        private void AddHeaderToFile(string filePath)
        {
            var tempFilePath = GetTempFileName(filePath);
            File.Move(filePath, tempFilePath);
            using (var stream = new StreamWriter(filePath, false))
            {
                using (var streamReader = new StreamReader("HeaderTemplate.txt"))
                {
                    stream.Write(streamReader.ReadToEnd());
                }
                using (var streamReader = new StreamReader(tempFilePath))
                {
                    stream.Write(streamReader.ReadToEnd());
                }
            }
            File.Delete(tempFilePath);
        }

        private static string GetTempFileName(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            var path = Path.GetDirectoryName(filePath) ?? string.Empty;
            var tempFilePath = Path.Combine(path, string.Format("~{0}", fileName));
            return tempFilePath;
        }
    }
}
