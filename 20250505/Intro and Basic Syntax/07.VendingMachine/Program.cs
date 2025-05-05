using System.ComponentModel.Design;

namespace Vending_Machine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double coins;
            double sum = 0.00;
            string command = Console.ReadLine();


            while (command != "Start")
            {
                if (command != "0.1" && command != "0.2" && command != "0.5" && command != "1" && command != "2")
                {
                    Console.WriteLine($"Cannot accept {command}");
                }
                else
                {
                    coins = double.Parse(command);
                    sum += coins;
                }

                command = Console.ReadLine();

            }

            command = Console.ReadLine();

            while (command != "End")
            {
                command = command.ToLower();
                if (command == "nuts")
                {
                    if (sum >= 2)
                    {
                        sum -= 2;
                        Console.WriteLine($"Purchased {command}");
                    }
                    else
                    {
                        Console.WriteLine("Sorry, not enough money");
                    }
                }
                else if (command == "water")
                {
                    if (sum >= 0.7)
                    {
                        sum -= 0.7;
                        Console.WriteLine($"Purchased {command}");
                    }
                    else
                    {
                        Console.WriteLine("Sorry, not enough money");
                    }
                }
                else if (command == "crisps")
                {
                    if (sum >= 1.5)
                    {
                        sum -= 1.5;
                        Console.WriteLine($"Purchased {command}");
                    }
                    else
                    {
                        Console.WriteLine("Sorry, not enough money");
                    }
                }
                else if (command == "soda")
                {
                    if (sum >= 0.8)
                    {
                        sum -= 0.8;
                        Console.WriteLine($"Purchased {command}");
                    }
                    else
                    {
                        Console.WriteLine("Sorry, not enough money");
                    }
                }
                else if (command == "coke")
                {
                    if (sum >= 1)
                    {
                        sum -= 1;
                        Console.WriteLine($"Purchased {command}");
                    }
                    else
                    {
                        Console.WriteLine("Sorry, not enough money");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid product");
                }
                command = Console.ReadLine();
            }

            Console.WriteLine($"Change: {sum:F2}");
        }
    }
}
