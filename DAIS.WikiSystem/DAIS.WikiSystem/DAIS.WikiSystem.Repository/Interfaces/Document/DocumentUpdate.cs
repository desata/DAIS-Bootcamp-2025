using System.Data.SqlTypes;

namespace DAIS.WikiSystem.Repository.Interfaces.Document
{
    public class DocumentUpdate
    {
        public SqlBinary? IsDeleted { get; set; }
    }
}
