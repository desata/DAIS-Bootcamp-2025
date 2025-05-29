namespace _02._Knights_of_Honor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] names = new string(Console.ReadLine()).Split(' ', StringSplitOptions.RemoveEmptyEntries);

            Action<string> print = name => Console.WriteLine($"Sir {name}");

            foreach (var name in names)
            {
                print(name);
            }
        }
    }
}
