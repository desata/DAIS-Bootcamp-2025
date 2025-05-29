namespace DirectoryTraversal
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;

    public class DirectoryTraversal
    {
        static void Main()
        {
            string path = Console.ReadLine();
            string reportFileName = @"\report.txt";

            string reportContent = TraverseDirectory(path);
            Console.WriteLine(reportContent);

            WriteReportToDesktop(reportContent, reportFileName);
        }

        public static string TraverseDirectory(string inputFolderPath)
        {
            Dictionary<string, List<FileInfo>> filesByExtension = new Dictionary<string, List<FileInfo>>();

            DirectoryInfo dir = new DirectoryInfo(inputFolderPath);
            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                string ext = file.Extension;

                if (!filesByExtension.ContainsKey(ext))
                {
                    filesByExtension[ext] = new List<FileInfo>();
                }

                filesByExtension[ext].Add(file);
            }

            var sortedExtensions = filesByExtension
                .OrderByDescending(e => e.Value.Count)
                .ThenBy(e => e.Key);

            StringBuilder sb = new StringBuilder();

            foreach (var extGroup in sortedExtensions)
            {
                sb.AppendLine(extGroup.Key);

                foreach (var file in extGroup.Value.OrderBy(f => f.Length))
                {
                    sb.AppendLine($"--{file.Name} - {(file.Length / 1024.0):F3}kb");
                }
            }

            return sb.ToString().TrimEnd();
        }

        public static void WriteReportToDesktop(string textContent, string reportFileName)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fullPath = desktopPath + reportFileName;

            File.WriteAllText(fullPath, textContent);
        }
    }
}