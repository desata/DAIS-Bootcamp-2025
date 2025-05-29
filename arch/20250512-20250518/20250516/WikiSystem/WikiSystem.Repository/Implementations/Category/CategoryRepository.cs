using Microsoft.Data.SqlClient;
using WikiSystem.Repository.Base;
using WikiSystem.Repository.Helpers;
using WikiSystem.Repository.Interfaces.Category;

namespace WikiSystem.Repository.Implementations.Category
{
    public class CategoryRepository : BaseRepository<Models.Category>, ICategoryRepository
    {
        private const string IdDbFieldEnumeratorName = "CategoriyId";

        protected override string GetTableName()
        {
            return "Categories";
        }

        protected override string[] GetColumns()
        {
            return new string[]
            {
                IdDbFieldEnumeratorName,
                "Name"
            };
        }

        protected override Models.Category MapEntity(SqlDataReader reader)
        {
            return new Models.Category
            {
                CategoryId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                Name = Convert.ToString(reader["Name"]),
            };

        }

        public Task<int> CreateAsync(Models.Category entity)
        {
            return base.CreateAsync(entity, IdDbFieldEnumeratorName);
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
                commandFilter.AddClause("Name", filter.Name);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, CategoryUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(
                connection,
                GetTableName(),
                IdDbFieldEnumeratorName, objectId);

            updateCommand.AddSetClause("Name", update.Name);

            return await updateCommand.ExecuteNonQueryAsync() == 1;
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            return base.DeleteAsync(IdDbFieldEnumeratorName, objectId);
        }



    }
}
