using System;


namespace _03._Shopping_Spree
{
    public class Startup
    {
        public static void Main()
        {
            try
            {
                Dictionary<string, Person> people = Console.ReadLine()
                    .Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .Select(p =>
                    {
                        var tokens = p.Split('=', StringSplitOptions.RemoveEmptyEntries);
                        return new Person(tokens[0], decimal.Parse(tokens[1]));
                    })
                    .ToDictionary(p => p.Name);

                Dictionary<string, Product> products = Console.ReadLine()
                    .Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .Select(p =>
                    {
                        var tokens = p.Split('=', StringSplitOptions.RemoveEmptyEntries);
                        return new Product(tokens[0], decimal.Parse(tokens[1]));
                    })
                    .ToDictionary(p => p.Name);

                string command;
                while ((command = Console.ReadLine()) != "END")
                {
                    string[] parts = command.Split();
                    string personName = parts[0];
                    string productName = parts[1];

                    var person = people[personName];
                    var product = products[productName];

                    if (person.BuyProduct(product))
                        Console.WriteLine($"{personName} bought {productName}");
                    else
                        Console.WriteLine($"{personName} can't afford {productName}");
                }

                foreach (var person in people.Values)
                {
                    Console.WriteLine(person);
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
