namespace _06.StrongNumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int numberInput = int.Parse(Console.ReadLine());
            int number = numberInput;
            string result = "";

            int numberFactorial = 0;

            for (int i = 0; i <= numberInput.ToString().Length; i++)
            {
                int sum = 1;

                if (number > 0)
                {
                    int digit = number % 10;
                    number /= 10;

                    for (int j = 1; j <= digit; j++)
                    {
                        sum *= j;
                    }

                    numberFactorial += sum;
                }

            }

            if (numberInput == numberFactorial)
            {
                result = "yes";
            }
            else
            {
                result = "no";
            }


            Console.WriteLine(result);

        }
    }
}