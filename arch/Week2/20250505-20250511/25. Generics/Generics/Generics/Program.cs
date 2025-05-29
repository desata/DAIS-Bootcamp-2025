namespace Generics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<int> items = new List<int>();

            for (int i = 0; i < n; i++)
            {
                items.Add(int.Parse(Console.ReadLine()));
            }

            string[] indices = Console.ReadLine().Split();
            int index1 = int.Parse(indices[0]);
            int index2 = int.Parse(indices[1]);

            SwapElements(items, index1, index2);

            foreach (var item in items)
            {
                Console.WriteLine($"{item.GetType()}: {item}");
            }
        }

        public static void SwapElements<T>(List<T> list, int index1, int index2)
        {
            T temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }
    }
    
}
