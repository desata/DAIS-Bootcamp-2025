using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiki.Models
{
    public class ChangeRegister
    {
        public DateTime Timestamp { get; set; }
        public string UserId { get; set; }
        public string? ChangeDescription { get; set; }


    }
}
