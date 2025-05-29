using Microsoft.Data.SqlClient;
using Wiki.Models;
using Wiki.Repository.Base;
using Wiki.Repository.Interfaces;

namespace Wiki.Repository.Implementations
{
    public class CollectionDocumentRepository : BaseRepository<CollectionDocument>, ICollectionDocumentRepository
    {
        public CollectionDocumentRepository(ConnectionFactory connectionFactory)
                : base(connectionFactory, "CollectionDocument")
        {
        }

        protected override string[] GetColumns()
        {
            return new[] { "CollectionId", "DocumentId" };
        }

        protected override string GetTableName() => "CollectionDocuments";

        protected override string GetColumnName() => "CollectionId";


        protected override object GetValueFromEntity(CollectionDocument entity, string columnName)
        {
            return columnName switch
            {
                "CollectionId" => entity.CollectionId,
                "DocumentId" => entity.DocumentId,
                _ => throw new ArgumentException($"Unknown column: {columnName}")
            };
        }

        protected override Dictionary<string, object> MapToParameters(CollectionDocument entity)
        {
            return new Dictionary<string, object>
        {
            { "CollectionId", entity.CollectionId },
            { "DocumentId", entity.DocumentId }
        };
        }

        // Override Delete for composite key
        public async Task<bool> DeleteByKeysAsync(int collectionId, int documentId)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM {GetTableName()} WHERE CollectionId = @CollectionId AND DocumentId = @DocumentId";
            command.Parameters.AddWithValue("@CollectionId", collectionId);
            command.Parameters.AddWithValue("@DocumentId", documentId);

            connection.Open();
            int rows = await command.ExecuteNonQueryAsync();
            return rows > 0;
        }

        // Optional: check if a relationship exists
        public async Task<bool> ExistsAsync(int collectionId, int documentId)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = connection.CreateCommand();
            command.CommandText = $"SELECT COUNT(*) FROM {GetTableName()} WHERE CollectionId = @CollectionId AND DocumentId = @DocumentId";
            command.Parameters.AddWithValue("@CollectionId", collectionId);
            command.Parameters.AddWithValue("@DocumentId", documentId);

            connection.Open();
            int count = (int)await command.ExecuteScalarAsync();
            return count > 0;
        }

        protected override CollectionDocument MapToEntity(SqlDataReader reader)

        {
            return new CollectionDocument
            {
                CollectionId = reader.GetInt32(reader.GetOrdinal("CollectionId")),
                DocumentId = reader.GetInt32(reader.GetOrdinal("DocumentId"))
            };

        }
    }
}