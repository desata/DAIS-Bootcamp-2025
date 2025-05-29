using Microsoft.AspNetCore.Identity;

public class Employee : IdentityUser
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }

