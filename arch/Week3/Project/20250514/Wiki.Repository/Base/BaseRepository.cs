using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Wiki.Repository;
using Wiki.Repository.Helpers;

namespace Wiki.Repository.Base
{
    public abstract class BaseRepository<T> where T : class
    {
        protected readonly ConnectionFactory _connectionFactory;
        protected abstract object GetValueFromEntity(T entity, string columnName);
        protected readonly string _baseName;

        public BaseRepository(ConnectionFactory connectionFactory, string baseName)
        {
            _connectionFactory = connectionFactory;
            _baseName = baseName;
        }

        protected virtual string GetTableName() => _baseName + "s";
        protected virtual string GetColumnName() => _baseName + "Id";
        protected abstract string[] GetColumns();
        protected abstract T MapToEntity(SqlDataReader reader);
        protected abstract Dictionary<string, object> MapToParameters(T entity);




        public virtual async Task<T> RetrieveById(int objectId)
        {
            var query = $"SELECT {string.Join(", ", GetColumns())} FROM {GetTableName()} WHERE {GetColumnName()} = @Id";

            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", objectId);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return MapToEntity(reader);
            }

            return null;
        }


        public virtual async Task<IEnumerable<T>> RetrieveCollection(Filter filter)
        {
            var table = GetTableName();
            var columns = string.Join(", ", GetColumns());
            var whereClause = filter.ToWhereClause(out var parameters);

            var query = $"SELECT {columns} FROM {table} {whereClause}";

            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand(query, connection);

            foreach (var (name, value) in parameters)
            {
                command.Parameters.AddWithValue(name, value ?? DBNull.Value);
            }

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            var results = new List<T>();

            while (await reader.ReadAsync())
            {
                results.Add(MapToEntity(reader));
            }

            return results;
        }


        public virtual async Task<bool> Update(int objectId, Update update)
        {
            var table = GetTableName();
            var setClause = update.ToSetClause(out var parameters);
            var query = $"UPDATE {table} SET {setClause} WHERE {GetColumnName()} = @Id";
            parameters.Add("@Id", objectId);
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand(query, connection);
            foreach (var (name, value) in parameters)
            {
                command.Parameters.AddWithValue(name, value ?? DBNull.Value);
            }
            await connection.OpenAsync();
            return await command.ExecuteNonQueryAsync() > 0;
        }


        public virtual async Task<bool> Delete(int objectId)
        {
            var table = GetTableName();        
            var idColumn = GetColumnName();    
            var query = $"DELETE FROM {table} WHERE {idColumn} = @Id";

            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", objectId);

            await connection.OpenAsync();
            var rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0;
        }

        public virtual async Task<int> Create(T entity)
        {
            var table = GetTableName(); 
            var columns = GetColumns(); 
            var values = new List<string>();
            var parameters = new List<SqlParameter>();

            for (int i = 0; i < columns.Length; i++)
            {
                string paramName = $"@param{i}";
                values.Add(paramName);
                var value = GetValueFromEntity(entity, columns[i]);
                parameters.Add(new SqlParameter(paramName, value ?? DBNull.Value));
            }

            var query = $"INSERT INTO {table} ({string.Join(", ", columns)}) " +
                        $"OUTPUT INSERTED.{GetColumnName()} " +
                        $"VALUES ({string.Join(", ", values)})";

            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddRange(parameters.ToArray());

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }
    }
}