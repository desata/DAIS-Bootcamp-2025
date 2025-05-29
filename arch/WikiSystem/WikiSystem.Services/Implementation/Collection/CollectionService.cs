using WikiSystem.Repository.Interfaces.Collection;
using WikiSystem.Repository.Interfaces.Employee;
using WikiSystem.Services.DTOs.Collection;
using WikiSystem.Services.Interfaces.Collection;

namespace WikiSystem.Services.Implementation.Collection
{
    public class CollectionService : ICollectionService
    {
        private readonly ICollectionRepository _collectionRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public CollectionService(ICollectionRepository collectionRepository, IEmployeeRepository employeeRepository)
        {
            _collectionRepository = collectionRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<CreateCollectionResponse> CreateCollectionAsync(CreateCollectionRequest request)
        {
            var filter = new CollectionFilter 
            { 
                Name = request.Name, 
                CreatorId = request.CreatorId 
            };
            
            var hasSameCollection = await _collectionRepository.RetrieveCollectionAsync(filter).AnyAsync();

            if (hasSameCollection)
            {
                return new CreateCollectionResponse()
                {
                    Success = false,
                    ErrorMessage = $"Collection with the same name {request.Name} already exists for this user."
                };
            }

            var collection = new Models.Collection
            {
                Name = request.Name,
                CreatorId = request.CreatorId
            };


            int collectionId = await _collectionRepository.CreateAsync(collection);

            collection.CollectionId = collectionId;
            var response = (CreateCollectionResponse)await MapToCollectionInfo(collection);
            response.Success = true;

            return response;
        }

        public async Task<GetCollectionByCreatorResponse> GetByCreatorIdAsync(int creatorId)
        {
            var filter = new CollectionFilter
            {
                CreatorId = creatorId
            };
            var collection = await _collectionRepository.RetrieveCollectionAsync(filter).ToListAsync();

            var allCollectionsByCreatorResponse = new GetCollectionByCreatorResponse()
            {
                 TotalCount = collection.Count
            };

            foreach (var item in collection)
            {
                var collectionInfo = await MapToCollectionInfo(item);
                allCollectionsByCreatorResponse.Collections.Add(collectionInfo);
            }

            return allCollectionsByCreatorResponse;
        }

        private async Task<CollectionInfo> MapToCollectionInfo(Models.Collection collection)
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