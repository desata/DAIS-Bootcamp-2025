using Microsoft.Data.SqlClient;
using WikiSystem.Repository.Base;
using WikiSystem.Repository.Helpers;
using WikiSystem.Repository.Interfaces.Document;

namespace WikiSystem.Repository.Implementations.Document
{
    public class DocumentRepository : BaseRepository<Models.Document>, IDocumentRepository
    {

        private const string IdDbFieldEnumeratorName = "DocumentId";
        protected override string GetTableName()
        {
            return "Documents";
        }
        protected override string[] GetColumns()
        {
            return new string[]
            {
                IdDbFieldEnumeratorName,
                "Title",
                "Tags",
                "AccessLevel",
                "IsDeleted",
                "CreatorId",
                "CategoryId"
            };
        }
        protected override Models.Document MapEntity(SqlDataReader reader)
        {
            return new Models.Document
            {
                DocumentId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                Title = Convert.ToString(reader["Title"]),
                Tags = Convert.ToString(reader["Tags"]),
                AccessLevel = Convert.ToInt32(reader["AccessLevel"]),
                IsDeleted = Convert.ToBoolean(reader["IsDeleted"]),
                CreatorId = Convert.ToInt32(reader["CreatorId"]),
                CategoryId = Convert.ToInt32(reader["CategoryId"])
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

            if (filter.Title is not null)
            {
                commandFilter.AddClause("Title", filter.Title);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public Task<bool> UpdateAsync(int objectId, DocumentUpdate update)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            throw new NotImplementedException();
        }


    }
}
