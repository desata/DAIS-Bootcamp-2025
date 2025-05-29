namespace _05._Top_Integers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            List<int> topIntegers = new List<int>();

            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = 1; j < numbers.Length; j++)
                {
                    if (numbers[i] > numbers[j])
                    {
                        if (j == numbers.Length - 1)
                        {
                            topIntegers.Add(numbers[i]);
                        }
                    }

                }
            }

            topIntegers.Add(numbers[numbers.Length - 1]);
            Console.WriteLine(string.Join(" ", topIntegers));
        }
    }
}
