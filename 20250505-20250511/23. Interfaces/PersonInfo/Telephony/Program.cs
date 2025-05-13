namespace Telephony
{
    internal class Program
    {
        static void Main(string[] args)
        {
 
            string[] phoneNumbers = Console.ReadLine().Split();
            string[] websites = Console.ReadLine().Split();


            ICallable smartphone = new Smartphone();
            ICallable stationaryPhone = new StationaryPhone();
            IBrawsable browsableSmartphone = new Smartphone(); 


            foreach (var number in phoneNumbers)
            {
                if (number.Length == 10) 
                {
                    smartphone.Call(number);
                }
                else if (number.Length == 7) 
                {
                    stationaryPhone.Call(number);
                }
                else
                {
                    Console.WriteLine("Invalid number!");
                }
            }


            foreach (var website in websites)
            {
                browsableSmartphone.Browse(website);
            }
        }
    }
}        