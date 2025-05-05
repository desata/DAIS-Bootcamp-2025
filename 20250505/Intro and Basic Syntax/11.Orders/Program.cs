namespace _11.Orders
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //((days * capsulesCount) * pricePerCapsule)

            int n = int.Parse(Console.ReadLine());
            double totalAmount = 0.00;

            for (int i = 0; i < n; i++)
            {
                double pricePerCapsule = double.Parse(Console.ReadLine());
                int days = int.Parse(Console.ReadLine());
                int capsulesCount = int.Parse(Console.ReadLine());
                double amount = ((days * capsulesCount) * pricePerCapsule);

                Console.WriteLine($"The price for the coffee is: ${amount:F2}");
                totalAmount += amount;

            }

            Console.WriteLine($"Total: ${ totalAmount:F2}");
        }
    }
}
