using Microsoft.Data.SqlClient;
using Wiki.Models;
using Wiki.Models.Enums;
using Wiki.Repository.Base;
using Wiki.Repository.Interfaces;

namespace Wiki.Repository.Implementations
{
    public class DocumentRepository : BaseRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(ConnectionFactory connectionFactory)
            : base(connectionFactory, "Document")
        {
        }
        protected override string[] GetColumns()
        {
            return new[]
            {
                "DocumentId",
                "Title",
                "Tags",
                "AccessLevel",
                "IsDeleted",
                "CreatorId",
                "CategoryId"
            };
        }

        protected override object GetValueFromEntity(Document document, string columnName)
        {
            return columnName switch
            {
                "Title" => document.Title,
                "Tags" => document.Tags,
                "AccessLevel" => (int)document.AccessLevel,
                "IsDeleted" => document.IsDeleted,
                "CreatorId" => document.CreatorId,
                _ => throw new ArgumentException($"Invalid column name: {columnName}")
            };
        }

        protected override Dictionary<string, object> MapToParameters(Document document)
        {
            return new Dictionary<string, object>
            {

                {  "Title", document.Title },
                { "Tags", document.Tags },
                { "AccessLevel", (int)document.AccessLevel },
                { "IsDeleted", document.IsDeleted },
                { "CreatorId", document.CreatorId },
                { "CategoryId", document.CategoryId }
            };
        }

        protected override Document MapToEntity(SqlDataReader reader)
        {
            return new Document
            {
                DocumentId = reader.GetInt32(reader.GetOrdinal("DocumentId")),
                Title = reader.GetString(reader.GetOrdinal("Title")),
                Tags = reader.GetString(reader.GetOrdinal("Tags")),
                AccessLevel = (AccessLevel)reader.GetInt32(reader.GetOrdinal("AccessLevel")),
                IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted")),
                CreatorId = reader.GetInt32(reader.GetOrdinal("CreatorId")),
                CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId"))
            };
        }

        
    }
}
