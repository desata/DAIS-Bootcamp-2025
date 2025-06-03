using Azure.Core;
using DAIS.WikiSystem.Models;
using DAIS.WikiSystem.Repository.Interfaces.Collection;
using DAIS.WikiSystem.Repository.Interfaces.CollectionDocument;
using DAIS.WikiSystem.Services.DTOs.Collection;
using DAIS.WikiSystem.Services.Interfaces.Collection;

namespace DAIS.WikiSystem.Services.Implementation.Collection
{
    public class CollectionService : ICollectionService
    {
        private readonly ICollectionRepository _collectionRepository;
        private readonly ICollectionDocumentRepository _collectionDocumentRepository;
        public CollectionService(ICollectionRepository collectionRepository, ICollectionDocumentRepository collectionDocumentRepository)
        {
            _collectionRepository = collectionRepository;
            _collectionDocumentRepository = collectionDocumentRepository;

        }

        public async Task<AddDocumentsToCollectionResponse> AddDocumentsToCollectionAsync(AddDocumentsToCollectionRequest request)
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

            return new AddDocumentsToCollectionResponse
            {
                IsSuccess = true
            };
        }

        public async Task<CreateCollectionResponse> CreateCollectionAsync(CreateCollectionRequest request)
        {
            var filter = new CollectionFilter
            {
                Name = request.Name,
                CreatorId = request.CreatorId
            };

            var existingCollection = await _collectionRepository.RetrieveCollectionAsync(filter).ToListAsync();

            if (existingCollection.Any())
            {
                return new CreateCollectionResponse
                {
                    IsSuccess = false,
                    ErrorMessage = "You already have a collection with this name."
                };
            }

            var newCollection = new Models.Collection
            {
                Name = request.Name,
                CreatorId = request.CreatorId,
                CreateDate = DateTime.UtcNow
            };

            int collectionId = await _collectionRepository.CreateAsync(newCollection);

            return new CreateCollectionResponse
            {
                IsSuccess = true,
                CollectionId = collectionId
            };
        }

        public async Task<GetAllCollectionResponse> GetAllAsync()
        {
            var collections = await _collectionRepository.RetrieveCollectionAsync(new CollectionFilter()).ToListAsync();
            var allCollections = new GetAllCollectionResponse
            {
                Collections = collections.Select(MapToCollectionInfo).ToList(),
                Count = collections.Count
            };
            return allCollections;
        }

        public async Task<GetAllCollectionResponse> GetAllByCreatorAsync(int? creatorId)
        {
            var filter = new CollectionFilter
            {
                 CreatorId = creatorId
            };
            var collections = await _collectionRepository.RetrieveCollectionAsync(filter).ToListAsync();
            var allCollections = new GetAllCollectionResponse
            {
                Collections = collections.Select(MapToCollectionInfo).ToList(),
                Count = collections.Count
            };
            return allCollections;
        }

        public async Task<GetCollectionResponse> GetByIdAsync(int collectionId)
        {
            var collection = await _collectionRepository.RetrieveAsync(collectionId);

            return (GetCollectionResponse)MapToCollectionInfo(collection);
        }


        private CollectionInfo MapToCollectionInfo(Models.Collection collection)
        {
            return new CollectionInfo
            {
                CollectionId = collection.CollectionId,
                Name = collection.Name,
                CreatorId = collection.CreatorId,
                CreateDate = collection.CreateDate
            };
        }
    }
}