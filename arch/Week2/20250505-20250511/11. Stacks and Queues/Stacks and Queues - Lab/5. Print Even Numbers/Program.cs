namespace _5._Print_Even_Numbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            Queue<int> queue = new Queue<int>();


            foreach (var item in numbers)
            {
                if (item % 2 == 0)
                {
                    queue.Enqueue(item);
                }
                
            }

            Console.WriteLine(string.Join(", ", queue));
        }
    }
}
