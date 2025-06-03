using DAIS.WikiSystem.Repository.Base;
using DAIS.WikiSystem.Repository.Helpers;
using DAIS.WikiSystem.Repository.Interfaces.DocumentTag;
using Microsoft.Data.SqlClient;

namespace DAIS.WikiSystem.Repository.Implementation.DocumentTag
{
    public class DocumentTagRepository : BaseMapperRepository<Models.DocumentTag>, IDocumentTagRepository
    {
        protected override string GetTableName() => "DocumentTags";
        protected override string[] GetColumns() => new[] { "DocumentId", "TagId" };
        protected override Models.DocumentTag MapEntity(SqlDataReader reader)
        {
            return new Models.DocumentTag
            {
                DocumentId = Convert.ToInt32(reader["DocumentId"]),
                TagId = Convert.ToInt32(reader["TagId"])
            };
        }
        public async Task<bool> AddIfNotExistsAsync(int documentId, List<int> tagIds)
        {
            bool allSucceeded = true;

            foreach (int tagId in tagIds)
            {
                bool success = await CreateMappingIfNotExistsAsync(new Models.DocumentTag
                {
                    DocumentId = documentId,
                    TagId = tagId
                });

                if (!success)
                {
                    allSucceeded = false;
                }
            }

            return allSucceeded;
        }

        public async Task<bool> CreateMappingIfNotExistsAsync(Models.DocumentTag documentTag)
        {
            return await CreateMappingIfNotExistsAsync(documentTag);
        }
        public async Task<bool> RemoveAsync(int documentId, int tagId)
        {
            return await DeleteMappingAsync(new Models.DocumentTag
            {
                DocumentId = documentId,
                TagId = tagId
            });
        }


        public Task<Models.DocumentTag> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync("DocumentId", objectId);
        }

        public IAsyncEnumerable<Models.DocumentTag> RetrieveCollectionAsync(DocumentTagFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.DocumentId.HasValue)
            {
                commandFilter.AddCondition("DocumentId", filter.DocumentId);
            }
            if (filter.TagId.HasValue)
            {
                commandFilter.AddCondition("TagId", filter.TagId);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

    }
}
