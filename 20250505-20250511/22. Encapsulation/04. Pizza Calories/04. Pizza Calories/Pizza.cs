using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCalories
{
    public class Pizza
    {
        private string name;
        private Dough dough;
        private readonly List<Topping> toppings;

        public Pizza(string name)
        {
            Name = name;
            toppings = new List<Topping>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length > 15)
                    throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
                name = value;
            }
        }

        public int NumberOfToppings => toppings.Count;

        public void SetDough(Dough dough)
        {
            this.dough = dough;
        }

        public void AddTopping(Topping topping)
        {
            if (toppings.Count >= 10)
                throw new ArgumentException("Number of toppings should be in range [0..10].");
            toppings.Add(topping);
        }

        public double GetTotalCalories()
        {
            double doughCalories = dough?.GetCalories() ?? 0;
            double toppingsCalories = toppings.Sum(t => t.GetCalories());
            return doughCalories + toppingsCalories;
        }

        public override string ToString()
        {
            return $"{Name} - {GetTotalCalories():F2} Calories.";
        }
    }
}
