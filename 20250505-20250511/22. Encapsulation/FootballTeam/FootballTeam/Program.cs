using System.Numerics;

namespace FootballTeam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Team> teams = new Dictionary<string, Team>();

            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                string[] parts = input.Split(';');
                string command = parts[0];
                string teamName = parts[1];

                try
                {
                    if (command == "Team")
                    {
                        teams[teamName] = new Team(teamName);
                    }
                    else if (command == "Add")
                    {
                        if (!teams.ContainsKey(teamName))
                        {
                            Console.WriteLine($"Team {teamName} does not exist.");
                            continue;
                        }

                        string playerName = parts[2];
                        int endurance = int.Parse(parts[3]);
                        int sprint = int.Parse(parts[4]);
                        int dribble = int.Parse(parts[5]);
                        int passing = int.Parse(parts[6]);
                        int shooting = int.Parse(parts[7]);

                        var player = new Player(playerName, endurance, sprint, dribble, passing, shooting);
                        teams[teamName].AddPlayer(player);
                    }
                    else if (command == "Remove")
                    {
                        if (!teams.ContainsKey(teamName))
                        {
                            Console.WriteLine($"Team {teamName} does not exist.");
                            continue;
                        }

                        string playerName = parts[2];
                        teams[teamName].RemovePlayer(playerName);
                    }
                    else if (command == "Rating")
                    {
                        if (!teams.ContainsKey(teamName))
                        {
                            Console.WriteLine($"Team {teamName} does not exist.");
                            continue;
                        }

                        Console.WriteLine($"{teamName} - {teams[teamName].Rating}");
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}