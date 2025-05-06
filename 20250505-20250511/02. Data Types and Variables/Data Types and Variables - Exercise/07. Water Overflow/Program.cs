using System.ComponentModel;

namespace _07._Water_Overflow
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int numberOfLines = int.Parse(Console.ReadLine());
            int totalLiters = 0;

            for (int i = 0; i < numberOfLines; i++)
            {
                int liters = int.Parse(Console.ReadLine());

                if (liters > 255 || liters + totalLiters > 255)
                {
                    Console.WriteLine("Insufficient capacity!");
                    continue;
                }
                else
                {
                    totalLiters += liters;
                }

            }
            Console.WriteLine(totalLiters);

        }
    }
}
