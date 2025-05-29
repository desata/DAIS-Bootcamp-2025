using WikiSystem.Repository.Base;

namespace WikiSystem.Repository.Interfaces.DocumentVersion
{
    public interface IDocumentVersionRepository : IBaseRepository<Models.DocumentVersion, DocumentVersionFilter, DocumentVersionUpdate>
    {
    }
}
