namespace _09._SoftUni_Exam_Results
{
    class Program
    {
        static void Main()
        {
            Dictionary<string, int> userPoints = new Dictionary<string, int>();
            Dictionary<string, int> languageSubmissions = new Dictionary<string, int>();

            string input;
            while ((input = Console.ReadLine()) != "exam finished")
            {
                string[] parts = input.Split('-');

                if (parts[1] == "banned")
                {
                    string username = parts[0];
                    userPoints.Remove(username); // Remove user from results
                }
                else
                {
                    string username = parts[0];
                    string language = parts[1];
                    int points = int.Parse(parts[2]);

                    // Count language submission
                    if (!languageSubmissions.ContainsKey(language))
                        languageSubmissions[language] = 0;
                    languageSubmissions[language]++;

                    // Update user points (keep highest)
                    if (!userPoints.ContainsKey(username))
                        userPoints[username] = points;
                    else if (points > userPoints[username])
                        userPoints[username] = points;
                }
            }

            // Print Results
            Console.WriteLine("Results:");
            foreach (var user in userPoints
                .OrderByDescending(u => u.Value)
                .ThenBy(u => u.Key))
            {
                Console.WriteLine($"{user.Key} | {user.Value}");
            }

            // Print Submissions
            Console.WriteLine("Submissions:");
            foreach (var lang in languageSubmissions
                .OrderByDescending(l => l.Value)
                .ThenBy(l => l.Key))
            {
                Console.WriteLine($"{lang.Key} - {lang.Value}");
            }
        }
    }
}