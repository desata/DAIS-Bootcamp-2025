namespace _10.RageExpenses
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int lostGamesCount = int.Parse(Console.ReadLine());

            double headsetPrice = double.Parse(Console.ReadLine());
            double mousePrice = double.Parse(Console.ReadLine());
            double keyboardPrice = double.Parse(Console.ReadLine());
            double displayPrice = double.Parse(Console.ReadLine());

            double expences = 0.00;

            if (lostGamesCount >= 12)
            {
                expences += lostGamesCount / 12 * displayPrice;
            }
            if (lostGamesCount >= 6)
            {
                expences += (lostGamesCount / 6) * keyboardPrice;
            }
            if (lostGamesCount >= 3)
            {
                expences += (lostGamesCount / 3) * mousePrice;
            }
            if (lostGamesCount >= 2)
            {
                expences += (lostGamesCount / 2) * headsetPrice;
            }

            Console.WriteLine($"Rage expenses: {expences:F2} lv.");

        }
    }
}
