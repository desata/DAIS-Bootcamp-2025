using Microsoft.Data.SqlClient;
using WikiSystem.Repository.Base;
using WikiSystem.Repository.Helpers;
using WikiSystem.Repository.Interfaces.DocumentTag;

namespace WikiSystem.Repository.Implementation.DocumentTag
{
    public class DocumentTagRepository : BaseMapperRepository<Models.DocumentTag>, IDocumentTagRepository
    {
        protected override string[] GetColumns() => new[]
        {
            "DocumentId",
            "TagId"
        };

        protected override string GetTableName()
        {
            return "DocumentTags";
        }

        protected override Models.DocumentTag MapEntity(SqlDataReader reader)
        {
            return new Models.DocumentTag
            {

                DocumentId = Convert.ToInt32(reader["DocumentId"]),
                TagId = Convert.ToInt32(reader["TagId"])
            };
        }

        public Task<int> CreateAsync(Models.DocumentTag entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            throw new NotImplementedException();
        }

        public Task<Models.DocumentTag> RetrieveAsync(int objectId)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<Models.DocumentTag> RetrieveCollectionAsync(DocumentTagFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.TagId is not null)
            {
                commandFilter.AddCondition("TagId", filter.TagId);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public Task<bool> UpdateAsync(int objectId, DocumentTagUpdate update)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> LinkAsync(int primaryId, int secondaryId)
        {
            var documentTag = new Models.DocumentTag
            {
                DocumentId = primaryId,
                TagId = secondaryId
            };

            return await CreateMappingIfNotExistsAsync(documentTag);
        }

        public async Task<int> LinkMultipleAsync(int primaryId, IEnumerable<int> secondaryIds)
        {
            int successCount = 0;

            foreach (int tagId in secondaryIds)
            {
                var documentTag = new Models.DocumentTag
                {
                    DocumentId = primaryId,
                    TagId = tagId
                };

                bool success = await CreateMappingIfNotExistsAsync(documentTag);
                if (success) successCount++;
            }

            return successCount;
        }
    }
}
