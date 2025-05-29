using System.Data.SqlTypes;

namespace WikiSystem.Repository.Interfaces.DocumentVersion
{
    public class DocumentVersionFilter
    {
        public SqlInt32? DocumentId { get; set; }
    }
}