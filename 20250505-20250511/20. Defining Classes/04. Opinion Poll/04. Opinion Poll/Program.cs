using System;
using System.Collections.Generic;
using System.Linq;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}

public class StartUp
{
    static void Main()
    {
        // Read the number of people to be processed
        int N = int.Parse(Console.ReadLine());

        // Initialize a list to store people
        List<Person> people = new List<Person>();

        // Read N lines of input
        for (int i = 0; i < N; i++)
        {
            string[] input = Console.ReadLine().Split();
            string name = input[0];
            int age = int.Parse(input[1]);
            people.Add(new Person(name, age));
        }

        // Filter people whose age is more than 30 and sort by name alphabetically
        var result = people
            .Where(p => p.Age > 30)
            .OrderBy(p => p.Name)
            .ToList();

        // Print the results
        foreach (var person in result)
        {
            Console.WriteLine($"{person.Name} - {person.Age}");
        }
    }
}