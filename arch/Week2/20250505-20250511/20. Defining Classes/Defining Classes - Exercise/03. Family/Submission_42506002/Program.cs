namespace DefiningClasses
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Family family = new Family();

            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split();
                Person person = new Person(input[0], int.Parse(input[1]));
                family.AddMember(person);

            }

            Console.WriteLine(family.GetOldestMember().Name +" "+ family.GetOldestMember().Age);
        }
        
    }
    public class Person
    {
        private string name;
        private int age;

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public string Name { get; set; }

        public int Age { get; set; }
    }

    public class Family
    {
        private List<Person> members;
        public Family()
        {
            members = new List<Person>();
        }
        public void AddMember(Person person)
        {
            members.Add(person);
        }
        public Person GetOldestMember()
        {
            return members.OrderByDescending(x => x.Age).FirstOrDefault();
        }
    }
}}
