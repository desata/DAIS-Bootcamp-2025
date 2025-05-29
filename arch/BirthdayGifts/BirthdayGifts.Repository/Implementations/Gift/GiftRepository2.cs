using BirthdayGifts.Repository.Base2;
using BirthdayGifts.Repository.Helpers;
using BirthdayGifts.Repository.Interfaces.Gift;
using Microsoft.Data.SqlClient;

namespace BirthdayGifts.Repository.Implementations.Gift
{
    public class GiftRepository2 : BaseRepository2<Models.Gift>, IGiftRepository2
    {
        private const string IdDbFieldEnumeratorName = "GiftId";

        protected override string GetTableName()
        {
            return "Gifts";
        }
        protected override string[] GetColumns() => new[]
        {
            IdDbFieldEnumeratorName,
            "Name",
            "Description",
            "Price"
        };

        protected override Models.Gift MapEntity(SqlDataReader reader)
        {
            return new Models.Gift
            {
                GiftId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                Name = Convert.ToString(reader["Name"]),
                Description = !reader.IsDBNull(reader.GetOrdinal("Description"))
                ? Convert.ToString(reader["Description"])
                : null,
                Price = Convert.ToDecimal(reader["Price"])
            };
        }

        public Task<int> CreateAsync(Models.Gift entity)
        {
            return base.CreateAsync(entity, IdDbFieldEnumeratorName);
        }

        public Task<Models.Gift> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.Gift> RetrieveCollectionAsync(GiftFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.Name is not null)
            {
                commandFilter.AddCondition("Name", filter.Name);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, GiftUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(
                connection,
                "Gifts",
                IdDbFieldEnumeratorName, objectId);

            updateCommand.AddSetClause("Name", update.Name);
            updateCommand.AddSetClause("Description", update.Description);
            updateCommand.AddSetClause("Price", update.Price);

            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            return base.DeleteAsync(IdDbFieldEnumeratorName, objectId);
        }
    }
}
