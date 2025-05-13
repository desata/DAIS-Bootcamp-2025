using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiki.Models
{

    public class Document
    {
        public string Id { get; set; } //TODO should be a static property       
        public string Title { get; set; } 
        public string Content { get; set; } //TODO should be in a different class, vith versions
        public List<string> Tags { get; set; } 
        public Category Category { get; set; } 
        public AccessLevel AccessLevel { get; set; }
        public List<DocumentVersion> Versions { get; set; } = new(); // TODO should be removed
        public List<ChangeRegister> ChangeHistory { get; set; } = new();

    }
}
