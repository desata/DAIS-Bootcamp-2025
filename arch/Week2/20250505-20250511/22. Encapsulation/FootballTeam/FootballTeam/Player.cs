namespace FootballTeam
{
    public class Player
    {
        private string name;
        private int endurance;
        private int sprint;
        private int dribble;
        private int passing;
        private int shooting;

        public Player(string name, int endurance, int sprint, int dribble, int passing, int shooting)
        {
            Name = name;
            Endurance = endurance;
            Sprint = sprint;
            Dribble = dribble;
            Passing = passing;
            Shooting = shooting;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("A name should not be empty.");
                name = value;
            }
        }

        private int Endurance
        {
            get => endurance;
            set
            {
                ValidateStat(value, nameof(Endurance));
                endurance = value;
            }
        }

        private int Sprint
        {
            get => sprint;
            set
            {
                ValidateStat(value, nameof(Sprint));
                sprint = value;
            }
        }

        private int Dribble
        {
            get => dribble;
            set
            {
                ValidateStat(value, nameof(Dribble));
                dribble = value;
            }
        }

        private int Passing
        {
            get => passing;
            set
            {
                ValidateStat(value, nameof(Passing));
                passing = value;
            }
        }

        private int Shooting
        {
            get => shooting;
            set
            {
                ValidateStat(value, nameof(Shooting));
                shooting = value;
            }
        }

        public double SkillLevel =>
            (Endurance + Sprint + Dribble + Passing + Shooting) / 5.0;

        private void ValidateStat(int stat, string statName)
        {
            if (stat < 0 || stat > 100)
                throw new ArgumentException($"{statName} should be between 0 and 100.");
        }
    }
}