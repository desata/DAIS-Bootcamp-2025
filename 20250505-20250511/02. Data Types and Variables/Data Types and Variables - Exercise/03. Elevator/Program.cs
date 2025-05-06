namespace _03._Elevator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int numberOfPeople = int.Parse(Console.ReadLine());
            int elevatorCapacity = int.Parse(Console.ReadLine());

            int courses = 0;

            while (numberOfPeople > 0)
            {
                numberOfPeople -= elevatorCapacity;
                courses++;
            }

            Console.WriteLine(courses);
        }
    }
}
