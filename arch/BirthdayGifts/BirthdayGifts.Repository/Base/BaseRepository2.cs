using BirthdayGifts.Repository.Helpers;
using Microsoft.Data.SqlClient;

namespace BirthdayGifts.Repository.Base2
{
    public abstract class BaseRepository2<TObj>
    {
        protected abstract string GetTableName();
        protected abstract string[] GetColumns();
        protected virtual string SelectAllCommandText()
        {
            var columns = string.Join(", ", GetColumns());
            return $"SELECT {columns} FROM {GetTableName()}";
        }
        protected abstract TObj MapEntity(SqlDataReader reader);

        protected async Task<int> CreateAsync(TObj entity, string idDbFieldEnumeratorName = null)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();
            using SqlCommand command = connection.CreateCommand();

            var properties = typeof(TObj).GetProperties()
                .Where(p => p.Name != idDbFieldEnumeratorName)
                .ToList();

            string columns = string.Join(", ", properties.Select(p => p.Name));
            string parameters = string.Join(", ", properties.Select(p => "@" + p.Name));

            command.CommandText = $@"INSERT INTO {GetTableName()} ({columns}) 
                                VALUES ({parameters});
                                SELECT CAST(SCOPE_IDENTITY() as int)";

            foreach (var prop in properties)
            {
                command.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(entity) ?? DBNull.Value);
            }

            return Convert.ToInt32(await command.ExecuteScalarAsync());
        }

        protected async Task<TObj> RetrieveAsync(string idDbFieldName, int idDbFieldValue)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();
            using SqlCommand sqlCommand = connection.CreateCommand();

            sqlCommand.CommandText =
                $"{SelectAllCommandText()} " +
                $"WHERE {idDbFieldName} = @${idDbFieldName}";

            sqlCommand.Parameters.AddWithValue($"@${idDbFieldName}", idDbFieldValue);
            using SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                TObj result = MapEntity(reader);

                if (await reader.ReadAsync())
                {
                    throw new Exception("Multiple records found for the same ID.");
                }

                return result;
            }
            else
            {
                throw new Exception("No record found for the given ID.");
            }
        }

        protected async IAsyncEnumerable<TObj> RetrieveCollectionAsync(Filter filter)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();
            using SqlCommand sqlCommand = connection.CreateCommand();

            sqlCommand.CommandText =
@$"{SelectAllCommandText()} 
WHERE 1 = 1";

            foreach (var condition in filter.Conditions)
            {
                sqlCommand.CommandText += $" AND {condition.Key} = @{condition.Key}";
                sqlCommand.Parameters.AddWithValue($"@{condition.Key}", condition.Value);
            }

            using SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                TObj employee = MapEntity(reader);
                yield return employee;
            }
        }

        protected async Task<bool> DeleteAsync(string idDbFieldName, int objectId)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();
            using SqlCommand command = connection.CreateCommand();
            using SqlTransaction transaction = command.Connection.BeginTransaction();

            command.CommandText = $"DELETE FROM {GetTableName()} WHERE {idDbFieldName} = @{idDbFieldName}";
            command.Parameters.AddWithValue($"@{idDbFieldName}", objectId);
            command.Transaction = transaction;

            // Execute the delete command and return the number of affected rows
            int rowsAffected = await command.ExecuteNonQueryAsync();

            if (rowsAffected != 1)
            {
                throw new Exception($"Just one row should be deleted! Command aborted, because {rowsAffected} could have been deleted!");
            }

            transaction.Commit();
            return true;
        }
    }
}