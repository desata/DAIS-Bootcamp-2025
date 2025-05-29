using Microsoft.Data.SqlClient;

namespace DAIS.WikiSystem.Repository.Helpers
{

    public class UpdateCommand : IDisposable
    {
        private readonly List<string> setClauses = new();
        private readonly SqlCommand sqlCommand;
        private readonly string idDbFieldName;
        private readonly int idDbFieldValue;

        public UpdateCommand(
            SqlConnection sqlConnection,
            string tableName,
            string idDbFieldName,
            int idDbFieldValue)
        {
            sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = $"UPDATE {tableName}";

            this.idDbFieldName = idDbFieldName;
            this.idDbFieldValue = idDbFieldValue;
        }

        public void AddSetClause(string dbFieldName, object? dbFieldValue)
        {
            if (dbFieldValue != null)
            {
                setClauses.Add($"{dbFieldName} = @{dbFieldName}");
                sqlCommand.Parameters.AddWithValue($"@{dbFieldName}", dbFieldValue);
            }
        }

        public async Task<int> ExecuteNonQueryAsync()
        {
            if (setClauses.Count == 0)
            {
                throw new InvalidOperationException("UpdateCommand must include at least one field to update.");
            }
            sqlCommand.CommandText +=
    $@" SET {string.Join(", ", setClauses)}
WHERE {idDbFieldName} = @{idDbFieldName}";

            sqlCommand.Parameters.AddWithValue($"@{idDbFieldName}", idDbFieldValue);

            SqlTransaction transaction = sqlCommand.Connection.BeginTransaction();
            sqlCommand.Transaction = transaction;

            int rowsAffected = await sqlCommand.ExecuteNonQueryAsync();

            if (rowsAffected != 1)
            {
                throw new InvalidOperationException($"Expected to update one row, but {rowsAffected} were affected.");
            }

            transaction.Commit();
            return rowsAffected;
        }

        public void Dispose()
        {
            sqlCommand.Dispose();
        }
    }
}
