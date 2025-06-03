using System.Data.SqlTypes;

namespace DAIS.WikiSystem.Repository.Interfaces.DocumentVersion
{
    public class DocumentVersionFilter
    {
        public SqlInt32? DocumentId { get; set; }

        public bool? IsArchived { get; set; }

    }
}