namespace _10._Lower_or_Upper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char symbol = char.Parse(Console.ReadLine());

            if (char.IsUpper(symbol))
            {
                Console.WriteLine("upper-case");
            }
            else if (char.IsLower(symbol))
            {
                Console.WriteLine("lower-case");
            }
        }
    }
}
