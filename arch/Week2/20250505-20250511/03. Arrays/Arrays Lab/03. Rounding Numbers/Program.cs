namespace _03._Rounding_Numbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Read an array of real numbers (space separated), round them in "away from 0" style and print the output as in the examples

            //Input: 1,4 2,5 3,6 4,7 5,8
            //0,9 1,5 2,4 2,5 3,14

            //Output: 1 3 4 5 6

            double[] numbers = Console.ReadLine()
                .Split(' ')
                .Select(double.Parse)
                .ToArray();

            for (int i = 0; i < numbers.Length; i++)
            {
                Console.WriteLine($"{numbers[i]} => {(int)Math.Round(numbers[i], MidpointRounding.AwayFromZero)}");
            }
        }
    }
}
