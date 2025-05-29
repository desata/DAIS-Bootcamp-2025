namespace PersonInfo
{
    internal class Citizen : IBirthable, IIdentifiable
    {
        private string? name;
        private int age;

        public Citizen(string? name, int age, string? id, string? birthdate)
        {
            this.name = name;
            this.age = age;
            Id = id;
            Birthdate = birthdate;
        }

        public string Id { get ; set ; }
        public string Birthdate { get; set; }
    }
}