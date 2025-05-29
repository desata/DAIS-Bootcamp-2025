using System.Data.SqlTypes;

namespace WikiSystem.Repository.Interfaces.CollectionDocument
{
    public class CollectionDocumentFilter
    {
        public SqlInt32? CollectionId { get; set; }
        public SqlInt32? DocumentId { get; set; }
    }
}