using BirthdayGifts.Models;
using BirthdayGifts.Repository.Base;
using BirthdayGifts.Repository.Helpers;
using BirthdayGifts.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayGifts.Repository.Implementations
{
    public class GiftRepository : BaseRepository<Gift>, IGiftRepository
    {
        public GiftRepository(IConfiguration configuration)
            : base(configuration, "Gift")
        {
        }

        protected override string[] GetColumns()
        {
            return new[]
            {
                "GiftId",
                "Name",
                "Description",
                "Price"
            };
        }

        protected override Gift MapToEntity(SqlDataReader reader)
        {
            return new Gift
            {
                GiftId = reader.GetInt32(reader.GetOrdinal("GiftId")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Description = reader.GetString(reader.GetOrdinal("Description")),
                Price = reader.GetDecimal(reader.GetOrdinal("Price"))
            };
        }

        protected override Dictionary<string, object> MapToParameters(Gift entity)
        {
            return new Dictionary<string, object>
            {
                { "Name", entity.Name },
                { "Description", entity.Description },
                { "Price", entity.Price }               
            };
        }

        public async Task<IEnumerable<Gift>> GetAvailableGifts()
        {
            var filter = new Filter();
            filter.Condition("IsAvailable", true);
            return await ReceiveCollection(filter);
        }
    }
}