using System;
using System.IO;
using System.Linq;

namespace AutoHeader.Options
{
    public class RemoveHeaderOption : Option
    {
        public override void Execute()
        {
            Console.WriteLine("Execute \'RemoveHeaderOption\': {0}", Arg);

            if (!Directory.Exists(Arg))
            {
                throw new ExecutionException(string.Format("Directory does not exist: {0}", Arg));
            }

            RemoveHeaderFromFilesInDirectory(Arg);
        }

        private void RemoveHeaderFromFilesInDirectory(string directory)
        {
            foreach (var filePath in Directory.GetFiles(directory).Where(f => f.EndsWith(".cs")))
            {
                RemoveHeaderFromFile(filePath);
            }
            foreach (var subDir in Directory.GetDirectories(directory))
            {
                RemoveHeaderFromFilesInDirectory(subDir);
            }
        }

        private void RemoveHeaderFromFile(string filePath)
        {
            string header;
            using (var streamReader = new StreamReader("HeaderTemplate.txt"))
            {
                header = streamReader.ReadToEnd();
            }

            string fileContent;
            using (var streamReader = new StreamReader(filePath))
            {
                fileContent = streamReader.ReadToEnd();
            }

            if (fileContent.Length >= header.Length)
            {
                var fileHeader = fileContent.Substring(0, header.Length);
                if (fileHeader == header)
                {
                    fileContent = fileContent.Substring(header.Length);
                    using (var stream = new StreamWriter(filePath, false))
                    {
                        stream.Write(fileContent);
                    }
                }
            }
        }
    }
}