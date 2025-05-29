using Microsoft.Data.SqlClient;
using WikiSystem.Repository.Base;
using WikiSystem.Repository.Helpers;
using WikiSystem.Repository.Interfaces.Category;

namespace WikiSystem.Repository.Implementation.Category
{
    public class CategoryRepository : BaseMapperRepository<Models.Category>, ICategoryRepository
    {
        private const string IdDbFieldEnumeratorName = "CategoryId";
        protected override string[] GetColumns() => new[]
        {
            IdDbFieldEnumeratorName,
            "Name"
        };

        protected override string GetTableName()
        {
            return "Categories";
        }

        protected override Models.Category MapEntity(SqlDataReader reader)
        {
            return new Models.Category
            {
                CategoryId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                Name = Convert.ToString(reader["Name"])
            };
        }

        public Task<int> CreateAsync(Models.Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Category> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.Category> RetrieveCollectionAsync(CategoryFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.Name is not null)
            {
                commandFilter.AddCondition("Name", filter.Name);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public Task<bool> UpdateAsync(int objectId, CategoryUpdate update)
        {
            throw new NotImplementedException();
        }


    }
}
