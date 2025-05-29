namespace _11._TriFunction
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            string[] names = Console.ReadLine().Split().ToArray();
            Console.WriteLine(names.First(name => name.Select(x => (int)x).Sum() >= n));

        }
    }
}
