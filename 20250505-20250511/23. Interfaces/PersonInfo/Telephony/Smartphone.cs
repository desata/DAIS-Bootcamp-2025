using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telephony
{
    class Smartphone : ICallable, IBrawsable
    {
        public void Call(string phoneNumber)
        {
            if (phoneNumber.All(char.IsDigit) && phoneNumber.Length == 10)
            {
                Console.WriteLine($"Calling... {phoneNumber}");
            }
            else
            {
                Console.WriteLine("Invalid number!");
            }
        }

        public void Browse(string website)
        {
            if (website.Any(char.IsDigit))
            {
                Console.WriteLine("Invalid URL!");
            }
            else
            {
                Console.WriteLine($"Browsing: {website}!");
            }
        }
    }
}
