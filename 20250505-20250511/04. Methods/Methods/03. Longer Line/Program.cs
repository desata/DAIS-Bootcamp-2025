namespace _03._Longer_Line
{
    internal class Program
    {
        static void Main(string[] args)
        {

            double x1 = int.Parse(Console.ReadLine());
            double y1 = int.Parse(Console.ReadLine());
            double x2 = int.Parse(Console.ReadLine());
            double y2 = int.Parse(Console.ReadLine());

            double x3 = int.Parse(Console.ReadLine());
            double y3 = int.Parse(Console.ReadLine());
            double x4 = int.Parse(Console.ReadLine());
            double y4 = int.Parse(Console.ReadLine());


            double lengthOne = CalculateDiagonal(x1, y1) + CalculateDiagonal(x2, y2);
            double lengthTwo = CalculateDiagonal(x3, y3) + CalculateDiagonal(x4, y4);

            if (lengthOne - lengthTwo >= 0)
            {
                if (CalculateDiagonal(x1, y1) <= CalculateDiagonal(x2, y2))
                {
                    Console.WriteLine($"({x1}, {y1})({x2}, {y2})");
                }
                else
                {
                    Console.WriteLine($"({x2}, {y2})({x1}, {y1})");
                }
            }
            else
            {
                if (CalculateDiagonal(x3, y3) <= CalculateDiagonal(x4, y4))
                {
                    Console.WriteLine($"({x3}, {y3})({x4}, {y4})");
                }
                else
                {
                    Console.WriteLine($"({x4}, {y4})({x3}, {y3})");
                }

            }
        }

        private static double CalculateDiagonal(double x, double y)
        {
            return Math.Sqrt((Math.Abs(x * x) + Math.Abs(y * y)));
        }
    
    }
}
/* 
 * AI generated code
 * 
 using System;

class Program
{
    static void Main()
    {
        // Example input
        double x1 = double.Parse(Console.ReadLine());
        double y1 = double.Parse(Console.ReadLine());
        double x2 = double.Parse(Console.ReadLine());
        double y2 = double.Parse(Console.ReadLine());

        double x3 = double.Parse(Console.ReadLine());
        double y3 = double.Parse(Console.ReadLine());
        double x4 = double.Parse(Console.ReadLine());
        double y4 = double.Parse(Console.ReadLine());

        double line1Length = GetDistance(x1, y1, x2, y2);
        double line2Length = GetDistance(x3, y3, x4, y4);

        if (line1Length >= line2Length)
        {
            PrintLineClosestToCenter(x1, y1, x2, y2);
        }
        else
        {
            PrintLineClosestToCenter(x3, y3, x4, y4);
        }
    }

    static double GetDistance(double x1, double y1, double x2, double y2)
    {
        return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
    }

    static double DistanceToOrigin(double x, double y)
    {
        return Math.Sqrt(x * x + y * y);
    }

    static void PrintLineClosestToCenter(double x1, double y1, double x2, double y2)
    {
        double dist1 = DistanceToOrigin(x1, y1);
        double dist2 = DistanceToOrigin(x2, y2);

        if (dist1 <= dist2)
        {
            Console.WriteLine($"({x1}, {y1})({x2}, {y2})");
        }
        else
        {
            Console.WriteLine($"({x2}, {y2})({x1}, {y1})");
        }
    }
}

 */