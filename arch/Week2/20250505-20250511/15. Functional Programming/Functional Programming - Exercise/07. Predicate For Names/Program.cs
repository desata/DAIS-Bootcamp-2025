namespace _07._Predicate_For_Names
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            string[] names = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .ToArray();
                       

            Predicate<string> isShorter = name => name.Length <= n;

            List<string> result = new List<string>();

            foreach (var name in names)
            {
                if (isShorter(name))
                {
                    result.Add(name);
                }
            }

            Console.WriteLine(string.Join(Environment.NewLine, result));
        }
    }
}
