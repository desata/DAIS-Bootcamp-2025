namespace _2._Stack_Sum
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            Stack<int> stack = new Stack<int>();

            foreach (var item in numbers)
            {
                stack.Push(item);
            }

            string[] input = Console.ReadLine().ToLower().Split(' ').ToArray();

            while (input[0] != "end")
            {
                if (input[0] == "add")
                {
                    int firstNum = int.Parse(input[1]);
                    int secondNum = int.Parse(input[2]);

                    stack.Push(firstNum);
                    stack.Push(secondNum);
                }
                if (input[0] == "remove")
                {
                    int n = int.Parse(input[1]);

                    if (stack.Count >= n)
                    {
                        for (int i = 0; i < n; i++)
                        {
                            stack.Pop();
                        }
                    }
                }

                input = Console.ReadLine().ToLower().Split(' ').ToArray();
            }

            int sum = 0;
            foreach (var item in stack)
            {
                sum += item;
            }
            Console.WriteLine($"Sum: {sum}");
        }
    }
}
