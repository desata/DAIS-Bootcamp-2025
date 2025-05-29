namespace _4._Matching_Brackets
{
    class Program
    {
        static void Main()
        {
            string input = Console.ReadLine();
            Stack<int> openBrackets = new Stack<int>();

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    openBrackets.Push(i);
                }
                else if (input[i] == ')')
                {
                    int startIndex = openBrackets.Pop();
                    string subExpression = input.Substring(startIndex, i - startIndex + 1);
                    Console.WriteLine(subExpression);
                }
            }
        }
    }

}
