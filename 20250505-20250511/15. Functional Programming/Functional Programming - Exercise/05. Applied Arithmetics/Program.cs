namespace _05._Applied_Arithmetics
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

            Func<int[], int[]> add = numbers =>
            {
                for (int i = 0; i < numbers.Length; i++)
                {
                    numbers[i] += 1;
                }
                return numbers;
            };

            Func<int[], int[]> multuply = numbers =>
            {
                for (int i = 0; i < numbers.Length; i++)
                {
                    numbers[i] *= 2;
                }
                return numbers;
            };

            Func<int[], int[]> subtract = numbers =>
            {
                for (int i = 0; i < numbers.Length; i++)
                {
                    numbers[i] -= 1;
                }
                return numbers;
            };  

            Func<int[], int[]> print = numbers =>
            {
                Console.WriteLine(string.Join(" ", numbers));
                return numbers;
            };

            while (command != "end")
            {
                if (command == "add")
                {
                    numbers = add(numbers);
                }
                else if (command == "multiply")
                {
                    numbers = multuply(numbers);
                }
                else if (command == "subtract")
                {
                    numbers = subtract(numbers);
                }
                else if (command == "print")
                {
                    numbers = print(numbers);
                }
                command = Console.ReadLine();
            }
        }
    }
}
