namespace _08._List_Of_Predicates
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            // Read the dividers and convert to int[]
            int[] dividers = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            // Define a predicate that checks divisibility by all dividers
            Func<int, bool> isDivisible = number =>
                dividers.All(div => number % div == 0);

            // Generate numbers from 1 to n and filter them using the predicate
            var result = Enumerable.Range(1, n).Where(isDivisible);

            // Print result
            Console.WriteLine(string.Join(" ", result));
        }
    }
}
