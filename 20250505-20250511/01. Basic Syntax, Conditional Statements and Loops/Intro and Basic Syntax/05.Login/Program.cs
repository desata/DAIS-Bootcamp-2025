namespace _05.Login
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string username = Console.ReadLine();
            

            for (int i = 0; i <=3; i++)
            {
                string password = Console.ReadLine();
                string revercedPassword = new string(password.Reverse().ToArray());

                if (username == revercedPassword)
                {
                    Console.WriteLine($"User {username} logged in.");
                    break;
                }

                if (i < 3)
                {
                    Console.WriteLine("Incorrect password. Try again.");
                }
                else
                {
                    Console.WriteLine($"User {username} blocked!");
                }                    
            }          

        }
    }
}
