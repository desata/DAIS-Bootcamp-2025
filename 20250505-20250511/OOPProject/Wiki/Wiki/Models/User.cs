using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Wiki.Models
{
    public enum Role { Admin, Editor, Viewer }
    public enum AccessLevel { Public, Internal, Restricted }
    public class User
    {
        public string UserId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public Role Role { get; set; }
        public AccessLevel AccessLevel { get; set; }

    }
}