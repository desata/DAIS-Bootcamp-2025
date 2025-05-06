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
            return (Math.Abs(x * x) + Math.Abs(y * y));
        }
    
    }
}
