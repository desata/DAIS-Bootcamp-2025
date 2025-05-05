namespace _03.Vacation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int countOfPeople = int.Parse(Console.ReadLine());
            string groupType = Console.ReadLine();
            string dayOfTheWeek = Console.ReadLine();
            double singlePrice = 0.00;
            double totalPrice = 0.00;

            if (groupType == "Students")
            {
                if (dayOfTheWeek == "Friday")
                {
                    singlePrice = 8.45;
                }
                else if (dayOfTheWeek == "Saturday")
                {
                    singlePrice = 9.80;
                }
                else if (dayOfTheWeek == "Sunday")
                {
                    singlePrice = 10.46;
                }

                totalPrice = singlePrice * countOfPeople;
                if (countOfPeople >= 30)
                {
                    totalPrice *= 0.85;
                }

            }
            else if (groupType == "Business")
            {
                if (dayOfTheWeek == "Friday")
                {
                    singlePrice = 10.90;
                }
                else if (dayOfTheWeek == "Saturday")
                {
                    singlePrice = 15.60;
                }
                else if (dayOfTheWeek == "Sunday")
                {
                    singlePrice = 16;
                }

                if (countOfPeople >= 100)
                {
                    countOfPeople -= 10;
                }

                totalPrice = singlePrice * countOfPeople;

            }
            else if (groupType == "Regular")
            {
                if (dayOfTheWeek == "Friday")
                {
                    singlePrice = 15;
                }
                else if (dayOfTheWeek == "Saturday")
                {
                    singlePrice = 20;
                }
                else if (dayOfTheWeek == "Sunday")
                {
                    singlePrice = 22.50;
                }

                totalPrice = singlePrice * countOfPeople;

                if (countOfPeople >= 10 && countOfPeople <= 20)
                {
                    totalPrice *= 0.95;
                }
            }

            Console.WriteLine($"Total price: {totalPrice:F2}");

        }
    }
}
