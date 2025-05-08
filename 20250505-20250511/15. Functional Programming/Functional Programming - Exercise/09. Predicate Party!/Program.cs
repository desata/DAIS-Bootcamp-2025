using System;
using System.Collections.Generic;
using System.Linq;

public class PartyReservation
{
    public static void Main()
    {
        List<string> guests = Console.ReadLine()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .ToList();

        string command;

        while ((command = Console.ReadLine()) != "Party!")
        {
            string[] parts = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string action = parts[0];
            string criteria = parts[1];
            string parameter = parts[2];

            Predicate<string> matchPredicate = GetPredicate(criteria, parameter);

            if (action == "Remove")
            {
                guests.RemoveAll(matchPredicate);
            }
            else if (action == "Double")
            {
                List<string> matches = guests.Where(g => matchPredicate(g)).ToList();
                foreach (var name in matches)
                {
                    int index = guests.IndexOf(name);
                    guests.Insert(index + 1, name); // Insert duplicate right after
                }
            }
        }

        if (guests.Any())
        {
            Console.WriteLine($"{string.Join(", ", guests)} are going to the party!");
        }
        else
        {
            Console.WriteLine("Nobody is going to the party!");
        }
    }

    static Predicate<string> GetPredicate(string criteria, string parameter)
    {
        return criteria switch
        {
            "StartsWith" => name => name.StartsWith(parameter),
            "EndsWith" => name => name.EndsWith(parameter),
            "Length" => name => name.Length == int.Parse(parameter),

        };
    }
}
