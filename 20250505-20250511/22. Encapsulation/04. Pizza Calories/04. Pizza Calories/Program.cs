namespace  PizzaCalories
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            try
            {
                string[] pizzaInput = Console.ReadLine().Split(); // Pizza Meatless
                Pizza pizza = new Pizza(pizzaInput[1]);

                string[] doughInput = Console.ReadLine().Split(); // Dough Wholegrain Crispy 100
                Dough dough = new Dough(doughInput[1], doughInput[2], double.Parse(doughInput[3]));
                pizza.SetDough(dough);

                string input;
                while ((input = Console.ReadLine()) != "END")
                {
                    string[] parts = input.Split(); // Topping Veggies 50
                    if (parts[0] == "Topping")
                    {
                        Topping topping = new Topping(parts[1], double.Parse(parts[2]));
                        pizza.AddTopping(topping);
                    }
                }

                Console.WriteLine(pizza);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
