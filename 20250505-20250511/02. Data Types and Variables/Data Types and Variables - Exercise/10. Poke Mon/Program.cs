namespace _10._Poke_Mon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int pokePower = int.Parse(Console.ReadLine());
            int originalPokePower = pokePower;
            int distance = int.Parse(Console.ReadLine());
            int exhaustionFactor = int.Parse(Console.ReadLine());
            int pokeCount = 0;

            while (pokePower >= distance)
            {
                pokePower -= distance;
                pokeCount++;

                if (pokePower == originalPokePower * 0.5 && exhaustionFactor > 0)
                {
                    pokePower /= exhaustionFactor;

                }
            }

            Console.WriteLine(pokePower);
            Console.WriteLine(pokeCount);
        }
    }
}
