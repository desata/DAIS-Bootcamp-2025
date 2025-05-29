using Microsoft.Data.SqlClient;

namespace WikiSystem.Repository.Base
{
    public abstract class BaseMapperRepository<TObj> : BaseRepository<TObj> where TObj : class
    {
        protected async Task<bool> CreateMappingIfNotExistsAsync(TObj entity)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();
            using SqlCommand command = connection.CreateCommand();

            var properties = typeof(TObj).GetProperties().ToList();
            string columns = string.Join(", ", properties.Select(p => p.Name));
            string parameters = string.Join(", ", properties.Select(p => "@" + p.Name));
            string whereClause = string.Join(" AND ", properties.Select(p => $"{p.Name} = @{p.Name}"));

            command.CommandText = $@"IF NOT EXISTS (SELECT 1 FROM {GetTableName()} WHERE {whereClause})
                            BEGIN
                                INSERT INTO {GetTableName()} ({columns}) 
                                VALUES ({parameters})
                            END";

            foreach (var prop in properties)
            {
                command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(entity) ?? DBNull.Value);
            }

            return Convert.ToInt32(await command.ExecuteNonQueryAsync()) > 0;
        }


    }
}
