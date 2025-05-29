using BirthdayGifts.Repository.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayGifts.Repository.Base
{
        public abstract class BaseRepository<T> where T : class
        {
            protected readonly string _connectionString;
            protected readonly string _baseName;

            public BaseRepository(IConfiguration configuration, string baseName)
            {
                _connectionString = configuration.GetConnectionString("DefaultConnection");
                _baseName = baseName;
            }

            protected string GetTableName() => _baseName + "s";
            protected string GetColumnName() => _baseName + "Id";
            protected abstract string[] GetColumns();

            protected abstract T MapToEntity(SqlDataReader reader);
            protected abstract Dictionary<string, object> MapToParameters(T entity);

            public virtual async Task<int> Create(T entity)
            {
                var parameters = MapToParameters(entity);
                var columns = string.Join(", ", parameters.Keys);
                var paramNames = string.Join(", ", parameters.Keys.Select(k => "@" + k));

                var query = $"INSERT INTO {GetTableName()} ({columns}) OUTPUT INSERTED.{GetColumnName()} VALUES ({paramNames})";

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(query, connection))
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue("@" + param.Key, param.Value ?? DBNull.Value);
                        }

                        return (int)await command.ExecuteScalarAsync();
                    }
                }
            }

            public virtual async Task<T> ReceiveById(int objectId)
            {
                var columns = string.Join(", ", GetColumns());
                var query = $"SELECT {columns} FROM {GetTableName()} WHERE {GetColumnName()} = @{GetColumnName()}";

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue($"@{GetColumnName()}", objectId);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return MapToEntity(reader);
                            }
                        }
                    }
                }

                return null;
            }

            public virtual async Task<IEnumerable<T>> ReceiveCollection(Filter filter)
            {
                var columns = string.Join(", ", GetColumns());
                var whereClause = filter?.AddCondition() ?? string.Empty;
                var query = $"SELECT {columns} FROM {GetTableName()} {whereClause}";

                var results = new List<T>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(query, connection))
                    {
                        if (filter != null)
                        {
                            foreach (var param in filter.GetParameters())
                            {
                                command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                            }
                        }

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                results.Add(MapToEntity(reader));
                            }
                        }
                    }
                }

                return results;
            }

            public virtual async Task<bool> Update(int objectId, Update update)
            {
                if (update == null )
                    return false;

                var setClause = update.GetSetClause();
                var query = $"UPDATE {GetTableName()} SET {setClause} WHERE {GetColumnName()} = @{GetColumnName()}";

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue($"@{GetColumnName()}", objectId);

                        foreach (var param in update.GetParameters())
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }

                        return await command.ExecuteNonQueryAsync() > 0;
                    }
                }
            }

            public virtual async Task<bool> Delete(int objectId)
            {
                var query = $"DELETE FROM {GetTableName()} WHERE {GetColumnName()} = @{GetColumnName()}";

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue($"@{GetColumnName()}", objectId);
                        return await command.ExecuteNonQueryAsync() > 0;
                    }
                }
            }
        }
    }
}