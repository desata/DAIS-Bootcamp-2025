namespace _7._Hot_Potato
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            int n = int.Parse(Console.ReadLine());

            Queue<string> queue = new Queue<string>(input.Split(' '));

            while (queue.Count > 1)
            {
                for (int i = 0; i < n - 1; i++)
                {
                    queue.Enqueue(queue.Dequeue());
                }
                Console.WriteLine($"Removed {queue.Dequeue()}");
            }
            Console.WriteLine($"Last is {queue.Peek()}");
        }
    }
}
