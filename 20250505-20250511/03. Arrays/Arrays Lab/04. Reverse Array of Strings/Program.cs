namespace _04._Reverse_Array_of_Strings
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] strings = Console.ReadLine()
                .Split(' ')
                .ToArray();

            string[] reversedStrings = new string[strings.Length];
            for (int i = 0; i < strings.Length; i++)
            {
                reversedStrings[i] = strings[strings.Length - 1 - i];
            }

            Console.Write(string.Join(" ", reversedStrings));

        }
    }
}
