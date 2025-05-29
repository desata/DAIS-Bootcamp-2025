using Microsoft.Data.SqlClient;
using WikiSystem.Repository.Base;
using WikiSystem.Repository.Helpers;
using WikiSystem.Repository.Interfaces.DocumentVersion;

namespace WikiSystem.Repository.Implementation.DocumentVersion
{
    public class DocumentVersionRepository : BaseMapperRepository<Models.DocumentVersion>, IDocumentVersionRepository
    {
        private const string IdDbFieldEnumeratorName = "DocumentVersionsId";
        protected override string[] GetColumns() => new[]
        {
            IdDbFieldEnumeratorName,
            "Content",
            "Version",
            "IsArchived",
            "CreateDate",
            "DocumentId"
        };
        protected override string GetTableName()
        {
            return "DocumentVersions";
        }
        protected override Models.DocumentVersion MapEntity(SqlDataReader reader)
        {
            return new Models.DocumentVersion
            {
                DocumentVersionsId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                Content = Convert.ToString(reader["Content"]),
                Version = Convert.ToString(reader["Version"]),
                IsArchived = Convert.ToBoolean(reader["IsArchived"]),
                CreateDate = Convert.ToDateTime(reader["CreateDate"]),
                DocumentId = Convert.ToInt32(reader["DocumentId"])
            };
        }

        public Task<int> CreateAsync(Models.DocumentVersion entity)
        {
            return base.CreateAsync(entity, IdDbFieldEnumeratorName);
        }
        public Task<Models.DocumentVersion> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.DocumentVersion> RetrieveCollectionAsync(DocumentVersionFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.DocumentId is not null)
            {
                commandFilter.AddCondition("DocumentId", filter.DocumentId);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public Task<bool> UpdateAsync(int objectId, DocumentVersionUpdate update)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            throw new NotImplementedException();
        }
    }
}
