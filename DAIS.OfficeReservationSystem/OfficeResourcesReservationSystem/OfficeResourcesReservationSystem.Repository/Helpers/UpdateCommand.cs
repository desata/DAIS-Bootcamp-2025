using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;

namespace OfficeResourcesReservationSystem.Repository.Helpers
{
    public class UpdateCommand : IDisposable
    {
        private SqlCommand sqlCommand;
        private List<string> setClauses { get; set; } = new List<string>();
        private readonly string idDbFieldName;
        private readonly int idDbFieldValue;

        public UpdateCommand(SqlConnection sqlConnection, string tableName, string idDbfieldName, int idDbfieldValue)
        {
            this.idDbFieldName = idDbFieldName;
            this.idDbFieldValue = idDbFieldValue;
            sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = $"UPDATE {tableName}";
        }

        public void AddSetClause(string field, INullable? value)
        {

            if (value is not null)
            {
                setClauses.Add($"{field} = @{field}");
                sqlCommand.Parameters.AddWithValue($"@{field}", value);
            }
        }

        public async Task<int> ExecuteAsync()
        {
            if (setClauses.Count == 0)
            {
                throw new InvalidOperationException("No set clauses have been added.");
            }
            sqlCommand.CommandText += @$"SET {string.Join(", ", setClauses)} WHERE {idDbFieldName} = @{idDbFieldName}";

            sqlCommand.Parameters.AddWithValue($"@{idDbFieldName}", idDbFieldValue);


            SqlTransaction transaction = sqlCommand.Connection.BeginTransaction();

            var rowsAffected = await sqlCommand.ExecuteNonQueryAsync();

            if (rowsAffected != 1)
            {
                throw new Exception($"Just one row should be updated! Command aborted, because {rowsAffected} could have been updated!");
            }

            transaction.Commit();
            return rowsAffected;
        }

        public async Task<int> ExecuteNonQueryAsync()
        {
            if (setClauses.Count == 0)
            {
                throw new Exception("No fields to update! You should pass at least one!");
            }

            sqlCommand.CommandText += @$"SET {string.Join(", ", setClauses)} WHERE {idDbFieldName} = @{idDbFieldName}";

            sqlCommand.Parameters.AddWithValue($"@{idDbFieldName}", idDbFieldValue);

            SqlTransaction transaction = sqlCommand.Connection.BeginTransaction();

            // Execute the update command and return the number of affected rows
            int rowsAffected = await sqlCommand.ExecuteNonQueryAsync();

            if (rowsAffected != 1)
            {
                throw new Exception($"Just one row should be updated! Command aborted, because {rowsAffected} could have been updated!");
            }

            transaction.Commit();
            return rowsAffected;
        }


        public void Dispose() => sqlCommand.Dispose();
    }
}