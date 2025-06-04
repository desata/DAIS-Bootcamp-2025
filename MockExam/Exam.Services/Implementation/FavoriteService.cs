using Exam.Models;
using Exam.Repository.Interfaces;
using Exam.Services.DTOs.Favorite;
using Exam.Services.Interfaces;

namespace Exam.Services.Implementation
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoriteService(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        public async Task<CreateFavoriteResponse> CreateFavoriteAsync(CreateFavoriteRequest request)
        {
            try
            {
                var userFavorites = await _favoriteRepository.RetrieveByUserIdAsync(request.UserId);

                if (userFavorites.Count > 3)
                {
                    return new CreateFavoriteResponse
                    {
                        Success = false,
                        ErrorMessage = "You can only have up to 3 favorite workplaces."
                    };
                }

                var alreadyExists = userFavorites.Any(f =>
                    f.WorkplaceId == request.WorkplaceId);

                if (alreadyExists)
                {
                    return new CreateFavoriteResponse
                    {
                        Success = false,
                        ErrorMessage = "This workplace is already in your favorites."
                    };
                }

                var favorite = new Favorite
                {
                    Name = request.Name,
                    UserId = request.UserId,
                    WorkplaceId = request.WorkplaceId
                };

                var favoriteId = await _favoriteRepository.CreateAsync(favorite);

                return new CreateFavoriteResponse
                {
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new CreateFavoriteResponse
                {
                    Success = false,
                    ErrorMessage = $"Unexpected error: {ex.Message}"
                };
            }
        }


        public async Task<DeleteFavoriteResponse> DeleteFavoriteAsync(DeleteFavoriteRequest request)
        {
            try
            {
                var favoriteId = await _favoriteRepository.RetrieveByIdAsync(request.FavoriteId);

                if (favoriteId == null)
                {
                    return new DeleteFavoriteResponse
                    {
                        Success = false,
                        ErrorMessage = "Favorite not found."
                    };
                }

                var isDeleted = await _favoriteRepository.DeleteAsync(request.FavoriteId);
                if (!isDeleted)
                {
                    return new DeleteFavoriteResponse
                    {
                        Success = false,
                        ErrorMessage = "Failed to delete favorite."
                    };
                }
                return new DeleteFavoriteResponse
                {
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new DeleteFavoriteResponse
                {
                    Success = false,
                    ErrorMessage = $"Unexpected error: {ex.Message}"
                };
            }
        }

        public async Task<List<FavoriteInfo>> GetAllByUserIdAsync(int userId)
        {
            var userFavorites = await _favoriteRepository.RetrieveByUserIdAsync(userId);

            return userFavorites.Select(f => new FavoriteInfo
            {
                FavoriteId = f.FavoriteId,
                Name = f.Name,
                WorkplaceId = f.WorkplaceId,
                UserId = f.UserId
            }).ToList();
        }

        public async Task<FavoriteInfo> GetByIdAsync(int favoriteId)
        {
            var favorite = await _favoriteRepository.RetrieveByIdAsync(favoriteId);

            return new FavoriteInfo
            {
                FavoriteId = favorite.FavoriteId,
                Name = favorite.Name,
                WorkplaceId = favorite.WorkplaceId,
                UserId = favorite.UserId
            };

        }

        public async Task<FavoriteInfo> GetByUserIdAsync(int userId)
        {
            var favorite = await _favoriteRepository.RetrieveByIdAsync(userId);

            return new FavoriteInfo
            {
                FavoriteId = favorite.FavoriteId,
                Name = favorite.Name,
                WorkplaceId = favorite.WorkplaceId,
                UserId = favorite.UserId
            };
        }
    }
}
