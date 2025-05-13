using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCalories
{
    public class Topping
    {
        private string type;
        private double weight;

        private readonly Dictionary<string, double> modifiers = new()
    {
        { "Meat", 1.2 },
        { "Veggies", 0.8 },
        { "Cheese", 1.1 },
        { "Sauce", 0.9 }
    };

        public Topping(string type, double weight)
        {
            Type = type;
            Weight = weight;
        }

        public string Type
        {
            get => type;
            private set
            {
                if (!modifiers.ContainsKey(value))
                    throw new ArgumentException($"Cannot place {value} on top of your pizza.");
                type = value;
            }
        }

        public double Weight
        {
            get => weight;
            private set
            {
                if (value < 1 || value > 50)
                    throw new ArgumentException($"{Type} weight should be in the range [1..50].");
                weight = value;
            }
        }

        public double GetCalories()
        {
            return 2 * Weight * modifiers[Type];
        }
    }
}
