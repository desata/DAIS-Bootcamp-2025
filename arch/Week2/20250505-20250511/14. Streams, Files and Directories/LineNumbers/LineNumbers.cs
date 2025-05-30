﻿namespace LineNumbers
{
    using System;
    using System.IO;
    using System.Linq;

    public class LineNumbers
    {
        static void Main()
        {
            string inputFilePath = @"..\..\..\text.txt";
            string outputFilePath = @"..\..\..\output.txt";

            ProcessLines(inputFilePath, outputFilePath);
        }

        public static void ProcessLines(string inputFilePath, string outputFilePath)
        {
            string[] lines = File.ReadAllLines(inputFilePath);

            for (int i = 0; i < lines.Length; i++)
            {
                int letterCount = lines[i].Count(char.IsLetter);
                int punctuationCount = lines[i].Count(char.IsPunctuation);
                File.AppendAllText(outputFilePath, $"Line {i + 1}: {lines[i]} ({letterCount}) ({punctuationCount}){Environment.NewLine}");
            }
        }
    }
}
