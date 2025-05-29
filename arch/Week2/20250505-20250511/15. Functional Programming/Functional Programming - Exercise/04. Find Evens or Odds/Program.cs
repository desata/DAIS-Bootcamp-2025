namespace _04._Find_Evens_or_Odds
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            string command = Console.ReadLine();

            Predicate<int> isEven = n => n % 2 == 0;

            for (int i = numbers[0]; i <= numbers[1]; i++)
            { 
                if (command == "even")
                {
                    if (isEven(i))
                    {
                        Console.Write(i + " ");
                    }
                }
                else if (command == "odd")
                {
                    if (!isEven(i))
                    {
                        Console.Write(i + " ");
                    }
                }
            }


        }
    }
}
