using System.Diagnostics.Metrics;

namespace _05._Decrypting_Message
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int key = int.Parse(Console.ReadLine());

            int n = int.Parse(Console.ReadLine());
            string finalWord = "";

            for (int i = 0; i < n; i++)
            {
                char letter = char.Parse(Console.ReadLine());
                letter = (char)((int)(letter) + key);
                finalWord += letter;
                
            }
            Console.WriteLine(finalWord);
        }
    }
}
