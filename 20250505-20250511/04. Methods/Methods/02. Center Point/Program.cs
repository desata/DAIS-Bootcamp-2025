namespace _02._Center_Point
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double x1 = int.Parse(Console.ReadLine());
            double y1 = int.Parse(Console.ReadLine());
            double x2 = int.Parse(Console.ReadLine());
            double y2 = int.Parse(Console.ReadLine());

            double diagonalOne = 0;
            double diagonalTwo = 0;

            if ((x1 == 0 && y1 != 0) || (y1 == 0 && x1 != 0))
            {
                diagonalOne = Math.Abs(x1 - y1);
                diagonalTwo = CalculateDiagonal(x2, y2);
            }
            else if ((x2 == 0 && y2 != 0) || (y2 == 0 && x2 != 0))
            {
                diagonalTwo = Math.Abs(x2 - y2);
                diagonalOne = CalculateDiagonal(x1, y1);
            }
            else
            {
                diagonalOne = CalculateDiagonal(x1, y1);
                diagonalTwo = CalculateDiagonal(x2, y2);
            }

            if (diagonalOne - diagonalTwo <= 0)
            {
                Console.WriteLine($"({x1}, {y1})");
            }
            else
            {
                Console.WriteLine($"({x2}, {y2})");
            }
       
       }

        private static double CalculateDiagonal(double x, double y)
        {
            return (Math.Abs(x * x) + Math.Abs(y * y));
        }
    }
}
