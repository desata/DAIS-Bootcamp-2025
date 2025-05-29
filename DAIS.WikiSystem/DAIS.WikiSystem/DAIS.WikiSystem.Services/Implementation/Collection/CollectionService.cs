using DAIS.WikiSystem.Models;
using DAIS.WikiSystem.Repository.Interfaces.Collection;
using DAIS.WikiSystem.Repository.Interfaces.CollectionDocument;
using DAIS.WikiSystem.Services.DTOs.Collection;
using DAIS.WikiSystem.Services.Interfaces.Collection;
using System.Data.SqlTypes;

namespace DAIS.WikiSystem.Services.Implementation.Collection
{
    public class CollectionService : ICollectionService
    {
        private readonly ICollectionRepository _collectionRepository;
        private readonly ICollectionDocumentRepository _collectionDocumentRepository;

        public CollectionService(
            ICollectionRepository collectionRepository,
            ICollectionDocumentRepository collectionDocumentRepository)
        {
            _collectionRepository = collectionRepository;
            _collectionDocumentRepository = collectionDocumentRepository;
        }
        public async Task<CreateCollectionResponse> CreateCollectionAsync(CreateCollectionRequest request)
        {
            var existingCollection = await _collectionRepository
     .RetrieveCollectionAsync(new CollectionFilter
     {
         Name = new SqlString(request.Name),
         CreatorId = request.CreatorId
     })
     .ToListAsync();

            if (existingCollection.Any())
            {
                return new CreateCollectionResponse
                {
                    Success = false,
                    ErrorMessage = "You already have a document with this title."
                };
            }

            var collection = new Models.Collection
            {
                Name = request.Name,
                CreatorId = request.CreatorId

            };

            int newId = await _collectionRepository.CreateAsync(collection);

            return new CreateCollectionResponse
            {
                Success = true,
                CollectionId = newId
            };
        }

        public async Task AddDocumentsToCollectionAsync(AddDocumentsToCollectionRequest request)
        {
            foreach (var docId in request.DocumentIds)
            {
                await _collectionDocumentRepository.CreateMappingIfNotExistsAsync(
                    new CollectionDocument
                    {
                        CollectionId = request.CollectionId,
                        DocumentId = docId
                    });
            }
        }



        public async Task<GetAllCollectionByCreatorIdResponse> GetAllByCreatorIdAsync(int creatorId)
        {
            var collections = await _collectionRepository
                .RetrieveCollectionAsync(new CollectionFilter { CreatorId = creatorId })
                .ToListAsync();

            var response = new GetAllCollectionByCreatorIdResponse
            {
                Collections = collections.Select(MapToCollectionInfo).ToList(),
                TotalCount = collections.Count
            };

            return response;
        }

        public async Task<GetCollectionResponse> GetByIdAsync(int collectionid)
        {
            var collection = await _collectionRepository.RetrieveAsync(collectionid);
            return (GetCollectionResponse)MapToCollectionInfo(collection);

        }

        private CollectionInfo MapToCollectionInfo(Models.Collection collection)
        {
            return new CollectionInfo
            {
                CollectionId = collection.CollectionId,
                Name = collection.Name,
                CreatorId = collection.CreatorId
            };
        }
    }
}