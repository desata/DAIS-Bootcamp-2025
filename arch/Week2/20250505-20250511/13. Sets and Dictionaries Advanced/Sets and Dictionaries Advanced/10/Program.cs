using System.Text;

namespace _10._ForceBook
{
    class Program
    {
        static void Main()
        {
            var userToSide = new Dictionary<string, string>();
            var input = string.Empty;

            while ((input = Console.ReadLine()) != "Lumpawaroo")
            {
                if (input.Contains(" | "))
                {
                    string[] tokens = input.Split(" | ");
                    string side = tokens[0];
                    string user = tokens[1];

                    if (!userToSide.ContainsKey(user))
                    {
                        userToSide[user] = side;
                    }
                }
                else if (input.Contains(" -> "))
                {
                    string[] tokens = input.Split(" -> ");
                    string user = tokens[0];
                    string side = tokens[1];

                    userToSide[user] = side;
                    Console.WriteLine($"{user} joins the {side} side!");
                }
            }

            var sideToUsers = new Dictionary<string, List<string>>();
            foreach (var kvp in userToSide)
            {
                string user = kvp.Key;
                string side = kvp.Value;

                if (!sideToUsers.ContainsKey(side))
                {
                    sideToUsers[side] = new List<string>();
                }

                sideToUsers[side].Add(user);
            }


            StringBuilder sb = new StringBuilder();

            foreach (var side in sideToUsers
                .Where(s => s.Value.Count > 0)
                .OrderByDescending(s => s.Value.Count)
                .ThenBy(s => s.Key))
            {
                sb.AppendLine($"Side: {side.Key}, Members: {side.Value.Count}");
                foreach (var user in side.Value.OrderBy(u => u))
                {
                    sb.AppendLine($"! {user}");
                }
            }

            Console.Write(sb.ToString());
        }
    }
}