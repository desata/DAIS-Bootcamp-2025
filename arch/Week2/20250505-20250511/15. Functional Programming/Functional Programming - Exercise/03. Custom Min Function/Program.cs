namespace _03._Custom_Min_Function
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            Func<int[], int> minFunc = numbers =>
            {
                int min = int.MaxValue;
                foreach (var number in numbers)
                {
                    if (number < min)
                    {
                        min = number;
                    }
                }
                return min;
            };

            int minNumber = minFunc(numbers);

            Console.WriteLine(minNumber);

        }
    }
}
