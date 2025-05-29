namespace _02._Pounds_to_Dollars
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Create a program that converts British pounds to US dollars formatted to the 3rd decimal point.
            //1 British Pound = 1.31 Dollars
        // 80 39

                double pound = double.Parse(Console.ReadLine());
            
            Console.WriteLine($"{pound*1.31f:F3}");

        }
    }
}
