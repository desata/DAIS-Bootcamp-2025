using System.Data.SqlTypes;

namespace WikiSystem.Repository.Interfaces.Tag
{
    public class TagFilter
    {
        public SqlString? Name { get; set; }
    }
}