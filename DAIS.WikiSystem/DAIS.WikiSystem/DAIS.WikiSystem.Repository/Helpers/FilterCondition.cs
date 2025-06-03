using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAIS.WikiSystem.Repository.Helpers
{
    public class FilterCondition
    {
        public string Field { get; set; } = null!;
        public object Value { get; set; } = null!;
        public string Operator { get; set; } = "=";
    }
}
