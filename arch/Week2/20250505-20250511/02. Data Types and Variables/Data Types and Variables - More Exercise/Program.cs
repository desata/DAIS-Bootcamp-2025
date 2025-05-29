namespace Data_Types_and_Variables___More_Exercise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            
            string result = string.Empty;

            while (input != "END")
            {
                if (int.TryParse(input, out int integer))
                {
                    result = "integer";
                }
                else if (double.TryParse(input, out double floating))
                {
                    result = "floating point";
                }
                else if (bool.TryParse(input, out bool boolean))
                {
                    result = "boolean";
                }
                else if (char.TryParse(input, out char character))
                {
                    result = "character";
                }
                else
                {
                    result = "string";
                }
                Console.WriteLine($"{input} is {result} type");
                input = Console.ReadLine();
            }

            
        }
    }
}
