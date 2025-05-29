using Microsoft.Data.SqlClient;
using WikiSystem.Repository.Base;
using WikiSystem.Repository.Helpers;
using WikiSystem.Repository.Interfaces.CollectionDocument;

namespace WikiSystem.Repository.Implementations.CollectionDocument
{
    public class CollectionDocumentRepository : BaseRepository<Models.CollectionDocument>, ICollectionDocumentRepository
    {
        private const string IdDbFieldEnumeratorName = "CollectionId";
        protected override string GetTableName()
        {
            return "CollectionDocuments";
        }

        protected override string[] GetColumns()
        {
            return new string[]
            {
                IdDbFieldEnumeratorName,
                "DocumentId"
            };

        }

        protected override Models.CollectionDocument MapEntity(SqlDataReader reader)
        {
            return new Models.CollectionDocument
            {
                CollectionId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                DocumentId = Convert.ToInt32(reader["DocumentId"])
            };
        }


        public Task<int> CreateAsync(Models.CollectionDocument entity)
        {
            return base.CreateAsync(entity, IdDbFieldEnumeratorName);
        }
        public Task<Models.CollectionDocument> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }


        public IAsyncEnumerable<Models.CollectionDocument> RetrieveCollectionAsync(CollectionDocumentFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.CollectionId is not null)
            {
                commandFilter.AddClause("CollectionId", filter.CollectionId);
            }
            if (filter.DocumentId is not null)
            {
                commandFilter.AddClause("DocumentId", filter.DocumentId);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }



        public Task<bool> UpdateAsync(int objectId, CollectionDocumentUpdate update)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            throw new NotImplementedException();
        }


    }
}
