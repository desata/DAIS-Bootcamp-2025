using Microsoft.Data.SqlClient;
using WikiSystem.Repository.Base;
using WikiSystem.Repository.Helpers;
using WikiSystem.Repository.Interfaces.DocumentVersion;

namespace WikiSystem.Repository.Implementations.DocumentVersion
{
    public class DocumentVersionRepository : BaseRepository<Models.DocumentVersion>, IDocumentVersionRepository
    {
        private const string IdDbFieldEnumeratorName = "DocumentVersionsId";
        protected override string GetTableName()
        {
            return "DocumentVersions";
        }
        protected override string[] GetColumns()
        {
            return new string[]
            {
                IdDbFieldEnumeratorName,
                "Version",
                "HasOtherVersions",
                "Content",
                "CreateDate",
                "DocumentId"
            };
        }
        protected override Models.DocumentVersion MapEntity(SqlDataReader reader)
        {
            return new Models.DocumentVersion
            {
                DocumentVersionsId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                Content = Convert.ToString(reader["Content"]),
                Version = Convert.ToString(reader["Version"]),
                HasOtherVersions = Convert.ToBoolean(reader["HasOtherVersions"]),
                CreateDate = Convert.ToDateTime(reader["CreateDate"]),
                DocumentId = Convert.ToInt32(reader["DocumentId"]),
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
                commandFilter.AddClause("DocumentId", filter.DocumentId);
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
