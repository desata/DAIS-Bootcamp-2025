using DAIS.WikiSystem.Repository.Base;
using DAIS.WikiSystem.Repository.Helpers;
using DAIS.WikiSystem.Repository.Interfaces.CollectionDocument;
using Microsoft.Data.SqlClient;

namespace DAIS.WikiSystem.Repository.Implementation.CollectionDocument
{
    public class CollectionDocumentRepository : BaseMapperRepository<Models.CollectionDocument>, ICollectionDocumentRepository
    {
        protected override string GetTableName() => "CollectionDocuments";

        protected override string[] GetColumns() => new[] { "CollectionId", "DocumentId" };

        protected override Models.CollectionDocument MapEntity(SqlDataReader reader)
        {
            return new Models.CollectionDocument
            {
                CollectionId = Convert.ToInt32(reader["CollectionId"]),
                DocumentId = Convert.ToInt32(reader["DocumentId"])
            };
        }
        public async Task<bool> AddIfNotExistsAsync(int collectionId, List<int> documentIds)
        {
            bool allSucceeded = true;

            foreach (int documentId in documentIds)
            {
                bool success = await CreateMappingIfNotExistsAsync(new Models.CollectionDocument
                {
                    CollectionId = collectionId,
                    DocumentId = documentId
                });

                if (!success)
                {
                    allSucceeded = false;
                }
            }

            return allSucceeded;
        }

        public async Task<bool> RemoveAsync(int collectionId, int documentId)
        {
            return await DeleteMappingAsync(new Models.CollectionDocument
            {
                CollectionId = collectionId,
                DocumentId = documentId
            });
        }

        public async Task<bool> CreateMappingIfNotExistsAsync(Models.CollectionDocument collectionDocument)
        {
            return await CreateMappingIfNotExistsAsync(collectionDocument);
        }

        public IAsyncEnumerable<Models.CollectionDocument> RetrieveCollectionAsync(CollectionDocumentFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.DocumentId.HasValue)
            {
                commandFilter.AddCondition("DocumentId", filter.DocumentId.Value);
            }
            if (filter.CollectionId.HasValue)
            {
                commandFilter.AddCondition("CollectionId", filter.CollectionId.Value);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public Task<Models.CollectionDocument> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync("CollectionDocumentId", objectId);
        }

    }
}