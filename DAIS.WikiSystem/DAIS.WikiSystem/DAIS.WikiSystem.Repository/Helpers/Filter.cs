using DAIS.WikiSystem.Repository.Interfaces.Document;

namespace DAIS.WikiSystem.Repository.Helpers
{
    public class Filter
    {
        private readonly List<FilterCondition> _conditions = new();

        public void AddCondition(string field, object value, string op = "=")
        {
            _conditions.Add(new FilterCondition { Field = field, Value = value, Operator = op });
        }

        public IEnumerable<FilterCondition> Conditions => _conditions;
    }

}