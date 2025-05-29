using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiki.Repository.Helpers
{
    public class Update
    {
        private readonly Dictionary<string, object> _changes = new();
        public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();

        public Update SetProperty(string propertyName, object value)
        {
            Properties[propertyName] = value;
            return this;
        }

        public Update SetProperties(Dictionary<string, object> properties)
        {
            foreach (var property in properties)
            {
                Properties[property.Key] = property.Value;
            }
            return this;
        }



        public void AddChange(string column, object value)
        {
            _changes[column] = value;
        }

        public string ToSetClause(out Dictionary<string, object> parameters)
        {
            parameters = new Dictionary<string, object>();
            var setClauses = new List<string>();
            int i = 0;

            foreach (var (column, value) in _changes)
            {
                var paramName = $"@param{i++}";
                setClauses.Add($"{column} = {paramName}");
                parameters[paramName] = value;
            }

            return string.Join(", ", setClauses);
        }
    }
}