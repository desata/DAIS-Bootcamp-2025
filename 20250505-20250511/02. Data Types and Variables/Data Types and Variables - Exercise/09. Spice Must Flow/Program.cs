namespace _09._Spice_Must_Flow
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int initialYield = int.Parse(Console.ReadLine());
            int days = 0;
            int totalYield = 0;

            while (initialYield >= 100)
            {
                totalYield += initialYield - 26;
                days++;
                initialYield -= 10;
            }
            if (totalYield > 26)
            {
                totalYield -= 26;
            }

            Console.WriteLine(days);
            Console.WriteLine(totalYield);
        }
    }
}
