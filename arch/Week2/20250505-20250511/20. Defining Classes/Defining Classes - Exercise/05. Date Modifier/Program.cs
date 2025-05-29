using System;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string firstDate = Console.ReadLine();
            string secondDate = Console.ReadLine();

            DateModifier dateModifier = new DateModifier(firstDate, secondDate);

            int daysDifference = dateModifier.GetDaysDifference();
            Console.WriteLine(daysDifference);

        }
    }

    public class DateModifier
    {
        private string firstDate;
        private string secondDate;
        public DateModifier(string firstDate, string secondDate)
        {
            FirstDate = firstDate;
            SecondDate = secondDate;
        }
        public string FirstDate { get; set; }
        public string SecondDate { get; set; }

        public int GetDaysDifference()
        {
            DateTime firstDateTime = DateTime.Parse(FirstDate);
            DateTime secondDateTime = DateTime.Parse(SecondDate);
            TimeSpan difference = firstDateTime - secondDateTime;
            return Math.Abs(difference.Days);
        }
    }
}