using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayGifts.Repository.Helpers
{
    public class Update
    {
        private readonly Dictionary<string, object> _changes = new();

        public void AddChange(string column, object value)
        {
            _changes[column] = value;
        }

        public IReadOnlyDictionary<string, object> Changes => _changes;

        public bool HasChanges => _changes.Any();

        public string BuildSetClause(out List<SqlParameter> parameters)
        {
            parameters = new List<SqlParameter>();

            if (!_changes.Any())
                return string.Empty;

            var clauses = new List<string>();
            foreach (var pair in _changes)
            {
                string paramName = $"@{pair.Key}";
                clauses.Add($"{pair.Key} = {paramName}");
                parameters.Add(new SqlParameter(paramName, pair.Value ?? DBNull.Value));
            }

            return "SET " + string.Join(", ", clauses);
        }
    }
}