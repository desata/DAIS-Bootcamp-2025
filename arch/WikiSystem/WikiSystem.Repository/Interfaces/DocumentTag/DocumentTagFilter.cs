using System.Data.SqlTypes;

namespace WikiSystem.Repository.Interfaces.DocumentTag
{
    public class DocumentTagFilter
    {
        public SqlInt32? DocumentId { get; set; }
        public SqlInt32? TagId { get; set; }
    }
}