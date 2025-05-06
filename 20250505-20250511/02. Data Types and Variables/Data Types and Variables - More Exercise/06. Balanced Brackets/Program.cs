namespace _06._Balanced_Brackets
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int lines = int.Parse(Console.ReadLine());
            string result = "UNBALANCED";
            Stack<string> brackets = new Stack<string>();


            for (int i = 0; i < lines; i++)
            {
                string input = Console.ReadLine();

                if (input == "(")
                {

                        brackets.Push(input);
                    
                }
                else if (input == ")")
                {
                    if (brackets.Count == 1)
                    {
                        brackets.Pop();
                    }
                }

            }

            if (brackets.Count == 0)
            {
                result = "BALANCED";
            }

            Console.WriteLine(result);

        }
    }
}
