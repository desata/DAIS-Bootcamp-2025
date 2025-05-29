using System.Data.SqlTypes;

namespace DAIS.WikiSystem.Repository.Interfaces.Tag
{
    public class TagFilter
    {
        public SqlString? Name { get; set; }

        public SqlInt32? TagId { get; set; }
    }
}
