namespace _06._Reverse_And_Exclude
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int n = int.Parse(Console.ReadLine());

            Predicate<int> isDivisible = number => number % n == 0;

            Func<int[], List<int>> reverse = numbers =>
            {
                List<int> result = new List<int>();
                for (int i = numbers.Length - 1; i >= 0; i--)
                {
                    if (!isDivisible(numbers[i]))
                    {
                        result.Add(numbers[i]);
                    }
                }
                return result;
            };

            Console.WriteLine(string.Join(" ", reverse(numbers)));
        }
    }
}
