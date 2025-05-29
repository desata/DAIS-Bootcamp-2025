namespace _12._Refactor_Special_Numbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int number = int.Parse(Console.ReadLine());
            int sum = 0;
            int currentNumber = 0;
            bool isSpecialNum = false;
            for (int ch = 1; ch <= number; ch++)
            {
                currentNumber = ch;
                while (ch > 0)
                {
                    sum += ch % 10;
                    ch = ch / 10;
                }
                isSpecialNum = (sum == 5) || (sum == 7) || (sum == 11);
                Console.WriteLine("{0} -> {1}", currentNumber, isSpecialNum);
                sum = 0;
                ch = currentNumber;
            }

        }
    }
}
