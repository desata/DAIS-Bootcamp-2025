namespace _3._Simple_Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split().ToArray();

            Stack<string> stack = new Stack<string>();

            for (int i = input.Length - 1; i >= 0; i--)
            {
                stack.Push(input[i]);
            }

            while (stack.Count > 1)
            {
                string firstNum = stack.Pop();
                string operation = stack.Pop();
                string secondNum = stack.Pop();
                int result = 0;
                if (operation == "+")
                {
                    result = int.Parse(firstNum) + int.Parse(secondNum);
                }
                else if (operation == "-")
                {
                    result = int.Parse(firstNum) - int.Parse(secondNum);
                }
                stack.Push(result.ToString());
            }

            Console.WriteLine(stack.Peek());

        }
    }
}
