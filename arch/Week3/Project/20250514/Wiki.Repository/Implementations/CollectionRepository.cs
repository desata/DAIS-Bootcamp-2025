using Microsoft.Data.SqlClient;
using System.Text;
using Wiki.Models;
using Wiki.Repository.Base;
using Wiki.Repository.Interfaces;

namespace Wiki.Repository.Implementations
{
    public class CollectionRepository : BaseRepository<Collection>, ICollectionRepository
    {
        public CollectionRepository(ConnectionFactory connectionFactory)
            : base(connectionFactory, "Collection")
        {
        }
        protected override string[] GetColumns()
        {
            return new[]
            {
                "CollectionId",
                "Name",
                "CreatorId"
            };
        }
        protected override object GetValueFromEntity(Collection collection, string columnName)
        {
            return columnName switch
            {
                "Name" => collection.Name,
                _ => throw new ArgumentException($"Invalid column name: {columnName}")
            };
        }
        protected override Collection MapToEntity(SqlDataReader reader)
        {
            return new Collection
            {
                CollectionId = reader.GetInt32(reader.GetOrdinal("CollectionId")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                CreatorId = reader.GetInt32(reader.GetOrdinal("CreatorId"))
            };
        }
        protected override Dictionary<string, object> MapToParameters(Collection collection)
        {
            return new Dictionary<string, object>
            {
                { "Name", collection.Name }
            };
        }
    }
}
