using DAIS.WikiSystem.Models;
using DAIS.WikiSystem.Repository.Base;
using DAIS.WikiSystem.Repository.Interfaces.Mappers;
using Microsoft.Data.SqlClient;

namespace DAIS.WikiSystem.Repository.Implementation.Mappers
{
    public class CollectionDocumentRepository : BaseMapperRepository<CollectionDocument>, ICollectionDocumentRepository
    {
        protected override string GetTableName() => "CollectionDocuments";

        protected override string[] GetColumns() => new[] { "CollectionId", "DocumentId" };

        protected override CollectionDocument MapEntity(SqlDataReader reader)
        {
            return new CollectionDocument
            {
                CollectionId = Convert.ToInt32(reader["CollectionId"]),
                DocumentId = Convert.ToInt32(reader["DocumentId"])
            };
        }

        public async Task<bool> AddIfNotExistsAsync(int collectionId, int documentId)
        {
            return await CreateMappingIfNotExistsAsync(new CollectionDocument
            {
                CollectionId = collectionId,
                DocumentId = documentId
            });
        }

        public async Task<bool> RemoveAsync(int collectionId, int documentId)
        {
            return await DeleteMappingAsync(new CollectionDocument
            {
                CollectionId = collectionId,
                DocumentId = documentId
            });
        }

        Task ICollectionDocumentRepository.CreateMappingIfNotExistsAsync(CollectionDocument collectionDocument)
        {
            return CreateMappingIfNotExistsAsync(collectionDocument);
        }
    }
}