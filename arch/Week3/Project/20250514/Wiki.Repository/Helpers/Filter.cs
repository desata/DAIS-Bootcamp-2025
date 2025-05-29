using System.Text;

namespace Wiki.Repository.Helpers
{
    public class Filter
    {
        private readonly List<(string Column, object Value)> _conditions = new();

        public void Condition(string column, object value)
        {
            _conditions.Add((column, value));
        }

        public IEnumerable<(string Column, object Value)> GetConditions()
        {
            return _conditions;
        }

        public string ToWhereClause(out Dictionary<string, object> parameters)
        {
            parameters = new Dictionary<string, object>();
            var sb = new StringBuilder();

            for (int i = 0; i < _conditions.Count; i++)
            {
                var (column, value) = _conditions[i];
                var paramName = $"@param{i}";

                if (i > 0)
                    sb.Append(" AND ");

                sb.Append($"{column} = {paramName}");
                parameters[paramName] = value;
            }

            return _conditions.Count > 0 ? "WHERE " + sb.ToString() : string.Empty;
        }
    }
}