﻿using DAIS.WikiSystem.Repository.Base;
using DAIS.WikiSystem.Repository.Helpers;
using DAIS.WikiSystem.Repository.Interfaces.Tag;
using Microsoft.Data.SqlClient;

namespace DAIS.WikiSystem.Repository.Implementation.Tag
{
    public class TagRepository : BaseRepository<Models.Tag>, ITagRepository
    {
        private const string IdDbFieldEnumeratorName = "TagId";
        protected override string GetTableName() => "Tags";
        protected override string[] GetColumns() => new[] { IdDbFieldEnumeratorName, "Name" };
        protected override Models.Tag MapEntity(SqlDataReader reader)
        {
            return new Models.Tag
            {
                TagId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                Name = Convert.ToString(reader["Name"])
            };
        }
        public Task<int> CreateAsync(Models.Tag entity)
        {
            return base.CreateAsync(entity, IdDbFieldEnumeratorName);
        }
        public Task<Models.Tag> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }
        public IAsyncEnumerable<Models.Tag> RetrieveCollectionAsync(TagFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.Name is not null)
            {
                commandFilter.AddCondition("Name", filter.Name);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }
        public Task<bool> UpdateAsync(int objectId, Interfaces.Tag.TagUpdate update)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            throw new NotImplementedException();
        }
    }
}
