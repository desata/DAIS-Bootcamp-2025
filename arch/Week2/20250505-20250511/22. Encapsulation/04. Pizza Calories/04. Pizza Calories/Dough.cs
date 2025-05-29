using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCalories
{
    public class Dough
    {
        private string flourType;
        private string bakingTechnique;
        private double weight;

        private readonly Dictionary<string, double> flourModifiers = new()
    {
        { "White", 1.5 },
        { "Wholegrain", 1.0 }
    };

        private readonly Dictionary<string, double> bakingModifiers = new()
    {
        { "Crispy", 0.9 },
        { "Chewy", 1.1 },
        { "Homemade", 1.0 }
    };

        public Dough(string flourType, string bakingTechnique, double weight)
        {
            FlourType = flourType;
            BakingTechnique = bakingTechnique;
            Weight = weight;
        }

        public string FlourType
        {
            get => flourType;
            private set
            {
                if (!flourModifiers.ContainsKey(value))
                    throw new ArgumentException("Invalid type of dough.");
                flourType = value;
            }
        }

        public string BakingTechnique
        {
            get => bakingTechnique;
            private set
            {
                if (!bakingModifiers.ContainsKey(value))
                    throw new ArgumentException("Invalid type of dough.");
                bakingTechnique = value;
            }
        }

        public double Weight
        {
            get => weight;
            private set
            {
                if (value < 1 || value > 200)
                    throw new ArgumentException("Dough weight should be in the range [1..200].");
                weight = value;
            }
        }

        public double GetCalories()
        {
            double baseCalories = 2;
            return baseCalories * weight * flourModifiers[FlourType] * bakingModifiers[BakingTechnique];
        }
    }
}