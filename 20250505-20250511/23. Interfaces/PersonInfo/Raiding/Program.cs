namespace Raiding
{
    public class Program
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine()); // Number of valid heroes to create
            List<BaseHero> raidGroup = new List<BaseHero>();

            // Create heroes until we have N valid heroes
            while (raidGroup.Count < n)
            {
                string heroName = Console.ReadLine();
                string heroType = Console.ReadLine();

                BaseHero hero = HeroFactory.CreateHero(heroName, heroType);
                if (hero != null)
                {
                    raidGroup.Add(hero);
                }
                else
                {
                    Console.WriteLine("Invalid hero!");
                }
            }

            // Boss power
            int bossPower = int.Parse(Console.ReadLine());

            // Calculate total power of the raid group
            int totalPower = 0;
            foreach (var hero in raidGroup)
            {
                Console.WriteLine(hero.CastAbility());
                totalPower += hero.Power;
            }

            // Check if the total power is enough to defeat the boss
            if (totalPower >= bossPower)
            {
                Console.WriteLine("Victory!");
            }
            else
            {
                Console.WriteLine("Defeat...");
            }
        }
    }



    public abstract class BaseHero
    {
        public string Name { get; set; }
        public int Power { get; set; }

        public BaseHero(string name, int power)
        {
            Name = name;
            Power = power;
        }

        public abstract string CastAbility();
    }

    public class Druid : BaseHero
    {
        public Druid(string name) : base(name, 80) { }

        public override string CastAbility()
        {
            return $"Druid - {Name} healed for {Power}";
        }
    }

    public class Paladin : BaseHero
    {
        public Paladin(string name) : base(name, 100) { }

        public override string CastAbility()
        {
            return $"Paladin - {Name} healed for {Power}";
        }
    }

    public class Rogue : BaseHero
    {
        public Rogue(string name) : base(name, 80) { }

        public override string CastAbility()
        {
            return $"Rogue - {Name} hit for {Power} damage";
        }
    }

    public class Warrior : BaseHero
    {
        public Warrior(string name) : base(name, 100) { }

        public override string CastAbility()
        {
            return $"Warrior - {Name} hit for {Power} damage";
        }
    }

    public class HeroFactory
    {
        public static BaseHero CreateHero(string heroName, string heroType)
        {
            switch (heroType.ToLower())
            {
                case "druid":
                    return new Druid(heroName);
                case "paladin":
                    return new Paladin(heroName);
                case "rogue":
                    return new Rogue(heroName);
                case "warrior":
                    return new Warrior(heroName);
                default:
                    return null; // Invalid hero type
            }
        }
    }
}

