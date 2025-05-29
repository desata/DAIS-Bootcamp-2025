using System.Text;

namespace _07._The_V_Logger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split().ToArray();
            Dictionary<string, HashSet<string>> vlogger = new Dictionary<string, HashSet<string>>();

            while (input[0] != "Statistics")
            {
                if (input[1] == "joined")
                {
                    if (!vlogger.ContainsKey(input[0]))
                    {
                        
                        vlogger.Add(input[0], new HashSet<string>());
                    }

                }
                if (input[1] == "followed")
                {
                    if (vlogger.ContainsKey(input[2]) && vlogger.ContainsKey(input[0]))
                    {
                        if (input[0] != input[2])
                        {
                            vlogger[input[2]].Add(input[0]);
                        }

                    }

                }
                input = Console.ReadLine().Split().ToArray();
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"The V-Logger has a total of {vlogger.Count} vloggers in its logs.");
            var mostFamousVlogger = vlogger
                .OrderByDescending(x => x.Value.Count)
                .ThenBy(x => vlogger.Count(kv => kv.Value.Contains(x.Key)))
                .ThenBy(x => x.Key)
                .ToDictionary(k => k.Key, v => v.Value);
            int index = 1;
            foreach (var item in mostFamousVlogger)
            {
                string name = item.Key;
                sb.AppendLine($"{index}. {item.Key} : {item.Value.Count} followers, {vlogger.Count(kv => kv.Value.Contains(name))} following");
                if (index == 1)
                {
                    var followers = item.Value
                        .OrderBy(x => x)
                        .ToList();
                    sb.AppendLine($"*  {string.Join(Environment.NewLine + "*  ", followers)}");
                }
                index++;
            }

            Console.WriteLine(sb.ToString().TrimEnd());

        }
    }
}
