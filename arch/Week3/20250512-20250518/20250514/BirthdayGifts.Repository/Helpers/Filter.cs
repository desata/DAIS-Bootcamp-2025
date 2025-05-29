using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayGifts.Repository.Helpers
{
    public class Filter
    {
        private readonly Dictionary<string, object> _conditions = new();

        public void AddCondition(string column, object value)
        {
            _conditions[column] = value;
        }

        public void Condition(string column, object value)
        {
            AddCondition(column, value);
        }

        public IReadOnlyDictionary<string, object> Conditions => _conditions;

        public bool HasConditions => _conditions.Any();

        public string BuildWhereClause(out List<SqlParameter> parameters)
        {
            parameters = new List<SqlParameter>();

            if (!_conditions.Any())
                return string.Empty;

            var clauses = new List<string>();
            foreach (var pair in _conditions)
            {
                string paramName = $"@{pair.Key}";
                clauses.Add($"{pair.Key} = {paramName}");
                parameters.Add(new SqlParameter(paramName, pair.Value ?? DBNull.Value));
            }

            return "WHERE " + string.Join(" AND ", clauses);
        }
    }
}