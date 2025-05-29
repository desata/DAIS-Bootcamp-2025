using System.Numerics;

namespace _04._Centuries_to_Minutes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Create a program to enter an integer number of centuries and convert it to years, days, hours and minutes.

            int centuries = int.Parse(Console.ReadLine());

            int years = centuries * 100;
            int days = (int)(years * 365.2422);
            int hours = days * 24;
            int minutes = hours * 60;            

            Console.WriteLine($"{centuries} centuries = {centuries*100} years = {days} days = {hours} hours = {minutes} minutes");
        }
    }
}
