using Microsoft.Data.SqlClient;
using WikiSystem.Models.Enums;
using WikiSystem.Repository.Base;
using WikiSystem.Repository.Helpers;
using WikiSystem.Repository.Interfaces.Document;

namespace WikiSystem.Repository.Implementation.Document
{
    public class DocumentRepository : BaseMapperRepository<Models.Document>, IDocumentRepository
    {
        private const string IdDbFieldEnumeratorName = "DocumentId";

        protected override string[] GetColumns() => new[]
        {
            IdDbFieldEnumeratorName,
            "Title",
            "AccessLevel",
            "IsArchived",
            "CreatorId",
            "CategoryId"
        };

        protected override string GetTableName()
        {
            return "Documents";
        }

        protected override Models.Document MapEntity(SqlDataReader reader)
        {
            return new Models.Document
            {
                DocumentId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                Title = Convert.ToString(reader["Title"]),
                AccessLevel = (AccessLevel)Convert.ToInt32(reader["AccessLevel"]),
                IsArchived = Convert.ToBoolean(reader["IsArchived"]),
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
                commandFilter.AddCondition("Title", filter.Title);
            }
            if (filter.IsArchived is not null)
            {
                commandFilter.AddCondition("IsArchived", filter.IsArchived);
            }
            if (filter.AccessLevel is not null)
            {
                commandFilter.AddCondition("AccessLevel", filter.AccessLevel);
            }
            if (filter.CreatorId is not null)
            {
                commandFilter.AddCondition("CreatorId", filter.CreatorId);
            }
            if (filter.CategoryId is not null)
            {
                commandFilter.AddCondition("CategoryId", filter.CategoryId);
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
