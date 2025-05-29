namespace _11._Key_Revolver
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int bulletPrice = int.Parse(Console.ReadLine());
            int gunBarrelSize = int.Parse(Console.ReadLine());
            int[] bulletsInput = Console.ReadLine().Split().Select(int.Parse).ToArray();
            Stack<int> bullets = new Stack<int>(bulletsInput);
            int[] locksInput = Console.ReadLine().Split().Select(int.Parse).ToArray();
            Queue<int> locks = new Queue<int>(locksInput);
            int valueOfIntelligence = int.Parse(Console.ReadLine());
            int shoots = 0;
            int bulletsShooted = 0;

            while (bullets.Count > 0 && locks.Count > 0)
            {


                int currentBullet = bullets.Pop();
                int currentLock = locks.Peek();
                bulletsShooted++;
                if (currentBullet <= currentLock)
                {
                    locks.Dequeue();
                    Console.WriteLine("Bang!");
                    shoots++;
                }
                else
                {
                    Console.WriteLine("Ping!");
                    shoots++;
                }
                if (bullets.Count > 0 && gunBarrelSize == shoots)
                {
                    Console.WriteLine("Reloading!");
                    shoots = 0;
                }
            }
            if (bullets.Count == 0 && locks.Count > 0)
            {
                Console.WriteLine($"Couldn't get through. Locks left: {locks.Count}");
            }
            if (bullets.Count >= 0 && locks.Count == 0)
            {
                int profit = valueOfIntelligence - (bulletsShooted * bulletPrice);
                Console.WriteLine($"{bullets.Count} bullets left. Earned ${profit}");
            }

        }
    }
}
