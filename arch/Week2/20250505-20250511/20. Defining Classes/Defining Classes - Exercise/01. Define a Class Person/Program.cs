namespace DefiningClasses
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Person person = new Person("Peter", 20);
            Person person1 = new Person("George", 18);
            Person person2 = new Person("Jose", 43);
        }
        //Define a class Person with private fields for name and age and public properties Name and Age.
       
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
}
