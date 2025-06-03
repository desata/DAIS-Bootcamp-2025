using DAIS.WikiSystem.Models;
using DAIS.WikiSystem.Repository.Base;
using DAIS.WikiSystem.Repository.Interfaces.Mappers;
using Microsoft.Data.SqlClient;

namespace DAIS.WikiSystem.Repository.Implementation.Mappers
{
    public class DocumentTagRepository : BaseMapperRepository<DocumentTag>, IDocumentTagRepository
    {
        protected override string GetTableName() => "DocumentTags";
        protected override string[] GetColumns() => new[] { "DocumentId", "TagId" };
        protected override DocumentTag MapEntity(SqlDataReader reader)
        {
            return new DocumentTag
            {
                DocumentId = Convert.ToInt32(reader["DocumentId"]),
                TagId = Convert.ToInt32(reader["TagId"])
            };
        }
        public async Task<bool> AddIfNotExistsAsync(int documentId, int tagId)
        {
            return await CreateMappingIfNotExistsAsync(new DocumentTag
            {
                DocumentId = documentId,
                TagId = tagId
            });
        }
        public async Task<bool> RemoveAsync(int documentId, int tagId)
        {
            return await DeleteMappingAsync(new DocumentTag
            {
                DocumentId = documentId,
                TagId = tagId
            });
        }

        Task IDocumentTagRepository.CreateMappingIfNotExistsAsync(DocumentTag documentTag)
        {
            return base.CreateMappingIfNotExistsAsync(documentTag);
        }
    }
}
