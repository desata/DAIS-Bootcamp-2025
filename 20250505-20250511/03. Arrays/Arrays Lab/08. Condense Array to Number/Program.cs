namespace _08._Condense_Array_to_Number
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();
            int result = 0;

            if (numbers.Length == 1)
            {
                result = numbers[0];

            }

            while (numbers.Length > 1)
            {
                int[] condensed = new int[numbers.Length - 1];
                for (int i = 0; i < numbers.Length - 1; i++)
                {
                    condensed[i] = numbers[i] + numbers[i + 1];
                }
                numbers = condensed;


                if (numbers.Length > 1)
                {
                    for (int i = 0; i < numbers.Length - 1; i++)
                    {
                        result += numbers[i] + numbers[i + 1];
                    }
                }
                else
                {
                    result = numbers[0];
                }

            }

            Console.WriteLine(result);


        }
    }
}
