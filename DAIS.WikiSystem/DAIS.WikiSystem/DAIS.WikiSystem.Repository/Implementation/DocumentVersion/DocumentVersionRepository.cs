using DAIS.WikiSystem.Repository.Base;
using DAIS.WikiSystem.Repository.Helpers;
using DAIS.WikiSystem.Repository.Interfaces.DocumentVersion;
using Microsoft.Data.SqlClient;

namespace DAIS.WikiSystem.Repository.Implementation.DocumentVersion
{
    public class DocumentVersionRepository : BaseRepository<Models.DocumentVersion>, IDocumentVersionRepository
    {
        private const string IdDbFieldEnumeratorName = "DocumentVersionId";

        protected override string GetTableName() => "DocumentVersions";
        protected override string[] GetColumns()
        {
            return new[]
            {
                "DocumentVersionId",
                "FilePath",
                "Version",
                "IsArchived",
                "CreateDate",
                "DocumentId"
            };
        }

        protected override Models.DocumentVersion MapEntity(SqlDataReader reader)
        {
            return new Models.DocumentVersion
            {
                DocumentVersionId = Convert.ToInt32(reader["DocumentVersionId"]),
                FilePath = Convert.ToString(reader["FilePath"]),
                Version = Convert.ToString(reader["Version"]),
                IsArchived = Convert.ToBoolean(reader["IsArchived"]),
                DocumentId = Convert.ToInt32(reader["DocumentId"]),
                CreateDate = Convert.ToDateTime(reader["CreateDate"])
            };
        }

        public async Task<int> CreateAsync(Models.DocumentVersion entity)
        {
            return await base.CreateAsync(entity, IdDbFieldEnumeratorName);
        }

        public Task<Models.DocumentVersion> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.DocumentVersion> RetrieveCollectionAsync(DocumentVersionFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.IsArchived.HasValue)
            {
                commandFilter.AddCondition("IsArchived", filter.IsArchived.Value);
            }
            if (filter.DocumentId.HasValue)
            {
                commandFilter.AddCondition("DocumentId", filter.DocumentId.Value);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, DocumentVersionUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            using var updateCommand = new UpdateCommand(connection, GetTableName(), IdDbFieldEnumeratorName, objectId);

            updateCommand.AddSetClause("IsArchived", update.IsArchived);

            return await updateCommand.ExecuteNonQueryAsync() == 1;
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            throw new NotImplementedException();
        }
    }
}
