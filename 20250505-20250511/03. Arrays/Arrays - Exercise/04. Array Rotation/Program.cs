namespace _04._Array_Rotation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Queue<int> queue = new Queue<int>(Console.ReadLine().Split(' ').Select(int.Parse));

            int rotations = int.Parse(Console.ReadLine());

            for (int i = 0; i < rotations; i++)
            {
                queue.Enqueue(queue.Dequeue());
            }

            Console.WriteLine(string.Join(" ", queue));
        }
    }
}
