using Microsoft.Data.SqlClient;
using WikiSystem.Repository.Base;
using WikiSystem.Repository.Helpers;
using WikiSystem.Repository.Interfaces.CollectionDocument;

namespace WikiSystem.Repository.Implementation.CollectionDocument
{
    public class CollectionDocumentRepository : BaseMapperRepository<Models.CollectionDocument>, ICollectionDocumentRepository
    {

        protected override string[] GetColumns() => new[]
        {
            "CollectionId",
            "DocumentId"
        };

        protected override string GetTableName()
        {
            return "CollectionDocuments";
        }

        protected override Models.CollectionDocument MapEntity(SqlDataReader reader)
        {
            return new Models.CollectionDocument
            {
                CollectionId = Convert.ToInt32(reader["CollectionId"]),
                DocumentId = Convert.ToInt32(reader["DocumentId"])
            };
        }

        public Task<int> CreateAsync(Models.CollectionDocument entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            throw new NotImplementedException();
        }

        public Task<Models.CollectionDocument> RetrieveAsync(int objectId)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<Models.CollectionDocument> RetrieveCollectionAsync(CollectionDocumentFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.CollectionId is not null)
            {
                commandFilter.AddCondition("CollectionId", filter.CollectionId);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public Task<bool> UpdateAsync(int objectId, CollectionDocumentUpdate update)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> LinkAsync(int primaryId, int secondaryId)
        {
            var collectionDocument = new Models.CollectionDocument
            {
                CollectionId = primaryId,
                DocumentId = secondaryId
            };

            return await CreateMappingIfNotExistsAsync(collectionDocument);
        }

        // Method to add multiple tags to a document
        public async Task<int> LinkMultipleAsync(int primaryId, IEnumerable<int> secondaryIds)
        {
            int successCount = 0;

            foreach (int secondaryId in secondaryIds)
            {
                var collectionDocument = new Models.CollectionDocument
                {
                    CollectionId = primaryId,
                    DocumentId = secondaryId
                };

                bool success = await CreateMappingIfNotExistsAsync(collectionDocument);
                if (success) successCount++;
            }

            return successCount;
        }

    }
}