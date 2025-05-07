using System.Text;

namespace _08._Ranking
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split(':').ToArray();
            Dictionary<string, string> contest = new Dictionary<string, string>();
            Dictionary<string, Dictionary<string, int>> candidates = new Dictionary<string, Dictionary<string, int>>();

            while (input[0] != "end of contests")
            {
                string contestName = input[0];
                string password = input[1];
                if (!contest.ContainsKey(contestName))
                {
                    contest.Add(contestName, password);
                }
                input = Console.ReadLine().Split(':').ToArray();
            }
            string[] input2 = Console.ReadLine().Split("=>").ToArray();
            while (input2[0] != "end of submissions")
            {
                string contestName = input2[0];
                string password = input2[1];
                string username = input2[2];
                int points = int.Parse(input2[3]);
                if (contest.ContainsKey(contestName) && contest[contestName] == password)
                {
                    if (!candidates.ContainsKey(username))
                    {
                        candidates.Add(username, new Dictionary<string, int>());
                    }
                    if (!candidates[username].ContainsKey(contestName))
                    {
                        candidates[username].Add(contestName, 0);
                    }
                    if (candidates[username][contestName] < points)
                    {
                        candidates[username][contestName] = points;
                    }
                }
                input2 = Console.ReadLine().Split("=>").ToArray();
            }



            StringBuilder sb = new StringBuilder();

            var bestCandidate = candidates.OrderByDescending(x => x.Value.Values.Sum()).FirstOrDefault();

            string user = bestCandidate.Key;
            int totalPoints = bestCandidate.Value.Values.Sum();

            //int index = 1;
            sb.AppendLine($"Best candidate is {user} with total {totalPoints} points.");

            sb.AppendLine("Ranking: ");
            foreach (var item in candidates.OrderBy(x => x.Key))
            {
                string name = item.Key;
                sb.AppendLine(name);
                foreach (var kvp in item.Value.OrderByDescending(x => x.Value))
                {
                    sb.AppendLine($"#  {kvp.Key} -> {kvp.Value}");
                }
            }

            Console.WriteLine(sb.ToString().TrimEnd());


        }
    }
}
