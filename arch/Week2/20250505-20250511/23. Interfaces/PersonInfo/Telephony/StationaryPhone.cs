using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telephony
{
    class StationaryPhone : ICallable
    {
        public void Call(string phoneNumber)
        {
            if (phoneNumber.All(char.IsDigit) && phoneNumber.Length == 7)
            {
                Console.WriteLine($"Dialing... {phoneNumber}");
            }
            else
            {
                Console.WriteLine("Invalid number!");
            }
        }
    }
}
