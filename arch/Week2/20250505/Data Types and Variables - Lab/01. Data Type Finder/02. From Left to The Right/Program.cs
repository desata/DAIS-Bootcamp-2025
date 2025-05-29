
namespace _02._From_Left_to_The_Right
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int lines = int.Parse(Console.ReadLine());
            string[] input = new string[lines];

            for (int i = 0; i < lines; i++)
            {
                input[i] = Console.ReadLine();
            }

            for (int i = 0; i < lines; i++)
            {
                string[] numbers = input[i].Split(' ');
                long firstNum = long.Parse(numbers[0]);
                long secondNum = long.Parse(numbers[1]);
                if (firstNum > secondNum)
                {
                    Console.WriteLine(SumDigits(firstNum));
                }
                else
                {
                    Console.WriteLine(SumDigits(secondNum));
                }
            }

        }

        private static long SumDigits(long number)
        {
            long sum = 0;
            long digit = 0;
            while (Math.Abs(number) > 0)
            {
                digit = number % 10;
                sum += digit;
                number = number / 10;

            }
            return Math.Abs(sum);
        }
    }
}
