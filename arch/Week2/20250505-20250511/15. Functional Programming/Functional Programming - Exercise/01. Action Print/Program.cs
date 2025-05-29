namespace _01._Action_Print
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] names = new string(Console.ReadLine()).Split(' ', StringSplitOptions.RemoveEmptyEntries);

            Action<string> print = name => Console.WriteLine(name);

            foreach (var name in names)
            {
                print(name);
            }
        }
    }
}
