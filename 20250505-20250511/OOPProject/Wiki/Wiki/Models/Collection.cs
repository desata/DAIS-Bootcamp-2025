
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiki.Models
{
    public class Collection
    {
        public string Name { get; set; }
        public User CreatedBy { get; set; }
        

        //public Collection(string name, User createdBy)
        //{
        //    Name = name;
        //    CreatedBy = createdBy;
        //}

    }
}