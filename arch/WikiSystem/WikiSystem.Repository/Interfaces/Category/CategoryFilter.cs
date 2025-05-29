using System.Data.SqlTypes;

namespace WikiSystem.Repository.Interfaces.Category
{
    public class CategoryFilter
    {
        public SqlString? Name { get; set; }
    }
}