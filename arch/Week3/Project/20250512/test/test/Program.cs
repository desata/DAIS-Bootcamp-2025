using System.Linq;
namespace test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var collumns = new List<string> { "Bob", "Ted", "Phill" };
            var values = string.Join(", ", collumns.Select(c => "@" + c));

            foreach (var value in values)
            {
                Console.WriteLine($"{value}");
            }
            
        }
    }
}
