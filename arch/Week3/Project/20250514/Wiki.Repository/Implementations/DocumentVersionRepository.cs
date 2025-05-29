using Microsoft.Data.SqlClient;
using Wiki.Models;
using Wiki.Repository.Base;
using Wiki.Repository.Interfaces;

namespace Wiki.Repository.Implementations
{
    public class DocumentVersionRepository : BaseRepository<DocumentVersion>, IDocumentVersionRepository
    {
        public DocumentVersionRepository(ConnectionFactory connectionFactory)
            : base(connectionFactory, "DocumentVersion")
        {
        }
        protected override string[] GetColumns()
        {
            return new[]
            {
            "DocumentVersionsId",
            "Content",
            "Version",
            "HasOtherVersions",
            "CreateDate",
            "DocumentId"
        };
        }

        protected override string GetTableName() => "DocumentVersions";

        protected override string GetColumnName() => "DocumentVersionsId";

        protected override object GetValueFromEntity(DocumentVersion documentVersion, string columnName)
        {
            return columnName switch
            {
                "DocumentVersionsId" => documentVersion.DocumentVersionsId,
                "Content" => documentVersion.Content,
                "Version" => documentVersion.Version,
                "HasOtherVersions" => documentVersion.HasOtherVersions,
                "CreateDate" => documentVersion.CreateDate,
                "DocumentId" => documentVersion.DocumentId,
                _ => throw new ArgumentException($"Unknown column: {columnName}")
            };
        }

        protected override Dictionary<string, object> MapToParameters(DocumentVersion documentVersion)
        {
            return new Dictionary<string, object>
        {
            { "Content", documentVersion.Content },
            { "Version", documentVersion.Version },
            { "HasOtherVersions", documentVersion.HasOtherVersions },
            { "CreateDate", documentVersion.CreateDate },
            { "DocumentId", documentVersion.DocumentId }
        };
        }

        protected override DocumentVersion MapToEntity(SqlDataReader reader)
        {
            return new DocumentVersion
            {
                DocumentVersionsId = reader.GetInt32(reader.GetOrdinal("DocumentVersionsId")),
                Content = reader.GetString(reader.GetOrdinal("Content")),
                Version = reader.GetString(reader.GetOrdinal("Version")),
                HasOtherVersions = reader.GetBoolean(reader.GetOrdinal("HasOtherVersions")),
                CreateDate = reader.GetDateTime(reader.GetOrdinal("CreateDate")),
                DocumentId = reader.GetInt32(reader.GetOrdinal("DocumentId"))
            };
        }
    }
}