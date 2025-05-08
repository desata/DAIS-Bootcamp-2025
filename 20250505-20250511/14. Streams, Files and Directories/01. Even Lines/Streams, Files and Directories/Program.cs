namespace Streams__Files_and_Directories
{
        using System;
        using System.IO;
        using System.Text;
        using System.Text.RegularExpressions;

        public class EvenLines
        {
            static void Main()
            {
                string inputFilePath = @"..\..\..\text.txt";

                Console.WriteLine(ProcessLines(inputFilePath));
            }

            public static string ProcessLines(string inputFilePath)
            {
                StringBuilder result = new StringBuilder();

                using (StreamReader reader = new StreamReader(inputFilePath))
                {
                    string line;
                    int lineNumber = 0;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (lineNumber % 2 == 0)
                        {
                            string replaced = Regex.Replace(line, @"[-,.!?]", "@");

                            string[] words = replaced.Split(' ', StringSplitOptions.RemoveEmptyEntries).Reverse().ToArray();
                            

                            result.AppendLine(string.Join(" ", words));
                        }

                        lineNumber++;
                    }
                }

                return result.ToString().TrimEnd(); 
            }
        
    }
}
