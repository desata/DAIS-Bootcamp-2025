using DAIS.WikiSystem.Repository.Base;
using DAIS.WikiSystem.Repository.Helpers;
using DAIS.WikiSystem.Repository.Interfaces;
using DAIS.WikiSystem.Repository.Interfaces.Category;
using Microsoft.Data.SqlClient;

namespace DAIS.WikiSystem.Repository.Implementation.Category
{
    public class CategoryRepository : BaseRepository<Models.Category>, ICategoryRepository
    {
        private const string IdDbFieldEnumeratorName = "CategoryId";

        protected override string GetTableName() => "Categories";
        protected override string[] GetColumns() => new[] { IdDbFieldEnumeratorName, "Name" };
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


        public async Task<bool> UpdateAsync(int objectId, CategoryUpdate update)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            throw new NotImplementedException();
        }
    }
}
