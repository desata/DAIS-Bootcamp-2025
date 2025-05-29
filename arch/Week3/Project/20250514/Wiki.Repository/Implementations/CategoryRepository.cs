using Microsoft.Data.SqlClient;
using Wiki.Models;
using Wiki.Repository.Base;
using Wiki.Repository.Interfaces;

namespace Wiki.Repository.Implementations
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ConnectionFactory connectionFactory)
            : base(connectionFactory, "Category")
        {
        }
        protected override string[] GetColumns()
        {
            return new[]
            {
                "CategoryId",
                "Name"
            };
        }

        protected override object GetValueFromEntity(Category category, string columnName)
        {
            return columnName switch
            {
                "Name" => category.Name,
                _ => throw new ArgumentException($"Invalid column name: {columnName}")
            };
        }

        protected override Category MapToEntity(SqlDataReader reader)
        {
            return new Category
            {
                CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                Name = reader.GetString(reader.GetOrdinal("Name"))
            };
        }

        protected override Dictionary<string, object> MapToParameters(Category category)
        {
            return new Dictionary<string, object>
            {
                { "Name", category.Name }
            };
        }
    }
}
