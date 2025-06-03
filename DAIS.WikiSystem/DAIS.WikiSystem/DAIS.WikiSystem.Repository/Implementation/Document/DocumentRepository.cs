using DAIS.WikiSystem.Models.Enums;
using DAIS.WikiSystem.Repository.Base;
using DAIS.WikiSystem.Repository.Helpers;
using DAIS.WikiSystem.Repository.Interfaces.Document;
using Microsoft.Data.SqlClient;

namespace DAIS.WikiSystem.Repository.Implementation.Document
{
    public class DocumentRepository : BaseRepository<Models.Document>, IDocumentRepository
    {
        private const string IdDbFieldEnumeratorName = "DocumentId";
        protected override string GetTableName() => "Documents";
        protected override string[] GetColumns() => new[]
        {
            IdDbFieldEnumeratorName,
            "Title",
            "IsDeleted",
            "CreatorId",
            "CategoryId",
            "AccessLevel",
        };
        protected override Models.Document MapEntity(SqlDataReader reader)
        {
            return new Models.Document
            {
                DocumentId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                Title = Convert.ToString(reader["Title"]),
                IsDeleted = Convert.ToBoolean(reader["IsDeleted"]),
                CreatorId = Convert.ToInt32(reader["CreatorId"]),
                CategoryId = Convert.ToInt32(reader["CategoryId"]),
                AccessLevel = (AccessLevel)Convert.ToInt32(reader["AccessLevel"])
            };
        }
        public Task<int> CreateAsync(Models.Document entity)
        {
            return base.CreateAsync(entity, IdDbFieldEnumeratorName);
        }

        public Task<Models.Document> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.Document> RetrieveCollectionAsync(DocumentFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.IsDeleted.HasValue)
            {
                commandFilter.AddCondition("IsDeleted", filter.IsDeleted.Value);
            }

            if (filter.Title.HasValue)
            {
                commandFilter.AddCondition("Title", filter.Title.Value);
            }

            if (filter.CategoryId.HasValue)
            {
                commandFilter.AddCondition("CategoryId", filter.CategoryId.Value.Value);
            }

            if (filter.CreatorId.HasValue)
            {
                commandFilter.AddCondition("CreatorId", filter.CreatorId.Value.Value);
            }

            if (filter.MaxAccessLevel.HasValue)
            {
                commandFilter.AddCondition("AccessLevel", (int)filter.MaxAccessLevel.Value, "<=");
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, DocumentUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            using var updateCommand = new UpdateCommand(connection, GetTableName(), IdDbFieldEnumeratorName, objectId);

            if (update.IsDeleted is not null)
            {
                updateCommand.AddSetClause("IsDeleted", update.IsDeleted);
            }

            return await updateCommand.ExecuteNonQueryAsync() == 1;
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            throw new NotImplementedException();
        }
    }
}
