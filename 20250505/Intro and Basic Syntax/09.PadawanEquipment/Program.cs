namespace _09.PadawanEquipment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double johnsMoney = double.Parse(Console.ReadLine());
            int studentsCount = int.Parse(Console.ReadLine());
            double priceOflightsabers = double.Parse(Console.ReadLine());
            double priceOfrobes = double.Parse(Console.ReadLine());
            double priceOfbelts = double.Parse(Console.ReadLine());
            double beltDiscount = 0;
            double cost;

            if (studentsCount >= 6)
            {
                beltDiscount = priceOfbelts * (studentsCount / 6);

            }
            cost = (studentsCount * (priceOfrobes + priceOfbelts)) + (Math.Ceiling(1.1 * studentsCount) * priceOflightsabers) - beltDiscount;


            if (johnsMoney - cost >= 0)
            {
                Console.WriteLine($"The money is enough - it would cost {cost:F2}lv.");
            }
            else
            {
                Console.WriteLine($"John will need {cost - johnsMoney:F2}lv more.");
            }
        }
    }
}
