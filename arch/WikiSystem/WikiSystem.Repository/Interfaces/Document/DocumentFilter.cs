using System.Data.SqlTypes;

namespace WikiSystem.Repository.Interfaces.Document
{
    public class DocumentFilter
    {
        public SqlString? Title { get; set; }
        public SqlInt32? AccessLevel { get; set; }
        public SqlBoolean? IsArchived { get; set; }
        public SqlInt32? CreatorId { get; set; }
        public SqlInt32? CategoryId { get; set; }
    }
}