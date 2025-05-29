namespace _12._Cups_and_Bottles
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] cupsInput = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int[] bottlesInput = Console.ReadLine().Split().Select(int.Parse).ToArray();

            Queue<int> cups = new Queue<int>(cupsInput);
            Stack<int> bottles = new Stack<int>(bottlesInput);

            int wastedWater = 0;

            while (cups.Count > 0 && bottles.Count > 0)
            {
                int currentCup = cups.Peek();
                int currentBottle = bottles.Pop();
                if (currentBottle >= currentCup)
                {
                    wastedWater += currentBottle - currentCup;
                    cups.Dequeue();
                }
                else
                {
                    currentCup -= currentBottle;
                    cups.Dequeue();
                    cups.Enqueue(currentCup);
                }
            }

            if (cups.Count > 0)
            {
                foreach (var cup in cups)
                {
                    Console.WriteLine($"Cups: {cup}");
                }
            }
            else if (bottles.Count > 0)
            {
                foreach (var bottle in bottles)
                {
                    Console.WriteLine($"Bottles: {bottle}");
                }
            }

            Console.WriteLine($"Wasted litters of water: {wastedWater}");

        }
    }
}
