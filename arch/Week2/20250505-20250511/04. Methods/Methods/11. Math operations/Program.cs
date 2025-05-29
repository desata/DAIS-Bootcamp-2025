namespace _11._Math_operations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}

/*
using System;

class Program
{
    static void Main()
    {
        double num1 = double.Parse(Console.ReadLine());
        string op = Console.ReadLine();
        double num2 = double.Parse(Console.ReadLine());

        double result = Calculate(num1, op, num2);
        Console.WriteLine(result);
    }

    static double Calculate(double num1, string op, double num2)
    {
        switch (op)
        {
            case "+": return num1 + num2;
            case "-": return num1 - num2;
            case "*": return num1 * num2;
            case "/": 
                if (num2 == 0)
                {
                    Console.WriteLine("Cannot divide by zero.");
                    return 0;
                }
                return num1 / num2;
            default:
                Console.WriteLine("Invalid operator.");
                return 0;
        }
    }
}
 \
*/
