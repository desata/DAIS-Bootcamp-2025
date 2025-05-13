namespace _03._Shopping_Spree
{
    public class Person
    {
        private string name;
        private decimal money;

        public Person(string name, decimal money)
        {
            Name = name;
            Money = money;
            Bag = new List<Product>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty");
                name = value;
            }
        }

        public decimal Money
        {
            get => money;
            private set
            {
                if (value < 0)
                    throw new ArgumentException("Money cannot be negative");
                money = value;
            }
        }

        public List<Product> Bag { get; }

        public bool BuyProduct(Product product)
        {
            if (Money >= product.Cost)
            {
                Money -= product.Cost;
                Bag.Add(product);
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            string products = Bag.Any() ? string.Join(", ", Bag) : "Nothing bought";
            return $"{Name} - {products}";
        }
    }
}
