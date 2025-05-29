namespace _1._Reverse_a_String
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            Stack<char> strings = new Stack<char>();

            foreach(var item in input)
            {
                strings.Push(item);
            }

            while (strings.Count > 0)
            {
                Console.Write($"{strings.Peek()}");

                strings.Pop();
            }
        }
    }
}
