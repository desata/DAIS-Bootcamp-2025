using WikiSystem.Repository.Base;

namespace WikiSystem.Repository.Interfaces.CollectionDocument
{
    public interface ICollectionDocumentRepository : IBaseRepository<Models.CollectionDocument, CollectionDocumentFilter, CollectionDocumentUpdate>
    {
    }
}
