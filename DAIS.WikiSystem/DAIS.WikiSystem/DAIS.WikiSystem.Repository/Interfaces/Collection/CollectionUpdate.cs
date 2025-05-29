using System.Data.SqlTypes;

namespace DAIS.WikiSystem.Repository.Interfaces.Collection
{
    public class CollectionUpdate
    {
        public SqlString? Name { get; set; }

        public SqlInt32? CreatorId { get; set; }
    }
}
