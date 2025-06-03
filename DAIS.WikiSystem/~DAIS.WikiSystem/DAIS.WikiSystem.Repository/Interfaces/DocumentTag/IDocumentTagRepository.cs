using DAIS.WikiSystem.Repository.Base;

namespace DAIS.WikiSystem.Repository.Interfaces.DocumentTag
{
    public interface IDocumentTagRepository : IBaseMapperRepository<Models.DocumentTag, DocumentTagFilter, DocumentTagUpdate>
    {
    }
}